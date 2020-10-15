using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBoxSystem : MonoBehaviour {
    public Queue<string> sentences;
    public string currentSentence;
    public TextMeshPro text;
    public GameObject quad;

    public void Ondialogue(string[] lines, Transform chatPoint) {
        transform.position = chatPoint.position;
        sentences = new Queue<string>();
        sentences.Clear();
        foreach (var line in lines) {
            sentences.Enqueue(line);
        }
        StartCoroutine(DialogueFlow(chatPoint));
    }
    // output Queue
    IEnumerator DialogueFlow(Transform chatPoint) {
        yield return null;
        while (sentences.Count > 0) {
            currentSentence = sentences.Dequeue();
            text.text = currentSentence;
            float x = text.preferredWidth;
            x = (x > 3) ? 3 : x + 0.3f;
            quad.transform.localScale = new Vector2(x, text.preferredHeight + 0.3f);

            // todo : 움직이는 NPC의 경우 말풍선이 따라다니지 않아 조금 어색함
            // 말풍선 따라다니는 로직을 만들어 보자
            transform.position = new Vector2(chatPoint.position.x, chatPoint.position.y + text.preferredHeight / 2);
            yield return new WaitForSeconds(2f);
        }
        Destroy(gameObject);
    }
}
