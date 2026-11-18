using System;
using Unity.VisualScripting;
using UnityEngine;

public class RecoilControl : MonoBehaviour
{
    [Range(0, 7f)] public float recoliAmountX;
    [Range(0, 7f)] public float recoliAmountY;
    public float currentRecoilXPos;
    public float currentRecoilYPos;

    [Range(0,10f)] public float maxRecoilTime = 4;

    private float timePressed;

    public void RecoilMath()
    {
        currentRecoilXPos = ((UnityEngine.Random.value - 0.5f) / 2) * recoliAmountX;
        currentRecoilYPos = ((UnityEngine.Random.value -0.5f) / 2) * (timePressed >= maxRecoilTime ? recoliAmountY / 4 : recoliAmountY);
        //wantedCameraXRotation -= Mathf.Abs(currentRecoilYPos);
        //wantedYRotation -= currentRecoilXPos;
    }
}
