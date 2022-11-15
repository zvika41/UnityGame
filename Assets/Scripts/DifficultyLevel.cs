using UnityEngine;
using UnityEngine.UI;

public class DifficultyLevel : MonoBehaviour
{
    private Button _difficultiesLevel;
    
    public int difficultlyLevelSelected;
    
    
    private void Start()
    {
        _difficultiesLevel = GetComponent<Button>();
        _difficultiesLevel.onClick.AddListener(OnDifficultySelected);
    }

    private void OnDifficultySelected()
    {
        GameManager.Instance.StartGame(difficultlyLevelSelected);
    }
}