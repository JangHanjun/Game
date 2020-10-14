using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBoxNPC : MonoBehaviour {
    public string[] sentences;  // NPC의 대사를 받을 배열
    public Transform chatBoxPos;  // 말풍선의 생성 위치
    public GameObject chatBoxPrefab;  // 만든 챗 박스
    public bool isTalk = false;  //default
    //public float sentenceLength;     chatboxsystem의 sentence 큐 길이를 구하면 이 변수를 이용해 아래 invoke 시간을 조정할 수 있을 것 같다.
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (isTalk == false) {
                TalkNpc();
            } else {
                return;
            }
        }
    }

    //다시 대화할 수 있게 해줌 = 이거 없으면 한번만 말하게 됨
    private void TFchange() {
        if (isTalk == true)
            isTalk = false;
    }

    public void TalkNpc() {
        isTalk = true;
        GameObject go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatBoxSystem>().Ondialogue(sentences, chatBoxPos);
        Invoke("TFchange", 7f); // 두 문장이라서 7초라고 했음   (문장의 길이)*2 + 1
    }
}
