using UnityEngine;

public class DestroyMissilesOutOfBounds : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 12)
        {
            Destroy(gameObject);
        }
    }
}
