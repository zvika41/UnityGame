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
        GameManager.Instance.GameStart += InitPlayer;
    }

    #endregion Mono Methods


    #region --- Private Methods ---

    private void InitPlayer()
    {
        GameManager.Instance.GameStart -= InitPlayer;
        _player.SetActive(true);
    }

    #endregion Private Methods
}