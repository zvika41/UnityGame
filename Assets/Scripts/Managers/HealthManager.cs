using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] private GameObject health1;
    [SerializeField] private GameObject health2;
    [SerializeField] private GameObject health3;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private List<GameObject> _healthList;
    private GameObject _health1;
    private GameObject _health2;
    private GameObject _health3;
    
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

    private void Start()
    {
        GameManager.Instance.GameStart += HandleGameStart;
    }

    #endregion Mono Methods


    #region --- Private Methods ---

    private void HandleGameStart()
    {
        _health1 = Instantiate(health1);
        _health2 = Instantiate(health2);
        _health3 = Instantiate(health3);

        _healthList = new List<GameObject> {_health1, _health2, _health3};

        foreach (GameObject health in _healthList)
        {
            health.SetActive(true);
        }
    }

    #endregion Private Methods
    
    
    #region --- Public Methods ---

    public void DisableHealthObject(int objectNumber)
    {
        if (objectNumber > _healthList.Count) return;
        
        _healthList[objectNumber].SetActive(false);
        _isHealthFull = false;
    }
    
    public void DisableAllHealthObjects()
    {
        foreach (GameObject health in _healthList)
        {
            health.SetActive(false);
        }
    }

    public void AddLife(int objectNumber)
    {
        _healthList[objectNumber].SetActive(true);
        
        if (objectNumber == 2)
        {
            _isHealthFull = true;
        }
    }

    #endregion Public Methods
}
