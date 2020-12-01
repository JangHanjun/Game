using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // 퀵슬롯에 들어갈 아이템에 넣어주는 스크립트
    // 인스펙터 slotItem에 아이템의 ui가 들어가야 하니
    // 하이어라키 > 이미지 를 통해 만든 아이템의 이미지를 slotItem에 넣어주면 됨
    public GameObject slotItem;     // 생성할 아이템의 ui 프리팹
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag.Equals("Player")){ //플레이어가 닫는 순간 실행
            Inventory inven = collision.GetComponent<Inventory>();
            for(int i = 0; i < inven.slots.Count; i++){        // 인벤토리 슬롯을
                if(inven.slots[i].isEmpty){                    // 처음부터 검사해서 비어있는지 확인
                    Instantiate(slotItem, inven.slots[i].slotObj.transform, false);  // 비어있는 칸에 아이템 ui 생성
                    inven.slots[i].isEmpty = false;     // 사용했으니 false
                    Destroy(gameObject);                // 필드에서 아이템 삭제
                    break;
                }
            }
        }
    }
}
