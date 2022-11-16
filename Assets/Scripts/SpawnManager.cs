using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] private List<GameObject> targets;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private float _spawnRate;
    
    #endregion Members


    #region -- Public Methods ---

    public void StartSpawn(int difficulty)
    {
        _spawnRate = 1;
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

    #endregion Private Methods
}
