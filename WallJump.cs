using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    // Walk
    public float maxSpeed;

    //Jump
    [SerializeField]
    Transform pos;
    [SerializeField]
    LayerMask islayer;
    public float jumpPower;
    public bool isGround;
    public float checkRadius;
    public int jumpCount;
    public int maxJump;

    //WallJump
    Vector3 dirVec; //wallJumpRay's direction
    //GameObject scanObject; // for debug
    bool isWall;
    float h;
    public float slidingSpeed;
    public float wallJumpPower;
    bool isWallJump;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        jumpCount = maxJump;
    }

    void Update() {
        // Flip sprite  
        if (Input.GetButton("Horizontal") && !isWallJump) {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;
        }

        // Walking Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3) {
            animator.SetBool("isWalk", false);
        } else {
            animator.SetBool("isWalk", true);
        }

        //JUMP
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, islayer);
        if (isGround == true && Input.GetKeyDown(KeyCode.Space) && jumpCount > 0) {
            rigid.velocity = Vector2.up * jumpPower;
        }
        if (isGround == false && Input.GetKeyDown(KeyCode.Space) && jumpCount > 0) {
            rigid.velocity = Vector2.up * jumpPower;
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            jumpCount--;
        }
        if (isGround) {
            jumpCount = maxJump;
        }

        //Direction (Right or Left)
        h = Input.GetAxisRaw("Horizontal");
        if (h == -1)
            dirVec = Vector3.left;
        else if (h == 1)
            dirVec = Vector3.right;
        /*
         // for debug wallJumpRay
        if (scanObject != null) {
            Debug.Log(scanObject.name);
        }
        */
    }
    void FixedUpdate() {
        // Moving
        float h = Input.GetAxisRaw("Horizontal");
        if (!isWallJump) {
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        }

        if (rigid.velocity.x > maxSpeed) {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);  //right
        } else if (rigid.velocity.x < -maxSpeed) {
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y); // left
        }

        //Wall Scan Ray
        Debug.DrawRay(rigid.position, dirVec, new Color(1, 0, 0));
        RaycastHit2D wallJumpRay = Physics2D.Raycast(rigid.position, dirVec, 0.4f, LayerMask.GetMask("Wall"));

        if (wallJumpRay.collider != null) {
            //scanObject = wallJumpRay.collider.gameObject;    // for debug
            isWall = true; // 이후 애니메이션 만든다면 사용할 것
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * slidingSpeed);
            //WallJump
            if(Input.GetAxis("Jump") != 0) {
                isWallJump = true;
                Invoke("FreezX", 0.3f);
                rigid.velocity = new Vector2(-h * wallJumpPower, 0.9f * wallJumpPower);
                if (spriteRenderer.flipX) {
                    spriteRenderer.flipX = false;
                     } else if (!spriteRenderer.flipX) {
                    spriteRenderer.flipX = true;
                }
            }
        } else {
            return;
        }
    }

    //Stop moving after walljump
    void FreezX() {
        isWallJump = false;
    }
}
