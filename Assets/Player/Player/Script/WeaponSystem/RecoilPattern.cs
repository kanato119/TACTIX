using UnityEngine;
using System;
using Unity.VisualScripting;

public class RecoilPattern : MonoBehaviour
{
    [SerializeField] private string sheetName = "RecoilPattern";
    [Header("反動位置")]
    public float currentRecoilXPos;
    public float currentRecoilYPos;

    public float targetRecoilXPos;
    public float targetRecoilYPos;


    [Header("弾の反動")]
    public float TotalBulletX;
    public float TotalBulletY;


    [Header("反動設定")]
    [SerializeField] private float recoilSpeed = 10f;
    [SerializeField] private float resetTime = 0.15f;

    private int bulletNumber = 0;
    private float lastShotTime;
    private bool isShooting;

    public bool IsShooting
    {
        get { return isShooting; }
    }
    public void RecoilMath()
    {
        if(!isShooting || Time.time - lastShotTime > resetTime)
        {
            bulletNumber = 0;

            isShooting = true;
        }

        lastShotTime = Time.time;

        bulletNumber++;

        MasterData.instance.RecoilPatten(bulletNumber, sheetName);

        if (MasterData.instance.dataRecoil == null)
            return;

        targetRecoilXPos += MasterData.instance.dataRecoil.CameraX;
        targetRecoilYPos += MasterData.instance.dataRecoil.CameraY;

        TotalBulletX += MasterData.instance.dataRecoil.BulletX;
        TotalBulletY += MasterData.instance.dataRecoil.BulletY;
    }

    private void Update()
    {
        if (isShooting && Time.time - lastShotTime > resetTime)
        {
            isShooting = false;
        }

        currentRecoilXPos = Mathf.Lerp(currentRecoilXPos, targetRecoilXPos, recoilSpeed * Time.deltaTime);

        currentRecoilYPos = Mathf.Lerp(currentRecoilYPos, targetRecoilYPos, recoilSpeed * Time.deltaTime);
    }

    public void ResetRecoil()
    {
        targetRecoilXPos = 0f;
        targetRecoilYPos = 0f;

        currentRecoilXPos = 0f;
        currentRecoilYPos = 0f;

        TotalBulletX = 0f;
        TotalBulletY = 0f;

        bulletNumber = 0;
        lastShotTime = 0f;
        isShooting = false;
    }
}
