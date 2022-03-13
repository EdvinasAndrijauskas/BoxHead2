using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public int speed = 100;
    public float timeToDestroy = 3f;
    private void OnEnable()
    {
        StartCoroutine(AutoDestroy(timeToDestroy));
    }
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);   
    }

    IEnumerator AutoDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        
        Destroy(gameObject);
    }
}
