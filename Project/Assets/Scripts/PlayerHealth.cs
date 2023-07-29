using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100; // Максимальное количество здоровья игрока
    public float currentHealth; // Текущее количество здоровья игрока

    public Slider healthSlider; // Ссылка на элемент UI Slider

    void Start()
    {
        currentHealth = maxHealth; // Устанавливаем начальное значение здоровья
        UpdateHealthUI(); // Обновляем UI для отображения текущего здоровья
    }

    // Функция для получения урона
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Уменьшаем текущее здоровье на величину полученного урона
        UpdateHealthUI(); // Обновляем UI для отображения текущего здоровья

        // Проверяем, не стало ли здоровье меньше или равным нулю
        if (currentHealth <= 0)
        {
            Die(); // Если здоровье меньше или равно нулю, вызываем функцию смерти игрока
        }
    }

    // Функция для вызова при смерти игрока
    void Die()
    {
        // В данном примере мы просто деактивируем игровой объект при смерти
        // Можете добавить свою логику обработки смерти игрока в этой функции
        gameObject.SetActive(false);
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
