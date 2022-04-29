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
   
    private void Start()
    {
        anim = GetComponent<Animator>();
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