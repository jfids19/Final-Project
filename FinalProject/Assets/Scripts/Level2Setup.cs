using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Setup : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToHide;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in objectsToHide)
        {
            if(obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
