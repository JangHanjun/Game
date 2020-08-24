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
/*
사용방법
 하이어라이키 - ui - image 생성 후 이미지를 검은색, 투명하게 조정
 이후 인스펙터 창에 Panel에 그 이미지를 넣으면 페이드인아웃 효과 완성
 
 텔레포드의 경우 왼쪽 컨트롤키에 반응하게 만들었으며
 텔레포트용 스프라이트에 콜라이더 추가 후 is trigger 한 뒤 원하는 위치에 있는 오브젝트를 To obj에 넣으면 됨.
 간단한 방식이고 한 씬 내에서 이동하는 방법이니 마을 안 집 내부로 이동할때 같은 간단한 작업에 
*/
