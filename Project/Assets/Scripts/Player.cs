using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения объекта
    public float rotationSpeed = 120f; // Скорость поворота объекта
    public float jumpForce = 8f; // Сила прыжка
    private float strafeSpeedScale = 0.5f; // Коэффициент масштабирования для бокового движения
    private bool isGrounded; 
    [SerializeField] float mouseSensitivity = 1f; // Настраиваемая чувствительность мыши
    [SerializeField] float maxHealth = 100; // Максимальное количество здоровья игрока
    [SerializeField] float currentHealth; // Текущее количество здоровья игрока

    [SerializeField] Transform groundCheck; // Точка, определяющая, находится ли объект на земле
    [SerializeField] LayerMask groundMask; // Слой, определяющий, что считать землей для прыжка
    [SerializeField] LayerMask obstacleMask; // Слой, определяющий, что считать препятствиями
    [SerializeField] Slider healthSlider; // Ссылка на элемент UI Slider
    private Rigidbody rb;


    void Start()
    {
        // Получаем нужные компоненты
        rb = GetComponent<Rigidbody>();
        SetupPlayer();
    }

    // Выставляет стартовые настройки для игрока
    private void SetupPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentHealth = maxHealth; // Устанавливаем начальное значение здоровья
        UpdateHealthUI(); // Обновляем UI для отображения текущего здоровья
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

    // Получаем изменение позиции мыши с предыдущего кадра
    private bool HasMouseMoved()
    {
        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        return mouseDelta != Vector3.zero;
    }

    void HandleMouseMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; // Масштабируем ввод от мыши

        // Поворачиваем объект по оси Y
        float rotationAmount = mouseX * rotationSpeed * Time.deltaTime;
        rb.rotation *= Quaternion.Euler(0f, rotationAmount, 0f);
    }

    private void Jump()
    {
        // Проверяем, находится ли объект на земле
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

        // Прыжок
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }




    //// Проверяем, есть ли столкновение с землей перед объектом (для предотвращения проваливания под землю)
    //bool isCollidingGround = Physics.Raycast(transform.position, transform.forward, 0.5f, groundMask);
    //if (isCollidingGround && moveZ > 0)
    //if (moveZ > 0)
    //{
    //    moveDirection = Vector3.zero;
    //}

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal"); // Движение по оси X (влево/вправо)
        float moveZ = Input.GetAxis("Vertical"); // Движение по оси Z (вперед/назад)

        // Двигаем объект вперед/назад и влево/вправо по соответствующим осям с учетом физики
        Vector3 moveDirection = (transform.forward * moveZ + transform.right * moveX * strafeSpeedScale) * moveSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + moveDirection);
    }




    // Функция для получения урона
    public void TakeDamage(int damageAmount)
    {

        currentHealth -= damageAmount; // Уменьшаем текущее здоровье на величину полученного урона
        UpdateHealthUI(); // Обновляем UI для отображения текущего здоровья

        // Проверяем, не стало ли здоровье меньше или равным нулю
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }


    // Функция для обновления UI для отображения текущего здоровья
    void UpdateHealthUI()
    {
        // Обновляем значение элемента UI Slider с текущим здоровьем игрока
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
}
