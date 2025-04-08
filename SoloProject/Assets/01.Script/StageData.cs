using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
	[SerializeField]
	private Vector2 limitMin;
	[SerializeField]
	private Vector2 limitMax;

	public Vector2 LimitMin => limitMin;
	public Vector2 LimitMax => limitMax;
}
