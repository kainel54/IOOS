using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public string nickName;

    public int age;

    public static int count=0;

    private void Awake()
    {
        count++;
    }

    public static void AnimalType()
    {
        Debug.Log("이것은 개입니다");
    }
    public void Bark()
    {
        Debug.Log(nickName + " Bark!! " + "총 마릿수" + count);

    }
    private void Start()
    {
        Bark();
    }
}
