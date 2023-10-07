using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    //  При дотиканні до колайдеру робить перехід на нову сцену 
    private void OnCollisionEnter()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Перевірка чи існує наступна сцена, якщо ні то запускається сама перша
        nextSceneIndex = (nextSceneIndex < SceneManager.sceneCountInBuildSettings) ? nextSceneIndex : 0;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
