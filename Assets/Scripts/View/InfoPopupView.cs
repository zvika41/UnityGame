using UnityEngine;
using UnityEngine.UI;

public class InfoPopupView : MonoBehaviour
{
    #region --- Const ---

    private const string GAME_VIEW_OBJECT_NAME = "GameView(Clone)";

    #endregion Const
    
    
    #region --- SerializeField ---
    
    [SerializeField] private Image infoPopup;
    [SerializeField] private Button closeInfoPopup;

    #endregion SerializeField
    
    
    #region --- Members ---

    private GameView _gameView;

    #endregion Members
    
    
    #region --- Properties ---

    public Button CloseInfoPopup => closeInfoPopup;

    #endregion Properties
    
    
    #region --- Mono Methods ---

    private void Start()
    { 
        _gameView = GameObject.Find(GAME_VIEW_OBJECT_NAME).GetComponent<GameView>();
        closeInfoPopup.gameObject.SetActive(true);
        infoPopup.gameObject.SetActive(true);
    }

    #endregion Mono Methods


    #region -- Event Handler ---

    public void OnExitButtonClicked()
    {
        infoPopup.gameObject.SetActive(false);
        closeInfoPopup.gameObject.SetActive(false);
        _gameView.StartGameText.gameObject.SetActive(true);
        _gameView.InfoButton.gameObject.SetActive(true);
        
        AssetsBundleService.UnloadBundle();
        Destroy(gameObject);
    }

    #endregion Event Handler
}
