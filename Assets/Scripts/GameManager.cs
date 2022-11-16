using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
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
    private bool _isGameActive;
    private float _spawnRate;

    #endregion Members


    #region --- Mono Methods ---

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = GameObject.Find(GAME_MANAGER_NAME).GetComponent<GameManager>();
        }

        _playerController = GameObject.Find(GlobalConstMembers.PLAYER).GetComponent<PlayerController>();
        _playerController.gameObject.SetActive(false);
    }

    #endregion Mono Methods


    #region --- Public Methods ---

    public void StartGame(int difficulty)
    {
        startGameText.gameObject.SetActive(false);
        _playerController.gameObject.SetActive(true);
        _isGameActive = true;
        _spawnRate = 1;
        _spawnRate /= difficulty;
        ScoringSystem.Instance.InitScore();
        StartCoroutine(SpawnTarget());
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


    #region --- Private Methods ---

    private IEnumerator SpawnTarget()
    {
        while (_isGameActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    #endregion Private Methods
}
