using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int requiredKeys = 3;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (UIManager.collectedKeysCount >= requiredKeys)
            {
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        Animator doorAnimator = GetComponentInParent<Animator>();// Посилання на компонент Animator
        doorAnimator.SetBool("isOpen", true);
    }
}