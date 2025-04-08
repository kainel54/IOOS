using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject[] items;
    PlayerController player;
    Vector3 direction = Vector3.down;
    [SerializeField] GameObject explosionPrefab;
    public int hp = 100;
    public int score = 100;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void SerDirection()
     {
        if (player == null) return;
        direction = player.transform.position - transform.position;
        direction = direction.normalized;
     }
    void Update()
    {
        transform.position += direction * Time.deltaTime * moveSpeed;
        if(transform.position.y <-6)
        {
            Destroy(gameObject);
        }
        SerDirection();
        transform.up = direction;
        if (hp <= 0)
        {
            EnemyDie();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            BulletDamage();
            
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Die();
        }
    }
    public void EnemyDie()
    {
        GameObject clone  = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(clone,clone.GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
        DropItem();
        GameManager.instance.AddScore(score);
    }
    void DropItem()
    {
        int rd = Random.Range(0, 100);
        if (rd < 5)
        {
            Instantiate(items[0], transform.position, Quaternion.identity);
        }
        else if (rd<10)
            Instantiate(items[1], transform.position, Quaternion.identity);
        else if (rd < 15)
        {
            Instantiate(items[2], transform.position, Quaternion.identity);
        }
        else if (rd < 20)
        {
            Instantiate(items[3], transform.position, Quaternion.identity);
        }
    }
    public void BulletDamage()
    {
        hp -= GameManager.instance.BulletDamage;
        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");
    }
    public void BoomDamage()
    {
        hp -= GameManager.instance.BoomDamage;
    }
    IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }
}
