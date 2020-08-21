using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public Transform target; //플레이어를 타깃으로 설정하면 됨
    public float speed; // 카메라 스피드

    public Vector2 center;
    public Vector2 size;

    float height;
    float width;

    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        //transform.position = new Vector3(transform.position.x, transform.position.y, -5f);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -5f);
    }

}


/*
  이 코드를 메인 카메라에 적용한 뒤 인스첵터 창에서 플레이어 설정, 스피드를 성정하면 됨
  스피드가 낮으니 프레임이 끊기는 것 같은 현상이 있어 속도를 1000이상으로 두는걸 
*/
