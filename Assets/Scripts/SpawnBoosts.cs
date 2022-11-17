using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoosts : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] private List<GameObject> boosts;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private float _spawnRate;
    
    #endregion Members


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
            //int index = Random.Range(0, boosts.Count);
            Instantiate(boosts[0]);
        }
    }

    #endregion Private Methods
}
