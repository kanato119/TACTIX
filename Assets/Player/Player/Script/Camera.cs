using TMPro;
using UnityEngine;
using Unity.Netcode;

public class Camera : NetworkBehaviour
{
    public Transform cameraPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        transform.position = cameraPosition.position;
        crouch();
    }

    private void crouch()
    {
        if (!IsOwner) return;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("押されている");
        }
    }
}
