using UnityEngine;

public class SoundsEffectController : MonoBehaviour
{
    #region --- SerializeField ---

    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip shootingSound;
    
    #endregion SerializeField
    
    
    #region --- Members ---

    private AudioSource _musicTheme;
    private AudioSource _explosionAudioSource;
    private AudioSource _collectAudioSource;

    #endregion Members


    #region --- Public Methods ---

    public void StopThemeMusic()
    {
        _musicTheme = GetComponent<AudioSource>();
        _musicTheme.Stop();
    }
    
    public void PlayExplosionSoundEffect()
    {
        _explosionAudioSource = GetComponent<AudioSource>();
        _explosionAudioSource.PlayOneShot(explosionSound);
    }
    
    public void PlayCollectSoundEffect()
    {
        _collectAudioSource = GetComponent<AudioSource>();
        _collectAudioSource.PlayOneShot(collectSound);
    }

    #endregion Public Methods
}