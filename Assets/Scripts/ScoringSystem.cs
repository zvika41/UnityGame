using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    private static ScoringSystem _instance;
    public static ScoringSystem Instance => _instance;

    
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

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private ScoreData _data;
    private int _score;
    public bool _isMultiplierBoost;
    public bool _shouldStartBoostTimer;
    private bool _isBoostTimerEnded;
    private float _timer;
    private int _seconds;

    #endregion Members


    #region --- Mono Methods ---

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = GameObject.Find("ScoringSystem").GetComponent<ScoringSystem>();
        }
        
        _data = new ScoreData();
        LoadScore();
        _isMultiplierBoost = false;
    }

    private void Update()
    {
        if (_shouldStartBoostTimer)
        {
            _shouldStartBoostTimer = false;
            _isMultiplierBoost = true;
            StartCoroutine(Timer());
        }
        
        if (_seconds > 10)
        {
            _isMultiplierBoost = false;
            StopCoroutine(Timer());
        }
    }

    #endregion Mono Methods


    #region --- Public Methods ---

    public void InitScore()
    {
        scoreText.gameObject.SetActive(true);
        scoreText.text = SCORE_TEXT;
        scoreNumber.text = SCORE_ON_START;
    }
    
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
    
    public void SaveData()
    {
        if (_data.score < _score)
        {
            _data.score = _score;
        }
        
        string json = JsonUtility.ToJson(_data);
        File.WriteAllText(Application.persistentDataPath + FILE_PATH, json);
    }
    
    #endregion Public Methods


    #region --- Private Methods ---
    
    private IEnumerator Timer()
    {
        while (_isMultiplierBoost)
        {
            TimeCount();
            yield return new WaitForSeconds(1);
        }
    }
    
    private void TimeCount()
    {
        _seconds += 1;
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

    #endregion Private Methods


    #region --- Internal Classes ---

    [Serializable]
    class ScoreData
    {
        public int score;
    }

    #endregion Internal Classes
}
