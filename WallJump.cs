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
    // bool isClimbing;
    float h;
    public float slidingSpeed;
    public float wallJumpPower;
    bool isWallJump;

    //Sliding
    public float slidingPower;

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

        // Sliding
        if(isGround == true && Input.GetKeyDown(KeyCode.LeftShift)){
            animator.SetBool("isSliding", true);
            //누르면 속도를 올릴려고 했으나 실패
           // rigid.velocity = new Vector2(h * 0.9f * slidingPower, rigid.velocity.y);
            Invoke("slidingFalse", 0.5f);
            // 아주 짧은 무적 시간을 해두자
            // Stamina
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
            animator.SetBool("isClimbing", true);
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * slidingSpeed);
            //WallJump
            if (Input.GetAxis("Jump") != 0) {
                isWallJump = true;
                Invoke("FreezX", 0.5f);
                rigid.velocity = new Vector2(-0.9f * wallJumpPower, 0.9f * wallJumpPower);
               if (spriteRenderer.flipX) {
                    spriteRenderer.flipX = false;
                      } else if (!spriteRenderer.flipX) {
                    spriteRenderer.flipX = true;
                      }
            }
        } else {
            animator.SetBool("isClimbing", false);
        }
    }

    //Stop moving after walljump
    void FreezX() {
        isWallJump = false;
    }
    //Stop Sliding
    void slidingFalse() {
        animator.SetBool("isSliding", false);
    }
    // Monster Damage
    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Enemy") {
            playerDamaged(collision.transform.position);
        }
    }
    void playerDamaged(Vector2 enemyPos) {
        gameObject.layer = 12;   //change layer to Player Damaged layer
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);   // Damaged Effect
        // Enemy > Add Force
        int dir = transform.position.x - enemyPos.x > 0 ? 1 : -1;                           // enemy is on right = 1, else = -1
        rigid.AddForce(new Vector2(dir, 1) * 7, ForceMode2D.Impulse);    // 
        // TODO : HP decrease
        // TODO : Animation
        Invoke("returnLayer", 1);  // invincible time
    }
    void returnLayer() {
        gameObject.layer = 11;  // change layer to Player layer
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
