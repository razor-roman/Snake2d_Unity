 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Snake : MonoBehaviour
{
    public Tilemap Level;
    private float _speed = 1;
    private Vector3 _position, _direction = Vector3.up;
    private Transform _selfTransform;
    private SpriteRenderer _render;

    public GameObject[] _tail = new GameObject[3];
    private Vector3 _oldPosition;
    public Sprite UpSprite, DownSprite, LeftSprite, RightSprite;
    public Sprite TailEndUpSprite, TailEndDownSprite, TailEndLeftSprite, TailEndRightSprite;
    public Sprite TailBendingUpRightSprite, TailBendingUpLeftprite, TailBendingDownRightSprite, TailBendingDownLeftSprite;
    public Sprite Horizontal, Vertical;
    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
        _render = GetComponent<SpriteRenderer>();
        _position = _selfTransform.position;
    }
    private void Update()
    {
        _position += _direction * _speed * Time.deltaTime;
        Vector3 newPosition = Level.WorldToCell(_position); 
        
        _selfTransform.position = newPosition;
        //SpriteRenderer.Instantiate(_tail[1]);
        if (_oldPosition != newPosition)
        {
            MoveTail(_oldPosition);
            
        }
        _oldPosition = newPosition;
    } 
    public void MoveUp()
    {
        _direction = Vector3.up;
    }
    public void MoveDown()
    {
        _direction = Vector3.down;
    }
    public void MoveLeft()
    {
        _direction = Vector3.left;
    }
    public void MoveRight()
    {
        _direction = Vector3.right;
    }
    //tail

    public void MoveTail(Vector3 target)
    {
        for(int i = _tail.Length - 1; i > 0; i--)
        {
            _tail[i].transform.position = _tail[i - 1].transform.position;
        }
        _tail[0].transform.position = target;
        for (int i = _tail.Length - 2; i >= 1; i--)
        {
            var prev = _tail[i - 1];
            var next = _tail[i + 1];
            var current = _tail[i];
            current.GetComponent<SpriteRenderer>().sprite =
                GetSprite(  current.transform.position,
                            next.transform.position, 
                            prev.transform.position);
        }
        _tail[0].GetComponent<SpriteRenderer>().sprite = 
            GetSprite(  _tail[0].transform.position, 
                        _selfTransform.position, 
                        _tail[1].transform.position);
        _tail[_tail.Length - 1].GetComponent<SpriteRenderer>().sprite = GetTailEnd(_tail[_tail.Length-2].transform.position);
        _render.sprite = GetTailHead();
        //_tail[0].GetComponent<SpriteRenderer>().sprite = GetBendingHead();
    }

    private Sprite GetTailEnd(Vector3 next)
    {
        var direction = (_tail[_tail.Length - 1].transform.position - next).normalized;

        if (direction.x == -1)
        {
            return TailEndRightSprite;
        }
        if (direction.x == 1)
        {
            return TailEndLeftSprite;
        }
        if (direction.y == -1)
        {
            return TailEndUpSprite;
        }
        if (direction.y == 1)
        {
            return TailEndDownSprite;
        }
        return null;
    }
    
    private Sprite GetTailHead()
    {
        
        if (_direction.x == -1)
        {
            return LeftSprite;
        }
        if (_direction.x == 1)
        {
            return RightSprite;
        }
        if (_direction.y == -1)
        {
            return DownSprite;
        }
        if (_direction.y == 1)
        {
            return UpSprite;
        }
        return null;
    }  
    private Sprite GetBendingHead()
    { 
        if (_direction.x == 1 && _direction.y == 0)
        
        {
            return TailBendingDownLeftSprite;
        }
        if (_direction.x == 1 && _direction.y == 0)
        {
            return TailBendingDownRightSprite;
        }
        if (_direction.y == -1)
        {
            return TailBendingUpLeftprite;
        }
        if (_direction.y == 1)
        {
            return TailBendingUpRightSprite;
        }
        return null;
    }

    private Sprite GetSprite(Vector3 current,Vector3 next, Vector3 prev)
    {
        if (prev.x == next.x)
        {
            return Vertical;
        }
        if (prev.y == next.y)
        {
            return Horizontal;
        }
        Debug.Log(prev);
        Debug.Log(next);
        if(prev.x > next.x && prev.y < next.y)
        {
            return TailBendingUpLeftprite;
        }
        if (prev.x < next.x && prev.y < next.y)
        {
            return TailBendingUpRightSprite;
        }
        if(prev.x > next.x && prev.y > next.y)
        {
            return TailBendingDownLeftSprite;
        }
        if (prev.x < next.x && prev.y > next.y)
        {
            return TailBendingDownRightSprite;
        }
        return null;
    }
}
