using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetsController : MonoBehaviour, ISpawnManager
{
    #region --- SerializeField ---

    [SerializeField] private List<GameObject> targets;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private Rigidbody _rigidBody;
    private float _speed;
    private float _minSpeed;
    private float _maxSpeed;
    private float _maxTorque;
    private float _xRange;
    private float _ySpawnPos;
    private bool _isObjectShowed;
    private float _spawnRate;

    #endregion Members
    
    
    #region --- Properties ---

    public float SpawnRate => _spawnRate;

    #endregion Properties
    
    
    #region --- Mono Methods ---

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
    
    #endregion Mono Methods
    
    
    #region -- Public Methods ---

    public void StartSpawn(int difficulty)
    {
        _spawnRate = 2;
        _spawnRate /= difficulty;
      
        StartCoroutine(SpawnTarget());
    }
    
    public void StopSpawn()
    {
        StopAllCoroutines();
    }

    #endregion Public Methods
   
    
    #region --- Private Methods ---
    
    private IEnumerator SpawnTarget()
    {
        while (GameManager.Instance.IsGameActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    private void HandleGameObjects()
    {
        if (gameObject.transform.position.y < 0 && (gameObject.CompareTag(GlobalConstMembers.ENEMY) || gameObject.CompareTag(GlobalConstMembers.BOMB) || gameObject.CompareTag(GlobalConstMembers.MILTIPLER_BOOST)))
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
        _xRange = 5;
        _ySpawnPos = 15;
        
        return new Vector3(Random.Range(-_xRange, _xRange), _ySpawnPos);
    }

    #endregion Private Methods
    
    
    #region --- Event Handler ---

    private void OnCollisionEnter(Collision other)
    {
        if (!gameObject.CompareTag(GlobalConstMembers.MILTIPLER_BOOST) && other.gameObject.CompareTag(GlobalConstMembers.PLAYER))
        {
            GameManager.Instance.SoundsEffectController.PlayEffect();
            Destroy(gameObject);
        }
    }

    #endregion Event Handler
}
