using System.Collections;
using UnityEngine;

public class BoostsController : BaseTargetController, ISpawnManager
{
    #region --- SerializeField ---

    [SerializeField] private GameObject[] boosts;

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
        _spawnRate = 10;
        _spawnRate /= difficulty;
      
        StartCoroutine(SpawnBoost());
    }
    
    public void StopSpawn()
    {
        StopAllCoroutines();
    }

    #endregion Public Methods
    
    
    #region --- Private Methods ---

    private IEnumerator SpawnBoost()
    {
        while (GameManager.Instance.IsGameActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, boosts.Length);
            Instantiate(boosts[index]);
        }
    }

    #endregion Private Methods
}