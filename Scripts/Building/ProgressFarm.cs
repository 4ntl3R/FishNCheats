using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressFarm : MonoBehaviour
{
    public float powerPerMinute = 20;
	public float secondsBetweenIncome = 5;
	private float timer;

	void Update()
    {
        timer+=Time.deltaTime;
		if (timer >= secondsBetweenIncome)
		{
			timer = 0;
			ScalesLogic.Instance.ChangeProgress(secondsBetweenIncome*powerPerMinute/60);
		}
    }
}
