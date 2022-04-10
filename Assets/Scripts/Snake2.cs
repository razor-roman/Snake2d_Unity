using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class Snake2 : MonoBehaviour
{
    private bool KeyisPressed;
    private Transform prevPos;
    private int MaxScore;
    public BoxCollider2D gridArea;
    private List<Transform> _boxes = new List<Transform>();
    private int Score;
    [SerializeField]
    private Text TextScore;
    [SerializeField]
    private Text TextMaxScore;
    [SerializeField]
    private Sprite Horizontal, Vertical;
    [SerializeField]
    private Sprite HeadLeft,HeadRight,HeadUp,HeadDown;
    [SerializeField]
    private Sprite BendLeftUp, BendRightUp, BendLeftDown, BendRightDown;
    [SerializeField]
    private Sprite TailLeft, TailRight, TailUp, TailDown;
    [SerializeField]
    private Transform applePrefab;
    private List<Transform> _segments = new List<Transform>();
    [SerializeField]
    private Transform segmentPrefab;
    [SerializeField]
    private Transform boxPrefab;
    Tilemap Level;
    float Vert;
    public int initialSize=3;
    GameObject[] Snake;
    Vector2 _direction = Vector2.right;
    // Start is called before
    // the first frame update
    private void Start()
    {
        ResetState();
        _boxes.Clear();
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
        //protect from kill itself - wrong direction
        if (this != prevPos)
        {
            prevPos = this.transform;
            if(_segments[0].position.x> _segments[1].position.x && 
               _direction == Vector2.left) // only right
            {
                _direction = Vector2.right;
            }
            if (_segments[0].position.x < _segments[1].position.x &&
               _direction == Vector2.right) // only right
            {
                _direction = Vector2.left;
            }
            if (_segments[0].position.y > _segments[1].position.y &&
               _direction == Vector2.down) // only right
            {
                _direction = Vector2.up;
            }
            if (_segments[0].position.y < _segments[1].position.y &&
               _direction == Vector2.up) // only right
            {
                _direction = Vector2.down;
            }
        }
        //Move all snake segments
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }        
        this.transform.position = new Vector2(
        Mathf.Round(this.transform.position.x) + _direction.x,
        Mathf.Round(this.transform.position.y) + _direction.y);

        
        //head sprite change
        {
            if (_direction == Vector2.right)
            {
                _segments[0].GetComponent<SpriteRenderer>().sprite = HeadRight;
            }

            else if (_direction == Vector2.left)
            {
                _segments[0].GetComponent<SpriteRenderer>().sprite = HeadLeft;
            }

            else if (_direction == Vector2.down)
            {
                _segments[0].GetComponent<SpriteRenderer>().sprite = HeadDown;
            }

            else if (_direction == Vector2.up)
            {
                _segments[0].GetComponent<SpriteRenderer>().sprite = HeadUp;
            }
        }
        //tail sprite change
        {
            if (_segments[_segments.Count - 2].position.x > _segments[_segments.Count - 1].position.x)
                _segments[_segments.Count - 1].GetComponent<SpriteRenderer>().sprite = TailRight;
            if (_segments[_segments.Count - 2].position.x < _segments[_segments.Count - 1].position.x)
                _segments[_segments.Count - 1].GetComponent<SpriteRenderer>().sprite = TailLeft;
            if (_segments[_segments.Count - 2].position.y > _segments[_segments.Count - 1].position.y)
                _segments[_segments.Count - 1].GetComponent<SpriteRenderer>().sprite = TailUp;
            if (_segments[_segments.Count - 2].position.y < _segments[_segments.Count - 1].position.y)
                _segments[_segments.Count - 1].GetComponent<SpriteRenderer>().sprite = TailDown;

        }
        //body and bending sprite change
        for (int i = 1; i < _segments.Count - 1;i++) //0 head, count-1 tail. 
        {
             if (_segments[i].position.x == _segments[i-1].position.x)
                 _segments[i].GetComponent<SpriteRenderer>().sprite = Vertical;
             if (_segments[i].position.y == _segments[i - 1].position.y)
                    _segments[i].GetComponent<SpriteRenderer>().sprite = Horizontal;
            //Bending
            {
                if (_segments[i].position.y > _segments[i + 1].position.y) //vert up
                {
                    if (_segments[i].position.x > _segments[i - 1].position.x) //left
                        _segments[i].GetComponent<SpriteRenderer>().sprite = BendLeftUp;
                    if (_segments[i].position.x < _segments[i - 1].position.x) //reight
                        _segments[i].GetComponent<SpriteRenderer>().sprite = BendRightUp;

                }
                if (_segments[i].position.y < _segments[i + 1].position.y) //vert down
                {
                    if (_segments[i].position.x > _segments[i - 1].position.x) //left
                        _segments[i].GetComponent<SpriteRenderer>().sprite = BendLeftDown;
                    if (_segments[i].position.x < _segments[i - 1].position.x) //reight
                        _segments[i].GetComponent<SpriteRenderer>().sprite = BendRightDown;

                }
                if (_segments[i].position.x > _segments[i + 1].position.x) //hor right
                {
                    if (_segments[i].position.y > _segments[i - 1].position.y) //up
                        _segments[i].GetComponent<SpriteRenderer>().sprite = BendLeftUp;
                    if (_segments[i].position.y < _segments[i - 1].position.y) //down
                        _segments[i].GetComponent<SpriteRenderer>().sprite = BendLeftDown;

                }
                if (_segments[i].position.x < _segments[i + 1].position.x) //hor left
                {
                    if (_segments[i].position.y > _segments[i - 1].position.y) //up
                        _segments[i].GetComponent<SpriteRenderer>().sprite = BendRightUp;
                    if (_segments[i].position.y < _segments[i - 1].position.y) //down
                        _segments[i].GetComponent<SpriteRenderer>().sprite = BendRightDown;

                }
            }
            
           
        }
        //

        
        
    }
    private void Grow()
    {
        Transform segment =  Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
        Score++;
        TextScore.text = Score.ToString();
        SpawnBox();
    }
    private void SpawnBox()
    {
        Transform segment = Instantiate(this.boxPrefab);
        _boxes.Add(segment);
        //pos rand
        Bounds bounds = this.gridArea.bounds;        
        do
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            //
            segment.transform.position = new Vector2(Mathf.Round(x), Mathf.Round(y));

        }
        while (CheckOnSpawn(segment.transform.position)==false);
    }

    private void ResetState()
    {
        //snake 
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        //box
        for (int i = 1; i < _boxes.Count; i++)
        {
            Destroy(_boxes[i].gameObject);
        }
        _boxes.Clear();
        _boxes.Add(this.transform);
        this.transform.position = Vector2.zero;
        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
            _segments[i].position = new Vector2(this.transform.position.x, this.transform.position.y);
            _segments[i].GetComponent<SpriteRenderer>().sprite = null;

        }
        if (Score > MaxScore) MaxScore = Score;
        TextMaxScore.text = MaxScore.ToString();
        Score = 0;
        TextScore.text = Score.ToString();
        Time.timeScale = 1f;
    }
    public bool CheckOnSpawn(Vector3 spawn)
    {
        //not in front of face
        float distance = 5;
        if (Mathf.Abs((this.transform.position.x-spawn.y))<distance &&
           Mathf.Abs((this.transform.position.y - spawn.y))<distance) return false;        
        //check segments and boxes
        for (int i = 0; i < _segments.Count; i++)
        {
            if (_segments[i].transform.position == spawn) return false;            
        }
        for (int i = 0; i < _boxes.Count-1; i++)
        {
            if (_boxes[i].transform.position == spawn) return false;
        }
        if (applePrefab.position == spawn) return false;
        return true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {

            Grow();
            Time.timeScale = Time.timeScale + 0.05f;
            
        }
        else 
        if (collision.tag == "Obstacle")
        {
            ResetState();            
        }
    }
    private void GameStart()
    {
        
    }
}
