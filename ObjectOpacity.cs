using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOpacity : MonoBehaviour
{
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        //sr.sortingOrder = Mathf.RoundToInt(transform.position.y) * -1;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            sr.color = new Color(1, 1, 1, 0.5f);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            sr.color = new Color(1, 1, 1, 1f);
    }
}
