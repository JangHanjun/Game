using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    Inventory inventory;
    public int num; // slot의 번호
    void Start(){
        // todo : 이후 게임을 만든다면 플레이어블 캐릭터의 이름을 모두 player 으로 통일해야 한다
        // 그에 따라 이후에 이 스크립트를 쓴다면 poong를 바꾸는걸 잊지 마라
        inventory = GameObject.Find("Poong").GetComponent<Inventory>();
        num = int.Parse(gameObject.name.Substring(gameObject.name.IndexOf("_") + 1));
    }
    void Update(){
        if(transform.childCount <= 0){
            inventory.slots[num].isEmpty = true;
        }
    }
}
