using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "New Level", fileName = "Level_")]

public class Levels_Data : ScriptableObject
{
    [SerializeField] private int[] _cellX;
    [SerializeField] private int[] _cellY;
    [SerializeField] private GameObject[] _blockData;

    public int[] CellX => _cellX;
    public int[] CellY => _cellY;
    public GameObject[] BlockData => _blockData;
}