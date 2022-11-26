using UnityEngine;

public class MissilesController : MonoBehaviour
{
    #region --- Const ---

    private const float SPEED = 18f;

    #endregion Const
    
    
    #region --- SerializeField ---

    [SerializeField] private ParticleSystem particleSystem;

    #endregion SerializeField
    
    
    #region --- Mono Methods ---

    private void Update()
    {
        MissileDirection();
        DisableMissileOutOfBounds();
    }

    #endregion Mono Methods


    #region --- Private Methods ---

    private void MissileDirection()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * SPEED);
    }

    private void DisableMissileOutOfBounds()
    {
        if (transform.position.y > 20)
        {
            gameObject.SetActive(false);
        }
    }

    private void DisableCollidedObjects(GameObject current, GameObject other)
    {
        current.SetActive(false);
        other.SetActive(false);
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
        PlayParticleEffect();
        DisableCollidedObjects(gameObject, colliderGameObject.gameObject);
        PlaySoundEffect(shouldPlayBoostSound);
    }

    #endregion Private Methods


    #region --- Event Handler ---

    private void OnCollisionEnter(Collision other)
    {
        if (!GameManager.Instance.IsGameActive || other.gameObject.CompareTag(GlobalConstMembers.PLAYER)) return;

        if (other.gameObject.CompareTag(GlobalConstMembers.ENEMY))
        {
            HandleCollision(other, false);
            GameManager.Instance.ScoringManager.UpdateScore(5);
        }
        else if (other.gameObject.CompareTag(GlobalConstMembers.BOMB))
        {
            HandleCollision(other, false);
            GameManager.Instance.HealthManager.DisableAllHealthObjects();
            GameManager.Instance.GameOver();
        }
        else if (other.gameObject.CompareTag(GlobalConstMembers.MULTIPLER_BOOST))
        {
            if (GameManager.Instance.ScoringManager.IsMultiplierBoost)
            {
                GameManager.Instance.ScoringManager.UpdateScore(5);
            }
            else
            {
                GameManager.Instance.ScoringManager.ShouldStartBoostTimer = true;
            }
            
            HandleCollision(other, true);
        }
        else if (other.gameObject.CompareTag(GlobalConstMembers.HEALTH))
        {
            if (!GameManager.Instance.HealthManager.IsHealthFull)
            {
                GameManager.Instance.HealthManager.AddLife(GameManager.Instance.HealthManager.HealthCounter);
                GameManager.Instance.HealthManager.HealthCounter++;
            }
            else
            {
                GameManager.Instance.ScoringManager.UpdateScore(5);
            }
            
            HandleCollision(other, true);
        }
    }

    #endregion Event Handler
}