using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAdapt : MonoBehaviour
{
    private inputManager IM;
    public float deletaValue = 0.05f;
    public float verticalDeletaValue = 0.1f;
    private int rightDirection;
    private float currentVerticalTargetValue;
    // Start is called before the first frame update
    void Start()
    {
        IM = GameObject.FindGameObjectWithTag("Player").GetComponent<inputManager>();
    }

    private void Update()
    {
        if(rightDirection > 0)
        {
            // turn right
            SmoothToRight(1);
        }else if(rightDirection < 0)
        {
            // turn left
            SmoothToLeft(-1);
        }
        else
        {
            // turn middle
            SmoothToMiddle();
        }
    }

    private void SmoothToRight(float target)
    {
        float incrementalValue = Time.deltaTime * deletaValue;
        float currentHorizontal = (IM.horizontal+incrementalValue);
        IM.horizontal = currentHorizontal > target ? target : currentHorizontal;
        print("SmoothToRight incre :" + incrementalValue + " hor: " + IM.horizontal);
    }

    private void SmoothToLeft(float target)
    {
        float incrementalValue = Time.deltaTime * deletaValue;
        float currentHorizontal = (IM.horizontal - incrementalValue);
        IM.horizontal = currentHorizontal < target ? target : currentHorizontal;
        print("SmoothToLeft incre :" + incrementalValue + " hor: " + IM.horizontal);
    }

    private void SmoothToMiddle()
    {
       if(IM.horizontal < 0f)
        {
            SmoothToRight(0);
        }else if(IM.horizontal > 0f)
        {
            SmoothToLeft(0);
        }
    }

    public void SetVerticalValue(float targetValue)
    {
        if (currentVerticalTargetValue == targetValue) return;
        currentVerticalTargetValue = targetValue;
        // todo: change other function to stop special coroutine
        StopAllCoroutines();
        StartCoroutine(SetVertical(targetValue));
    }

    public IEnumerator SetVertical(float targetValue)
    {
        float vertical = IM.vertical;
        bool inIncrement = targetValue > vertical;
        print("SetVertical " + inIncrement);
        while (IM.vertical != targetValue)
        {
            float changeValue = (inIncrement ? verticalDeletaValue : -verticalDeletaValue) * Time.deltaTime;
            vertical += changeValue;
            IM.vertical = inIncrement ? Math.Min(targetValue, vertical) : Math.Max(targetValue, vertical);
            print("while vertical: " + vertical + "IM vertical: " + IM.vertical + "target:" + targetValue);
            yield return null;
        }
    }

    public void SetHorizontal(int value)
    {
        rightDirection = value;
    }

    public void SetHandbrake(bool value)
    {
        IM.handbrake = value;
    }

    public void SetBoosting(bool value)
    {
        IM.boosting = value;
        if (value && IM.vertical != 1f)
        {
            IM.vertical = 1f;
        }
    }
}
