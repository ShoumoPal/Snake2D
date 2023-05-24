using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    [SerializeField]
    private float speedMultiplier = 1;

    [SerializeField]
    private int initialSize;

    public BoxCollider2D grid;
    private Bounds bounds;

    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;

    public Transform segmentPrefab;

    private void Start()
    {
        bounds = grid.bounds;
        _segments = new List<Transform>();
        _segments.Add(transform);
        for(int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(segmentPrefab, transform.position, Quaternion.identity));
        }
    }
    private void Update()
    {
        SnakeMovement();
        MovementWrap();
    }
    private void FixedUpdate()
    {
        for(int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + _direction.x * speedMultiplier,
            Mathf.Round(transform.position.y) + _direction.y * speedMultiplier,
            0.0f);
    }
    private void MovementWrap()
    {
        if (transform.position.x > bounds.max.x)
        {
            transform.position = new Vector3(bounds.min.x, transform.position.y, 0.0f);
        }
        else if (transform.position.x < bounds.min.x)
        {
            transform.position = new Vector3(bounds.max.x, transform.position.y, 0.0f);
        }
        else if (transform.position.y < bounds.min.y)
        {
            transform.position = new Vector3(transform.position.x, bounds.max.y, 0.0f);
        }
        else if (transform.position.y > bounds.max.y)
        {
            transform.position = new Vector3(transform.position.x, bounds.min.y, 0.0f);
        }
    }
    private void SnakeMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
    }
    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<FoodController>() != null)
        {
            Grow();
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            //GameOver
            SceneManager.LoadScene(0);
        }
    }
}
