using UnityEngine;

public class PistolBulletHead : MonoBehaviour
{
    [SerializeField] AudioSource audioBulletHead;
    [SerializeField] AudioClip[] hitFlesh = new AudioClip[4];
    [SerializeField] AudioClip[] hitIron = new AudioClip[3];
    [SerializeField] AudioClip[] hitRicochet = new AudioClip[3];

    void Start()
    {
        audioBulletHead = GetComponent<AudioSource>();            
    }

    void OnCollisionEnter(Collision collision)
    {
        audioBulletHead.pitch = RandomPitchInRange(0.05f);

        if (collision.collider.CompareTag("Iron")) {
            HitOnIron();
        } else if (collision.collider.CompareTag("Flesh")) {
            HitOnFlesh();
        } else if (collision.collider.CompareTag("Ground")) {
            HitOnRicochet();
        }

        Destroy(gameObject, 2f);    //annetaan 2 sekuntia aikaa soittaa äänet. JUUH, EI NÄIN
    }   

    void HitOnIron()
    {
        int rClip = Random.Range(0, hitIron.Length);    
        audioBulletHead.PlayOneShot(hitIron[rClip]);
    }

    void HitOnFlesh()
    {
        int rClip = Random.Range(0, hitFlesh.Length);
        audioBulletHead.PlayOneShot(hitFlesh[rClip]);
    }

    void HitOnRicochet()
    {
        int rClip = Random.Range(0, hitRicochet.Length);
        audioBulletHead.PlayOneShot(hitRicochet[rClip]);
    }

    float RandomPitchInRange(float range)
    {
        return Random.Range(1f - range, 1f + range);
    }
}
