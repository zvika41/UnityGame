using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    #region --- Const ---

    private const string MAIN_TEX_NAME = "_MainTex";

    #endregion Const
    

    #region --- Members ---
    
    private Material _material;
    private float _offset;
    private float _scrollSpeed;
    private bool _shouldRepeatBackground;

    #endregion Members


    #region --- Mono Methods ---

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _scrollSpeed = 6f;
        
        RegisterToCallbacks();
    }

    private void Update()
    {
        if (_shouldRepeatBackground)
        {
            Repeat();
        }
    }

    #endregion Mono Methods
    

    #region --- Private Methods ---

    private void InitRepeat()
    {
        _shouldRepeatBackground = true;
    }

    private void Repeat()
    {
        _offset += (Time.deltaTime * _scrollSpeed) / 10f;
        _material.SetTextureOffset(MAIN_TEX_NAME, new Vector2(0, -_offset));
    }
    
    private void StopRepeat()
    {
       UnRegisterToCallbacks();
        _shouldRepeatBackground = false;
    }

    #endregion Private Methods
    
    
    #region --- Event Handler ---

    private void RegisterToCallbacks()
    {
        GameManager.Instance.GameStart += InitRepeat;
        GameManager.Instance.InvokeGameOver += StopRepeat;
    }
    
    private void UnRegisterToCallbacks()
    {
        GameManager.Instance.GameStart -= Repeat;
        GameManager.Instance.InvokeGameOver -= StopRepeat;
    }
    
    #endregion Event Handler
}
