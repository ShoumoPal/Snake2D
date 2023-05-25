using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    [SerializeField]
    private float speedMultiplier = 1;

    [SerializeField]
    private int initialSize;

    [SerializeField]
    private GameOverController gameOverController;

    [SerializeField]
    private ParticleSystem _foodParticle;

    [SerializeField]
    private ParticleSystem _burnParticle;

    public ScoreManager scoreManager;

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
    private void OnEnable()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.Spawn);
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
        if (Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down)
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up)
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left)
        {
            _direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right)
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
        FoodController _foodController = collision.gameObject.GetComponent<FoodController>();

        if(_foodController != null && _foodController.GetFoodType() == FoodType.MassGainer)
        {
            _foodParticle.transform.position = transform.position;
            _foodParticle.Play();

            SoundManager.Instance.Play(SoundManager.Sounds.Pickup);
            scoreManager.AddScore(10);
            Grow();
        }
        else if(_foodController != null && _foodController.GetFoodType() == FoodType.MassBurner)
        {
            _burnParticle.transform.position = transform.position;
            _burnParticle.Play();

            SoundManager.Instance.Play(SoundManager.Sounds.BadPickup);
            scoreManager.RemoveScore(5);
            Burn();
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            //GameOver
            GameOver();
        }
    }
    private void GameOver()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.Death);
        this.enabled = false;
        gameOverController.ShowGameOverPanel();
    }
    private void Burn()
    {
        if(_segments.Count == 1)
        {
            GameOver();
        }
        else
        {
            Destroy(_segments[_segments.Count - 1].gameObject);
            _segments.Remove(_segments[_segments.Count - 1]);
        }
    }
}
