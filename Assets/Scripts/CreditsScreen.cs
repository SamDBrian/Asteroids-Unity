using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreen : MonoBehaviour
{
    public GameManager GameManager;

    public void DisplayCreditsScreen(){
        gameObject.SetActive(true);
    }

    public void TearDownCreditsScreen(){
        gameObject.SetActive(false);
    }

    public void Update(){
        if (Input.GetKey(KeyCode.Escape)){
            MainMenuButton();
        }
    }

    public void MainMenuButton(){
        //GameManager.audioManager.Play("ButtonPressed");
        GameManager.DisplayStartScreen();
        TearDownCreditsScreen();
    }
}
