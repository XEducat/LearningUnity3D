using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    private static UIManager uiManager; // Поле для хранения ссылки на UIManager

    private void Awake()
    {
        // Находим экземпляр UIManager в сцене
        uiManager = FindObjectOfType<UIManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что столкнулись с игроком
        {
            // Вызываем метод UpdateKeyIcons из UIManager
            uiManager.CollectKey();

            Destroy(gameObject);
        }
    }
}