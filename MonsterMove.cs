using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int moveDir;    // Moving direction, Random

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        monsterAI();
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(moveDir, rigid.velocity.y);   // no jump monster

        // raycast for detect fall
        Vector2 monsterVec = new Vector2(rigid.position.x + moveDir, rigid.position.y);
        //Debug.DrawRay(monsterVec, Vector3.down, new Color(1, 0, 0));   // for debug
        RaycastHit2D raycastHit = Physics2D.Raycast(monsterVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if(raycastHit.collider == null) {
            if (raycastHit.distance < 0.5f) {
                moveDir = -moveDir;  // Change direction
            }
        }

    }

    void monsterAI() {
        moveDir = Random.Range(-1, 2);   // -1<= ranNum <2
        // change frequency,  2 can be random num like moveDir   or    public float to see in inspector
        Invoke("monsterAI", 2);
    }
}
