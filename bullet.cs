using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class bullet : MonoBehaviour {
    public float speed;                                     // 공속
    public float destoryTime;                               // 사정거리
    float power;                                    // 공격력
    public float distance;
    public LayerMask layer;

    void Awake(){
        power = playerAttack.atk;
    }
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
                EnemyDamaged enemy = ray.collider.GetComponent<EnemyDamaged>();
                Debug.Log("Enemy Hit");
                enemy.curHp -= power;
                enemy.Damaged();
                
            }
        }
    }
}
