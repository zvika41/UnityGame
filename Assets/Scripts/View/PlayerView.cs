using UnityEngine;

public class PlayerView : MonoBehaviour
{
    #region --- Const ---

    private const float SPEED = 15f;
    private const string MISSILE_OBJECT_NAME = "Missile";

    #endregion Const
    
    
    #region --- Members ---
    
    private AudioSource _shootingSound;

    #endregion Members
    
    
    #region --- Mono Methods ---
    
    private void Awake()
    {
        _shootingSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HandlePlayerMovement();
        FireMissile();
        HandlePlayerBounds();
    }
    
    #endregion Mono Methods
    
    
    #region --- Private Methods ---

    private void HandlePlayerMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * SPEED);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * SPEED);
        }
    }

    private void FireMissile()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _shootingSound.Play();
            GameManager.Instance.ObjectPoolerController.SpawnFromPool(MISSILE_OBJECT_NAME, transform.position);
        }
    }

    private void HandlePlayerBounds()
    {
        if (transform.position.x < -9)
        {
            transform.position = new Vector3(-9, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 9)
        {
            transform.position = new Vector3(9, transform.position.y, transform.position.z);
        }
    }
    
    private void HandleCollision(Collision colliderGameObject, bool shouldPlayBoostSound)
    {
        if (shouldPlayBoostSound)
        {
            GameManager.Instance.SoundsEffectController.PlayCollectSoundEffect();
        }
        else
        {
            GameManager.Instance.SoundsEffectController.PlayExplosionSoundEffect();
        }
        
        colliderGameObject.gameObject.SetActive(false);
    }

    #endregion Private Methods
    
    
    #region --- Event Handler ---

    private void OnCollisionEnter(Collision other)
    {
        if(!GameManager.Instance.IsGameActive || other.gameObject.CompareTag(MISSILE_OBJECT_NAME)) return;

        if (other.gameObject.CompareTag(GlobalConstMembers.ENEMY) || other.gameObject.CompareTag(GlobalConstMembers.BOMB))
        {
            HandleCollision(other, false);

            GameManager.Instance.HealthManager.HealthCounter--;
            GameManager.Instance.HealthManager.DisableHealthObject(GameManager.Instance.HealthManager.HealthCounter);

            if (GameManager.Instance.HealthManager.HealthCounter != 0) return;
            
            Destroy(gameObject);
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
