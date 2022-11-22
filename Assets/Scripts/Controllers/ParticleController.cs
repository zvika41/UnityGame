using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    #region --- Const ---

    private const float SPEED = 15f;

    #endregion Const
    
    
    #region --- SerializeField ---

    [SerializeField] private ParticleSystem particle;

    #endregion SerializeField
    

    #region --- Mono Methods ---

    private void Start()
    {
        //Instantiate(particle, transform.position, particle.transform.rotation);
    }

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
            transform.Translate(Vector3.right * Time.deltaTime * SPEED);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * SPEED);
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * SPEED);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * SPEED);
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
}