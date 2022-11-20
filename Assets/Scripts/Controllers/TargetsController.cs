using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetsController : BaseTargetController, ISpawnManager
{
    #region --- SerializeField ---

    [SerializeField] private GameObject[] targets;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private float _spawnRate;

    #endregion Members
    
    
    #region --- Properties ---

    public float SpawnRate => _spawnRate;

    #endregion Properties
    
    
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
            int index = Random.Range(0, targets.Length);
            Instantiate(targets[index]);
        }
    }
    
    #endregion Private Methods
}
