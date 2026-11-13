using System;
using Unity.VisualScripting;
using UnityEngine;

public class RecoilControl : MonoBehaviour
{
    [SerializeField] private Transform orientation;

    [SerializeField] private PlayerCamera playerCamera;

    [SerializeField]
    Vector2[] RecoilPattern =
    {
        new Vector2(1.2f, 0f),
        new Vector2(1.4f, 0.2f),
        new Vector2(1.6f, -0.2f),
        new Vector2(1.8f, 0.3f),
        new Vector2(2.0f, -0.4f)
    };

    [SerializeField] private float TimeReaction = 0.4f;

    [SerializeField] private float resetDelay = 0.8f;

    [SerializeField] private float randomSide = 0.1f;

    private Vector2 targetRecoil;

    private Vector2 CurrentRecoil;

    private Vector2 recoilVelocity;

    private int shotCount = 0;
    private float lastShotTime;

    // Update is called once per frame
    private void Update()
    {
        if(shotCount > 0 && Time.time - lastShotTime >= resetDelay)
        {
            shotCount = 0;
            CurrentRecoil = Vector2.zero;
        }

        CurrentRecoil =
            Vector2.SmoothDamp(
                CurrentRecoil,
                targetRecoil,
                ref recoilVelocity,
                TimeReaction);
    }


    private void LateUpdate()
    {
        float pitch = playerCamera.yRotation - CurrentRecoil.x;

        float yaw = playerCamera.xRotation - CurrentRecoil.y;


        pitch = Mathf.Clamp(pitch,-89f,89);

        transform.rotation = Quaternion.Euler(pitch,yaw,0);

        orientation.rotation = Quaternion.Euler(0, playerCamera.xRotation, 0);
    }

    public void AddRecoil()
    {
        if (RecoilPattern == null || RecoilPattern.Length == 0)
            return;

        lastShotTime = Time.time;

        int index = Mathf.Min(shotCount,RecoilPattern.Length -1);

        Vector2 recoil = RecoilPattern[index];

        recoil.y += UnityEngine.Random.Range(-randomSide, randomSide);

        targetRecoil += recoil;

        shotCount++;
    }
}
