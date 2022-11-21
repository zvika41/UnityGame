using UnityEngine;

public class SoundsEffectController : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip missileShot;
    
    #endregion SerializeField
    
    
    #region --- Members ---

    private AudioSource _musicTheme;
    private AudioSource _explosionAudioSource;
    private AudioSource _collectAudioSource;
    private AudioSource _missileShotAudioSource;

    #endregion Members


    #region --- Public Methods ---

    public void PlayExplosionSoundEffect()
    {
        _explosionAudioSource = GetComponent<AudioSource>();
        _explosionAudioSource.PlayOneShot(explosionSound);
    }
    
    public void PlayMissileShoSoundEffect()
    {
        _missileShotAudioSource = GetComponent<AudioSource>();
        _missileShotAudioSource.PlayOneShot(missileShot);
    }
    
    public void PlayCollectSoundEffect()
    {
        _collectAudioSource = GetComponent<AudioSource>();
        _collectAudioSource.PlayOneShot(collectSound);
    }

    #endregion Public Methods
}