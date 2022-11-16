using UnityEngine;

public class MissilesController : MonoBehaviour
{
    private const float SPEED = 15f;
    
    [SerializeField] private ParticleSystem particleSystem;
    
    private ScoringSystem _scoringSystem;
    

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * SPEED);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(GlobalConstMembers.ENEMY))
        {
            Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
            ScoringSystem.Instance.UpdateScore(5);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(GlobalConstMembers.BOMB))
        {
            Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
            GameManager.Instance.GameOver();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
