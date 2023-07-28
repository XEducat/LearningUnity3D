using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� �������� �������
    public float rotationSpeed = 120f; // �������� �������� �������
    public float jumpForce = 8f; // ���� ������
    public Transform groundCheck; // �����, ������������, ��������� �� ������ �� �����
    public LayerMask groundMask; // ����, ������������, ��� ������� ������ ��� ������

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �������� ���� �� ���������� �� ���� X � Z
        float moveX = Input.GetAxis("Vertical"); // �������� �� ��� X (������/�����)
        float moveZ = Input.GetAxis("Horizontal"); // �������� �� ��� Z (�����/������)

        // ���������, ��������� �� ������ �� �����
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

        // ������� ������ �� ��� X
        Vector3 moveDirection = new Vector3(-moveX, 0f, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection);

        // ������������ ������ �� ��� Z
        float rotationAmount = moveZ * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("������ ������");
            Debug.Log($"IsGrounded = {isGrounded}");
        }

        // ������
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
