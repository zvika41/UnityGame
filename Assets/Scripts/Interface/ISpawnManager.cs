using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnManager
{
    float SpawnRate { get;}

    void StartSpawn(int level);

    void StopSpawn();

}
