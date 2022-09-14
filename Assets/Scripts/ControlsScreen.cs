using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager GameManager;
    // Start is called before the first frame update
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
        GameManager.DisplayStartScreen();
        TearDownControlsScreen();
    }
}
