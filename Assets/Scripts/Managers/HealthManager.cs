using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] private List<GameObject> healthList;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private bool _isHealthFull;
    
    #endregion Members
    
    
    #region --- Properties ---

    public bool IsHealthFull => _isHealthFull;

    #endregion Properties
    
    
    #region --- Public Methods ---

    public void ActiveHealthObjects()
    {
        _isHealthFull = true;
        
        foreach (GameObject health in healthList)
        {
            health.SetActive(true);
        }
    }
    
    public void DisableHealthObject(int objectNumber)
    {
        _isHealthFull = false;

        if (objectNumber <= healthList.Count)
        {
            healthList[objectNumber].SetActive(false);
        }
        else
        {
            foreach (GameObject health in healthList)
            {
                health.SetActive(false);
            }
        }
    }

    public void AddLife()
    {
        foreach (GameObject health in healthList)
        {
            if (!health.gameObject.activeInHierarchy)
            {
                health.SetActive(true);
            }
        }
    }

    #endregion Public Methods
}
