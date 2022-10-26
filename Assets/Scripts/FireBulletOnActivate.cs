using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPos;
    public float speed = 20f;
    PistolSounds pistolSounds;

    void Start()
    {
        pistolSounds = GetComponent<PistolSounds>();

        //XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        XRGrabInteractableTwoAttach grabbable = GetComponent <XRGrabInteractableTwoAttach>();
        grabbable.activated.AddListener(FireBullet);
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        GameObject spawnedBullet = Instantiate(bullet);     //tehdään luoti GameObject, "poolaa" tämä ettei sitä aina tehdä uudestaan
        spawnedBullet.transform.position = spawnPos.position;
        spawnedBullet.transform.rotation = Quaternion.LookRotation(spawnPos.forward, spawnPos.up);  //asetetaan myös rotaatio jota ei ollu tutoriaalissa
        pistolSounds.PlayShotSound();
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPos.forward * speed;
        Destroy(spawnedBullet, 3f);
    }
}
