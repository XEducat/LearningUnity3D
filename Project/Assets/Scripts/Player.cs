using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� �������� �������
    public float rotationSpeed = 120f; // �������� �������� �������
    public float jumpForce = 8f; // ���� ������
    private float strafeSpeedScale = 0.5f; // ����������� ��������������� ��� �������� ��������
    private bool isGrounded; 
    [SerializeField] float mouseSensitivity = 1f; // ������������� ���������������� ����
    [SerializeField] float maxHealth = 100; // ������������ ���������� �������� ������
    [SerializeField] float currentHealth; // ������� ���������� �������� ������

    [SerializeField] Transform groundCheck; // �����, ������������, ��������� �� ������ �� �����
    [SerializeField] LayerMask groundMask; // ����, ������������, ��� ������� ������ ��� ������
    [SerializeField] LayerMask obstacleMask; // ����, ������������, ��� ������� �������������
    [SerializeField] Slider healthSlider; // ������ �� ������� UI Slider
    private Rigidbody rb;


    void Start()
    {
        // �������� ������ ����������
        rb = GetComponent<Rigidbody>();
        SetupPlayer();
    }

    // ���������� ��������� ��������� ��� ������
    private void SetupPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentHealth = maxHealth; // ������������� ��������� �������� ��������
        UpdateHealthUI(); // ��������� UI ��� ����������� �������� ��������
    }

    void Update()
    {
        if (HasMouseMoved())
        {
            HandleMouseMovement();
        }

        //if (Input.GetKeyDown(KeyCode.Space)) 
        //{
        //    Jump(); 
        //}

        Move();

    }

    // �������� ��������� ������� ���� � ����������� �����
    private bool HasMouseMoved()
    {
        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        return mouseDelta != Vector3.zero;
    }

    void HandleMouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; // ������������ ���� �� ����

        // ������������ ������ �� ��� Y
        float rotationAmount = mouseX * rotationSpeed * Time.deltaTime;
        rb.rotation *= Quaternion.Euler(0f, rotationAmount, 0f);
    }

    private void Jump()
    {
        // ���������, ��������� �� ������ �� �����
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

        // ������
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }




    //// ���������, ���� �� ������������ � ������ ����� �������� (��� �������������� ������������ ��� �����)
    //bool isCollidingGround = Physics.Raycast(transform.position, transform.forward, 0.5f, groundMask);
    //if (isCollidingGround && moveZ > 0)
    //if (moveZ > 0)
    //{
    //    moveDirection = Vector3.zero;
    //}

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal"); // �������� �� ��� X (�����/������)
        float moveZ = Input.GetAxis("Vertical"); // �������� �� ��� Z (������/�����)

        // ������� ������ ������/����� � �����/������ �� ��������������� ���� � ������ ������
        Vector3 moveDirection = (transform.forward * moveZ + transform.right * moveX * strafeSpeedScale) * moveSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + moveDirection);
    }




    // ������� ��� ��������� �����
    public void TakeDamage(int damageAmount)
    {

        currentHealth -= damageAmount; // ��������� ������� �������� �� �������� ����������� �����
        UpdateHealthUI(); // ��������� UI ��� ����������� �������� ��������

        // ���������, �� ����� �� �������� ������ ��� ������ ����
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }


    // ������� ��� ���������� UI ��� ����������� �������� ��������
    void UpdateHealthUI()
    {
        // ��������� �������� �������� UI Slider � ������� ��������� ������
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
}
