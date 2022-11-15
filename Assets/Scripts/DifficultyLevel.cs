using UnityEngine;
using UnityEngine.UI;

public class DifficultyLevel : MonoBehaviour
{
    private Button _difficulteLevel;
    private GameManager _gameManager;
    
    public int difficultlyLevelSelected;
    
    
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _difficulteLevel = GetComponent<Button>();
        _difficulteLevel.onClick.AddListener(OnDifficultySelected);
        
        
    }

    private void OnDifficultySelected()
    {
        Debug.Log(gameObject.name + " was selected");
        _gameManager.StartGame(difficultlyLevelSelected);
    }
}