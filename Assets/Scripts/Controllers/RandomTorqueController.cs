using UnityEngine;

public class RandomTorqueController : MonoBehaviour
{
    #region --- Members ---
    
    private Rigidbody _rigidBody;
    private float _spawnRate;
    private float _maxTorque;

    #endregion Members
    
    
    #region --- Mono Methods ---

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    #endregion Mono Methods
    
    
    #region --- Private Methods ---
    
    private float RandomTorque()
    {
        _maxTorque = 3;
         
        return Random.Range(-_maxTorque, _maxTorque);
    }

    #endregion Private Methods
}