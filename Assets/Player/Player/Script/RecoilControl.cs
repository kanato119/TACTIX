using System;
using Unity.VisualScripting;
using UnityEngine;

public class RecoilControl : MonoBehaviour
{
    [Range(0, 7f)] public float recoliAmountX;
    [Range(0, 7f)] public float recoliAmountY;
    public float currentRecoilXPos;
    public float currentRecoilYPos;


    public float targetRecoilXPos;
    public float targetRecoilYPos;

    [Range(0,10f)] public float maxRecoilTime = 4;
    private float timePressed;

    private float lastShotTime;

    private bool isShooting;

    public bool IsShooting
    {
        get { return isShooting; }
    }

    public void RecoilMath()
    {
        if(!isShooting)
        {
            timePressed = 0f;
            isShooting = true;
        }

        lastShotTime = Time.time;

        timePressed += Time.deltaTime;

        float recoilPower = 1f + timePressed / Mathf.Max(maxRecoilTime, 0.01f);

        targetRecoilXPos += recoliAmountX * recoilPower;
        targetRecoilYPos += UnityEngine.Random.Range(-recoliAmountY,recoliAmountY) * recoilPower;
    }

    private void Update()
    {
        if (isShooting && Time.time - lastShotTime > 0.15f)
        {
            isShooting = false;
        }

        currentRecoilXPos = Mathf.Lerp(currentRecoilXPos, targetRecoilXPos , 10f * Time.deltaTime);

        currentRecoilYPos = Mathf.Lerp(currentRecoilYPos, targetRecoilYPos,10f * Time.deltaTime);
    }

    public void ResetRecoil()
    {
        targetRecoilXPos = 0f;
        targetRecoilYPos = 0f;

        currentRecoilXPos = 0f;
        currentRecoilYPos = 0f;
    }
}
