using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public Transform target;                                                     //target = player
    Vector2 playerPos;
    // warnning before actual attack
    public GameObject preAtk1;
    // actual attack prefab
    public GameObject atk1;
    bool isAtk1;
    private void Awake() {
        isAtk1 = false;
        //Invoke("pattern1", 1);
        pattern1();
    }
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerPos = target.position;
            Debug.Log(playerPos);
            isAtk1 = true;
        }
    }

    // attack pattern 1
    // 선 경고 후 공격이 되야 한다
    // pattern1 의 경우 경고 sprite를 내보내고
    // pattern1Atk에서 실제 공격이 이루어진다고 하자
    void pattern1() {
        if(isAtk1 == true) {
            Instantiate(atk1, playerPos, transform.rotation);
            isAtk1 = false;
        }
        Invoke("pattern1", 3);
    }


}
