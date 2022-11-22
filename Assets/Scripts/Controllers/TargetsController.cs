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

    #endregion Override Methods
}