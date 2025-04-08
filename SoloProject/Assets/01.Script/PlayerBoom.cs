using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoom : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Vector3 targetPos = Vector3.zero;
    Animator animator;
    int damage = 100;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    { 
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.001f)
            {
                transform.position = targetPos;
                animator.SetTrigger("onBoom");
            }
    }
    public void Boom()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().BoomDamage();
        }
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if (boss != null)
        {
            // 보스의 체력을 damage만큼 감소시킨다
            boss.GetComponent<BossHp>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
