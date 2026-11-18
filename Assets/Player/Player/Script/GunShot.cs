using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunShot : MonoBehaviour
{
    public static GunShot instance { get; private set; }

    [Header("弾")]
    [SerializeField] GameObject bulletPrefab;

    [Header("銃口")]
    [SerializeField] Transform muzzle;

    [Header("発射速度")]
    [SerializeField] float fireRate = 0.2f;

    [Header("最大距離")]
    [SerializeField] float maxDistance = 1000f;

    [Header("次の発射までの時間")]
    [SerializeField] float nextFireTime = 0f;

    [SerializeField] int RemainingBullets = 25;
    [SerializeField] int TotalAmmo = 50;

    [Header("連射ON/OFF")]
    [SerializeField] bool isRapidFire = true;

    [SerializeField] public int Damage;

    [SerializeField] UnityEngine.Camera cam;

    [SerializeField] TextMeshProUGUI RemainingBulletsText;
    [SerializeField] TextMeshProUGUI MaxRemainingBulletsText;

    [SerializeField] private RecoilControl recoil;

    float pressetStartTime;

    Vector3 defalutPoition;

    private bool isNotShoot = false;

    private int AmmoCount;

    private int ShotNumber;

    private bool isReloading = false;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        defalutPoition = transform.localPosition;

        if (cam == null)
        {
            cam = UnityEngine.Camera.main;
        }

        AmmoCount = RemainingBullets;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        //押した瞬間だけ記録
        if (Input.GetMouseButtonDown(0))
        {
            pressetStartTime = Time.time;

        }

        if(!isNotShoot)
        {
            //フルオート
            if (isRapidFire)
            {
                if (Input.GetMouseButton(0))
                {
                    if (Time.time - pressetStartTime > 0.2f)
                    {
                        if (Time.time >= nextFireTime)
                        {
                            Fire();

                            nextFireTime = Time.time + fireRate;
                        }
                    }
                }

            }

            //単発
            if (Input.GetMouseButtonUp(0))
            {
                if (Time.time - pressetStartTime <= 0.2f)
                {
                    if (Time.time >= nextFireTime)
                    {
                        Fire();
                        nextFireTime = Time.time + fireRate;
                    }
                }
            }

        }

        if(AmmoCount != RemainingBullets)
        {
            if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            {
                isReloading = true;

                StartCoroutine(Reload(2f));
            }

        }

        ShowRemainingBullets();
    }

    private void Fire()
    {
        
        //画面中央からRay
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;

        //狙った場所を決める
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(maxDistance);
        }

        //銃口の狙った場所への方向
        Vector3 direction = (targetPoint - muzzle.position).normalized;

        //近距離ズレ防止
        if (Physics.Raycast(muzzle.position, direction, out RaycastHit hit2, maxDistance))
        {
            targetPoint = hit2.point;
            direction = (targetPoint - muzzle.position).normalized;
        }


        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject bullet = Instantiate
            (bulletPrefab,
            muzzle.position,
            rotation);

        recoil.RecoilMath();

        Reloading();

        Collider[] playerCollider = transform.root.GetComponentsInChildren<Collider>();

        Collider[] bulletCollider = bullet.GetComponentsInChildren<Collider>();

        foreach (Collider playerCol in playerCollider)
        {
            foreach (Collider bulletCol in bulletCollider)
            {
                Physics.IgnoreCollision(playerCol, bulletCol);
            }
        }
        
    }

    private void ShowRemainingBullets()
    {
        RemainingBulletsText.text = AmmoCount.ToString();
        MaxRemainingBulletsText.text = TotalAmmo.ToString();
    }

    private void Reloading()
    {

        AmmoCount -= 1;
        ShotNumber += 1;

        if (AmmoCount <= 0)
        {
            AmmoCount = 0;

            isNotShoot = true;
        }

        if(TotalAmmo <= 0)
        {
            TotalAmmo = 0;

            isReloading = true;
        }

    }

    IEnumerator Reload(float time)
    {
        isNotShoot = true;
        int reloadAmount = RemainingBullets - AmmoCount;

        int currentAmmo = AmmoCount;

        Debug.Log("リロード中");
        AmmoCount = 0;
        yield return new WaitForSeconds(time);
        int actualReloadAmount = Mathf.Min(reloadAmount, TotalAmmo);

        AmmoCount =currentAmmo + actualReloadAmount;

        TotalAmmo -= actualReloadAmount;

        isNotShoot = false;
        isReloading = false;
        Debug.Log("リロード完了");
    }
}
