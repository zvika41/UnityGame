using UnityEngine;
using Random = UnityEngine.Random;

public class Targets : MonoBehaviour
{
    public ParticleSystem particleSystem;

    private Rigidbody _rigidBody;
    private float _speed;
    private float _minSpeed;
    private float _maxSpeed;
    private float _maxTorque;
    private float _xRange;
    private float _ySpawnPos;
    private bool _isObjectShowed;
    private bool _test;
    
    
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.AddForce(RandomForce());
        _rigidBody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomPosition();
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
            if (GameManager.Instance.score > 0)
            {
                GameManager.Instance.UpdateScore(-1);
            }
            // else if (_gameManager.score == 0)
            // {
            //     _gameManager.GameOver();
            // }
            
            Destroy(gameObject);
        }
        else if (_isObjectShowed && gameObject.transform.position.y < 0 && gameObject.CompareTag("Bad"))
        {
            Destroy(gameObject);
        }
    }

    private Vector3 RandomForce()
    {
        _minSpeed = 0.1f;
        _maxSpeed = 0.3f;
        
        return Vector3.down * Random.Range(_minSpeed, _maxSpeed);
    }
    
    private float RandomTorque()
    {
        _maxTorque = 10;
        
        return Random.Range(-_maxTorque, _maxTorque);
    }
    
    private Vector3 RandomPosition()
    {
        _xRange = 6;
        _ySpawnPos = 12;
        
        return new Vector3(Random.Range(-_xRange, _xRange), _ySpawnPos);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
            GameManager.Instance.GameOver();
        }
    }
}
