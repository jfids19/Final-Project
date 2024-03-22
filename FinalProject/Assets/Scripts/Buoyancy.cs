using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    [SerializeField]private float waterLevel = 0.0f;
    [SerializeField]private float floatHeight = 2.0f;
    [SerializeField]private float bounceDamping = 0.05f;

    private Rigidbody boatRigidbody;

    void Start()
    {
        boatRigidbody = GetComponent<Rigidbody>();

        boatRigidbody.freezeRotation = true;
    }

    void FixedUpdate()
    {
        float buoyancyForce = -Physics.gravity.y * boatRigidbody.mass;
        
        float waveHeight = waterLevel + Mathf.Sin(Time.time) * 0.1f;

        float forceFactor = 1.0f - ((transform.position.y - waveHeight) / floatHeight);

        if(forceFactor > 0.0f)
        {
            Vector3 upForce = Vector3.up * buoyancyForce * forceFactor;
            boatRigidbody.AddForceAtPosition(upForce, transform.position);
        }
    }
}
