using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentRotator : MonoBehaviour
{
    public float startRotationPos = 30;
    public float stopRotationPos = -60;
    public float rotationFreq = 1;
    private float timer;
    private Vector3 fromRot;
    private Vector3 toRot;

    private void SwitchDirection()
    {
        Vector3 swap = fromRot;
        fromRot = toRot;
        toRot = swap;
        timer = 0;
    }
    private void Awake()
    {
        timer = 0;
        toRot = new Vector3(0, 0, stopRotationPos);
        fromRot = new Vector3(0, 0, startRotationPos);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer>rotationFreq)
            SwitchDirection();
        transform.eulerAngles = Vector3.Lerp(fromRot, toRot, timer/rotationFreq);
    }
}
