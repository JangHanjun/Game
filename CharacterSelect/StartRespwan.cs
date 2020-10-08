using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRespwan : MonoBehaviour
{
    public GameObject[] charPrefabs;
    public GameObject player;

    void Start() {
        player = Instantiate(charPrefabs[(int)CharacterSelect.instance.currentChar]);
        player.transform.position = transform.position;
    }
}
