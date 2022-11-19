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

    #endregion Members
    

    #region --- Properties ---

    public bool ShouldRepeatBackground { get; set; }

    #endregion Properties
    

    #region --- Mono Methods ---

    private void Start()
    {
        _renderer = GetComponent<Renderer> ();
        _scrollSpeed = 0.3f;
    }

    private void Update()
    {
        if (ShouldRepeatBackground)
        {
            Repeat();
        }
    }

    #endregion Mono Methods
    

    #region --- Private Methods ---

    private void Repeat()
    {
        float y = Mathf.Repeat (Time.time * _scrollSpeed, 1);
        Vector2 offset = new Vector2 (0, -y);
        _renderer.sharedMaterial.SetTextureOffset(MAIN_TEX_NAME, offset);
    }

    #endregion Private Methods
    
}
