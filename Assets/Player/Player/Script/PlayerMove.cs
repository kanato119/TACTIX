using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
     
        rb = GetComponent<Rigidbody>();

        //始めの速度
        currentSpeed = runSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        //network系統
        if (!IsOwner) return;

        // 地面に接地していない場合
        if (!grounded)
        {
            //ジャンプできない
            isReadyToJump = false;

            // ジャンプ可能状態をリセットする処理を呼び出す
            Invoke(nameof(IsResetJump), 0f);
        }

        //スペースが押され、ジャンプ可能かつ接地している場合
        if (Input.GetKeyDown(KeyCode.Space) && isReadyToJump && grounded)
        {
            //ジャンプ
            Jump();
        }

        // プレイヤーの移動処理
        MovePlayer();

        //移動速度の制限
        SpeedControl();

        // 左Ctrlキーが押されているか判定
        bool isRuning = Input.GetKey(KeyCode.LeftControl);

        // 左Ctrlキーを押している間は歩き速度、それ以外は走る速度を設定
        currentSpeed = isRuning ? moveSpeed : runSpeed;

        //地面と接触している場合
        if (grounded)
            //地面との摩擦を設定
            rb.linearDamping = groundDrag;
        else
            //空中なら摩擦を0に
            rb.linearDamping = 0;

    }


    private void FixedUpdate()
    {
        if (!IsOwner) return;

        // Y方向の速度がマイナスなら落下中と判定
        if (rb.linearVelocity.y < 0)
        {
            // 落下中のみ追加の重力を加えて、落下速度を速くする
            rb.AddForce(
                Vector3.up * Physics.gravity.y * (fallMutipler - 1),
                ForceMode.Acceleration
            );
        }
    }


    /// <summary>
    /// Playerの移動
    /// クライアント側で移動をするとServer側に伝える
    /// </summary>
    private void MovePlayer()
    {
        if (!IsOwner) return;

        //左右の移動キー
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //上下の移動キー
        verticalInput = Input.GetAxisRaw("Vertical");

        //移動量のキャスト
        Vector3 direction = moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //地面にいたら
        if (grounded)
        {
            rb.AddForce(direction.normalized * currentSpeed * 10f, ForceMode.Force);
        }

        else
        {
            rb.AddForce(direction.normalized * currentSpeed * 10f * airMultiplier, ForceMode.Force);
        }


        //rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }


   
    /// <summary>
    /// スピードを抑える
    /// </summary>
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

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    private void Jump()
    {
        if (!IsOwner) return;
        //上方向速度リセット
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //上方向へ力を加える
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    }
    
    /// <summary>
    /// もう一度ジャンプができるように
    /// </summary>
    private void IsResetJump()
    {
        if (!IsOwner) return;

        //ジャンプリセット
        isReadyToJump = true;
    }




}
