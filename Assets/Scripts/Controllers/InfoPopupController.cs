using UnityEngine;
using UnityEngine.UI;

public class InfoPopupController : MonoBehaviour
{
    
    #region --- SerializeField ---
    
    [SerializeField] private Image infoPopup;
    [SerializeField] private Button closeInfoPopup;

    #endregion SerializeField
    
    
    #region --- Properties ---

    public Image InfoPopup => infoPopup;
    public Button CloseInfoPopup => closeInfoPopup;

    #endregion Properties
    
    
    #region --- Mono Methods ---

    private void Awake()
    {
        closeInfoPopup.gameObject.SetActive(false);
        infoPopup.gameObject.SetActive(false);
    }

    #endregion Mono Methods


    #region -- Event Handler ---

    public void OnExitButtonClicked()
    {
        infoPopup.gameObject.SetActive(false);
        closeInfoPopup.gameObject.SetActive(false);
        GameManager.Instance.StartGameText.gameObject.SetActive(true);
    }

    #endregion Event Handler
}
