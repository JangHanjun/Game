using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSentence : MonoBehaviour
{
    public string[] sentences;  // NPC의 대사를 받을 배열
    public Transform chatTr;  // 말풍선의 생성 위치를 담을 변수
    public GameObject chatBoxPrefab;  // 만든 챗 박스
    public bool isTalk = false;




    //아래 코드는 플레이어가 왔다갔다 하면 계속 나온다는 거지같은 기능이 있다.
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            if(isTalk == false) {
                TalkNpc();
                isTalk = true;
            }
            Invoke("TFchange", 6f); // 두 문장이라서 6초라고 했음
        } 
    }

    //다시 대화할 수 있게 해줌 = 이거 없으면 한번만 말하게 됨
    private void TFchange (){
        if (isTalk == true)
            isTalk = false;
    }

    public void TalkNpc() {
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
     }
}
