using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float moveSpeed = 3f; // Скорость движения врага
    public int damageAmount = 10; // Урон, который враг наносит игроку
    public float attackCooldown = 1f; // Задержка между ударами врага

    private Transform player; // Ссылка на трансформ игрока
    private Rigidbody rb; // Компонент Rigidbody врага
    private bool canAttack = true; // Флаг, позволяющий врагу атаковать

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>(); // Получаем ссылку на Rigidbody врага
    }

    void Update()
    {
        // Если идет откидывание, обновляем таймер и проверяем его окончание
        if (isKnockback)
        {
            knockbackTimer += Time.deltaTime;
            if (knockbackTimer >= knockbackDuration)
            {
                isKnockback = false; // Откидывание завершено
            }
        }
        else
        {
            // Направление движения врага к игроку (прежнее поведение)
            Vector3 direction = player.position - transform.position;
            direction.y = 0f;
            direction.Normalize();
            rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        }
    }


    public float knockbackForce = 5f; // Сила откидывания игрока при ударе
    public float knockbackDuration = 0.2f; // Продолжительность откидывания игрока
    private bool isKnockback = false; // Флаг, определяющий, происходит ли откидывание
    private float knockbackTimer = 0f; // Таймер для отслеживания времени откидывания
                                       // Обработка столкновения с игроком


    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Проверяем, может ли враг атаковать
            if (canAttack)
            {
                // Наносим урон игроку
                Player playerHealth = collision.gameObject.GetComponent<Player>();
                playerHealth?.TakeDamage(damageAmount);

                // Запускаем процесс откидывания игрока
                if (!isKnockback)
                {
                    isKnockback = true;
                    knockbackTimer = 0f;

                    // Вычисляем направление откидывания
                    Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
                    knockbackDirection.y = 0f; // Откидываем только по горизонтали

                    // Применяем силу откидывания к игроку
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                }

                // Запускаем таймер задержки перед следующей атакой
                canAttack = false;
                Invoke("ResetAttackCooldown", attackCooldown);
            }
        }
    }

    // Сбрасываем таймер задержки перед следующей атакой
    void ResetAttackCooldown()
    {
        canAttack = true;
    }
}