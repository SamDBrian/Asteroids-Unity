using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public GameManager GameManager;

    public void DisplayStartScreen(){
        gameObject.SetActive(true);
    }

    public void DisplayControlsScreen(){
        GameManager.DisplayControlsScreen();
        TearDownStartScreen();
    }

    public void TearDownStartScreen(){
        gameObject.SetActive(false);
    }

    public void NewGameButton(){
        GameManager.Respawn();
        GameManager.DisplayUI();
        GameManager.ActivateEnemySpawning();
        TearDownStartScreen();
    }

    public void EndGameButton(){
        // Comment Me before building:
        //GameManager.audioManager.Play("ButtonPressed");
        //UnityEditor.EditorApplication.isPlaying = false;
        // Uncomment Me before build:
        Application.Quit();
    }
}
