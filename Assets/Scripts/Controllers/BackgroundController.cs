using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    #region --- Const ---

    private const string MAIN_TEX_NAME = "_MainTex";

    #endregion Const
    

    #region --- Members ---
    
    private Renderer _renderer;
    private Vector2 _savedOffset;
    private float _scrollSpeed;
    private bool _shouldRepeatBackground;

    #endregion Members


    #region --- Mono Methods ---

    private void Start()
    {
        GameManager.Instance.GameStart += Repeat;
        GameManager.Instance.InvokeGameOver += StopRepeat;
        _renderer = GetComponent<Renderer> ();
        _scrollSpeed = 0.3f;
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

    private void Repeat()
    {
        GameManager.Instance.GameStart -= Repeat;
        _shouldRepeatBackground = true;
        float y = Mathf.Repeat (Time.time * _scrollSpeed, 1);
        Vector2 offset = new Vector2 (0, -y);
        _renderer.sharedMaterial.SetTextureOffset(MAIN_TEX_NAME, offset);
    }
    
    private void StopRepeat()
    {
        GameManager.Instance.InvokeGameOver -= StopRepeat;
        _shouldRepeatBackground = false;
    }

    #endregion Private Methods
}
