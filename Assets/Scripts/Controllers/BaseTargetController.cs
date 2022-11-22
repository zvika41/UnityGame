using System.Collections;
using UnityEngine;

public class BaseTargetsController : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] protected float spawnRate;
    [SerializeField] protected string[] tags;

    #endregion SerializeField


    #region -- Virtual Methods ---

    public virtual void StartSpawn(int difficulty){ }
    
    public virtual void StopSpawn(){ }
    

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
}
