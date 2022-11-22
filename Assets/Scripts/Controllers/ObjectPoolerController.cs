using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ObjectPoolerController : MonoBehaviour
{
   #region --- SerializeField ---

   [SerializeField]  private List<PoolObject> poolList;

   #endregion SerializeField


   #region --- Members ---

   private Dictionary<string, Queue<GameObject>> _poolDictionary;

   #endregion Members
   
   
   #region --- Mono Methods ---

   private void Start()
   {
      _poolDictionary = new Dictionary<string,  Queue<GameObject>>();
      
      foreach (PoolObject poolObject in poolList)
      {
         Queue<GameObject> objectPool = new Queue<GameObject>();
      
         for (int i = 0; i < poolObject.Size; i++)
         {
            GameObject obj = Instantiate(poolObject.Prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
         }
         
         _poolDictionary.Add(poolObject.Tag, objectPool);
      }
   }

   #endregion Mono Methods
   
   
   #region --- Public Methods ---

   public void SpawnFromPool(string objectTag, [Optional] Vector3 position)
   {
      if(!_poolDictionary.ContainsKey(objectTag)) return;
      
      GameObject objectToSpawn = _poolDictionary[objectTag].Dequeue();
      
      objectToSpawn.SetActive(true);
      objectToSpawn.transform.position = position;
      
      _poolDictionary[objectTag].Enqueue(objectToSpawn);
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