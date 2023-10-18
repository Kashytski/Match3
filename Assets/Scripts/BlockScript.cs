using DG.Tweening;
using UnityEngine;
using System;

public class BlockScript : MonoBehaviour
{
    public static event Action<bool> OnStartSwipe;
    public static event Action OnEndSwipe;
    private SpriteRenderer _spriteRender;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private bool _isDraw;
    private RaycastHit2D _hit2d;
    private bool _isReady = true;

    private void Awake()
    {
        OnStartSwipe += ChangeBlockReadyState;
        Board.OnEndNormalize += ChangeBlockReadyState;
    }

    private void OnDestroy()
    {
        OnStartSwipe -= ChangeBlockReadyState;
        Board.OnEndNormalize -= ChangeBlockReadyState;
    }

    private void ChangeBlockReadyState(bool state)
    {
        _isReady = state;
    }
    
    private void Start()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (_isReady)
        {
            _isReady = false;
            OnStartSwipe?.Invoke(_isReady);
            _isDraw = true;  
        }
    }


    private void Update()
    {
        if (_isDraw)
        {
            _endPosition = Input.mousePosition - GameManager.Instance.CameraMain.WorldToScreenPoint(transform.position);
            _endPosition = new Vector3(_endPosition.x,_endPosition.y,0);
            _hit2d = Physics2D.Raycast(transform.position, _endPosition,0.7f);
            Debug.DrawLine(transform.position,_endPosition,Color.red);
            if (Input.GetMouseButtonUp(0))
            {
                if (_hit2d.collider)
                {
                    Vector3 oldPos = transform.position;
                    Vector3 otherBlockPosition = _hit2d.collider.transform.position;
                    int oldSortingOrder = _spriteRender.sortingOrder;

                    _spriteRender.sortingOrder = (int)(otherBlockPosition.x + otherBlockPosition.y);
                    if (CompareTag(_hit2d.collider.tag))
                        _hit2d.collider.GetComponent<SpriteRenderer>().sortingOrder = oldSortingOrder;
                        
                    transform.DOMove(otherBlockPosition,0.5f);
                    _hit2d.collider.transform.DOMove(oldPos,0.5f).OnComplete(() => OnEndSwipe?.Invoke());
                }
                _isDraw = false;
            }
        }
    }
}
