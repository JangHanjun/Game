using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destory : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float time;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 1.9f);
    }

    void Update() {
        if( time < 0.5f) {
            spriteRenderer.color = new Color(1, 1, 1, 1 - time);
        } else {
            spriteRenderer.color = new Color(1, 1, 1, time);
            if( time > 1f) {
                time = 0;
            }
        }
        time += Time.deltaTime;
    }

}
