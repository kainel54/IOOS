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
        Debug.Log("�̰��� ���Դϴ�");
    }
    public void Bark()
    {
        Debug.Log(nickName + " Bark!! " + "�� ������" + count);

    }
    private void Start()
    {
        Bark();
    }
}
