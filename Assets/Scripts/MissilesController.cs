using UnityEngine;

public class MissilesController : MonoBehaviour
{
    #region --- Const ---

    private const float SPEED = 15f;

    #endregion Const
    
    
    #region --- SerializeField ---

    [SerializeField] private ParticleSystem particleSystem;

    #endregion SerializeField
    
    
    #region --- Members ---
    
    private ScoringSystem _scoringSystem;

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
        if (transform.position.y > 12)
        {
            Destroy(gameObject);
        }
    }

    #endregion Private Methods


    #region --- Event Handler ---

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(GlobalConstMembers.ENEMY) && other.gameObject.transform.position.y < 12)
        {
            Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
            ScoringSystem.Instance.UpdateScore(5);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(GlobalConstMembers.BOMB) && other.gameObject.transform.position.y < 12)
        {
            Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
            GameManager.Instance.GameOver();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    #endregion Event Handler
}
