using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class controller : MonoBehaviour
{

    public WheelCollider[] wheels = new WheelCollider[4];

    public GameObject[] wheelMeshs = new GameObject[4];

    public float torque = 200;

    public float steeringMax = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        animateWheels();
        if(Input.GetKey(KeyCode.W))
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque= torque;
            }
        }

        if(Input.GetAxis("Horizontal") != 0)
        {
            for (int i = 0; i < wheels.Length-2; i++)
            {
                wheels[i].steerAngle = Input.GetAxis("Horizontal") * steeringMax;
                   
            }
        } else
        {
            for (int i = 0; i < wheels.Length - 2; i++)
            {
                wheels[i].steerAngle = 0;

            }
        }
    }

    private void animateWheels()
    {
        Vector3 wheekPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose(out wheekPosition, out wheelRotation);
            wheelMeshs[i].transform.position = wheekPosition;
            wheelMeshs[i].transform.rotation = wheelRotation;
        }
    }
}
