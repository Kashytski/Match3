using UnityEngine;

public class CellTile : MonoBehaviour
{
    private int _blockSortingOrder;
    private GameObject _blockData;
    
    private void Initialize()
    {
        GameObject block = Instantiate(_blockData, transform.position, Quaternion.identity);
        block.GetComponent<SpriteRenderer>().sortingOrder = _blockSortingOrder;
        block.transform.parent = transform;
    }

    public void SetBlockData(int sortingOrder,GameObject block)
    {
        _blockSortingOrder = sortingOrder;
        _blockData = block;
        Initialize();
    }
}
