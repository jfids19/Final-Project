using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene1 : MonoBehaviour
{
    public GameObject player;
    public GameObject cutscene;
    public GameObject mainCamera;
    public PlayableDirector timeline;

    void Start()
    {
        cutscene.SetActive(false);
        timeline.enabled = false;
    }
    
    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        cutscene.SetActive(true);
        mainCamera.SetActive(false);
        player.SetActive(false);
        timeline.enabled = true;
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(5.35f);
        mainCamera.SetActive(true);
        player.SetActive(true);
        cutscene.SetActive(false);
    }
}
