using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] private GameObject health1;
    [SerializeField] private GameObject health2;
    [SerializeField] private GameObject health3;

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
        health1.SetActive(true);
        health2.SetActive(true);
        health3.SetActive(true);
    }
    
    public void DisableHealthObject(int objectNumber)
    {
        _isHealthFull = false;
        
        if (objectNumber == 1)
        {
            health1.SetActive(false);
        }
        else if (objectNumber == 2)
        {
            health2.SetActive(false);
        }
        else if (objectNumber == 3)
        {
            health3.SetActive(false);
        }
        else
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(false);
        }
    }

    public void AddLife()
    {
        if (!health1.gameObject.activeInHierarchy)
        {
            health1.SetActive(true);
        }
        else if (!health2.gameObject.activeInHierarchy)
        {
            health2.SetActive(true);
        }
        else if (!health3.gameObject.activeInHierarchy)
        {
            health3.SetActive(true);
        }
    }

    #endregion Public Methods
}
