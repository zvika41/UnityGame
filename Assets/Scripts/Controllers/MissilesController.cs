using UnityEngine;

public class MissilesController : MonoBehaviour
{
    #region --- Const ---

    private const float SPEED = 18f;

    #endregion Const
    
    
    #region --- SerializeField ---

    [SerializeField] private ParticleSystem particleSystem;

    #endregion SerializeField
    
    
    #region --- Members ---

    private bool _isCollied;

    #endregion Members


    #region --- Mono Methods ---

    private void Update()
    {
        MissileDirection();
        DestroyMissileOutOfBounds();
    }

    #endregion Mono Methods


    #region --- Private Methods ---

    private void MissileDirection()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * SPEED);
    }

    private void DestroyMissileOutOfBounds()
    {
        if (transform.position.y > 18)
        {
            Destroy(gameObject);
        }
    }

    private void DestroyCollidedObjects(GameObject current, GameObject other)
    {
        Destroy(current);
        Destroy(other.gameObject);
    }

    private void PlayParticleEffect()
    {
        Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
    }

    private void PlaySoundEffect()
    {
        GameManager.Instance.SoundsEffectController.PlayEffect();
    }

    #endregion Private Methods


    #region --- Event Handler ---

    private void OnCollisionEnter(Collision other)
    {
        if (!GameManager.Instance.IsGameActive) return;

        if (_isCollied)
        {
            _isCollied = false;
            
            return;
        }
        
        if (other.gameObject.CompareTag(GlobalConstMembers.ENEMY) && other.gameObject.transform.position.y < 12)
        {
            _isCollied = true;
            PlayParticleEffect();
            GameManager.Instance.ScoringManager.UpdateScore(5);
            DestroyCollidedObjects(gameObject, other.gameObject);
            PlaySoundEffect();
        }
        else if (other.gameObject.CompareTag(GlobalConstMembers.BOMB) && other.gameObject.transform.position.y < 12)
        {
            _isCollied = true;
            PlayParticleEffect();
            DestroyCollidedObjects(gameObject, other.gameObject);
            PlaySoundEffect();
            GameManager.Instance.GameOver();
        }
        else if (other.gameObject.CompareTag(GlobalConstMembers.MILTIPLER_BOOST) && other.gameObject.transform.position.y < 12)
        {
            if (!GameManager.Instance.ScoringManager.IsMultiplierBoost)
            {
                GameManager.Instance.ScoringManager.ShouldStartBoostTimer = true;
            }

            PlayParticleEffect();
            DestroyCollidedObjects(gameObject, other.gameObject);
            PlaySoundEffect();
        }
        else if (other.gameObject.CompareTag(GlobalConstMembers.HEALTH) && other.gameObject.transform.position.y < 12)
        {
            PlayParticleEffect();
            DestroyCollidedObjects(gameObject, other.gameObject);
            PlaySoundEffect();
        }
    }

    #endregion Event Handler
}