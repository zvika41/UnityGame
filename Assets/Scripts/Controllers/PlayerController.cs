using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region --- Const ---

    private const float SPEED = 15f;

    #endregion Const
    
    
    #region --- SerializeField ---

    [SerializeField] private GameObject missile;
    [SerializeField] private ParticleSystem particle;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private AudioSource _shootingSound;
    private int _lifeCounter;

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

    #endregion Private Methods
    
    
    #region --- Event Handler ---

    private void OnCollisionEnter(Collision other)
    {
        if(!GameManager.Instance.IsGameActive) return;
        
        if (other.gameObject.CompareTag(GlobalConstMembers.ENEMY) || other.gameObject.CompareTag(GlobalConstMembers.BOMB))
        {
            GameManager.Instance.SoundsEffectController.PlayEffect();
            Destroy(other.gameObject);
            
            if (_lifeCounter == 0)
            {
                Destroy(gameObject);
                GameManager.Instance.GameOver();
                
                return;
            }
            
            GameManager.Instance.HealthManager.DisableHealthObject(_lifeCounter);
            _lifeCounter--;
        }
        else if (other.gameObject.CompareTag(GlobalConstMembers.MILTIPLER_BOOST))
        {
            if (GameManager.Instance.ScoringManager.IsMultiplierBoost)
            {
                GameManager.Instance.ScoringManager.UpdateScore(5);
            }
            
            //GameManager.Instance.SoundsEffectController.PlayEffect();
            Destroy(other.gameObject);
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
            
            Destroy(other.gameObject);
        }
    }

    #endregion Event Handler
}