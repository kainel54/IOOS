using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	[SerializeField]
	private int damage = 1;
	[SerializeField]
	private GameObject explosionPrefab;     // Æø¹ß È¿°ú

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.GetComponent<PlayerController>().Die();
			Destroy(gameObject);
		}
	}

	public void OnDie()
	{
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
