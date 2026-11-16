using Unity.Netcode;
using UnityEngine;

public class PlayerCamera : NetworkBehaviour
{
    
    [Range(1f, 500f)]
    [SerializeField] float Sens = 500;
    //[Range(1f, 500f)]
    //private float sensY = 500;

    public Transform orientation;

    

    public float xRotation = 0;
    public float yRotation = 0;

    private float xViewpointControl = -89;
    private float yViewpointControl = 89;

    [SerializeField] AudioListener audioListener;
    [SerializeField] CameraPosition _playerCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (!IsOwner) return;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public override void OnNetworkSpawn()
    {

        //AudioListener audioListener = GetComponent<AudioListener>();
        //Camera camera = GetComponent<Camera>();

        //if (camera == null) return;
        //if (audioListener == null) return;
        if (_playerCamera == null)
        {
            Debug.LogError("CameraÇ™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÅI");
            return;
        }

        if (audioListener == null)
        {
            Debug.LogError("AudioListenerÇ™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÅI");
            return;
        }

        _playerCamera.enabled = IsOwner;
        audioListener.enabled = IsOwner;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;


        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * Sens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * Sens;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, xViewpointControl, yViewpointControl);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
