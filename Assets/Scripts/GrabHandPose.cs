using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabHandPose : MonoBehaviour
{
    public HandData rightHandPose;

    Vector3 startingHandPosition;
    Vector3 finalHandPosition;
    Quaternion startingHandRotation;
    Quaternion finalHandRotation;
    Quaternion[] startingFingerRotations;
    Quaternion[] finalFingerRotations;

    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        if(grabInteractable == null) {
            Debug.Log("grabInteractable == null");
        } else {
            Debug.Log("grabInteractable.name=" + grabInteractable.name);
        }
        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(UnSetPose);

        rightHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs arg)
    {
        Debug.Log("setuppose, TÄNNE EI KOSKAAN MENNÄ!??");

        if(arg.interactorObject is XRDirectInteractor) {
            
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = false;
            Debug.Log("handData " + handData.gameObject.name);

            SetHandDataValues(handData, rightHandPose);
            SetHandData(handData, finalHandPosition, finalHandRotation, finalFingerRotations);
        }
    }

    public void UnSetPose(BaseInteractionEventArgs arg)
    {
        Debug.Log("UNsetuppose, ");

        if (arg.interactorObject is XRDirectInteractor) {

            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = true;
            Debug.Log("handData " + handData.gameObject.name);

            SetHandData(handData, startingHandPosition, startingHandRotation, startingFingerRotations);
        }
    }

    public void SetHandDataValues(HandData h1, HandData h2)
    {
        Debug.Log("SetHandDataValues");

        //pistoolin scale vaikuttaa käden kokoon, siksi tämä ei toimi oikein
        //startingHandPosition = h1.root.localPosition;
        //finalHandPosition = h2.root.localPosition;
        startingHandPosition = new Vector3(h1.root.localPosition.x / h1.root.localScale.x,
                                            h1.root.localPosition.y / h1.root.localScale.y,
                                            h1.root.localPosition.z / h1.root.localScale.z);
        finalHandPosition = new Vector3(h2.root.localPosition.x / h2.root.localScale.x,
                                        h2.root.localPosition.y / h2.root.localScale.y,
                                        h2.root.localPosition.z / h2.root.localScale.z);


        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingFingerRotations = new Quaternion[h1.fingerBones.Length];
        finalFingerRotations = new Quaternion[h1.fingerBones.Length];

        for(int i = 0; i < h1.fingerBones.Length; i++) {
            startingFingerRotations[i] = h1.fingerBones[i].localRotation;
            finalFingerRotations[i] = h2.fingerBones[i].localRotation;
        }
    }

    public void SetHandData(HandData h, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation)
    {
        Debug.Log("SetHandData");

        h.root.localPosition = newPosition;
        h.root.localRotation = newRotation;

        for(int i = 0; i < newBonesRotation.Length; i++) {
            h.fingerBones[i].localRotation = newBonesRotation[i];
        }
    }
}
