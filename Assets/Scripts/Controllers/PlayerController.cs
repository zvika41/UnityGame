using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] private GameObject playerPrefab;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private GameObject _player;

    #endregion Members
    

    #region --- Mono Methods ---
    
    private void Awake()
    {
        _player = Instantiate(playerPrefab);
        _player.SetActive(false);
    }

    private void Start()
    {
        RegisterToCallbacks();
    }

    #endregion Mono Methods


    #region --- Private Methods ---

    private void InitPlayer()
    {
        UnRegisterFromCallbacks();
        _player.SetActive(true);
    }

    #endregion Private Methods
    
    
    #region --- Event Handler ---

    private void RegisterToCallbacks()
    {
        GameManager.Instance.GameStart += InitPlayer;
    }
    
    private void UnRegisterFromCallbacks()
    {
        GameManager.Instance.GameStart -= InitPlayer;
    }

    #endregion Event Handler
}