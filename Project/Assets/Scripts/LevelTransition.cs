using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    //  ��� �������� �� ��������� ������ ������� �� ���� ����� 
    private void OnCollisionEnter()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // �������� �� ���� �������� �����, ���� � �� ����������� ���� �����
        nextSceneIndex = (nextSceneIndex < SceneManager.sceneCountInBuildSettings) ? nextSceneIndex : 0;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
