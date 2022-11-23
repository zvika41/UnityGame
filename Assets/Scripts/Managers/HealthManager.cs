using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] private List<GameObject> healthList;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private bool _isHealthFull;
    private int _healthCounter;
    
    #endregion Members
    
    
    #region --- Properties ---

    public bool IsHealthFull => _isHealthFull;

    public int HealthCounter
    {
        get => _healthCounter;
        set => _healthCounter = value;
    }

    #endregion Properties
    
    
    #region --- Mono Methods ---
    
    private void Awake()
    {
        _healthCounter = 3;
    }

    #endregion Mono Methods
    
    
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
        if (objectNumber > healthList.Count) return;

        healthList[objectNumber].SetActive(false);
        _isHealthFull = false;
    }
    
    public void DisableAllHealthObjects()
    {
        foreach (GameObject health in healthList)
        {
            health.SetActive(false);
        }
    }

    public void AddLife(int objectNumber)
    {
        healthList[objectNumber].SetActive(true);
        
        if (objectNumber == 2)
        {
            _isHealthFull = true;
        }
    }

    #endregion Public Methods
}
