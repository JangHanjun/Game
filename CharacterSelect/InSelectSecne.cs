using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InSelectScene : MonoBehaviour
{
    public Character character; 
    Animator anim;
    SpriteRenderer spriteRenderer;
    public inSelectScene[] chars;
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(CharacterSelect.instance.currentChar == character) {
            OnSelect();
        } else {
            UnSelect();
        }
    }

    private void OnMouseUpAsButton() {
        CharacterSelect.instance.currentChar = character;
        OnSelect();
        for (int i = 0; i < chars.Length; i++) {
            if (chars[i] != this) {
                chars[i].UnSelect();
            }
        }
    }

    void OnSelect() {
        anim.SetBool("isWalk", true);
        spriteRenderer.color = new Color(1f, 1f, 1f);
    }

    void UnSelect() {
        anim.SetBool("isWalk", false);
        spriteRenderer.color = new Color(0.4f, 0.4f, 0.4f);
    }
}
