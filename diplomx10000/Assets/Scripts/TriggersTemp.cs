using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggersTemp : MonoBehaviour
{
    //ссылки на элементы
    public GameObject miniGame;
    public GameObject AfterminiGame;
    public GameObject AfterNewminiGame;
    public GameObject hintEnd;
    public GameObject miniGameText;
    public GameObject miniGameNew;
    public GameObject miniGameTextNew;
    public Text collectT;
    //хранение количества собранных объектов
    int collect;
   //проверка запуска игры
    bool gameStarted;
    // Update is called once per frame
    private void Start()
    {
        miniGameText.SetActive(false);
    }
    //методы на запуск и выключение мини-игр
    void StartMGame()
    {
        miniGame.SetActive(true);
    }
    void StartMGameNew()
    {
        miniGameNew.SetActive(true);
    }
    public void EndMGame()
    {
        collect += 5;
        collectT.text = collect.ToString();
        gameStarted = false;
        miniGame.SetActive(false);
        AfterminiGame.SetActive(true);
    }
    public void EndMGameNew()
    {
        collect += 5;
        collectT.text = collect.ToString();
        gameStarted = false;
        miniGameNew.SetActive(false);
        AfterNewminiGame.SetActive(true);
    }
    //обработка вхождени€ в коллайдеры
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MiniGame"))
            {
            
            miniGameText.SetActive(true);
        }
        if (collision.CompareTag("MiniGameNew"))
        {

            miniGameTextNew.SetActive(true);
        }

        if(collision.CompareTag("EndGame")&& collect==20)
        {
            FindObjectOfType<TimeLineEnd>().end = true;

            FindObjectOfType<MenuController>().Invoke("ToMenu", 3f);
            FindObjectOfType<PlayerMove>().enabled = false;
        }
        else if(collision.CompareTag("EndGame") && collect != 20)
        {
            hintEnd.SetActive(true);
        }

        if (collision.CompareTag("Take"))
        {
            collision.GetComponent <Renderer>().material.SetFloat("_OutlineEnabled", 1);
        }
        }
    //обработка выхода из коллайдеров
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("MiniGame"))
        {
            miniGameText.SetActive(false);
            
        }
        if (collision.CompareTag("MiniGameNew"))
        {
            miniGameTextNew.SetActive(false);
        }
        AfterminiGame.SetActive(false);
        AfterNewminiGame.SetActive(false);

        hintEnd.SetActive(false);
        if (collision.CompareTag("Take"))
        {
            collision.GetComponent<Renderer>().material.SetFloat("_OutlineEnabled", 0);
        }
    }
    //обработка нахождени€ в коллайдерах
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("MiniGame") && Input.GetKey(KeyCode.E) && !gameStarted)
        {
            gameStarted = true;
            StartMGame();
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("MiniGameNew") && Input.GetKey(KeyCode.E) && !gameStarted)
        {
            gameStarted = true;
            StartMGameNew();
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Take") && Input.GetKey(KeyCode.E))
        {
            collect += 1;
            collectT.text = collect.ToString();
            Destroy(collision.gameObject);
        }
    }
  
}
