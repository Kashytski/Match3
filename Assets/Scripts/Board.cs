using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Board : MonoBehaviour
{
    public static event Action<bool> OnEndNormalize;
    [SerializeField] private GameObject clearBlock;
    private int _width;
    private int _height;

    private List<GameObject> _allBlocks;
    private List<GameObject> _allCells;

    private void Awake()
    {
        BlockScript.OnEndSwipe += NormalizeField;
    }

    private void OnDestroy()
    {
        BlockScript.OnEndSwipe -= NormalizeField;
    }

    private void Start()
    {
        _width = GameManager.Instance.Width;
        _height = GameManager.Instance.Height;
        SetUp();
    }

    private void SetUp()
    {
        int k = 0;
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Levels_Data level = GameManager.Instance.GetLevel();
                
                 Vector2 tempPosition = new Vector2(i,j);
                
                if (k < level.BlockData.Length && level.CellX[k] == i && level.CellY[k] == j)
                {
                    GameObject block = Instantiate(level.BlockData[k++], tempPosition, Quaternion.identity);
                    _allBlocks.Add(block);
                    block.GetComponent<SpriteRenderer>().sortingOrder = i+j;
                    block.transform.parent = transform;
                    block.name = $"Block[ {i} , {j} ]";
                }
                else
                {
                    GameObject block = Instantiate(this.clearBlock, tempPosition, Quaternion.identity);
                    _allCells.Add(block);
                    block.transform.parent = transform;
                    block.name = $"ClearBlock[ {i} , {j} ]";
                }
            }
        }

        Sort();
    }

    private void NormalizeField()
    {
        Sort();
        MoveBlockOnGround();
        
    }

    private void MoveBlockOnGround()
    {
        foreach (var block in _allBlocks)
        {
            if (block.transform.position.y > 0)
            {
                GameObject lowerBlock = _allBlocks.Find(g
                    => g.transform.position.x == block.transform.position.x
                       && g.transform.position.y == block.transform.position.y - 1);
                if (!lowerBlock)
                {
                    GameObject lowerCell;
                    Vector3 oldPos = block.transform.position;
                    lowerCell = _allCells.Find(g => g.transform.position == oldPos + Vector3.down);
                    lowerCell.transform.position = oldPos;
                    block.transform.DOMove(oldPos + Vector3.down, 0.5f);
                }
            }
        }
    }
    
    private void Explosion()
    {
        Sort();
        
    }
    
    private void Sort()
    {
        _allBlocks = _allBlocks.OrderBy(g => g.transform.position.y).ToList();
    }
}
