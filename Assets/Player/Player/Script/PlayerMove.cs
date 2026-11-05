using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    //現在のスピードを保存
    public float currentSpeed;

    //通常時のスピード・走っているときのスピード
    private float moveSpeed = 1.5f , runSpeed = 3;

    //Rayの長さ
    private float playerHeight = 2;

    //地面の摩擦力
    private float groundDrag;

    //地面にいるかどうか
    private bool grounded;

    //どこを向いているか
    public Transform orientation;
    
    //縦・横のキー設定
    private float horizontalInput, verticalInput;
    
    private Vector3 moveDirection;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentSpeed = runSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);


        MovePlayer();

        SpeedControl();

        bool isRuning = Input.GetKey(KeyCode.LeftControl);

        currentSpeed = isRuning ? moveSpeed : runSpeed;

        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
    }
    private void MovePlayer()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            if (moveDirection.magnitude > 0)
            {
                rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);
            }
            else
            {
                rb.AddForce(moveDirection.normalized * currentSpeed * 10f , ForceMode.Force);
            }
        }

        //rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > currentSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * currentSpeed;

            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
