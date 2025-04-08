using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float paddingLeft = 0.2f;
    public float paddingRight = 0.2f;
    public float paddingTop = 0.2f;
    public float paddingBottom = 0.2f;
    public Transform playerSpawnpos;
    Vector2 minPos;
    Vector2 maxPos;
    Vector2 MP;
    float dashDelay = 1.5f;

    Animator animator;
    AudioSource audioSource;
    CircleCollider2D playerCollider;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    private void OnEnable()
    {
        
        transform.position = playerSpawnpos.position;
        StartCoroutine("Fire");
        StartCoroutine("Revive");
        StartCoroutine("Dash");
    }

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject boomPrefab;
    public float delayTime=0.2f;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        minPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        minPos = new Vector2(minPos.x + paddingLeft, minPos.y + paddingBottom);
        maxPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
        maxPos = new Vector2(maxPos.x - paddingRight, maxPos.y - paddingTop);
        //bulletPrefab = Resources.Load<GameObject>("Prefab/PlayerBullet");
        //boomPrefab = Resources.Load<GameObject>("Prefab/PlayerBoom");
    }
    void Update()
    {
        Move();
        PlayerLook();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Boom();
        }
    }
    public void PlayerLook()
    {
        MP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 _direction = new Vector2(MP.x - transform.position.x, MP.y - transform.position.y);
        transform.up = _direction;
    }
    

    private void Boom()
    {
        if (GameManager.instance.BoomCount >0)
        {   
            Instantiate(boomPrefab, transform.position, Quaternion.identity);
            GameManager.instance.BoomCount--;
        }
        
    }
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, y, 0);

        transform.position += dir.normalized * speed * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, minPos.x, maxPos.x);
        pos.y = Mathf.Clamp(transform.position.y, minPos.y, maxPos.y);
        transform.position = pos;
    }

    public void Die()
    {
        playerCollider.enabled = false;
        animator.SetTrigger("onDie");
        
    }
    public void DieEvent()
    {
        GameManager.instance.DecreaseLife();
        gameObject.SetActive(false);
    }
    IEnumerator Fire()
    {
        while (true)
        {
            if (Input.GetMouseButton(0) && GameManager.instance.BulletCount!=0&&GameManager.instance.Reloading ==false)
            {
                switch (GameManager.instance.Power)
                {
                    case 1:
                        Instantiate(bulletPrefab, transform.position, transform.rotation);
                        break;
                    case 2:
                        Instantiate(bulletPrefab, transform.position + Vector3.left * 0.2f, transform.rotation);
                        Instantiate(bulletPrefab, transform.position + Vector3.right * 0.2f, transform.rotation);
                        break;
                    case 3:
                        Instantiate(bulletPrefab, transform.position + Vector3.left*0.5f, transform.rotation);
                        Instantiate(bulletPrefab, transform.position, transform.rotation);
                        Instantiate(bulletPrefab, transform.position + Vector3.right*0.5f, transform.rotation);
                        break;
                }
                GameManager.instance.BulletCount--;
                audioSource.Play();
                yield return new WaitForSeconds(0.1f);
            }
            yield return 0;
        }
    }
    IEnumerator Dash()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rigid.AddForce(MP*6,ForceMode2D.Impulse);
                yield return new WaitForSeconds(0.2f);
                rigid.velocity = Vector3.zero;
                yield return new WaitForSeconds(dashDelay);
            }
            yield return 0;
        }
    }

    IEnumerator Revive()
    {
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.3f);
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.color = new Color(1, 1, 1, 1f);
            yield return new WaitForSeconds(0.25f);
        }
        playerCollider.enabled = true;
    }
}
