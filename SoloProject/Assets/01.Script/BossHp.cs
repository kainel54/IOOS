using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHp : MonoBehaviour
{
    [SerializeField]
    private float            maxHP = 1000;      // 최대 체력
    private float            currentHP;         // 현재 체력
    private SpriteRenderer   spriteRenderer;
    private Boss             boss;

    public  float MaxHP => maxHP;
    public  float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP;             
        spriteRenderer = GetComponent<SpriteRenderer>();
        boss = GetComponent<Boss>();
    }

    public void TakeDamage(float damage)
    {
        // 현재 체력을 damage만큼 감소
        currentHP -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if ( currentHP <= 0 )
        {
            boss.OnDie();
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }
}
