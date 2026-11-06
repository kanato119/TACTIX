using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Range(1f, 500f)]
    [SerializeField] float Sens = 500;
    //[Range(1f, 500f)]
    //private float sensY = 500;

    public Transform orientation;

    

    private float xRotation = 0;
    private float yRotation = 0;

    private float xViewpointControl = -89;
    private float yViewpointControl = 89;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * Sens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * Sens;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, xViewpointControl, yViewpointControl);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
