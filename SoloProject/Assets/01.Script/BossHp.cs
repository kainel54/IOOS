using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHp : MonoBehaviour
{
    [SerializeField]
    private float            maxHP = 1000;      // �ִ� ü��
    private float            currentHP;         // ���� ü��
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
        // ���� ü���� damage��ŭ ����
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
