using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public GameObject targetObj;
    public GameObject toObj;

    public Image Panel;   // 페이드인-아웃을 위한 이미지 불러오기
    float time = 0f;
    float F_time = 0.3f;


    // 텔레포트
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            targetObj = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.LeftControl) ){
            Invoke("StartTeleport", 0.3f);
            Fade();
        }
    }

    public void StartTeleport() {
        StartCoroutine(TeleportRoutine());
    }

    IEnumerator TeleportRoutine() {
        yield return null;
        targetObj.transform.position = toObj.transform.position;
    }

    //텔레포트시 페이드 인 아웃
    public void Fade() {
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow() {
        Panel.gameObject.SetActive(true);
        Color alpha = Panel.color;
        //어두워지기
        while (alpha.a < 1f) {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        time = 0f;

       yield return new WaitForSeconds(0.3f);
        //밝아지기
        while (alpha.a > 0f) {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }
}
