using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObject;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public GameObject ThirdPersonCam;
    public GameObject CombatCam;

    public CameraStyle currentStyle;

    public enum CameraStyle
    {
        Basic,
        Combat
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //switch styles
        if(Input.GetMouseButtonDown(1))
        {
            if(currentStyle == CameraStyle.Basic)
            {
                SwitchCameraStyle(CameraStyle.Combat);
            }
             else if (currentStyle == CameraStyle.Combat)
            {
                SwitchCameraStyle(CameraStyle.Basic);
            }
        }
        
        //rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        //rotate player object
        if(currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            playerObject.forward = Vector3.Slerp(playerObject.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        }

        else if(currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObject.forward = dirToCombatLookAt.normalized;
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        CombatCam.SetActive(false);
        ThirdPersonCam.SetActive(false);

        if (newStyle == CameraStyle.Basic) ThirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Combat) CombatCam.SetActive(true);

        currentStyle = newStyle;
    }
}
