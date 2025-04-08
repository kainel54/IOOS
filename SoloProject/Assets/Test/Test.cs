using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Dog jack;
    void Start()
    {
        jack.Bark();
        Dog.AnimalType();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
