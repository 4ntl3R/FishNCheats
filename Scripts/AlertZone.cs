using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AlertZone : MonoBehaviour
{
	[FormerlySerializedAs("cautioness")] public float attentiveness = 5;
	public int interest = 12;
	public float xSize = 5;
	public float ySize = 20;
	public Color defaultColor;
	public Color alertColor;
	public float timeToPrepare = 1;
	public List<AlertState> objectsInView;
	private SpriteRenderer sprite;

	void Awake()
    {
        objectsInView = new List<AlertState>();
		transform.localScale = new Vector3(xSize, ySize, 0);
		sprite = gameObject.GetComponent<SpriteRenderer>();
		sprite.color = defaultColor;
		InvokeRepeating(nameof(StartPreparing), attentiveness-1, attentiveness);
    }


    private void SetObjectsInView()
	{
		objectsInView.Clear();
		foreach(var o in FindObjectsOfType(typeof(AlertState)))
		{
			var currObj = (AlertState) o;
			if ((Mathf.Abs(currObj.transform.position.x-transform.position.x) < xSize/2) && (Mathf.Abs(currObj.transform.position.y-transform.position.y) < ySize/2))
				objectsInView.Add(currObj);
		}
	}

    private void StartPreparing()
    {
	    StartCoroutine(ColorChanger());
	    Invoke(nameof(AlertTrigger), timeToPrepare);
    }

    private void AlertTrigger()
	{
		SetObjectsInView();
		interest--;
		IncreaseAlert();
		if (interest == 0)
			GoAway();
		StartCoroutine(ColorChanger());
	}

	IEnumerator ColorChanger()
	{
		float fadeTimer = 0;
		float fader = 0;
		
		bool isIncreasing = !sprite.color.Equals(alertColor);
		Color fromColor = defaultColor;
		Color toColor = alertColor;
		if (!isIncreasing)
		{
			toColor = defaultColor;
			fromColor = alertColor;
		}

		while (true)
		{
			fadeTimer += Time.deltaTime;
			fader = fadeTimer / timeToPrepare;
			if (fader >= 1)
			{
				sprite.color = toColor;
				StopAllCoroutines();
			}
			else
			{
				sprite.color = Color.Lerp(fromColor, toColor, fader);
			}
			yield return null;
		}
	}
	
	private void GoAway()
	{
		CancelInvoke();
		Destroy(transform.parent.gameObject);
	}
	
	private void IncreaseAlert()
	{
		foreach (AlertState objectInView in objectsInView)
		{
			objectInView.AlertOn();
		}
	}
}
