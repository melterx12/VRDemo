using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public XRController leftTeleportRay, rightTeleportRay;
    public InputHelpers.Button activationButton;
    public float activationThreshold = 0.1f;

    private bool leftActive, rightActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leftTeleportRay)
            leftActive = CheckRayActivated(leftTeleportRay);
        if (rightTeleportRay)
            rightActive = CheckRayActivated(rightTeleportRay);

        if (leftActive && !rightActive)
        {
            leftTeleportRay.gameObject.SetActive(true);
            rightTeleportRay.gameObject.SetActive(false);
            rightActive = false;
        }
        else if (rightActive && !leftActive)
        {
            leftTeleportRay.gameObject.SetActive(false);
            rightTeleportRay.gameObject.SetActive(true);
            leftActive = false;
        }
        else
        {
            leftTeleportRay.gameObject.SetActive(false);
            rightTeleportRay.gameObject.SetActive(false);
        }
    }

    public bool CheckRayActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, activationButton, out bool isPressed, activationThreshold);
        return isPressed;
    }
}
