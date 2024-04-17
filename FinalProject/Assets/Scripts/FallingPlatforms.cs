using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    [SerializeField] private CannonController cannonController;
    [SerializeField] private GameObject[] platforms; // Array to hold different platforms
    private int count;
    [SerializeField] private HashSet<GameObject> platformsDisabled = new HashSet<GameObject>();
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Quaternion> originalRotations = new Dictionary<GameObject, Quaternion>();
    private Dictionary<GameObject, Vector3> originalScales = new Dictionary<GameObject, Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        count = cannonController.ShotCounter();

        foreach(GameObject platform in platforms)
        {
            originalPositions.Add(platform, platform.transform.position);
            originalRotations.Add(platform, platform.transform.rotation);
            originalScales.Add(platform, platform.transform.localScale);

            foreach(Transform child in platform.transform)
            {
                originalPositions.Add(child.gameObject, child.position);
                originalRotations.Add(child.gameObject, child.rotation);
                originalScales.Add(child.gameObject, child.localScale);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count = cannonController.ShotCounter();

        // Check if count is within the range of the platforms array
        if (count >= 1 && count <= platforms.Length)
        {
            // Get the index corresponding to the count
            int platformIndex = count - 1;

            foreach (Transform platformChild in platforms[platformIndex].transform)
            {
                Rigidbody rb = platformChild.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = platformChild.gameObject.AddComponent<Rigidbody>();
                }
            }

            if(!platformsDisabled.Contains(platforms[platformIndex]))
            {
                StartCoroutine(DisablePlatformDelayed(platforms[platformIndex].gameObject)); // Pass gameObject property
            }
        }
    }

    private IEnumerator DisablePlatformDelayed(GameObject platform)
    {
        yield return new WaitForSeconds(5f);
        if(!platformsDisabled.Contains(platform))
        {
            platformsDisabled.Add(platform);
            Debug.Log("Group Added");
            Debug.Log("Shot Count: " + cannonController.ShotCounter());
            platform.SetActive(false);
        }
    }

    public void ResetPlatforms()
    {
        foreach(GameObject platform in platformsDisabled)
        {
            Debug.Log("Group Reset");
            Transform platformTransform = platform.transform;

            platformTransform.position = originalPositions[platform];

            platformTransform.rotation = originalRotations[platform];

            platformTransform.localScale = originalScales[platform];
            
            foreach(Transform child in platform.transform)
            {
                Rigidbody rb = child.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    Destroy(rb);
                }

                child.position = originalPositions[child.gameObject];
                child.rotation = originalRotations[child.gameObject];
                child.localScale = originalScales[child.gameObject];
            }

            platform.SetActive(true);
        }
        platformsDisabled.Clear();
        count = cannonController.ShotCounter();
    }
}
