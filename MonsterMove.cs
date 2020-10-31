using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    BoxCollider2D collider;
    PlayerMove playerMove;
    Vector3 dirVec;
    public int moveDir;    // Moving direction, Random
    public int moveF;
    public float monsterHp;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        playerMove = GetComponent<PlayerMove>();
        monsterAI();
        monsterHp = 3;
    }
    void Update() {
        if (rigid.velocity.x > 0.1f) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }

        if (moveDir == -1)
            dirVec = Vector3.left;
        else if (moveDir == 1)
            dirVec = Vector3.right;

    }
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(moveDir, rigid.velocity.y);   // no jump monster

        //Player Scan Ray
        Debug.DrawRay(rigid.position, dirVec, new Color(1, 0, 0));
        RaycastHit2D monsterRay = Physics2D.Raycast(rigid.position, dirVec, 4f, LayerMask.GetMask("Wall"));
        if(monsterRay.collider != null) {
            if (monsterRay.distance < 0.5f) {
                moveDir = -moveDir;  // Change direction
            }
        }
    }

    void monsterAI() {
        moveDir = Random.Range(-1, 2);   // -1<= ranNum <2
        // change frequency,  2 can be random num like moveDir   or    public float to see in inspector

        Invoke("monsterAI", moveF);
    }
    // Monster Damage
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "bullet") {
            Damaged();
        }
    }
    // Damaged
    public void Damaged() {
        monsterHp = monsterHp - 1; // playerMove.power;
        if (monsterHp <= 0) {
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            collider.enabled = false;
            Invoke("Destory", 3f);
        }
    }

    void Destory() {
        gameObject.SetActive(false);
    }
}
