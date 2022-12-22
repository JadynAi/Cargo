using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private controller cr;

    public GameObject needle;
    private float startPosition = 200f, endPosition = -49f;
    private float desiredPosition;
    private float vehicleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cr = GameObject.FindGameObjectWithTag("Player").GetComponent<controller>();
    }

    // Update is called once per frame
    void Update()
    {
        vehicleSpeed = cr.KPH;
        updateNeedle();
    }

    private void updateNeedle()
    {
        desiredPosition = startPosition-endPosition;
        float temp = vehicleSpeed / 180;
        needle.transform.eulerAngles = new Vector3(0, 0, (startPosition - temp * desiredPosition));
    }
}
