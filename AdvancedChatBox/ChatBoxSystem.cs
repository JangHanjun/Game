using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBoxNPC : MonoBehaviour {
    public string[] sentences;  // NPC의 대사를 받을 배열
    public Transform chatBoxPos;  // 말풍선의 생성 위치
    public GameObject chatBoxPrefab;  // 만든 챗 박스
    public bool isTalk = false;  //default

    // NPC Moving
    // 움직이는 NPC들의 경우 rigidbody를 사용하기 때문에 y축 고정을 사용하고 is trigger을 해줌
    Rigidbody2D rigid;
    public int moveDir;
    SpriteRenderer spriteRenderer;   // to flip

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

    // Make NPC Moving
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        NPCMoving();
    }
    private void FixedUpdate() {
        rigid.velocity = new Vector2(moveDir, rigid.velocity.y);   // no jump
    }
    void NPCMoving() {
        moveDir = Random.Range(-1, 2);   // -1<= ranNum <2
        // flip sprite
        if(moveDir > 0) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
        // change frequency,  2 can be random num like moveDir   or    public float to see in inspector
        Invoke("NPCMoving", 0.5f);
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
