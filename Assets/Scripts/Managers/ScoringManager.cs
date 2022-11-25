using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoringManager : MonoBehaviour
{
    #region --- Const ---
    
    private const string FILE_PATH = "/savefile.json";
    private const string SCORE_TEXT = "Score: ";
    private const string BEST_SCORE_TEXT = "Best Score: ";
    private const string SCORE_ON_START = "0";

    #endregion Const
    
    
    #region --- SerializeField ---

    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreNumber;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text boostModeIndicator;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private ScoreData _data;
    private int _score;
    private bool _isMultiplierBoost;
    private bool _shouldStartBoostTimer;
    private bool _isBoostTimerEnded;
    private float _timer;
    private float _seconds;

    #endregion Members


    #region --- Properties ---

    public bool IsMultiplierBoost
    {
        get => _isMultiplierBoost;
        set => _isMultiplierBoost = value;
    }
    
    public bool ShouldStartBoostTimer
    {
        set => _shouldStartBoostTimer = value;
    }

    #endregion Properties


    #region --- Mono Methods ---

    private void Awake()
    {
        _data = new ScoreData();
        LoadScore();
        GameManager.Instance.TimeStart += HandleTimeStart;
        GameManager.Instance.GameStart += InitScore;
        GameManager.Instance.InvokeGameOver += HandleGameOver;
    }

    private void Update()
    {
        if (_shouldStartBoostTimer)
        {
            _seconds = 10f;
            _shouldStartBoostTimer = false;
            _isMultiplierBoost = true;
            boostModeIndicator.gameObject.SetActive(true);
            StartCoroutine(FlickerBoostText());
        }

        if (_seconds > 0 && _isMultiplierBoost)
        {
            _seconds -= Time.deltaTime;
        }
        else
        {
            _isMultiplierBoost = false;
            boostModeIndicator.gameObject.SetActive(false);
            StopCoroutine(FlickerBoostText());
        }
    }

    #endregion Mono Methods


    #region --- Public Methods ---
    
    public void UpdateScore(int addScore)
    {
        if (_isMultiplierBoost)
        {
            _score += addScore * 2;
        }
        else
        {
            _score += addScore;
        }
       
        scoreNumber.text = _score.ToString();
    }
    
    #endregion Public Methods


    #region --- Private Methods ---

    private void HandleTimeStart()
    {
        bestScoreText.gameObject.SetActive(false);
    }
    
    private void InitScore()
    {
        GameManager.Instance.GameStart -= InitScore;
        
        
        scoreText.gameObject.SetActive(true);
        scoreText.text = SCORE_TEXT;
        scoreNumber.text = SCORE_ON_START;
    }

    private void SaveData()
    {
        if (_data.score < _score)
        {
            _data.score = _score;
        }
        
        string json = JsonUtility.ToJson(_data);
        File.WriteAllText(Application.persistentDataPath + FILE_PATH, json);
    }

    private IEnumerator FlickerBoostText()
    {
        while (_isMultiplierBoost)
        {
            boostModeIndicator.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            boostModeIndicator.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
        }
    }

    private void LoadScore()
    {
        string path = Application.persistentDataPath + FILE_PATH;

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            _data = JsonUtility.FromJson<ScoreData>(json);

            if (!bestScoreText.gameObject.activeInHierarchy && _data.score > 0)
            {
                bestScoreText.gameObject.SetActive(true);
            }
            
            bestScoreText.text = BEST_SCORE_TEXT + _data.score;
        }
    }

    private void HandleGameOver()
    {
        GameManager.Instance.InvokeGameOver -= HandleGameOver;

        _isMultiplierBoost = false;
        SaveData();
    }

    #endregion Private Methods


    #region --- Internal Classes ---

    [Serializable]
    class ScoreData
    {
        public int score;
    }

    #endregion Internal Classes
}
