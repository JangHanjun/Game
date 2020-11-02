using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //아이템 종류와 값을 저장할 변수들
    public enum  Type { Coin, Heart, Weapon};
    public Type type;
    public int value;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag =="Player"){
            Debug.Log("플레이어 감지!");
            Destroy(gameObject);
            playerAttack.atk += 1;
        }
    }
}
