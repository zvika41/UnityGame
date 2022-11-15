using UnityEngine;

public class MissilesController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private GameObject missile;
    
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Good"))
        {
            Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
            GameManager.Instance.UpdateScore(5);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Bad"))
        {
            Instantiate(particleSystem, transform.position, particleSystem.transform.rotation);
            GameManager.Instance.GameOver();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
