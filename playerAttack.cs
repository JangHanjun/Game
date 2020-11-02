using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public GameObject bullet;
    public Transform pos;    
    [SerializeField]
    public static float atk;

    public float coolTime;
    private float curTime;

    void Awake(){
        atk = 2f;
    }
    void Update(){
        // Mouse Position
        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);

        if (curTime <=0){
            if (Input.GetMouseButton(0)){    // Left Mouse Button
                Instantiate(bullet, pos.position, transform.rotation);
            }
            curTime = coolTime;
        }
        curTime -= Time.deltaTime;
    }
}
