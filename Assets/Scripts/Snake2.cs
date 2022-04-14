using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Snake2 : MonoBehaviour
{
     
    public BoxCollider2D gridArea;
    public GameLogic gameLogic;
    public BoxSpawner boxSpawner;
    [SerializeField] private Sprite horizontal, vertical;
    [SerializeField] private Sprite headLeft,headRight,headUp,headDown;
    [SerializeField] private Sprite bendLeftUp, bendRightUp, bendLeftDown, bendRightDown;
    [SerializeField] private Sprite tailLeft, tailRight, tailUp, tailDown;

    [SerializeField] private Transform applePrefab;
    [SerializeField] private Transform segmentPrefab;

    private List<Transform> _segments = new List<Transform>();
    private Transform prevPos;

    public int initialSizeSnake=3;
    Vector2 _direction = Vector2.right;

    private const float timeStep = 0.05f;
    private const float startTime = 1f;

    private void Start()
    {
        ResetSnake();       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {            
                _direction = Vector2.up;
            
        }            
        else if (Input.GetKeyDown(KeyCode.S))
        {
                _direction = Vector2.down;
        }
            
         else if (Input.GetKeyDown(KeyCode.A))
        {
                _direction = Vector2.left;
            
        }
                
        else if (Input.GetKeyDown(KeyCode.D))
        {            
                _direction = Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void FixedUpdate()
    {        
        if (this != prevPos)
        {
            prevPos = this.transform;
            if(_segments[0].position.x> _segments[1].position.x && 
               _direction == Vector2.left) 
            {
                _direction = Vector2.right;
            }
            if (_segments[0].position.x < _segments[1].position.x &&
               _direction == Vector2.right) 
            {
                _direction = Vector2.left;
            }
            if (_segments[0].position.y > _segments[1].position.y &&
               _direction == Vector2.down) 
            {
                _direction = Vector2.up;
            }
            if (_segments[0].position.y < _segments[1].position.y &&
               _direction == Vector2.up) 
            {
                _direction = Vector2.down;
            }
        }
       
            for (int i = _segments.Count - 1; i > 0; i--)
            {
                _segments[i].position = _segments[i - 1].position;
            }  
            this.transform.position = new Vector2(
        Mathf.Round(this.transform.position.x) + _direction.x,
        Mathf.Round(this.transform.position.y) + _direction.y);

        HeadSpriteChange();
        SegmentSpriteChange();
        TailSpriteChange();


    }
    private void Grow()
    {
        Transform segment =  Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
        
    }
    

    private void ResetSnake()
    {
       
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        
        for (int i = 0; i < this.initialSizeSnake; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
            _segments[i].position = new Vector2(0, 0);
            _segments[i].GetComponent<SpriteRenderer>().sprite = null;

        }      
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            boxSpawner.Spawn();
            Grow();
            gameLogic.IncreaseScore();
            Time.timeScale = Time.timeScale + timeStep;            
        }
        else 
        if (collision.tag == "Obstacle" || collision.tag == "Player")
        {
            ResetSnake();
            boxSpawner.BoxClear();
            Time.timeScale = startTime;
            gameLogic.ResetScore();
        }
    }
    private void HeadSpriteChange()
    {
        if (_direction == Vector2.right)
        {
            _segments[0].GetComponent<SpriteRenderer>().sprite = headRight;
        }

        else if (_direction == Vector2.left)
        {
            _segments[0].GetComponent<SpriteRenderer>().sprite = headLeft;
        }

        else if (_direction == Vector2.down)
        {
            _segments[0].GetComponent<SpriteRenderer>().sprite = headDown;
        }

        else if (_direction == Vector2.up)
        {
            _segments[0].GetComponent<SpriteRenderer>().sprite = headUp;
        }
    }

    private void TailSpriteChange()
    {
        if (_segments[_segments.Count - 2].position.x > _segments[_segments.Count - 1].position.x)
            _segments[_segments.Count - 1].GetComponent<SpriteRenderer>().sprite = tailRight;
        if (_segments[_segments.Count - 2].position.x < _segments[_segments.Count - 1].position.x)
            _segments[_segments.Count - 1].GetComponent<SpriteRenderer>().sprite = tailLeft;
        if (_segments[_segments.Count - 2].position.y > _segments[_segments.Count - 1].position.y)
            _segments[_segments.Count - 1].GetComponent<SpriteRenderer>().sprite = tailUp;
        if (_segments[_segments.Count - 2].position.y < _segments[_segments.Count - 1].position.y)
            _segments[_segments.Count - 1].GetComponent<SpriteRenderer>().sprite = tailDown;
    }

    private void SegmentSpriteChange()
    {
        for (int i = 1; i < _segments.Count - 1; i++)
        {
            if (_segments[i].position.x == _segments[i - 1].position.x)
                _segments[i].GetComponent<SpriteRenderer>().sprite = vertical;
            if (_segments[i].position.y == _segments[i - 1].position.y)
                _segments[i].GetComponent<SpriteRenderer>().sprite = horizontal;

            {
                if (_segments[i].position.y > _segments[i + 1].position.y)
                {
                    if (_segments[i].position.x > _segments[i - 1].position.x)
                        _segments[i].GetComponent<SpriteRenderer>().sprite = bendLeftUp;
                    if (_segments[i].position.x < _segments[i - 1].position.x)
                        _segments[i].GetComponent<SpriteRenderer>().sprite = bendRightUp;

                }
                if (_segments[i].position.y < _segments[i + 1].position.y)
                {
                    if (_segments[i].position.x > _segments[i - 1].position.x)
                        _segments[i].GetComponent<SpriteRenderer>().sprite = bendLeftDown;
                    if (_segments[i].position.x < _segments[i - 1].position.x)
                        _segments[i].GetComponent<SpriteRenderer>().sprite = bendRightDown;

                }
                if (_segments[i].position.x > _segments[i + 1].position.x)
                {
                    if (_segments[i].position.y > _segments[i - 1].position.y)
                        _segments[i].GetComponent<SpriteRenderer>().sprite = bendLeftUp;
                    if (_segments[i].position.y < _segments[i - 1].position.y)
                        _segments[i].GetComponent<SpriteRenderer>().sprite = bendLeftDown;

                }
                if (_segments[i].position.x < _segments[i + 1].position.x)
                {
                    if (_segments[i].position.y > _segments[i - 1].position.y) //up
                        _segments[i].GetComponent<SpriteRenderer>().sprite = bendRightUp;
                    if (_segments[i].position.y < _segments[i - 1].position.y) //down
                        _segments[i].GetComponent<SpriteRenderer>().sprite = bendRightDown;

                }
            }

        }
    }
}
