using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    //переменные для определения скоростей и сопротивления
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float walkSpeed;
    //проверка готовности к прыжку
    bool readyToJump;
    //проверка земли
    public bool grounded;

    //определение кнопки прыжка
    public KeyCode jumpKey = KeyCode.Space;
    //определение размера персонажа
    public float playerHeight;
    //определения слоя земли
    public LayerMask whatIsGround;
    //переменные для управления
    float horizontalInput;
    float verticalInput;
    //вектор направления движения
    Vector3 moveDirection;
    //ссылки на компоненты
    Animator anim;
    Rigidbody2D rb;
    //проверка поворота персонажа 
    bool lookRight = true;
 
    private void Start()
    {
        //получение компонентов и установления дефолтных позиций бул переменных
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        anim = GetComponentInChildren<Animator>();
        readyToJump = true;
    }
    private void Update()
    {
        // проверка земли под ногами
        grounded = Physics2D.Raycast(transform.position, Vector2.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        //вызов основных методов
        MyInput();
        SpeedControl();
        // установка сопротивления
        if (grounded)
        {
            rb.drag = groundDrag;
            anim.SetBool("IsJumping", false);
        } 
        else
        {
            rb.drag = 0;
            anim.SetBool("IsJumping", true);
        }
        //поворот персонажа
        if (horizontalInput > 0 && !lookRight)
        {
            Flip();
        }
         if (horizontalInput < 0 && lookRight)
        {
            Flip();
        }
         //запуск анимации бега
         if (moveDirection.magnitude>0)
        {
            anim.SetBool("isRun", true);
        }
        else
        { anim.SetBool("isRun", false); }
    }
    //вызов метода передвижения каждые 0.2 секунды
    private void FixedUpdate()
    {
        MovePlayer();
    }
    //метод разворота персонажа
    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        lookRight = !lookRight;
    }
    //метод получения нажатий на клавиши
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    //метод движения персонажа
    private void MovePlayer()
    {
        // просчитываем направление 
        moveDirection = transform.right * horizontalInput;
        //если на земле - двигаемся
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 5f, ForceMode2D.Force);
        }
        // in air
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 5f * airMultiplier, ForceMode2D.Force);
        }  
    }
    //контроль скорости
    private void SpeedControl()
    {
        Vector2 flatVel = new Vector2(rb.velocity.x, 0f);
        // ограничение ускорения
        if (flatVel.magnitude > moveSpeed)
        {
            Vector2 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector2(limitedVel.x, rb.velocity.y);
        }
    }
    //метод прыжка
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        anim.SetTrigger("Jump");
    }
    //перезагрузка прыжка
    private void ResetJump()
    {
        readyToJump = true;
    }
}
