using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    #region --- Const ---

    private const string GO_TEXT = "GO";
    
    #endregion Const
    
    
    #region --- SerializeField ---

    [SerializeField] public Text timerText;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private float _timerLeft;

    #endregion Members
    

    #region --- Properties ---
    
    public bool IsTimerOn { get; set; }
    
    #endregion Properties
    
    
    #region --- Mono Methods ---

    private void Start()
    {
        _timerLeft = 4;
    }

    private void Update()
    {
        if (!IsTimerOn) return;
        
        if (_timerLeft > 0)
        {
            _timerLeft -= Time.deltaTime;
            HandleTimerText(_timerLeft);
        }
        else
        {
            IsTimerOn = false;
        }
    }

    #endregion Mono Methods
  

    #region --- Private Methods ---

    private void HandleTimerText(float currentTime)
    {
        currentTime += 1;
        float seconds = Mathf.FloorToInt(currentTime % 60);
        seconds--;
        
        if (_timerLeft < 1)
        {
            timerText.text = GO_TEXT;
            
            return;
        }
        timerText.text = seconds.ToString(CultureInfo.InvariantCulture);
    }

    #endregion Private Methods
}