using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;

    public GameObject leftGrabRay;
    public GameObject rightGrabRay;


    void Update()
    {
        if (leftGrabRay.activeInHierarchy) {
            leftTeleportation.SetActive(leftActivate.action.ReadValue<float>() > 0.1f);
        }
        if (rightGrabRay.activeInHierarchy) {
            rightTeleportation.SetActive(rightActivate.action.ReadValue<float>() > 0.1f);
        }
    }
}
