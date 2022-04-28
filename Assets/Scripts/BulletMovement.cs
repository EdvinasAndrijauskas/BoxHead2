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
    private void Update()
    {
        transform.Translate(Vector3.up * (Time.deltaTime * speed));   
    }

    private IEnumerator AutoDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
