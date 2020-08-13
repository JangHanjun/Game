using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;

    public float maxSpeed; //속도 제한을 위해
    public float jumpPower; //점프
    SpriteRenderer spriteRenderer; //좌우 이동시 회전, 방향전환

    Vector3 dirVec; //방향을 가져오기 위한 변수
    float h;

    GameObject scanObject;

    Animator anim;     //애니메이션 
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update() //단발적인 키 입력
    {
        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            //normalized 단위벡터로 만들어줌
            rigid.velocity = new Vector2(rigid.velocity.normalized.x  * 0.5f,  rigid.velocity.y);
        }

        //방향전환
        if(Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;

        //애니메이션 컨트롤
        if (rigid.velocity.normalized.x == 0) //속도가 0이라면 = 단위벡터가 0
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);

        
        //점프
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        { 
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }

        //바라보는 방향
        h = Input.GetAxisRaw("Horizontal");

        if ( h == -1)
            dirVec = Vector3.left;
        else if (h == 1)
            dirVec = Vector3.right;

        // 조사 액션을 위한 코드 = 인게임 f키
        if(Input.GetButtonDown("Interaction") && scanObject != null)
        {
            Debug.Log(scanObject.name);
        }
    }

    void FixedUpdate() //지속적인 키 입력은 이쪽
    {
        // 이동을 위한 코드
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h,  ForceMode2D.Impulse);

        if(rigid.velocity.x  > maxSpeed) //오른쪽
            // y축을 0으로 하면 점프 기능이 없어짐
            rigid.velocity = new Vector2(maxSpeed,  rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed*(-1)) //왼쪽
            rigid.velocity = new Vector2(maxSpeed*(-1),  rigid.velocity.y);


        //점프를 위한 레이캐스트 = 2단점프 방지용, 내려오는 중에만 빔이 쏴지도록
        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null) {//빔이 맞으면?
                if (rayHit.distance < 4)
                    anim.SetBool("isJumping", false);
            }
        }

        // 조사를 하기 위한 Ray
        Debug.DrawRay(rigid.position, dirVec * 1.5f, new Color(1, 0, 0));
        RaycastHit2D rayHit2 = Physics2D.Raycast(rigid.position, dirVec, 1.5f, LayerMask.GetMask("Object"));

        if(rayHit2.collider != null)
        {
            scanObject = rayHit2.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
        
    }
}
