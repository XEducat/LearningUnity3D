using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float moveSpeed = 3f; // Скорость движения врага
    public int damageAmount = 10; // Урон, который враг наносит игроку

    private Transform player; // Ссылка на трансформ игрока

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }


    public float knockbackForce = 5f; // Сила откидывания игрока при ударе
    public float knockbackDuration = 0.2f; // Продолжительность откидывания игрока
    private bool isKnockback = false; // Флаг, определяющий, происходит ли откидывание
    private float knockbackTimer = 0f; // Таймер для отслеживания времени откидывания
    // Обработка столкновения с игроком
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Наносим урон игроку
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);

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
            }
        }
    }
}