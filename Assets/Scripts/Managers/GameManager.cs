using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region --- Singleton ---

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    #endregion Singleton
   
    
    #region --- Const ---

    private const string GAME_MANAGER_OBJECT_NAME = "GameManager";
    private const string TARGETS_NAME = "Targets";
    private const string SCORING_MANAGER_OBJECT_NAME = "ScoringManager";
    private const string BOOST_OBJECT_NAME = "Boost";
    private const string HEALTH_OBJECT_NAME = "HealthManager";
    private const string SOUND_EFFECT_OBJECT_NAME = "SoundsEffect";
    private const string BCKGROUND_OBJECT_NAME = "Background";
    private const string INFO_POPUP_OBJECT_NAME = "InfoPopup";
    private const string TIMER_OBJECT_NAME = "Timer";
    private const string TARGET_POOLER_OBJECT_NAME = "TargetPooler";

    #endregion Const
    
    
    #region --- SerializeField ---

    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI startGameText;
    [SerializeField] private Button button;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button infoButton;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private AudioSource _musicTheme;
    private bool _isGameActive;
    private bool _isTimerStarted;
    private int _gameDifficulty;

    #endregion Members


    #region --- Controllers/Managers ---

    private PlayerController _playerController;
    private TargetsController _targetsController;
    private SoundsEffectController _soundsEffectController;
    private BackgroundController _backgroundController;
    private BoostsController _boostsController;
    private InfoPopupController _infoPopupController;
    private TimerController _timerController;
    private ObjectPoolerController _objectPoolerController;
    
    private ScoringManager _scoringManager;
    private HealthManager _healthManager;
    
    #endregion Controllers/Managers


    #region --- Properties ---

    public bool IsGameActive => _isGameActive;

    public TextMeshProUGUI StartGameText => startGameText;

    #region ___Controllers___

    public SoundsEffectController SoundsEffectController
    {
        get => _soundsEffectController;
        private set => _soundsEffectController = value;
    }

    public ObjectPoolerController ObjectPoolerController
    {
        get => _objectPoolerController;
        private set => _objectPoolerController = value;
    }

    #endregion Controllers

    #region ___ Managers ___

    public ScoringManager ScoringManager
    {
        get => _scoringManager;
        private set => _scoringManager = value;
    }
    
    public HealthManager HealthManager
    {
        get => _healthManager;
        private set => _healthManager = value;
    }

    #endregion Managers
    
    #endregion Properties
    

    #region --- Mono Methods ---

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = GameObject.Find(GAME_MANAGER_OBJECT_NAME).GetComponent<GameManager>();
        }
        
        Init();
        
        _playerController.gameObject.SetActive(false);
        _timerController.gameObject.SetActive(false);
        _healthManager.DisableHealthObject(4);
        _musicTheme = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_timerController.IsTimerOn || !_isTimerStarted) return;
        
        _isTimerStarted = false;
        _timerController.gameObject.SetActive(false);
        StartGame(_gameDifficulty);
    }

    #endregion Mono Methods


    #region --- Private Methods ---

    private void Init()
    {
        _playerController = GameObject.Find(GlobalConstMembers.PLAYER).GetComponent<PlayerController>();
        _targetsController = GameObject.Find(TARGETS_NAME).GetComponent<TargetsController>();
        _boostsController = GameObject.Find(BOOST_OBJECT_NAME).GetComponent<BoostsController>();
        _backgroundController = GameObject.Find(BCKGROUND_OBJECT_NAME).GetComponent<BackgroundController>();
        _soundsEffectController = GameObject.Find(SOUND_EFFECT_OBJECT_NAME).GetComponent<SoundsEffectController>();
        _infoPopupController = GameObject.Find(INFO_POPUP_OBJECT_NAME).GetComponent<InfoPopupController>();
        _timerController = GameObject.Find(TIMER_OBJECT_NAME).GetComponent<TimerController>();
        _objectPoolerController = GameObject.Find(TARGET_POOLER_OBJECT_NAME).GetComponent<ObjectPoolerController>();

        _scoringManager = GameObject.Find(SCORING_MANAGER_OBJECT_NAME).GetComponent<ScoringManager>();
        _healthManager = GameObject.Find(HEALTH_OBJECT_NAME).GetComponent<HealthManager>();
    }
    
    private void StartGame(int difficulty)
    {
        _playerController.gameObject.SetActive(true);
        _healthManager.ActiveHealthObjects();
        _isGameActive = true;
        _scoringManager.InitScore();
        _targetsController.StartSpawn(difficulty);
        _boostsController.StartSpawn(difficulty);
        _backgroundController.ShouldRepeatBackground = true;
    }

    #endregion Private Methods
    

    #region --- Public Methods ---

    public void StartTimer(int difficulty)
    {
        _timerController.gameObject.SetActive(true);
        _gameDifficulty = difficulty;
        infoButton.gameObject.SetActive(false);
        startGameText.gameObject.SetActive(false);
        _timerController.IsTimerOn = true;
        _isTimerStarted = true;
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
        
        #if UNITY_EDITOR
        exitButton.gameObject.SetActive(true);
        #endif
        
        
        infoButton.gameObject.SetActive(false);
        _targetsController.StopSpawn();
        _boostsController.StopSpawn();
        _backgroundController.ShouldRepeatBackground = false;
        _scoringManager.SaveData();
        _scoringManager.IsMultiplierBoost = false;
        _musicTheme.Stop();
    }

    #endregion Public Methods
    
    
    #region -- Event Handler ---

    public void OnInfoButtonClicked()
    {
        StartGameText.gameObject.SetActive(false);
        _infoPopupController.InfoPopup.gameObject.SetActive(true);
        _infoPopupController.CloseInfoPopup.gameObject.SetActive(true);
        _soundsEffectController.PlayMissileShoSoundEffect();
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnExitButtonClicked()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
    }

    #endregion Event Handler
}