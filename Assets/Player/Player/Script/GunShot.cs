using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class GunShot : NetworkBehaviour
{

    [Header("弾")]
    [SerializeField] GameObject bulletPrefab;

    [Header("銃口")]
    [SerializeField] Transform muzzle;

    [Header("最大距離")]
    [SerializeField] float maxDistance = 1000f;

    [SerializeField] UnityEngine.Camera cam;

    [SerializeField] TextMeshProUGUI RemainingBulletsText;
    [SerializeField] TextMeshProUGUI MaxRemainingBulletsText;

    [SerializeField] private RecoilControl recoil;

    [SerializeField]
    private eWeapons currentWeapon;

    float pressetStartTime;

    Vector3 defalutPoition;

    private bool isNotShoot = false;

    private float nextFireTime;

    // private int AmmoCount;

    private int ShotNumber;

    private bool isReloading = false;

    WeaponManager weaponManager;
    WeaponData weaponData;


    //private NetworkObject playerNetworkObject;

    ///networkvariable
    ///弾数の同機
    public NetworkVariable<int> AmmoCount = new NetworkVariable<int>(
        25,//仮数値
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
        );

    public NetworkVariable<int> TotalAmmo = new NetworkVariable<int>(
        100,//仮数値
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
        );


    private void Awake()
    {
        // WeaponManagerを取得
        weaponManager = GetComponentInParent<WeaponManager>();

        // 武器データを取得
        weaponData = weaponManager.GetWeaponData(currentWeapon);

    }
    // Start is called before the first frame update
    void Start()
    {

        if (!IsOwner) return;


        defalutPoition = transform.localPosition;

        if (cam == null)
            cam = UnityEngine.Camera.main;


    }






    // Update is called once per frame

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            AmmoCount.Value = weaponData.MaxAmmo;
            TotalAmmo.Value = weaponData.MaxTotalAmmo;
        }
    }

    void Update()
    {
        if (!IsOwner) return;

        Debug.Log("Updateに入りました");

        Shooting();

        //リロード
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            if (AmmoCount.Value < weaponData.MaxAmmo && TotalAmmo.Value > 0)
            {
                isReloading = true;

                StartCoroutine(Reload(weaponData.ReloadTime));
            }
        }

        ShowRemainingBullets();
    }


    private void Shooting()
    {
        if (Time.timeScale == 0) return;
        if (weaponData == null) return;
        if (isReloading) return;
        if (isNotShoot) return;

        bool isFire = weaponData.IsRapidFire
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        if (isFire && Time.time >= nextFireTime)
        {
            Debug.Log("左クリックを検知しました");

            Fire();
            nextFireTime = Time.time + weaponData.ShootInterval;
        }
    }

    private void Fire()
    {

        if (!IsOwner) return;

        Debug.Log("ServerRpcを呼び出します");


        //画面中央からRay
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;

        //狙った場所を決める
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(maxDistance);


        //銃口の狙った場所への方向
        Vector3 direction = (targetPoint - muzzle.position).normalized;

        //近距離ズレ防止
        if (Physics.Raycast(muzzle.position, direction, out RaycastHit hit2, maxDistance))
        {
            targetPoint = hit2.point;
            direction = (targetPoint - muzzle.position).normalized;
        }


        Quaternion rotation = Quaternion.LookRotation(direction);


        FireServerRpc(muzzle.position, rotation);
    }

    [ServerRpc]
    private void FireServerRpc(Vector3 position, Quaternion rotation)
    {
        Debug.Log("ServerRpcに入りました");

        if (AmmoCount.Value <= 0)
        {
            Debug.Log("弾切れ");
            return;
        }

        AmmoCount.Value--;

        Debug.Log("現在の弾数：" + AmmoCount.Value);

        GameObject bullet = Instantiate
            (bulletPrefab,
            position,
            rotation);

        Debug.Log("弾を生成しました");

        recoil.RecoilMath();

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

        if (!IsOwner) return;
        RemainingBulletsText.text = AmmoCount.Value.ToString();
        MaxRemainingBulletsText.text = TotalAmmo.Value.ToString();

    }

    private IEnumerator Reload(float time)
    {
        if (!IsOwner) yield return false;

        Debug.Log("リロード開始");

        isNotShoot = true;

        yield return new WaitForSeconds(time);

        isNotShoot = false;
        isReloading = false;

        ReloadServerRpc();
    }

    [ServerRpc]
    void ReloadServerRpc()
    {

        Debug.Log("リロード中");

        int reloadAmount = weaponData.MaxAmmo - AmmoCount.Value;

        int actualReloadAmount = Mathf.Min(reloadAmount, TotalAmmo.Value);

        AmmoCount.Value += actualReloadAmount;

        TotalAmmo.Value -= actualReloadAmount;



        Debug.Log("リロード完了");

    }


}
