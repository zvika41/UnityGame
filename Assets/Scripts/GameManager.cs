using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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


    [SerializeField] private List<GameObject> targets;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreNumber;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI startGameText;
    [SerializeField] private Button button;
    [SerializeField] private Button exitButton;
    [SerializeField] private Text bestScoreText;
    
    public bool isGameActive;
    public int score;

    private PlayerController _playerController;
    private ScoreData _data;
    private float _spawnRate;
    

    void Awake()
    {
        if (_instance == null)
        {
            _instance = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        
        _data = new ScoreData();
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _playerController.gameObject.SetActive(false);
        LoadScorer();
    }

    void Start()
    {
        _spawnRate = 1;
    }
    

    public void StartGame(int difficulty)
    {
        startGameText.gameObject.SetActive(false);
        _playerController.gameObject.SetActive(true);
        isGameActive = true;
        _spawnRate /= difficulty;
        InitScore();
        StartCoroutine(SpawnTarget());
       
    }
    
    private IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;

        if (score == 0)
        {
            scoreNumber.text = 0.ToString();


            GameOver();
            
            return;
        }
        
        scoreNumber.text = score.ToString();
    }

    public void GameOver()
    {
        if (_playerController.gameObject.activeInHierarchy)
        {
            _playerController.gameObject.SetActive(false);
        }
        isGameActive = false;
        StopAllCoroutines();
        gameOverText.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        
        SaveData();
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

    private void InitScore()
    {
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Score: ";
        scoreNumber.text = 0.ToString();
    }
    
    private void SaveData()
    {
        if (_data.score < score)
        {
            _data.score = score;
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
