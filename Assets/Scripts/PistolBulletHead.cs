using UnityEngine;

public class PistolBulletHead : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] AudioSource audioBulletHead;
    [SerializeField] AudioClip[] hitFlesh = new AudioClip[4];
    [SerializeField] AudioClip[] hitIron = new AudioClip[3];
    [SerializeField] AudioClip[] hitSand = new AudioClip[5];
    [SerializeField] AudioClip[] hitRicochet = new AudioClip[3];
    [SerializeField] AudioClip[] hitWood = new AudioClip[4];

    [SerializeField] GameObject impactFleshBig;
    [SerializeField] GameObject impactFleshSmall;
    [SerializeField] GameObject impactMetal;
    [SerializeField] GameObject impactSand;
    [SerializeField] GameObject impactStone;
    [SerializeField] GameObject impactWood;

    float defaultPitch;
    public BulletTime bulletTime;

    void Start()
    {
        bulletTime = Camera.main.GetComponent<BulletTime>();
    }

    void OnCollisionEnter(Collision collision)
    {
        audioBulletHead.pitch = RandomPitchInRange(0.05f);

        if (collision.collider.CompareTag("Iron")) {
            HitOnIron(collision);
        } else if (collision.collider.CompareTag("Flesh")) {
            rigidBody.isKinematic = true;
            HitOnFlesh(collision);
        } else if (collision.collider.CompareTag("Ground")) {
            HitOnRicochet(collision);
        } else if (collision.collider.CompareTag("Wood")) {
            rigidBody.isKinematic = true;
            HitOnWood(collision);
        } else if (collision.collider.CompareTag("Stone")) {
            HitOnStone(collision);        
        } else if (collision.collider.CompareTag("Sand")) {
            rigidBody.isKinematic = true;
            HitOnSand(collision);
        }

        Destroy(gameObject, 3f);    //annetaan 3 sekuntia aikaa soittaa ‰‰net. TOISTAISEKSI toimii n‰in
    }   

    void HitOnIron(Collision col)
    {
        int rClip = Random.Range(0, hitIron.Length);    
        audioBulletHead.PlayOneShot(hitIron[rClip]);

        GameObject hitStoneGO = Instantiate(impactMetal);
        hitStoneGO.transform.position = transform.position;
        hitStoneGO.transform.rotation = Quaternion.LookRotation(col.GetContact(0).normal, col.transform.right);

        Destroy(hitStoneGO, 3f);
    }

    void HitOnFlesh(Collision col)
    {
        int rClip = Random.Range(0, hitFlesh.Length);
        audioBulletHead.PlayOneShot(hitFlesh[rClip]);

        GameObject hitFleshGO = Instantiate(impactFleshBig);
        hitFleshGO.transform.position = transform.position;
        hitFleshGO.transform.rotation = Quaternion.LookRotation(col.GetContact(0).normal, col.transform.right);
        
        Destroy(hitFleshGO, 3f);
    }

    void HitOnRicochet(Collision col)
    {
        int rClip = Random.Range(0, hitRicochet.Length);
        audioBulletHead.PlayOneShot(hitRicochet[rClip]);
    }

    void HitOnWood(Collision col)
    {
        int rClip = Random.Range(0, hitWood.Length);
        audioBulletHead.PlayOneShot(hitWood[rClip]);

        GameObject hitWoodGO = Instantiate(impactWood);
        hitWoodGO.transform.position = transform.position;
        hitWoodGO.transform.rotation = Quaternion.LookRotation(col.GetContact(0).normal, col.transform.right);
        
        Destroy(hitWoodGO, 3f);
    }

    void HitOnStone(Collision col)
    {
        int rClip = Random.Range(0, hitRicochet.Length);
        audioBulletHead.PlayOneShot(hitRicochet[rClip]);

        GameObject hitStoneGO = Instantiate(impactStone);
        hitStoneGO.transform.position = transform.position;
        hitStoneGO.transform.rotation = Quaternion.LookRotation(col.GetContact(0).normal, col.transform.right);

        Destroy(hitStoneGO, 3f);
    }

    void HitOnSand(Collision col)
    {
        int rClip = Random.Range(0, hitSand.Length);
        audioBulletHead.PlayOneShot(hitSand[rClip]);

        GameObject hitSandGO = Instantiate(impactSand);
        hitSandGO.transform.position = transform.position;
        hitSandGO.transform.rotation = Quaternion.LookRotation(col.GetContact(0).normal, col.transform.right);

        Destroy(hitSandGO, 3f);
    }

    float RandomPitchInRange(float range)
    {
        if (bulletTime.bulletTimeActive) {
            defaultPitch = 0.5f;
        } else {
            defaultPitch = 1f;
        }
        return Random.Range(defaultPitch - range, defaultPitch + range);
    }
}
