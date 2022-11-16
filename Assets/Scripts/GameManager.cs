using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    #region --- Const ---

    private const string GAME_MANAGER_NAME = "GameManager";
    private const string SPAWN_MANAGER_NAME = "SpawnManager";

    #endregion Const
    
    
    #region --- SerializeField ---

    [SerializeField] private List<GameObject> targets;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI startGameText;
    [SerializeField] private Button button;
    [SerializeField] private Button exitButton;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private PlayerController _playerController;
    private SpawnManager _spawnManager;
    private bool _isGameActive;

    #endregion Members


    #region --- Properties ---

    public bool IsGameActive => _isGameActive;

    #endregion Properties
    

    #region --- Mono Methods ---

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = GameObject.Find(GAME_MANAGER_NAME).GetComponent<GameManager>();
        }

        _playerController = GameObject.Find(GlobalConstMembers.PLAYER).GetComponent<PlayerController>();
        _playerController.gameObject.SetActive(false);

        _spawnManager = GameObject.Find(SPAWN_MANAGER_NAME).GetComponent<SpawnManager>();
    }

    #endregion Mono Methods


    #region --- Public Methods ---

    public void StartGame(int difficulty)
    {
        startGameText.gameObject.SetActive(false);
        _playerController.gameObject.SetActive(true);
        _isGameActive = true;
        ScoringSystem.Instance.InitScore();
        _spawnManager.StartSpawn(difficulty);
    }
    
    public void GameOver()
    {
        if (_playerController.gameObject.activeInHierarchy)
        {
            _playerController.gameObject.SetActive(false);
        }
        
        _isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        _spawnManager.StopSpawn();
        ScoringSystem.Instance.SaveData();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    #endregion Public Methods
}
