using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] private List<GameObject> health;

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
        
        foreach (GameObject o in health)
        {
            o.SetActive(true);
        }
    }
    
    public void DisableHealthObject(int objectNumber)
    {
        _isHealthFull = false;
        
        if (objectNumber == 1)
        {
            health[1].SetActive(false);
        }
        else if (objectNumber == 2)
        {
            health[2].SetActive(false);
        }
        else if (objectNumber == 3)
        {
            health[3].SetActive(false);
        }
        else
        {
            foreach (GameObject o in health)
            {
                o.SetActive(false);
            }
        }
    }

    public void AddLife()
    {
        if (!health[1].gameObject.activeInHierarchy)
        {
            health[1].SetActive(true);
        }
        else if (!health[2].gameObject.activeInHierarchy)
        {
            health[2].SetActive(true);
        }
        else if (!health[3].gameObject.activeInHierarchy)
        {
            health[3].SetActive(true);
        }
    }

    #endregion Public Methods
}
