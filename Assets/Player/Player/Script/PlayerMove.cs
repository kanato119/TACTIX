using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Netcode;

public class PlayerMove : NetworkBehaviour
{
    public static PlayerMove instance { get; private set; }

    //現在のスピードを保存
    public float currentSpeed;

    //通常時のスピード・走っているときのスピード
    [SerializeField] private float moveSpeed = 1.5f, runSpeed = 3;

    [SerializeField] private float airMultiplier = 0.6f;

    [SerializeField] private float fallMutipler = 3f;

    //ジャンプ力
    [SerializeField] private float jumpForce = 14;

    //Rayの長さ
    private float playerHeight = 2;

    //地面の摩擦力
    [SerializeField] private float groundDrag = 5;

    //地面にいるかどうか
    public bool grounded, isReadyToJump;

    //どこを向いているか
    public Transform orientation;

    //縦・横のキー設定
    private float horizontalInput, verticalInput;

    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Awake()
    {
        if (!IsOwner) return;
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!IsOwner) return;
        rb = GetComponent<Rigidbody>();

        currentSpeed = runSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);
        //network系統
        if (!IsOwner) return;

        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);

        if (!grounded)
        {
            isReadyToJump = false;

            Invoke(nameof(IsResetJump), 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isReadyToJump && grounded)
        {
            Jump();
        }

        MovePlayer();

        SpeedControl();

        bool isRuning = Input.GetKey(KeyCode.LeftControl);

        currentSpeed = isRuning ? moveSpeed : runSpeed;

        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        if (rb.linearVelocity.y < 0)
        {
            rb.AddForce(
                Vector3.up * Physics.gravity.y * (fallMutipler - 1),
                ForceMode.Acceleration
            );
        }
    }

    private void MovePlayer()
    {
        if (!IsOwner) return;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);
        }

        else
        {
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        //rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        if (!IsOwner) return;
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > currentSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * currentSpeed;

            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        if (!IsOwner) return;
        //上方向速度リセット
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //上方向へ力を加える
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    }

    private void IsResetJump()
    {
        if (!IsOwner) return;
        isReadyToJump = true;
    }

    
}
