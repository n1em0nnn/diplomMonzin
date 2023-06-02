using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    //���������� ��� ����������� ��������� � �������������
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float walkSpeed;
    //�������� ���������� � ������
    bool readyToJump;
    //�������� �����
    public bool grounded;

    //����������� ������ ������
    public KeyCode jumpKey = KeyCode.Space;
    //����������� ������� ���������
    public float playerHeight;
    //����������� ���� �����
    public LayerMask whatIsGround;
    //���������� ��� ����������
    float horizontalInput;
    float verticalInput;
    //������ ����������� ��������
    Vector3 moveDirection;
    //������ �� ����������
    Animator anim;
    Rigidbody2D rb;
    //�������� �������� ��������� 
    bool lookRight = true;
 
    private void Start()
    {
        //��������� ����������� � ������������ ��������� ������� ��� ����������
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        anim = GetComponentInChildren<Animator>();
        readyToJump = true;
    }
    private void Update()
    {
        // �������� ����� ��� ������
        grounded = Physics2D.Raycast(transform.position, Vector2.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        //����� �������� �������
        MyInput();
        SpeedControl();
        // ��������� �������������
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
        //������� ���������
        if (horizontalInput > 0 && !lookRight)
        {
            Flip();
        }
         if (horizontalInput < 0 && lookRight)
        {
            Flip();
        }
         //������ �������� ����
         if (moveDirection.magnitude>0)
        {
            anim.SetBool("isRun", true);
        }
        else
        { anim.SetBool("isRun", false); }
    }
    //����� ������ ������������ ������ 0.2 �������
    private void FixedUpdate()
    {
        MovePlayer();
    }
    //����� ��������� ���������
    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        lookRight = !lookRight;
    }
    //����� ��������� ������� �� �������
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
    //����� �������� ���������
    private void MovePlayer()
    {
        // ������������ ����������� 
        moveDirection = transform.right * horizontalInput;
        //���� �� ����� - ���������
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
    //�������� ��������
    private void SpeedControl()
    {
        Vector2 flatVel = new Vector2(rb.velocity.x, 0f);
        // ����������� ���������
        if (flatVel.magnitude > moveSpeed)
        {
            Vector2 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector2(limitedVel.x, rb.velocity.y);
        }
    }
    //����� ������
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        anim.SetTrigger("Jump");
    }
    //������������ ������
    private void ResetJump()
    {
        readyToJump = true;
    }
}
