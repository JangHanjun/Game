using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Character {
    Settia, Poong
}

public class CharacterSelect : MonoBehaviour
{
    public static CharacterSelect instance;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public Character currentChar;

    public void OnStartButton() {
        // Start Button
        SceneManager.LoadScene("SampleScene");
    }
}
