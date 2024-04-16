using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    [SerializeField] private CannonController cannonController;
    [SerializeField] private GameObject[] platforms; // Array to hold different platforms
    private Vector3[][] originalPositions; // Store original positions of each platform's children
    private Quaternion[][] originalRotations; // Store original rotations of each platform's children
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = cannonController.ShotCounter();

        // Store original positions and rotations of all platforms and their children
        originalPositions = new Vector3[platforms.Length][];
        originalRotations = new Quaternion[platforms.Length][];
        for (int i = 0; i < platforms.Length; i++)
        {
            StoreOriginalData(platforms[i], i);
        }
    }

    // Update is called once per frame
    void Update()
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

                StartCoroutine(DisablePlatformDelayed(platformChild.gameObject, platformIndex));
            }
        }
    }

    private IEnumerator DisablePlatformDelayed(GameObject platform, int platformIndex)
    {
        yield return new WaitForSeconds(5f);
        platform.SetActive(false);
    }

    private void StoreOriginalData(GameObject platform, int platformIndex)
    {
        Transform[] children = platform.GetComponentsInChildren<Transform>();
        originalPositions[platformIndex] = new Vector3[children.Length];
        originalRotations[platformIndex] = new Quaternion[children.Length];
        for (int i = 0; i < children.Length; i++)
        {
            originalPositions[platformIndex][i] = children[i].position;
            originalRotations[platformIndex][i] = children[i].rotation;
        }
    }

    private void OnDisable()
    {
        // Reset positions and rotations of platforms' children
        for (int i = 0; i < platforms.Length; i++)
        {
            ResetPlatformTransform(platforms[i], i);
        }
    }

    private void ResetPlatformTransform(GameObject platform, int platformIndex)
    {
        Vector3[] platformOriginalPositions = originalPositions[platformIndex];
        Quaternion[] platformOriginalRotations = originalRotations[platformIndex];
        Transform[] children = platform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < children.Length; i++)
        {
            children[i].position = platformOriginalPositions[i];
            children[i].rotation = platformOriginalRotations[i];
        }
    }

    public void SetPlatformsActive()
    {
        foreach(GameObject platform in platforms)
        {
            platform.SetActive(true);
            foreach(Transform child in platform.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}

