using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    private static UIManager uiManager; // ���� ��� �������� ������ �� UIManager

    private void Awake()
    {
        // ������� ��������� UIManager � �����
        uiManager = FindObjectOfType<UIManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ���������, ��� ����������� � �������
        {
            // �������� ����� UpdateKeyIcons �� UIManager
            uiManager.CollectKey();

            Destroy(gameObject);
        }
    }
}