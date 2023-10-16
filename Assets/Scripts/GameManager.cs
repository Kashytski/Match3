using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Levels_Data[] _level;
    void Start()
    {
        if (GameManager.instance != null)
            Destroy(GameManager.instance.gameObject);
        instance = this;
    }

    public Levels_Data GetLevel()
    {
        int levelNum = PlayerPrefs.GetInt("level");
        return _level[levelNum];
    }
}
