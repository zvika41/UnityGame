using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    #region --- Const ---

    private const string INFO_POPUP_ASSET_NAME = "infopopup";

    #endregion
    
    
    #region --- SerializeField ---

    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI startGameText;
    [SerializeField] private List<Button> difficultyButtons;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button infoButton;
    [SerializeField] private GameObject player;

    #endregion SerializeField


    #region --- Members ---

    private AssetsBundleService _assetsBundleService;
    private AudioSource _musicTheme;

    private bool _isAssetDownloaded;

    #endregion Members


    #region --- Properties ---

    public TextMeshProUGUI StartGameText => startGameText;

    public Button InfoButton => infoButton;

    #endregion Properties
    

    #region --- Mono Methods ---

    private void Awake()
    {
        _assetsBundleService = gameObject.AddComponent<AssetsBundleService>();
        _musicTheme = GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        RegisterToCallbacks();
        HandleDifficultyLevelSelected();
        PlayGameMusic();
    }

    #endregion Mono Methods


    #region --- Private Methods ---

    private void HandleDifficultyLevelSelected()
    {
        for(int i = 0; i < difficultyButtons.Count; ++i)
        {
            int capturedButtonIndex = i;
            difficultyButtons[capturedButtonIndex].onClick.AddListener(() => { OnDifficultyButtonClicked(capturedButtonIndex); });
        }
    }

    private void TimerStarted()
    {
        GameManager.Instance.TimeStart -= TimerStarted;
        infoButton.gameObject.SetActive(false);
        startGameText.gameObject.SetActive(false);
    }
    
    private void HandleGameStart()
    {
        GameManager.Instance.GameStart -= HandleGameStart;
        Instantiate(player);
    }

    private void HandleGameOver()
    {
        GameManager.Instance.InvokeGameOver -= HandleGameOver;
        gameOverText.gameObject.SetActive(true);
        tryAgainButton.gameObject.SetActive(true);
        infoButton.gameObject.SetActive(false);
        _musicTheme.Stop();
        
#if UNITY_EDITOR
        exitButton.gameObject.SetActive(true);
#endif
    }

    private void PlayGameMusic()
    {
        _musicTheme.Play();
    }

    #endregion Private Methods

    
    #region -- Event Handler ---
    
    private void RegisterToCallbacks()
    {
        GameManager.Instance.TimeStart += TimerStarted;
        GameManager.Instance.GameStart += HandleGameStart;
        GameManager.Instance.InvokeGameOver += HandleGameOver;
    }
    
    private void OnDifficultyButtonClicked(int level)
    {
        GameManager.Instance.StartTimer(level + 1);
    }

    public void OnInfoButtonClicked()
    {
        StartGameText.gameObject.SetActive(false);
        infoButton.gameObject.SetActive(false);

        if (_isAssetDownloaded)
        {
            AssetsBundleService.LoadAssetsBundleFromServer(INFO_POPUP_ASSET_NAME);
        }
        else
        {
            _isAssetDownloaded = true;
            _assetsBundleService.StartDownloadAsset(INFO_POPUP_ASSET_NAME);
        }
        
        GameManager.Instance.SoundsEffectController.PlayMissileShoSoundEffect();
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