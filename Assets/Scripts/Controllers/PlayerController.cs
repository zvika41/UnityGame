using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region --- Const ---

    private const float SPEED = 15f;

    #endregion Const
    
    
    #region --- SerializeField ---

    [SerializeField] private GameObject missile;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private AudioSource _shootingSound;
    private int _lifeCounter;
    private bool _isCollied;

    #endregion Members
    

    #region --- Mono Methods ---
    
    private void Awake()
    {
        _shootingSound = GetComponent<AudioSource>();
        _lifeCounter = 3;
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
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * SPEED);
        }
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * SPEED);
        }
    }

    private void FireMissile()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _shootingSound.Play();
            Instantiate(missile, transform.position, missile.transform.rotation);
        }
    }

    private void HandlePlayerBounds()
    {
        if (transform.position.x < -6)
        {
            transform.position = new Vector3(-6, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 6)
        {
            transform.position = new Vector3(6, transform.position.y, transform.position.z);
        }
        
        if (transform.position.y > 6)
        {
            transform.position = new Vector3(transform.position.x, 6, transform.position.z);
        }
        
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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
        
        Destroy(colliderGameObject.gameObject);
    }

    #endregion Private Methods
    
    
    #region --- Event Handler ---

    private void OnCollisionEnter(Collision other)
    {
        if(!GameManager.Instance.IsGameActive || other.gameObject.CompareTag("Misile")) return;
        
        if (other.gameObject.CompareTag(GlobalConstMembers.ENEMY) || other.gameObject.CompareTag(GlobalConstMembers.BOMB))
        {
            HandleCollision(other, false);
            
            if (_lifeCounter == 0)
            {
                Destroy(gameObject);
                GameManager.Instance.GameOver();
                
                return;
            }
            
            GameManager.Instance.HealthManager.DisableHealthObject(_lifeCounter);
            _lifeCounter--;
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
                GameManager.Instance.HealthManager.AddLife();
                _lifeCounter++;
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