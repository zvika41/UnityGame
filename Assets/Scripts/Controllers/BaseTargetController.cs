using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseTargetsController : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] protected float spawnRate;
    [SerializeField] protected string[] tags;

    #endregion SerializeField
    

    #region --- Mono Methods ---
    
    private void Start()
    {
       RegisterToCallbacks();
    }

    #endregion Mono Methods
 
    
    #region -- Virtual Methods ---

    protected virtual void StartSpawn(int difficulty){ }

    protected virtual void StopSpawn()
    {
        GameManager.Instance.InvokeGameOver -= StopSpawn;
    }

    #endregion Virtual Methods


    #region --- Protected Methods ---

    protected  IEnumerator SpawnTarget()
    {
        while (GameManager.Instance.IsGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            string objectTag = tags[Random.Range(0, tags.Length)];
            GameManager.Instance.ObjectPoolerController.SpawnFromPool(objectTag);
        }
    }

    #endregion Protected Methods


    #region --- Private Methods ---
    
    private void StartSpawning()
    {
        GameManager.Instance.GameStart -= StartSpawning;
        StartSpawn(GameManager.Instance.GameDifficulty);
    }
    
    #endregion Private Methods
    
    
    #region --- Event Handler ---

    private void RegisterToCallbacks()
    {
        GameManager.Instance.GameStart += StartSpawning;
        GameManager.Instance.InvokeGameOver += StopSpawn;
    }
    
    #endregion Event Handler
}
