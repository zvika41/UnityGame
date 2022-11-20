using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTargetsController : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] public GameObject[] targets;
    [SerializeField] public float spawnRate;

    #endregion SerializeField


    #region -- Virtual Methods ---

    public virtual void StartSpawn(int difficulty) { }
    
    public virtual void StopSpawn() { }
    
    public virtual IEnumerator SpawnTarget()
    {
        yield return new WaitForSeconds(0);
    }

    #endregion Virtual Methods
}
