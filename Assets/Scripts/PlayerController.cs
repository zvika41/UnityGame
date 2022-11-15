using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    
    void Start()
    {
        speed = 9f;
    }

    void FixedUpdate()
    {
        HandlePlayerMovement();
        HandlePlayerBounds();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Shooting");
        }
    }

    private void HandlePlayerMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
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
}
