using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeEnemyGenerator : MonoBehaviour
{
    [SerializeField] Transform[] enemySpawnPosition;

    public GameObject EnemyPrefab;
    public float delayTime = 10f;
    public static LargeEnemyGenerator instance;

    
    public float DelayTime
    {
        get
        {
            return delayTime;
        }
        set
        {
            if (delayTime > 0) delayTime = value;
        }
    }
    int Position;

    private void Awake()
    {
        enemySpawnPosition = GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        if (GameManager.instance.Score >= 5000)
        {
            StopSpawn();
        }
    }
    void Start()
    {
        StartCoroutine("GenerateEnemy");
        instance = GetComponent<LargeEnemyGenerator>();
    }
    public void StopSpawn()
    {
        StopCoroutine("GenerateEnemy");
    }

    IEnumerator GenerateEnemy()
    {
        while (true)
        {
            Position = Random.Range(1, 13);
            Instantiate(EnemyPrefab, enemySpawnPosition[Position].position, enemySpawnPosition[Position].rotation);
            yield return new WaitForSeconds(delayTime);
        }
    }
}
