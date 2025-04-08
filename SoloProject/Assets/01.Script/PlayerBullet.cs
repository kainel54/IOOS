using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletspeed = 10;
    int damage = 30;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * bulletspeed);
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            // �ε��� ������Ʈ ü�� ���� (����)
            collision.GetComponent<BossHp>().TakeDamage(damage);
            // �� ������Ʈ ���� (�߻�ü)
            Destroy(gameObject);
        }
    }

}
