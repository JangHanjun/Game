// 플레이어를 쫒아다니는 몬스터 구현 
// 몬스터용 콜라이더랑 감지용 원형 콜라이더(이건트리거) 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingMonster : MonoBehaviour {
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public int moveDir;    // Moving direction, Random
    public Transform target; //target = player

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterAI();
    }

    void Update() {
        if (rigid.velocity.x > 0.1f) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate() {
        rigid.velocity = new Vector2(moveDir, rigid.velocity.y);   // no jump monster
    }

    void monsterAI() {
        moveDir = Random.Range(-1, 2);   // -1<= ranNum <2
        Invoke("monsterAI", 3);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // find player
        if (collision.gameObject.tag == "Player") {
            CancelInvoke("monsterAI");   // 이 부분에서 함수를 바로 취소하지 못해 왔다갔다 하면 딜레이가 조금 있다.
            if(target.position.x > transform.position.x) {
                moveDir = 3;     // speed up
            } else if (target.position.x < transform.position.x) {
                moveDir = -3;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player")
            monsterAI();    // return to normal
    }

}
