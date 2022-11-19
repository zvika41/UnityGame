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

    private const string GAME_MANAGER_OBJECT__NAME = "GameManager";
    private const string SPAWN_MANAGER_NAME = "SpawnManager";
    private const string SPAWN_BOOST_NAME = "SpawnBoosts";
    private const string HEALTH_OBJECT_NAME = "HealthManager";
    private const string SOUND_EFFECT_OBJECT__NAME = "SoundsEffect";
    private const string BCKGROUND_OBJECT__NAME = "Background";

    #endregion Const
    
    
    #region --- SerializeField ---

    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI startGameText;
    [SerializeField] private Button button;
    [SerializeField] private Button exitButton;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private AudioSource _musicTheme;
    private bool _isGameActive;

    #endregion Members


    #region --- Controllers/Managers ---

    public PlayerController playerController;
    public SoundsEffectController soundsEffectController;
    public BackgroundController backgroundController;
    
    public HealthManager healthManager;
    public SpawnManager spawnManager;
    public SpawnBoosts spawnBoosts;
    
    #endregion Controllers/Managers


    #region --- Properties ---

    public bool IsGameActive => _isGameActive;

    #endregion Properties
    

    #region --- Mono Methods ---

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = GameObject.Find(GAME_MANAGER_OBJECT__NAME).GetComponent<GameManager>();
        }
        
        Init();
        
        playerController.gameObject.SetActive(false);
        healthManager.DisableHealthObject(4);
        _musicTheme = GetComponent<AudioSource>();
        _musicTheme.Play();
    }

    #endregion Mono Methods


    #region --- Private Methods ---

    private void Init()
    {
        playerController = GameObject.Find(GlobalConstMembers.PLAYER).GetComponent<PlayerController>();
        healthManager = GameObject.Find(HEALTH_OBJECT_NAME).GetComponent<HealthManager>();
        spawnManager = GameObject.Find(SPAWN_MANAGER_NAME).GetComponent<SpawnManager>();
        spawnBoosts  = GameObject.Find(SPAWN_BOOST_NAME).GetComponent<SpawnBoosts>();
        backgroundController = GameObject.Find(BCKGROUND_OBJECT__NAME).GetComponent<BackgroundController>();
        soundsEffectController  = GameObject.Find(SOUND_EFFECT_OBJECT__NAME).GetComponent<SoundsEffectController>();
    }

    #endregion Private Methods
    

    #region --- Public Methods ---

    public void StartGame(int difficulty)
    {
        startGameText.gameObject.SetActive(false);
        playerController.gameObject.SetActive(true);
        healthManager.ActiveHealthObjects();
        _isGameActive = true;
        ScoringSystem.Instance.InitScore();
        spawnManager.StartSpawn(difficulty);
        spawnBoosts.StartSpawn(difficulty);
       backgroundController.ShouldRepeatBackground = true;
    }
    
    public void GameOver()
    {
        if (playerController.gameObject.activeInHierarchy)
        {
            playerController.gameObject.SetActive(false);
        }
        
        _isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        spawnManager.StopSpawn();
        spawnBoosts.StopSpawn();
        backgroundController.ShouldRepeatBackground = false;
        ScoringSystem.Instance.SaveData();
        ScoringSystem.Instance.IsMultiplierBoost = false;
        _musicTheme.Stop();
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
