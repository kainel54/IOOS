using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BossState { MoveToAppearPoint = 0,Phase01,Phase02,Phase03}

public class Boss : MonoBehaviour
{
	[SerializeField]
	private StageData stageData;
	[SerializeField]
	private GameObject explosionPrefab;
	[SerializeField]
	GameObject endingPanel;
	[SerializeField]
    private float bossApperPoint = 3f;
    float speed = 3f;
    private BossState bossState = BossState.MoveToAppearPoint;
	private MoveMent2D movement2D;
	private BossWeapon bossWeapon;
	private BossHp bossHp;

    private void Awake()
    {
		movement2D = GetComponent<MoveMent2D>();
		bossWeapon = GetComponent<BossWeapon>();
		bossHp = GetComponent<BossHp>();
	}

    public void ChageState(BossState newState)
    {



        StopCoroutine(bossState.ToString());
        bossState = newState;
        StartCoroutine(bossState.ToString());
    }
	private IEnumerator MoveToAppearPoint()
	{
		// 이동방향 설정 [코루틴 실행 시 1회 호출]
		movement2D.MoveTo(Vector3.down);

		while (true)
		{
			if (transform.position.y <= bossApperPoint)
			{
				movement2D.MoveTo(Vector3.zero);
				ChageState(BossState.Phase01);
			}

			yield return null;
		}
	}

	private IEnumerator Phase01()
	{
		bossWeapon.StartFiring(AttackType.CircleFire);

		while (true)
		{
			if (bossHp.CurrentHP <= bossHp.MaxHP * 0.7f)
			{
				bossWeapon.StopFiring(AttackType.CircleFire);
				ChageState(BossState.Phase02);
			}
			yield return null;
		}
	}

	private IEnumerator Phase02()
	{
		bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);

		Vector3 direction = Vector3.right;
		movement2D.MoveTo(direction);

		while (true)
		{
			if (transform.position.x <= stageData.LimitMin.x ||
				 transform.position.x >= stageData.LimitMax.x)
			{
				direction *= -1;
				movement2D.MoveTo(direction);
			}

			if (bossHp.CurrentHP <= bossHp.MaxHP * 0.3f)
			{
				bossWeapon.StopFiring(AttackType.SingleFireToCenterPosition);
				ChageState(BossState.Phase03);
			}
			yield return null;
		}
	}

	private IEnumerator Phase03()
	{
		
		bossWeapon.StartFiring(AttackType.CircleFire);
		
		bossWeapon.StartFiring(AttackType.SingleFireToCenterPosition);

		
		Vector3 direction = Vector3.right;
		movement2D.MoveTo(direction);

		while (true)
		{
			
			if (transform.position.x <= stageData.LimitMin.x ||
				 transform.position.x >= stageData.LimitMax.x)
			{
				direction *= -1;
				movement2D.MoveTo(direction);
			}

			yield return null;
		}
	}
	public void OnDie()
	{
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		GameManager.instance.Score += 10000;
		endingPanel.SetActive(true);
		Destroy(gameObject);
	}
}
