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

    private Rigidbody rigidBody;

    private GameObject centerOfMass;

    private WheelCollider[] wheels = new WheelCollider[4];

    private GameObject[] wheelMeshs = new GameObject[4];

    public float torque = 200;

    public float steeringMax = 5;
    public float radius = 6;
    public float downForceValue = 50;

    [HideInInspector]public float KPH;

    public float brakePower = 300;
    private GameObject wheelColliders, wheelMeshes;
    

    // Start is called before the first frame update
    void Start()
    {
        getGameObject();
    }

    private void getGameObject()
    {
        IM= GetComponent<inputManager>();
        rigidBody = GetComponent<Rigidbody>();
        centerOfMass = GameObject.Find("mass");
        rigidBody.centerOfMass = centerOfMass.transform.localPosition;
        wheelColliders = gameObject.transform.Find("wheelColliders").gameObject;
        wheelMeshes = gameObject.transform.Find("wheelMeshes").gameObject;
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i] = wheelColliders.transform.Find(i.ToString()).gameObject.GetComponent<WheelCollider>();
        }

        for (int i = 0; i < wheelMeshs.Length; i++)
        {
            wheelMeshs[i] = wheelMeshes.transform.Find(i.ToString()).gameObject;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        addDownForce();
        animateWheels();
        moveVehicle();
        steerVehicle();
    }

    private void addDownForce()
    {
        rigidBody.AddForce(downForceValue * rigidBody.velocity.magnitude * -transform.up);
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

        KPH = rigidBody.velocity.magnitude * 3.6f;
        
        if(IM.handbrake)
        {
            wheels[2].brakeTorque = wheels[3].brakeTorque = brakePower;
        }
        else
        {
            wheels[2].brakeTorque = wheels[3].brakeTorque = 0;
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
