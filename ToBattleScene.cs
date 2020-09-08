using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBattleScene : MonoBehaviour {

    // 일단 랜덤 인카운터로 만듬
    int bValue;
    public float timeSpan;     // 지나간 시간 재는거
    float checkTime;  // 밑에 4초로 초기화함
    private void Start() {
        timeSpan += 0.0f;
        checkTime = 4.0f;
    }
    void Update() {
        timeSpan += Time.deltaTime;
        if (timeSpan > checkTime) {
            ToBattle();
            timeSpan = 0; //다시 0으로 초기화
        }
    }

    void ToBattle() {
        bValue = 50;   // 배틀이 일어날 확률
        if (Random.Range(1, 100) < bValue) {
            SceneManager.LoadScene("TurnBasedBattle");   // 배틀 씬으로 전환
        }
    }
}
