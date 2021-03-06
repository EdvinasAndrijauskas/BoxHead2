using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float speed;
    [SerializeField] 
    private float rotationSpeed;
    private float horizontalInput;
    private float verticalInput;
    private Animator anim;
    private Rigidbody2D _rigidbody2D;
   
    private void Start()
    {
        anim = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();
        
        transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime,Space.World);

        if (movementDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward,movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,rotationSpeed * Time.deltaTime);
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0;
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -33f, 33f), Mathf.Clamp(transform.position.y, -17f, 17f));
        }
    }
    
    private void FixedUpdate()
    {
        if (horizontalInput == 0)
        {
            anim.SetFloat("Speed", verticalInput);
        }
        else
        {
            anim.SetFloat("Speed", horizontalInput);
        }
    }
}