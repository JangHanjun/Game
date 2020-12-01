using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<SlotData> slots = new List<SlotData>();
    private int maxSlot = 3;            // 슬롯의 갯수
    public GameObject slotPrefab;       // 슬롯 Ui 프리팹

    private void Start(){
        GameObject slotPanel = GameObject.Find("Panel");
        for(int i = 0; i < maxSlot; i++){
            // 최대 슬롯의 수만큼 슬롯 생성
            GameObject go = Instantiate(slotPrefab, slotPanel.transform, false);  //프리팹의 생성 위cl
            go.name= "Slot_" + i;         //생성되는 슬롯의 이름
            SlotData a = new SlotData();
            a.isEmpty = true;             // 처음은 비어있으니 true로 초기화
            a.slotObj = go;               //
            slots.Add(a);

        }
    }
}
