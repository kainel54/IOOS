using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum ItemType
{
    Power,
    Boom,
    Life,
    Ammo
}

public class Item : MonoBehaviour
{
    [SerializeField] ItemType itemType;
    [SerializeField] float deleteTime=3f;
    private SpriteRenderer spriteRenderer;
    float current = 0;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        current += Time.deltaTime;
        if(current>= deleteTime)
        {
            spriteRenderer.DOFade(0, 1f).OnComplete(()=>
            {
                Destroy(this.gameObject);
            });

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UseItem();
            Destroy(gameObject);
        }
    }

    private void UseItem()
    {
        switch (itemType)
        {
            case ItemType.Power:
                GameManager.instance.Power++;
                break;
            case ItemType.Life:
                GameManager.instance.PlayerLife++;
                GameManager.instance.UpdateItemLife();
                break;
            case ItemType.Boom:
                GameManager.instance.BoomCount++;
                GameManager.instance.UpdateBoomText();
                break;
            case ItemType.Ammo:
                GameManager.instance.Ammo += 10;
                break;
        }
    }
}