using UnityEngine;

public class PistolBulletCasing : MonoBehaviour
{
    [SerializeField] AudioSource casingAudio;
    [SerializeField] AudioClip[] casingClip = new AudioClip[3];

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        casingAudio.pitch = RandomPitchInRange(0.1f);
        casingAudio.PlayOneShot(casingClip[Random.Range(0,2)]);
    }

    float RandomPitchInRange(float range)
    {
        return Random.Range(1f - range, 1f + range);
    }
}