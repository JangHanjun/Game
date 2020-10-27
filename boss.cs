using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour {
    public Transform target;                                                     //target = player
    Vector2 playerPos;
    // Vector2 atk1Pos;

    // warnning before actual attack
    public GameObject preAtk1;
    Vector2 atkPos;
    // actual attack prefab
    public GameObject atk1;
    bool isAtk1;

    // Pattern2

    private void Awake() {
        isAtk1 = false;
        prePattern1();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerPos = target.position;
            // Debug.Log(playerPos);            // 플레이어의 위치를 감지하고 있는가 아니면 첫 좌표만 기억하고 있는가 알기 위한 디버그 코드
            isAtk1 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        isAtk1 = false;
    }
    // attack pattern 1
    // 선 경고 후 공격이 되야 한다
    // pattern1 의 경우 경고 sprite를 내보내고 pattern1Atk에서 실제 공격이 이루어진다
    // destory는 각 프리팹에서 실행된다
    void prePattern1() {
        if (isAtk1 == true) {
            atkPos = playerPos;                 // preAtk1.position == atk1.position을 하기 위한 변수
            Instantiate(preAtk1, atkPos, transform.rotation);     // preAtk1 프리팹 안에 스스로 Destory하는 코드가 있다.
        }
        Invoke("pattern1", 2f);
    }
    void pattern1() {
        if (isAtk1 == true) {
            //Destroy(this.preAtk1);
            Instantiate(atk1, atkPos, transform.rotation);
        }
        Invoke("prePattern1", 2f);
    }


}
