using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения объекта
    public float rotationSpeed = 120f; // Скорость поворота объекта
    public float jumpForce = 8f; // Сила прыжка
    public Transform groundCheck; // Точка, определяющая, находится ли объект на земле
    public LayerMask groundMask; // Слой, определяющий, что считать землей для прыжка

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Получаем ввод от клавиатуры по осям X и Z
        float moveX = Input.GetAxis("Vertical"); // Движение по оси X (вперед/назад)
        float moveZ = Input.GetAxis("Horizontal"); // Движение по оси Z (влево/вправо)

        // Проверяем, находится ли объект на земле
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

        // Двигаем объект по оси X
        Vector3 moveDirection = new Vector3(-moveX, 0f, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(moveDirection);

        // Поворачиваем объект по оси Z
        float rotationAmount = moveZ * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Нажали пробел");
            Debug.Log($"IsGrounded = {isGrounded}");
        }

        // Прыжок
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
