using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Sprite filledKey;
    public Sprite emptyKey;
    public List<Image> keyIcons;
    public static int collectedKeysCount { get; private set; } = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectKey( )
    {
        collectedKeysCount++;

        for (int i = 0; i < keyIcons.Count; i++)
        {
            if (i < collectedKeysCount)
            {
                keyIcons[i].sprite = filledKey;
            }
            else
            {
                keyIcons[i].sprite = emptyKey;
            }
        }
    }
}
