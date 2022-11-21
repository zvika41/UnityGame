using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetsController : BaseTargetsController
{
    #region -- Override Methods ---

    public override void StartSpawn(int difficulty)
    {
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
    }
    
    public override void StopSpawn()
    {
        StopAllCoroutines();
    }
    
    public override IEnumerator SpawnTarget()
    {
        while (GameManager.Instance.IsGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            string objectTag = tags[Random.Range(0, tags.Length)];
            GameManager.Instance.ObjectPoolerController.SpawnFromPool(objectTag);
        }
    }

    #endregion Override Methods
}