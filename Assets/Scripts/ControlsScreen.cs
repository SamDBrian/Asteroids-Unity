using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScreen : MonoBehaviour
{
    public GameManager GameManager;

    public void DisplayControlsScreen(){
        gameObject.SetActive(true);
    }

    public void TearDownControlsScreen(){
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
        TearDownControlsScreen();
    }
}
