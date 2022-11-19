using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] private GameObject health1;
    [SerializeField] private GameObject health2;
    [SerializeField] private GameObject health3;

    #endregion SerializeField
    
    
    #region --- Public Methods ---

    public void ActiveHealthObjects()
    {
        health1.SetActive(true);
        health2.SetActive(true);
        health3.SetActive(true);
    }
    
    public void DisableHealthObject(int objectNumber)
    {
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

    #endregion Public Methods
}
