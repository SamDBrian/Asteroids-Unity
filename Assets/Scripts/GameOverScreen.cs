using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameManager GameManager;
    // Start is called before the first frame update
    public void DisplayGameOver(){
        gameObject.SetActive(true);
    }

    public void TearDownGameOver(){
        gameObject.SetActive(false);
    }

    public void NewGameButton(){
        SceneManager.LoadScene("SampleScene");
    }
    public void EndGameButton(){
        // Comment Me before building:
        UnityEditor.EditorApplication.isPlaying = false;
        // Uncomment Me before build:
        //Application.Quit();
    }
}
