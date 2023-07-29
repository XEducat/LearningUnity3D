using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100; // ������������ ���������� �������� ������
    public float currentHealth; // ������� ���������� �������� ������

    public Slider healthSlider; // ������ �� ������� UI Slider

    void Start()
    {
        currentHealth = maxHealth; // ������������� ��������� �������� ��������
        UpdateHealthUI(); // ��������� UI ��� ����������� �������� ��������
    }

    // ������� ��� ��������� �����
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // ��������� ������� �������� �� �������� ����������� �����
        UpdateHealthUI(); // ��������� UI ��� ����������� �������� ��������

        // ���������, �� ����� �� �������� ������ ��� ������ ����
        if (currentHealth <= 0)
        {
            Die(); // ���� �������� ������ ��� ����� ����, �������� ������� ������ ������
        }
    }

    // ������� ��� ������ ��� ������ ������
    void Die()
    {
        // � ������ ������� �� ������ ������������ ������� ������ ��� ������
        // ������ �������� ���� ������ ��������� ������ ������ � ���� �������
        gameObject.SetActive(false);
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
