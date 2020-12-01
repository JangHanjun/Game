using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  // 클래스 직렬화
public class SlotData
{
    // 슬롯의 데이터만 나타내는 것으로 따로 들어가는데는 없음
    public bool isEmpty;   //슬롯이 비어있는가
    public GameObject slotObj;  // 슬롯 오브젝트를 담을 변수
}
