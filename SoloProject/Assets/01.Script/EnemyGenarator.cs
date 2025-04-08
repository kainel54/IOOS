using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenarator : MonoBehaviour
{
    [SerializeField] Transform[] enemySpawnPosition;

    public GameObject EnemyPrefab;
    [SerializeField] GameObject textBossWarning;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossHpPenal;
    float delayTime = 1.2f;
    public static EnemyGenarator instance;

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
    void Start()
    {
        StartCoroutine("GenerateEnemy");
        StartCoroutine("BossWarning");
        instance = GetComponent<EnemyGenarator>();
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
            if (GameManager.instance.Score >= 5000)
            {
                StartCoroutine("BossSpawn");
                break;
            }
        }
    }
    IEnumerator BossSpawn()
    {
        textBossWarning.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        textBossWarning.SetActive(false);
        boss.SetActive(true);
        bossHpPenal.SetActive(true);
        boss.GetComponent<Boss>().ChageState(BossState.MoveToAppearPoint);
    }
}
