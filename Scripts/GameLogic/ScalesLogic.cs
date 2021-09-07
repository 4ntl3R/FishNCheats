using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScalesLogic : Singleton<ScalesLogic>
{
    
	
	public float timeStatus;
	public const float timeLimit = 360;
	
	public float alertStatus;
	public const float alertLimit = 50;
	public const float alertCooldown = 360;
	
	public float progressStatus;
	public const float progressLimit = 100;


	// Start is called before the first frame update
    void Awake()
    {
        progressStatus = 0;
		alertStatus = 0;
		timeStatus = 1;
    }

    // Update is called once per frame
    void Update()
    {
		ChangeTime(-Time.deltaTime);
		ChangeAlert(-Time.deltaTime/alertCooldown*alertLimit);
		CheckConditions();
    }
	
	public void ChangeTime(float changeValue)
	{
		timeStatus+=changeValue/timeLimit;
		if (timeStatus>1)
			timeStatus = 1;
		InfoHandler.Instance.OnTimeChange(timeStatus);
	}
	
	public void ChangeAlert(float changeValue)
	{
		alertStatus+=changeValue/alertLimit;
		if (alertStatus<0)
			alertStatus = 0;
		InfoHandler.Instance.OnAlertChange(alertStatus);
	}
	
	public void ChangeProgress(float changeValue)
	{
		progressStatus+=changeValue/progressLimit;
		if (progressStatus<0)
			progressStatus = 0;
		InfoHandler.Instance.OnProgressChange(progressStatus);
	}
	
	public void CheckConditions()
	{
		if (timeStatus <=0 || alertStatus>=1)
			YouLost();
		if (progressStatus >=1)
			YouWon();
	}
	
	private void YouWon()
	{
		/*GameObject[] heroes = GameObject.FindGameObjectsWithTag("Heroes");
		foreach(GameObject hero in heroes)
        {
			hero.transform
        }*/

		GoToScene.Instance.NextLevel(5);
	}
	
	private void YouLost()
	{
		bool endGame = false;

		if (alertStatus >= 1)
		{
			endGame = true;
			GoToScene.Instance.NextLevel(3);
		}

		if (timeStatus <= 0 && (!endGame))
		{
			endGame = true;
			GoToScene.Instance.NextLevel(4);
		}
	}
}
