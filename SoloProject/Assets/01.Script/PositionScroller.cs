using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScroller: MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float scrollRange = 9.9f;
    [SerializeField] float moveSpeed = 2f;
    
    void Update()
    {
        if (GameManager.instance.GetGameOver()) return;
        transform.position += Vector3.down * Time.deltaTime * moveSpeed;

        if(transform.position.y <= -scrollRange)
        {
            transform.position = target.position + Vector3.up * scrollRange;
        }
    }
}
