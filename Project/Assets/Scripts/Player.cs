using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� �������� �������
    public float rotationSpeed = 120f; // �������� �������� �������
    public float jumpForce = 8f; // ���� ������
    private float cameraRotationX = 0f; // ���������� ��� �������� ���� ������� ������ �� ��� X
    private bool isGrounded; 
    [SerializeField] float mouseSensitivity = 1f; // ������������� ���������������� ����
    [SerializeField] float maxHealth = 100; // ������������ ���������� �������� ������
    [SerializeField] float currentHealth; // ������� ���������� �������� ������

    [SerializeField] Transform groundCheck; // �����, ������������, ��������� �� ������ �� �����
    [SerializeField] LayerMask notStayedMask; // ����, ������������, ��� ������� ������ ��� ������

    public Camera playerCamera; // ���������� ��� �������� ������ �� ������
    public HealthBar healthBar;
    private Rigidbody rigitbody;
    private Animator animator;



    // TODO LIST:
    // ��������������� ������ (������������ ���� ��� �������)
    // �������� ������������� ����
    // ������ ����������� ���� ��� �������� ������
    // ��������� �������� �������� ��� �����

    void Start()
    {
        // �������� ������ ����������
        rigitbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        SetupPlayer();
    }

    // ���������� ��������� ��������� ��� ������
    private void SetupPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentHealth = maxHealth; // ������������� ��������� �������� ��������
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.10f, notStayedMask);

        if (HasMouseMoved())
        {
            HandleMouseMovement();
        }

        Move();

        if (isGrounded)
        {
            animator.SetBool("IsInAir", false);
        }
        else
        {
            animator.SetBool("IsInAir", true);
        }

        // ���������, ����� �� ��������� ������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

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
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity; // ������������ ���� �� ����

        // �������� ���� ������� ������ ������ �� ������ ��� (��� X)
        cameraRotationX -= mouseY * rotationSpeed * Time.deltaTime;

        // ������������ ���� �������, ����� ������������� ��������� ������
        cameraRotationX = Mathf.Clamp(cameraRotationX, -12f, 12f);

        // ��������� ����� ���� ������� ������
        playerCamera.transform.localEulerAngles = new Vector3(cameraRotationX, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z);

        // ������������ ������ ������ �� �����������
        float rotationAmountX = mouseX * rotationSpeed * Time.deltaTime;
        rigitbody.rotation *= Quaternion.Euler(0f, rotationAmountX, 0f);
    }

    private void Jump()
    {
        // ������
        if (isGrounded)
        {
            rigitbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        float moveZ = Input.GetAxis("Horizontal"); // �������� �� ��� X (�����/������)
        float moveX = Input.GetAxis("Vertical"); // �������� �� ��� Z (������/�����)
        Vector3 directionVectorX = transform.forward * moveX;
        Vector3 directionVectorZ = transform.right * moveZ;

        // ���������, ��������� �� ������ �� �����, � ������ ����� ��������� ���������
        //if (isGrounded)
        //{
            animator.SetFloat("speedX", moveX);
            animator.SetFloat("speedZ", moveZ);
            Vector3 moveDirection = Vector3.ClampMagnitude(directionVectorX + directionVectorZ, 1) * moveSpeed;
            rigitbody.velocity = new Vector3(moveDirection.x, rigitbody.velocity.y, moveDirection.z);
            // TODO: ������� ���� �������� ��� � ��������� �� �������
        //}

    }

    // ������� ��� ��������� �����
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // ��������� ������� �������� �� �������� ����������� �����
        healthBar.SetHealth(currentHealth);

        // ���������, �� ����� �� �������� ������ ��� ������ ����
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
