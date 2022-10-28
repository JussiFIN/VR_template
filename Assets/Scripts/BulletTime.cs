using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletTime : MonoBehaviour
{
    [SerializeField] InputActionProperty bulletTimeActivator;
    public bool bulletTimeActive;

    // Start is called before the first frame update
    void Start()
    {
        bulletTimeActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ääh, miksi löydy boolia input actioneista?
        //eiku joo, tarkasta ne "touched" kohdat "pressedin" sijaan jos tämä toimii
        if (bulletTimeActivator.action.ReadValue<float>() == 1f) {
            StartCoroutine(JustWait());
            bulletTimeActive = !bulletTimeActive;
        }
    }

    IEnumerator JustWait()
    {
        yield return new WaitForSeconds(1);
    }
}
