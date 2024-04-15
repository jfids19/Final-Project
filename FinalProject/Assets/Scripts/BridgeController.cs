using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    [SerializeField] private bool shouldFall;
    [SerializeField] private GameObject[] bridgeSections;

    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && shouldFall && !activated)
        {
            foreach(GameObject section in bridgeSections)
            {
                Rigidbody sectionRb = section.GetComponent<Rigidbody>();
                if(sectionRb == null)
                {
                    sectionRb = section.AddComponent<Rigidbody>();
                }
                sectionRb.useGravity = true;

                section.transform.localScale = new Vector3(0.25f, 0.4f, 1f);
            }
            activated = true;

            StartCoroutine(DeactivateSectionsAfterDelay(5f));
        }
    }

    IEnumerator DeactivateSectionsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach(GameObject section in bridgeSections)
        {
            section.SetActive(false);
        }
    }
}
