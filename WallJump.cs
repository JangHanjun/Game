using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
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
    bool isGround;
    public float checkRadius;
    public int jumpCount;
    public int maxJump;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        jumpCount = maxJump;
    }

    void Update() {
        // Flip sprite  
        if (Input.GetButton("Horizontal")) {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;
        }
        // Walking Animation
        if( Mathf .Abs(rigid.velocity.x) <  0.3) { 
            animator.SetBool("isWalk", false);
        }else {
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
    }
    void FixedUpdate()
    {
        // Moveing
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if(rigid.velocity.x > maxSpeed) {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);  //right
        } else if(rigid.velocity.x < -maxSpeed) {
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y); // left
        }
        
    }
}
