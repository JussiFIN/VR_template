using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletCasing;
    public Transform spawnPos;
    public Transform spawnPosCasing;
    public float speed = 10f;
    PistolSounds pistolSounds;
    [SerializeField] GameObject muzzleFlash;

    bool bulletTimeActivated = false;
    public int bulletsInMagazine = 28;

    void Start()
    {
        pistolSounds = GetComponent<PistolSounds>();

        //XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        XRGrabInteractableTwoAttach grabbable = GetComponent<XRGrabInteractableTwoAttach>();
        grabbable.activated.AddListener(PistolTriggerActivated);      
    }

    public void PistolTriggerActivated(ActivateEventArgs arg)
    {
        if (bulletsInMagazine <= 0) {
            pistolSounds.PlayEmptySound();   
            return;
        } else {
            bulletsInMagazine--;
        }          

        if (bulletTimeActivated) {
            FireBulletRigidbody();
        } else {            
            FireBulletHitScan();
        }

        pistolSounds.PlayShotSound();
        MakeMuzzleFlash();
        ThrowOutBulletCasing();
    }

    void FireBulletRigidbody()
    {
        GameObject spawnedBullet = Instantiate(bullet);     //tehd‰‰n luoti GameObject, "poolaa" t‰m‰ ettei sit‰ aina tehd‰ uudestaan
        spawnedBullet.transform.position = spawnPos.position;
        spawnedBullet.transform.rotation = Quaternion.LookRotation(spawnPos.up, spawnPos.right);  //asetetaan myˆs rotaatio jota ei ollu tutoriaalissa
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPos.forward * speed;        
        //Destroy(spawnedBullet, 3f);   //tuhotaanki PistolBulletHead scriptiss‰
    }

    void FireBulletHitScan()
    { 
        RaycastHit hit;        
        Physics.Raycast(spawnPos.position, spawnPos.forward, out hit, 50f);

        if (!hit.collider) {
            Debug.Log("hit = null  ");
        } else {

            GameObject spawnedBullet = Instantiate(bullet);     //tehd‰‰n luoti GameObject, "poolaa" t‰m‰ 
            spawnedBullet.transform.position = hit.point;
            //Destroy(spawnedBullet, 3f);   //tuhotaanki PistolBulletHead scriptiss‰
        }
    }

    void MakeMuzzleFlash()
    {
        GameObject muzzleFlashGO = Instantiate(muzzleFlash);
        muzzleFlashGO.transform.position = spawnPos.position;
        muzzleFlashGO.transform.rotation = Quaternion.LookRotation(spawnPos.up, spawnPos.right);
        Destroy(muzzleFlashGO, 0.2f);
    }

    void ThrowOutBulletCasing()
    {
        GameObject casingGO = Instantiate(bulletCasing);
        casingGO.transform.position = spawnPosCasing.position;
        casingGO.transform.rotation = Quaternion.LookRotation(spawnPosCasing.up, spawnPosCasing.right);
        casingGO.GetComponent<Rigidbody>().velocity = spawnPosCasing.forward * 10f;
    }
}
