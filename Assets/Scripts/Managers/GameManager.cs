using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region --- Singleton ---

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    #endregion Singleton
   
    
    #region --- Const ---

    private const string GAME_MANAGER_OBJECT_NAME = "GameManager";

    #endregion Const
    
    
    #region --- SerializeField ---

    [SerializeField] private GameObject gameView;
    [SerializeField] private ObjectPoolerController objectPoolView;
    [SerializeField] private ScoringManager scoringView;
    [SerializeField] private HealthManager healthView;
    [SerializeField] private SoundsEffectController soundsEffect;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private bool _isGameActive;
    private bool _isTimerStarted;
    private int _gameDifficulty;

    #endregion Members
    
    
    #region --- Controllers/Managers ---

    private SoundsEffectController _soundsEffectController;
    private ObjectPoolerController _objectPoolerController;
    
    private ScoringManager _scoringManager;
    private HealthManager _healthManager;
    
    #endregion Controllers/Managers


    #region --- Properties ---

    public bool IsGameActive => _isGameActive;
    public int GameDifficulty => _gameDifficulty;
    public Action TimeStart { get; set; }
    public Action GameStart { get; set; }
    public Action InvokeGameOver { get; set; }

    #region ___Controllers___

    public SoundsEffectController SoundsEffectController => _soundsEffectController;
    public ObjectPoolerController ObjectPoolerController => _objectPoolerController;

    #endregion Controllers

    #region ___ Managers ___

    public ScoringManager ScoringManager => _scoringManager;
    public HealthManager HealthManager => _healthManager;

    #endregion Managers
    
    #endregion Properties
    

    #region --- Mono Methods ---

    private void Awake()
    {
        Init();
        SetupGameView();
    }

    #endregion Mono Methods


    #region --- Private Methods ---

    private void Init()
    {
        if (_instance != null)
        {
            Destroy(_instance);
        }
        
        _instance = GameObject.Find(GAME_MANAGER_OBJECT_NAME).GetComponent<GameManager>();

        _soundsEffectController = Instantiate(soundsEffect);
        _objectPoolerController = Instantiate(objectPoolView);
        _scoringManager = Instantiate(scoringView);
        _healthManager = Instantiate(healthView);
    }

    private void SetupGameView()
    {
        Instantiate(gameView);
    }

    #endregion Private Methods
    

    #region --- Public Methods ---

    public void StartTimer(int difficulty)
    {
        _gameDifficulty = difficulty;
        TimeStart?.Invoke();
    }
    
    public void StartGame()
    {
        _isGameActive = true;
        GameStart?.Invoke();
    }
    
    public void GameOver()
    {
        _isGameActive = false;
        InvokeGameOver?.Invoke();
    }

    #endregion Public Methods
}

