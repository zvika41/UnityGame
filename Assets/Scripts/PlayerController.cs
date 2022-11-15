using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed;
    [SerializeField] private GameObject missile;

    
    void Start()
    {
        _speed = 9f;
    }

    void FixedUpdate()
    {
        HandlePlayerMovement();
        HandlePlayerBounds();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(missile, transform.position, missile.transform.rotation);
        }
    }

    private void HandlePlayerMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
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
        
        if (missile.gameObject.transform.position.y > 12)
        {
           Destroy(missile.gameObject);
        }
    }
}
