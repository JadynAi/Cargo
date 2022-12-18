using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class controller : MonoBehaviour
{

    internal enum DriveType
    {
        frontWheelType,
        rearWheelType,
        allWheelType
    }

    [SerializeField]private DriveType driveType;

    private inputManager IM;

    public WheelCollider[] wheels = new WheelCollider[4];

    public GameObject[] wheelMeshs = new GameObject[4];

    public float torque = 200;

    public float steeringMax = 5;
    public float radius = 6;

    // Start is called before the first frame update
    void Start()
    {
        getGameObject();
    }

    private void getGameObject()
    {
        IM= GetComponent<inputManager>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        animateWheels();
        moveVehicle();
        steerVehicle();
    }

    private void moveVehicle()
    {
        if (driveType == DriveType.frontWheelType)
        {
            for (int i = 0; i < 2; i++)
            {
                wheels[i].motorTorque = IM.vertical * (torque / 2);
            }
        }
        else if (driveType == DriveType.rearWheelType)
        {
            for (int i = 2; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = IM.vertical * (torque / 2);
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = IM.vertical * (torque / 4);
            }
        }       
    }

    private void steerVehicle()
    {
        // acerman steering
        if (IM.horizontal > 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
        }else if (IM.horizontal < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * IM.horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * IM.horizontal;
        }else
        {                          
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
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
