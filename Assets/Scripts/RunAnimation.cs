using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private float moveH;
    private float moveV;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
   
    }

    void FixedUpdate()
    {
        if (moveH == 0)
        {
            anim.SetFloat("Speed", moveV);
        }
        else
        {
            anim.SetFloat("Speed", moveH);
        }
    }
}