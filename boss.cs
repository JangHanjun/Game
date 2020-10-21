using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public Transform target;                                                     //target = player
    Vector2 playerPos;

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            playerPos = target.position;
            Debug.Log(playerPos);
            //todo 이 받은 위치에 공격 생성
        }
    }
}
