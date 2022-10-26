using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PistolSounds : MonoBehaviour
{
    [SerializeField] AudioSource pistolAudio;
    [SerializeField] AudioClip shot;
    [SerializeField] AudioClip emptyShot;
    [SerializeField] AudioClip hammerMoving;
    [SerializeField] AudioClip sliderGoingBack;
    [SerializeField] AudioClip sliderGoingForward;

    public void PlayShotSound()
    {
        //pistolAudio.clip = shot;
        pistolAudio.PlayOneShot(shot);
    }
}

