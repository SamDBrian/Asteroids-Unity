using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameManager GameManager;

    public void DisplayGameOver(){
        gameObject.SetActive(true);
    }

    public void TearDownGameOver(){
        gameObject.SetActive(false);
    }

    public void NewGameButton(){
        GameManager.audioManager.Play("ButtonPressed");
        SceneManager.LoadScene("Disasteroids");
    }
    public void EndGameButton(){
        // Comment Me before building:
        //GameManager.audioManager.Play("ButtonPressed");
        //UnityEditor.EditorApplication.isPlaying = false;
        // Uncomment Me before build:
        Application.Quit();
    }
}
