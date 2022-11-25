using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI startGameText;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button infoButton;
    [SerializeField] private GameObject infoPopup;
    [SerializeField] private GameObject player;

    #endregion SerializeField


    #region --- Members ---

    private AudioSource _musicTheme;

    #endregion Members


    #region --- Properties ---

    public TextMeshProUGUI StartGameText => startGameText;

    public Button InfoButton => infoButton;

    #endregion Properties
    

    #region --- Mono Methods ---

    private void Awake()
    {
        _musicTheme = GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        GameManager.Instance.TimeStart += TimerStarted;
        GameManager.Instance.GameStart += HandleGameStart;
        GameManager.Instance.InvokeGameOver += HandleGameOver;
        _musicTheme.Play();
    }

    #endregion Mono Methods


    #region --- Private Methods ---

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

    #endregion Private Methods

    
    #region -- Event Handler ---

    public void OnInfoButtonClicked()
    {
        StartGameText.gameObject.SetActive(false);
        infoButton.gameObject.SetActive(false);
        Instantiate(infoPopup);
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