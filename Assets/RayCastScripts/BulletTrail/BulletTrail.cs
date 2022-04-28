using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{

    private Vector3 _startPosition;
    private Vector2 _endPosition;
    private float _progress;

    [SerializeField] private float _speed = 40f;
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _progress += Time.deltaTime * _speed;
        transform.position = Vector3.Lerp(_startPosition, _endPosition, _progress);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _endPosition = targetPosition;
    }
}
