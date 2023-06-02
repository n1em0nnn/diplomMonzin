using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForMiniGame : MonoBehaviour
{
    Rigidbody2D rb;
    float moveVer;
    public float moveSpeed;
    float moveHor;
    Vector2 dir;
    GameObject player;
    public GameObject maincam;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMove>().enabled = false;
      
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        moveHor = Input.GetAxis("Horizontal");
        moveVer = Input.GetAxis("Vertical");
        if (moveHor > 0)
         sprite.flipX = true; 
        if(moveHor < 0)
            sprite.flipX = false;

    }
    private void FixedUpdate()
    {
        dir = transform.right * moveHor + transform.up * moveVer;
        rb.AddForce(dir.normalized * moveSpeed, ForceMode2D.Force);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Finish"))
        {
            player.GetComponent<PlayerMove>().enabled = true;
            maincam.SetActive(true);
            player.GetComponent<TriggersTemp>().EndMGame();
        }
    }

}
