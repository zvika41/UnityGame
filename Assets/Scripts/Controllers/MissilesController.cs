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

    private void PlaySoundEffect(bool shouldPlayBoostSound)
    {
        if (shouldPlayBoostSound)
        {
            GameManager.Instance.SoundsEffectController.PlayCollectSoundEffect();
        }
        else
        {
            GameManager.Instance.SoundsEffectController.PlayExplosionSoundEffect();
        }
    }

    private void HandleCollision(Collision colliderGameObject, bool shouldPlayBoostSound)
    {
        _isCollied = true;
        PlayParticleEffect();
        DestroyCollidedObjects(gameObject, colliderGameObject.gameObject);
        PlaySoundEffect(shouldPlayBoostSound);
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

        if (!(other.gameObject.transform.position.y < 11.5f)) return;
        
        if (other.gameObject.CompareTag(GlobalConstMembers.ENEMY))
        {
            HandleCollision(other, false);
            GameManager.Instance.ScoringManager.UpdateScore(5);
        }
        else if (other.gameObject.CompareTag(GlobalConstMembers.BOMB))
        {
            HandleCollision(other, false);
            GameManager.Instance.GameOver();
        }
        else if (other.gameObject.CompareTag(GlobalConstMembers.MILTIPLER_BOOST) || other.gameObject.CompareTag(GlobalConstMembers.HEALTH))
        {
            if (!GameManager.Instance.ScoringManager.IsMultiplierBoost)
            {
                GameManager.Instance.ScoringManager.ShouldStartBoostTimer = true;
            }

            HandleCollision(other, true);
        }
    }

    #endregion Event Handler
}