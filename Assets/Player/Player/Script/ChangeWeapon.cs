using Unity.VisualScripting;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{


    [SerializeField] GameObject Knife;
    [SerializeField] GameObject Pistol;
    [SerializeField] GameObject Assault;

    [SerializeField] GameObject AmmoCountText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //1を押したらナイフ
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Knife.SetActive(true);

            Pistol.SetActive(false);

            Assault.SetActive(false);

            AmmoCountText.SetActive(false);
        }

        //2を押したらピストルを装備
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Knife.SetActive(false);

            Pistol.SetActive(true);

            Assault.SetActive(false);

            AmmoCountText.SetActive(true);
        }

        //3を押したらアサルトを装備
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Knife.SetActive(false);

            Pistol.SetActive(false);

            Assault.SetActive(true);
            AmmoCountText.SetActive(true);
        }
    }
}
