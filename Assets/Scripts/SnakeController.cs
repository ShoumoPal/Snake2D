using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.PlayerLoop.PreLateUpdate;

public class SnakeController : MonoBehaviour
{
    [SerializeField]
    private float powerUpDuration;

    [SerializeField]
    private bool hasShield;

    [SerializeField]
    public bool hasScoreBuff;

    [SerializeField]
    private bool hasSpeedBuff;

    [SerializeField]
    private int initialSize;

    [SerializeField]
    private int growAmount;

    [SerializeField]
    private int burnAmount;

    [SerializeField]
    private GameOverController gameOverController;

    [SerializeField]
    private ParticleSystem _foodParticle;

    [SerializeField]
    private ParticleSystem _burnParticle;

    [SerializeField]
    private FloatingTextController _textController;

    public ScoreManager scoreManager;

    public float speed = 20f;
    public float speedMultiplier = 1f;

    public BoxCollider2D grid;
    private Bounds bounds;

    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;
    private float nextUpdate;

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
        if (Time.time < nextUpdate)
        {
            return;
        }
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        Move();

        if(hasSpeedBuff)
        {
            nextUpdate = Time.time + (1f / (speed * 2 * speedMultiplier));
        }
        else
        {
            nextUpdate = Time.time + (1f / (speed * speedMultiplier));
        }
    }
    private void Move()
    {
        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + _direction.x,
            Mathf.Round(transform.position.y) + _direction.y,
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
        // Only allow turning up or down while moving in the x-axis
        if (_direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _direction = Vector2.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (_direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                _direction = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                _direction = Vector2.left;
            }
        }
    }
    private void Grow(int _growAmount)
    {
        for(int i = 0; i < _growAmount; i++) 
        {
            Transform segment = Instantiate(segmentPrefab);
            segment.position = _segments[_segments.Count - 1].position;
            _segments.Add(segment);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FoodController _foodController = collision.gameObject.GetComponent<FoodController>();
        PowerUpBehaviour _powerUpBehaviour = collision.gameObject.GetComponent<PowerUpBehaviour>();

        if (_foodController != null && _foodController.GetFoodType() == FoodType.MassGainer)
        {
            _foodParticle.transform.position = transform.position;
            _foodParticle.Play();

            SoundManager.Instance.Play(SoundManager.Sounds.Pickup);
            scoreManager.AddScore(10);
            Grow(growAmount);
        }
        else if(_foodController != null && _foodController.GetFoodType() == FoodType.MassBurner && hasShield == false)
        {
            _burnParticle.transform.position = transform.position;
            _burnParticle.Play();

            SoundManager.Instance.Play(SoundManager.Sounds.BadPickup);
            scoreManager.RemoveScore(5);
            Burn(burnAmount);
        }
        else if(_powerUpBehaviour != null) //picking up powerUp
        {
            PowerUpTypes powerUp = _powerUpBehaviour.GetPowerUpType();

            if(hasShield == true || hasScoreBuff == true || hasSpeedBuff == true) 
            {
                _textController.ShowText("One buff is already active!");
            }
            else if(powerUp == PowerUpTypes.Shield)
            {
                hasShield = true;
                _textController.ShowText("Picked up a shield! \nInvincible for 5 seconds!");
                SoundManager.Instance.Play(SoundManager.Sounds.PowerUpPickup);
                StartCoroutine(nameof(PowerUpDuration));
            }
            else if(powerUp == PowerUpTypes.ScoreBoost)
            {
                hasScoreBuff = true;
                _textController.ShowText("Picked up a Score booster! \nScore gains doubled!");
                SoundManager.Instance.Play(SoundManager.Sounds.PowerUpPickup);
                StartCoroutine(nameof(PowerUpDuration));
            }
            else if(powerUp == PowerUpTypes.SpeedUp)
            {
                hasSpeedBuff = true;
                _textController.ShowText("Picked up a Speed booster! \nx2 speed for a short duration!");
                SoundManager.Instance.Play(SoundManager.Sounds.PowerUpPickup);
                StartCoroutine(nameof(PowerUpDuration));
            }
        }
        else if(collision.gameObject.CompareTag("Obstacle") && hasShield == false)
        {
            //GameOver
            GameOver();
        }
    }
    IEnumerator PowerUpDuration()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasShield = false;
        hasScoreBuff = false;
        hasSpeedBuff= false;
        _textController.ShowText("Buff has ended!");
    }
    private void GameOver()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.Death);
        this.enabled = false;
        gameOverController.ShowGameOverPanel();
    }
    private void Burn(int _burnAmount)
    {
        for(int i = 0; i < _burnAmount; i++)
        {
            if (_segments.Count == 1)
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
}
