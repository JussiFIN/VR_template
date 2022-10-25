using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPos;
    public float speed = 20f;

    void Start()
    {
        //XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        XRGrabInteractableTwoAttach grabbable = GetComponent < XRGrabInteractableTwoAttach>();
        grabbable.activated.AddListener(FireBullet);
    }

    public void FireBullet(ActivateEventArgs arg)
    {
        GameObject spawnedBullet = Instantiate(bullet);     //tehdään luoti GameObject, "poolaa" tämä ettei sitä aina tehdä uudestaan
        spawnedBullet.transform.position = spawnPos.position;   //asetetaan sen position
        spawnedBullet.transform.rotation = Quaternion.LookRotation(spawnPos.forward, spawnPos.up);  //asetetaan myös rotaatio jota ei ollu tutoriaalissa
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPos.forward * speed;    //annetaan vauhtia
        Destroy(spawnedBullet, 3f);     //tuhotaan luoti 3 sekunnin kuluttua
    }
}
