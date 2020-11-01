using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour {
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    BoxCollider2D collider;
    PlayerMove playerMove;
    Vector3 dirVec;
    public int moveDir;    // Moving direction, Random
    public int moveF;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        monsterAI();
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
    void FixedUpdate() {
        rigid.velocity = new Vector2(moveDir, rigid.velocity.y);   // no jump monster

        //Player Scan Ray
        Debug.DrawRay(rigid.position, dirVec, new Color(1, 0, 0));
        RaycastHit2D monsterRay = Physics2D.Raycast(rigid.position, dirVec, 4f, LayerMask.GetMask("Wall"));
        if (monsterRay.collider != null) {
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
}
