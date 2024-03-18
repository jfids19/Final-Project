using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    public GameObject player;
    public GameObject cutscene;
    public GameObject mainCamera;
    public PlayableDirector timeline;

    [SerializeField] private GameObject[] objectsToHide;

    void Start()
    {
        cutscene.SetActive(false);
        timeline.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        
        foreach(GameObject obj in objectsToHide)
        {
            if(obj != null)
            {
                obj.SetActive(false);
            }
        }
        
        cutscene.SetActive(true);
        mainCamera.SetActive(false);
        player.SetActive(false);
        timeline.enabled = true;

        float cutsceneDuration = (float)timeline.duration;
        StartCoroutine(FinishCut(cutsceneDuration));
    }

    IEnumerator FinishCut(float duration)
    {
        yield return new WaitForSeconds(duration);
        mainCamera.SetActive(true);
        player.SetActive(true);
        
        foreach(GameObject obj in objectsToHide)
        {
            if(obj != null)
            {
                obj.SetActive(true);
            }
        }
        
        cutscene.SetActive(false);
    }
}