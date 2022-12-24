using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class controller : MonoBehaviour
{

    internal enum DriveType
    {
        frontWheelType,
        rearWheelType,
        allWheelType
    }

    [SerializeField] private DriveType driveType;

    internal enum gearBox
    {
        Auto,
        Maunal
    }
    [SerializeField] private gearBox gearType;


    private inputManager IM;

    private GameManager GM;

    private Rigidbody rigidBody;

    private GameObject centerOfMass;

    private WheelCollider[] wheels = new WheelCollider[4];

    private GameObject[] wheelMeshs = new GameObject[4];


    // 在面板给字段加描述
    [Header("Variables")]
    public float totalPower;
    public AnimationCurve enginePower;
    public float[] gears;

    //车轮转速
    private float wheelsRPM;

    public float steeringMax = 5;
    public float radius = 6;
    public float downForceValue = 50;

    [HideInInspector] public float KPH;
    //发动机转速
    [HideInInspector] public float engineRPM;
    [HideInInspector] public int gearNum;

    public float brakePower = 300;
    public float thrust = 1000f;
    public float smoothTime = 0.01f;


    private GameObject wheelColliders, wheelMeshes;
    

    // Start is called before the first frame update
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "SelectVehicle")
        {
            return;
        }
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
        GM = GameObject.Find("GameManager").gameObject.transform.GetComponent<GameManager>();
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
        if (SceneManager.GetActiveScene().name=="SelectVehicle")
        {
            return;
        }
        addDownForce();
        animateWheels();
        steerVehicle();
        calculateEnginePower();
        shifter();
    }

    
    private void calculateEnginePower()
    {
        calculateWheelRPM();

        totalPower = enginePower.Evaluate(engineRPM) * 3.6f * IM.vertical;
        float velocity = 0.0f;
        engineRPM = Mathf.SmoothDamp(engineRPM, 1000 + (Mathf.Abs(wheelsRPM) * 3.6f * (gears[gearNum])), ref velocity, smoothTime);

        moveVehicle();
    }

    private void calculateWheelRPM()
    {
        float sum = 0;
        int R = 0;
        for (int i = 0; i < wheels.Length; i++)
        {
            sum += wheels[i].rpm;
            R++;
        }
        wheelsRPM = (R != 0) ? sum / R : 0;
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
                wheels[i].motorTorque = totalPower / 2;
            }
        }
        else if (driveType == DriveType.rearWheelType)
        {
            for (int i = 2; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = totalPower / 2;
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].motorTorque = totalPower / 4;
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

        if(IM.boosting)
        {
            rigidBody.AddForce(Vector3.forward * thrust);
        }
    }

    private void shifter()
    {
      if(IM.shiftUp)
        {
            gearNum++;
            GM.updateGear(gearNum);
        }
      if(IM.shiftDown)
        {
            gearNum--;
            GM.updateGear(gearNum);
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
