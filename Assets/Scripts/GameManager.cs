using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Levels_Data[] level;
    [SerializeField] private int width = 5;
    [SerializeField] private int height = 8;

    public int Width => width;
    public int Height => height;
    
    [SerializeField] private Camera cameraMain;
    public Camera CameraMain => cameraMain;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        if (GameManager.Instance != null)
            Destroy(GameManager.Instance.gameObject);
        Instance = this;
    }

    public Levels_Data GetLevel()
    {
        int levelNum = PlayerPrefs.GetInt("level");
        return level[levelNum];
    }
}
