using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProgressBar : MonoBehaviour
{
    public GameObject mask;
    private Vector3 emptyBarValue;

    private void Awake()
    {
        emptyBarValue = mask.transform.localScale;
        transform.parent.GetComponent<BuildingFinishingComponent>().OnBuildStep += UpdateBar;
    }

    private void UpdateBar(float percent)
    {
        mask.transform.localScale = new Vector3(Mathf.Lerp(emptyBarValue.x, 0, percent / 100), emptyBarValue.y, emptyBarValue.z);
        if (percent >= 100)
            Destroy(this.gameObject);
    }
}
