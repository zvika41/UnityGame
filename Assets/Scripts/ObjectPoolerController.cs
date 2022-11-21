using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ObjectPoolerController : MonoBehaviour
{
   #region --- SerializeField ---

   [SerializeField]  private List<PoolObject> poolList;

   #endregion SerializeField


   #region --- Members ---

   private Dictionary<string, Queue<GameObject>> poolDictionary;

   #endregion Members

   
   #region --- Mono Methods ---

   private void Start()
   {
      poolDictionary = new Dictionary<string,  Queue<GameObject>>();

      foreach (PoolObject poolObject in poolList)
      {
         Queue<GameObject> objectPool = new Queue<GameObject>();

         for (int i = 0; i < poolObject.Size; i++)
         {
            GameObject obj = Instantiate(poolObject.Prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
         }
         
         poolDictionary.Add(poolObject.Tag, objectPool);
      }
   }

   #endregion Mono Methods
   
   
   #region --- Public Methods ---

   public void SpawnFromPool(string objectTag, [Optional] Vector3 position)
   {
      if(!poolDictionary.ContainsKey(objectTag)) return;
      
      GameObject objectToSpawn = poolDictionary[objectTag].Dequeue();

      if (objectToSpawn != null)
      {
         objectToSpawn.SetActive(true);
         objectToSpawn.transform.position = position;
      
         poolDictionary[objectTag].Enqueue(objectToSpawn);
      }
   }

   #endregion Private Methods
   

   #region --- Internal Classes ---

   [System.Serializable]
   public class PoolObject
   {
      public string Tag;
      public GameObject Prefab;
      public int Size;
   }

   #endregion Internal Classes
}