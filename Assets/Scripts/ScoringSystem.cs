using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    private static ScoringSystem _instance;

    public static ScoringSystem Instance => _instance;
    
    
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreNumber;
    [SerializeField] private Text bestScoreText;
    
    private ScoreData _data;
    private int _score;


    public int Score => _score;
    
    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = GameObject.Find("ScoringSystem").GetComponent<ScoringSystem>();
        }
        
        LoadScorer();
        _data = new ScoreData();
    }
    
    
    public void UpdateScore(int addScore)
    {
        _score += addScore;

        if (_score == 0)
        {
            scoreNumber.text = 0.ToString();


            GameManager.Instance.GameOver();
            
            return;
        }
        
        scoreNumber.text = _score.ToString();
    }
    
    public void InitScore()
    {
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Score: ";
        scoreNumber.text = 0.ToString();
    }
    
    public void SaveData()
    {
        if (_data.score < _score)
        {
            _data.score = _score;
        }
        
        string json = JsonUtility.ToJson(_data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private void LoadScorer()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            _data = JsonUtility.FromJson<ScoreData>(json);

            if (!bestScoreText.gameObject.activeInHierarchy)
            {
                bestScoreText.gameObject.SetActive(true);
            }
            
            bestScoreText.text = "Best Score: " + _data.score;
        }
    }
    
    [Serializable]
    class ScoreData
    {
        public int score;
    }
}
