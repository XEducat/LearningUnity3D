using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения объекта
    public float rotationSpeed = 120f; // Скорость поворота объекта
    public float jumpForce = 8f; // Сила прыжка
    private float cameraRotationX = 0f; // Переменная для хранения угла наклона камеры по оси X
    private bool isGrounded; 
    [SerializeField] float mouseSensitivity = 1f; // Настраиваемая чувствительность мыши
    [SerializeField] float maxHealth = 100; // Максимальное количество здоровья игрока
    [SerializeField] float currentHealth; // Текущее количество здоровья игрока

    [SerializeField] Transform groundCheck; // Точка, определяющая, находится ли объект на земле
    [SerializeField] LayerMask notStayedMask; // Слой, определяющий, что считать землей для прыжка

    public Camera playerCamera; // Переменная для хранения ссылки на камеру
    public HealthBar healthBar;
    private Rigidbody rigitbody;
    private Animator animator;



    // TODO LIST:
    // Декомпозировать логику (Передвижение мыши как вариант)
    // Улучшить читабельность кода
    // Убрать возможность бега при анимации прыжка
    // Исправить анимацию хождения под углом

    void Start()
    {
        // Получаем нужные компоненты
        rigitbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        SetupPlayer();
    }

    // Выставляет стартовые настройки для игрока
    private void SetupPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentHealth = maxHealth; // Устанавливаем начальное значение здоровья
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

        // Проверяем, можно ли совершить прыжок
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

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
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity; // Масштабируем ввод от мыши

        // Изменяем угол наклона камеры вокруг ее правой оси (оси X)
        cameraRotationX -= mouseY * rotationSpeed * Time.deltaTime;

        // Ограничиваем угол наклона, чтобы предотвратить переворот камеры
        cameraRotationX = Mathf.Clamp(cameraRotationX, -12f, 12f);

        // Применяем новый угол наклона камеры
        playerCamera.transform.localEulerAngles = new Vector3(cameraRotationX, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z);

        // Поворачиваем объект игрока по горизонтали
        float rotationAmountX = mouseX * rotationSpeed * Time.deltaTime;
        rigitbody.rotation *= Quaternion.Euler(0f, rotationAmountX, 0f);
    }

    private void Jump()
    {
        // Прыжок
        if (isGrounded)
        {
            rigitbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        float moveZ = Input.GetAxis("Horizontal"); // Движение по оси X (влево/вправо)
        float moveX = Input.GetAxis("Vertical"); // Движение по оси Z (вперед/назад)
        Vector3 directionVectorX = transform.forward * moveX;
        Vector3 directionVectorZ = transform.right * moveZ;

        // Проверяем, находится ли объект на земле, и только тогда позволяем двигаться
        //if (isGrounded)
        //{
            animator.SetFloat("speedX", moveX);
            animator.SetFloat("speedZ", moveZ);
            Vector3 moveDirection = Vector3.ClampMagnitude(directionVectorX + directionVectorZ, 1) * moveSpeed;
            rigitbody.velocity = new Vector3(moveDirection.x, rigitbody.velocity.y, moveDirection.z);
            // TODO: Зробити зміну швидкості бігу в залежності від напряму
        //}

    }

    // Функция для получения урона
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Уменьшаем текущее здоровье на величину полученного урона
        healthBar.SetHealth(currentHealth);

        // Проверяем, не стало ли здоровье меньше или равным нулю
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
