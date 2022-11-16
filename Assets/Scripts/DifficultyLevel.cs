using UnityEngine;
using UnityEngine.UI;

public class DifficultyLevel : MonoBehaviour
{
    #region --- Enum ---

    private enum LevelOptions
    {
        Easy,
        Medium,
    }

    #endregion Enum
    
    
    #region --- Members ---
    
    private Button _difficultiesLevel;
    private int _difficultlyLevelSelected;

    #endregion Members
    
    
    #region --- Mono Methods ---

    private void Start()
    {
        _difficultiesLevel = GetComponent<Button>();
        _difficultiesLevel.onClick.AddListener(OnDifficultySelected);
    }

    #endregion Mono Methods
    

    #region --- Private Methods ---

    private int GetLevelSelected()
    {
        if (_difficultiesLevel.gameObject.name == LevelOptions.Easy.ToString())
        {
            _difficultlyLevelSelected = 1;
        }
        else if (_difficultiesLevel.gameObject.name == LevelOptions.Medium.ToString())
        {
            _difficultlyLevelSelected = 2;
        }
        else
        {
            _difficultlyLevelSelected = 3;
        }
        
        return _difficultlyLevelSelected;
    }

    #endregion Private Methods
    
    
    #region --- Event Handler ---

    private void OnDifficultySelected()
    {
        int level = GetLevelSelected();
        GameManager.Instance.StartGame(level);
    }

    #endregion Event Handler
}