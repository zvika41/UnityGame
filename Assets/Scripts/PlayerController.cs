using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region --- Const ---

    private const float Speed = 15f;

    #endregion Const
    
    
    #region --- SerializeField ---

    [SerializeField] private GameObject missile;

    #endregion SerializeField
    

    #region --- Mono Methods ---

    private void Update()
    {
        HandlePlayerMovement();
        HandlePlayerBounds();
    }
    
    #endregion Mono Methods


    #region --- Private Methods ---

    private void HandlePlayerMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * Speed);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
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
    }

    #endregion Private Methods
}
