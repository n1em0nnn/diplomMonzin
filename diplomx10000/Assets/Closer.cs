using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closer : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
        } 
        
    }
}
