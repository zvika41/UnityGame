using UnityEngine;

public class SoundsEffectController : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] private AudioClip _explosionSound;
    
    #endregion SerializeField
    
    
    #region --- Members ---

    private AudioSource _audioSource;
    
    #endregion Members


    #region --- Public Methods ---

    public void PlayEffect()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_explosionSound);
    }

    #endregion Public Methods
}
