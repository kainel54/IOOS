using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI boomText;
    [SerializeField] Image[] lifeIcons;
    [SerializeField] GameObject panel;
    GameObject player;
    int score;
    private int hp;
    bool isGameOver = false;
    int highScore = 500;
    int boomCount = 3;
    [SerializeField]
    int bulletCount = 50;
    int ammo = 50;
    float reloadTime = 1;
    bool reloading;
    int bulletDamage = 30;
    int boomDamage = 100;
    int power = 1;


    [SerializeField]
    Slider slider;
    [SerializeField] Image back, main;

    public int PlayerLife
    {
        get => hp;
        set => hp = Mathf.Clamp(value, 0, 3);
    }
    public int BulletDamage
    {
        get
        {
            return bulletDamage;
        }
        set
        {
            if (bulletDamage > 0) bulletDamage = value;
        }
    }
    public int BoomDamage
    {
        get
        {
            return boomDamage;
        }
        set
        {
            if (boomDamage > 0) boomDamage = value;
        }
    }
    public int Power
    {
        get => power;
        set => power = Mathf.Clamp(value, 0, 3);
    }
    public int BoomCount { get => boomCount; set => boomCount = Mathf.Clamp(value, 0, 3); }
    public bool Reloading
    {
        get
        {
            return reloading;
        }
        set
        {
            reloading = value;
        }
    }
    public int Ammo
    {
        get
        {
            return ammo;
        }
        set
        {
            if (value >= 0) ammo = value;
        }
    }
    public int BulletCount
    {
        get
        {
            return bulletCount;
        }
        set
        {
            if (value >= 0) bulletCount = value;
        }
    }
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            if (value > 0) score = value;
        }
    }

    private void Awake()
    {
        hp = lifeIcons.Length;
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        highScore = PlayerPrefs.GetInt("High Score", 100);
        UpdateScoreText();
    }
    public void UpdateItemLife()
    {
        lifeIcons[hp - 1].enabled = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            StartCoroutine(Reload(reloadTime));
        }
        UpdateAmmoText();
        UpdateBoomText();
    }

    public void AddScore(int point)
    {
        int s = score % 1000 + point;
        score += point;
        if (s >= 1000)
        {
            //속도 늘려주기
            EnemyGenarator.instance.DelayTime -= 0.05f;
            MediumEnemyGenerator.instance.DelayTime -= 0.08f;
            LargeEnemyGenerator.instance.DelayTime -= 0.1f;
        }
        if (highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt("High Score", highScore);
        }
        UpdateScoreText();

    }
    //public int GetScore()
    //{
    //    return score;
    //}
    public void SetScore(int value)
    {
        if (value > 0) score = value;
    }
    public void UpdateScoreText()
    {
        highScoreText.text = "High Score : " + highScore;
        scoreText.text = "Score : " + score;
    }
    public void UpdateAmmoText()
    {
        ammoText.text = bulletCount + " / " + ammo;
    }
    public void UpdateBoomText()
    {
        boomText.text = "X " + boomCount;
    }
    public void DecreaseLife()
    {
        hp--;
        UpdateLifeIcon();
        if (hp > 0)
        {
            StartCoroutine(SetPlayerRevive());
        }
        else
        {
            SetGameOver();
            panel.SetActive(true);
        }
    }

    private void SetGameOver()
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            panel.SetActive(true);
            FindObjectOfType<EnemyGenarator>().StopSpawn();
            FindObjectOfType<MediumEnemyGenerator>().StopSpawn();
            FindObjectOfType<LargeEnemyGenerator>().StopSpawn();
        }
    }
    public bool GetGameOver()
    {
        return isGameOver;
    }

    public void UpdateLifeIcon()
    {
        lifeIcons[hp].enabled = false;
    }

    IEnumerator SetPlayerRevive()
    {
        yield return new WaitForSeconds(0.5f);
        player.SetActive(true);
    }

    IEnumerator Reload(float time)
    {
        reloading = true;
        main.DOFade(1, 0.2f);
        back.DOFade(1, 0.2f);
        float percent = 0, currentTime = 0;
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / time;
            slider.value = percent;
            yield return null;
        }
        bulletCount = ammo;
        main.DOFade(0, 0.5f);
        back.DOFade(0, 0.5f);
        reloading = false;
        yield return null;
    }

    public void RetryButtonClick()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
