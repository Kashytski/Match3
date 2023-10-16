using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    private int _width = 5;
    private int _height = 8;
    private CellTile[,] _allTiles;
    private int _sortingOrder;
    
    void Start()
    {
        _allTiles = new CellTile[_width, _height];
        SetUp();
    }

    void SetUp()
    {
        int k = 0;
        for (int i = 0; i < _width; i++)
        {
            _sortingOrder = i;
            for (int j = 0; j < _height; j++)
            {
                Levels_Data level = GameManager.instance.GetLevel();
                
                if (k < level.BlockData.Length && level.CellX[k] == i && level.CellY[k] == j)
                {
                    Vector2 tempPosition = new Vector2(i,j);
                    GameObject backgroundTile = Instantiate(_tilePrefab, tempPosition, Quaternion.identity);
                    _allTiles[i, j] = backgroundTile.GetComponent<CellTile>();
                    backgroundTile.GetComponent<CellTile>().SetBlockData(_sortingOrder++,level.BlockData[k++]);
                    backgroundTile.transform.parent = transform;
                    backgroundTile.name = $"Tile[ {i} , {j} ]";
                }
            }
        }
    }
}
