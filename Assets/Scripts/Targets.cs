using TreeEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Targets : MonoBehaviour
{
    public ParticleSystem particleSystem;

    private Rigidbody _rigidBody;
    private GameManager _gameManager;
    private float _speed;
    private float _minSpeed;
    private float _maxSpeed;
    private float _maxTorque;
    private float _xRange;
    private float _ySpawnPos;
    private int _points;
    private bool _isObjectShowed;
    private bool _test;
    
    
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        _rigidBody.AddForce(RandomForce(), ForceMode.Impulse);
        _rigidBody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomPosition();

        _points = 5;
        
    }

    private void Update()
    {
       HandleGameObjects();
    }

    private void HandleGameObjects()
    {
        if (gameObject.transform.position.y > 0 && gameObject.CompareTag("Good") || gameObject.transform.position.y > 0 && gameObject.CompareTag("Bad"))
        {
            _isObjectShowed = true;
        }

        if (_isObjectShowed && gameObject.transform.position.y < 0 && gameObject.CompareTag("Good"))
        {
            if (_gameManager.score > 0)
            {
                _gameManager.UpdateScore(-_points);
            }
            else if (_gameManager.score == 0)
            {
                _gameManager.GameOver();
            }
            
            Destroy(gameObject);
        }
        else if (_isObjectShowed && gameObject.transform.position.y < 0 && gameObject.CompareTag("Bad"))
        {
            Destroy(gameObject);
        }
    }

    private Vector3 RandomForce()
    {
        _minSpeed = 12;
        _maxSpeed = 16;
        
        return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
    }
    
    private float RandomTorque()
    {
        _maxTorque = 10;
        
        return Random.Range(-_maxTorque, _maxTorque);
    }
    
    private Vector3 RandomPosition()
    {
        _xRange = 4;
        _ySpawnPos = -6;
        
        return new Vector3(Random.Range(-_xRange, _xRange), _ySpawnPos);
    }
    
    // private void OnMouseDown()
    // {
    //     if (_gameManager.isGameActive)
    //     {
    //         if (gameObject.CompareTag("Good"))
    //         {
    //            
    //             _gameManager.UpdateScore(_points);
    //         }
    //         else if (gameObject.CompareTag("Bad"))
    //         {
    //             
    //             _gameManager.GameOver();
    //         }
    //     }
    // }
    
    private void OnCollisionEnter(Collision other)
    {
        if (_gameManager.isGameActive && other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
            _gameManager.GameOver();
        }
    }
}
