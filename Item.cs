using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //아이템 종류와 값을 저장할 변수들
    public enum  Type { Coin, Heart, Weapon};
    public Type type;
    public int value;
    void Update() {
        transform.Rotate(Vector3.up * 70 * Time.deltaTime);    // Item spinning
    }
}
