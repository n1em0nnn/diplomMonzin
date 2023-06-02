using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject LosePause;
    bool itActive = false;
    PlayerMove moving;
    private void Start()
    {
        moving = FindObjectOfType<PlayerMove>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !itActive)
        {
            ShowPause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && itActive)
        {
            HidePause();
        }
    }
    public void ShowPause()
    {
        LosePause.SetActive(true);
        moving.enabled = false;
        itActive = true;
        Time.timeScale = 0;
    }
    public void HidePause()
    {
        LosePause.SetActive(false);
        moving.enabled = true;
        itActive = false;
        Time.timeScale = 1;
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StarGame()
    {
        SceneManager.LoadScene(1);
    }
 
    public void ExitGame()
    {
        Application.Quit();
    }
   
}
