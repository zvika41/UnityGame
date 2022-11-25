public class TargetsController : BaseTargetsController
{
    #region -- Override Methods ---

    protected override void StartSpawn(int difficulty)
    {
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
    }

    protected override void StopSpawn()
    {
        base.StopSpawn();
        StopAllCoroutines();
    }

    #endregion Override Methods
}