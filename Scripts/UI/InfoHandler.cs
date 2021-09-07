using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoHandler : Singleton<InfoHandler>
{
	public GameObject maskTech;
	public GameObject maskAlert;
	public GameObject maskTime;

	private float maxScaleMaskTech;
	private float maxScaleMaskAlert;
	private float maxScaleMaskTime;

	private void Awake()
    {
	    maxScaleMaskTech = maskTech.transform.localScale.x;
	    maxScaleMaskAlert = maskAlert.transform.localScale.x;
	    maxScaleMaskTime = maskTime.transform.localScale.x;
		OnAlertChange(0);
		OnProgressChange(0);
		OnTimeChange(1);
	}

    public void OnResourcesChange(float newValue)
	{
		if (newValue<0)
			newValue = 0;
		transform.Find("Header/Panel1/Image/Text").GetComponent<UnityEngine.UI.Text>().text = Mathf.RoundToInt(newValue).ToString();
	}
	
	public void OnTimeChange(float newValue)
	{
		float changeScaleX = newValue;

		maskTime.transform.localScale = new Vector3(maxScaleMaskTime * changeScaleX,
			maskTime.transform.localScale.y, maskTime.transform.localScale.z);

		//transform.Find("Header/Panel2/Text").GetComponent<UnityEngine.UI.Text>().text = "Time: " + Mathf.RoundToInt(100*newValue).ToString()+"%";
	}
	
	public void OnAlertChange(float newValue)
	{
		float changeScaleX = 1.0f - newValue;

		maskAlert.transform.localScale = new Vector3(maxScaleMaskAlert * changeScaleX,
			maskAlert.transform.localScale.y, maskAlert.transform.localScale.z);
		//transform.Find("Header/Panel3/Text").GetComponent<UnityEngine.UI.Text>().text = "Alert: " + Mathf.RoundToInt(100*newValue).ToString()+"%";
	}
	
	public void OnProgressChange(float newValue)
	{
		float changeScaleX = 1.0f - newValue;

		maskTech.transform.localScale = new Vector3(maxScaleMaskTech * changeScaleX,
			maskTech.transform.localScale.y, maskTech.transform.localScale.z);
			

		//transform.Find("Header/Panel4/Text").GetComponent<UnityEngine.UI.Text>().text = "Progress: " + Mathf.RoundToInt(100*newValue).ToString()+"%";
	}
}
