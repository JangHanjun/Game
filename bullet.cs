
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class bullet : MonoBehaviour {
    public float speed;                                     // 공속
    public float destoryTime;                   // 사정거리
    public float power;                                    // 공격력

    public LayerMask layer;

    //MonsterMove monsterMove;
    void Start() {
        Destroy(gameObject, destoryTime);
    }
    void Update() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Destory bullet 
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, 0f, layer);
        if (ray.collider != null) {
            Destroy(gameObject);
        }
    }
}

/*  총알이 떄리는 것에 초점을 둔 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class bullet : MonoBehaviour {
    public float speed;                                     // 공속
    public float destoryTime;                   // 사정거리

    public float distance;
    public LayerMask layer;

    MonsterMove monsterMove;
    void Start() {
        Destroy(gameObject, destoryTime);
    }
    void Update() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Destory bullet 
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, layer);
        if (ray.collider != null) {
            Destroy(gameObject);
            if (ray.collider.tag == "Enemy") {
                monsterMove = ray.collider.GetComponent<MonsterMove>();
                Debug.Log("Enemy Hit");
                monsterMove.Damaged();
            }
        }
    }
}
*/
