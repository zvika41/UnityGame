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
            int index = Random.Range(0, targets.Length);
            Instantiate(targets[index]);
        }
    }

    #endregion Override Methods
}