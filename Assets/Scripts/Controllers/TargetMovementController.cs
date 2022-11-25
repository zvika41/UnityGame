using UnityEngine;

public class TargetMovementController : MonoBehaviour
{
    #region --- Members ---
    
    private Rigidbody _rigidBody;
    private float _minSpeed;
    private float _maxSpeed;
    private float _xRange;
    private float _ySpawnPos;

    #endregion Members
    
    
    #region --- Mono Methods ---

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.AddForce(RandomForce());

        transform.position = RandomPosition();
    }

    private void Update()
    {
        HandleGameObjects();
    }
    
    #endregion Mono Methods
    
    
    #region --- Private Methods ---
    
    private void HandleGameObjects()
    {
        if (!(gameObject.transform.position.y < 0)) return;
        
        if (gameObject.CompareTag(GlobalConstMembers.ENEMY) || gameObject.CompareTag(GlobalConstMembers.BOMB) ||
            gameObject.CompareTag(GlobalConstMembers.MULTIPLER_BOOST) ||
            gameObject.CompareTag(GlobalConstMembers.HEALTH))
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
    
    private Vector3 RandomPosition()
    {
        _xRange = 9;
        _ySpawnPos = 15;
        
        return new Vector3(Random.Range(-_xRange, _xRange), _ySpawnPos);
    }

    #endregion Private Methods
}
