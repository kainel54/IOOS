using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFuntionTest : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake");
    }
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }
    private void Start()
    {
        Debug.Log("Start");
    }
    private void Update()
    {
        Debug.Log("Update");
    }
    private void LateUpdate()
    {
        Debug.Log("LateUpdate");
    }


    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
        Debug.Log("FIxedUpdate:" + Time.deltaTime);
    }
    private void OnDisable()
    {
        Debug.Log("FixedUpdate");
        Debug.Log("FIxedUpdate:" + Time.deltaTime);
    }
}
    
