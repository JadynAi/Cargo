using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeManage : MonoBehaviour
{
    private GameObject displayCabinet;
    public float rotateSpeed = 10f;
    public vehicleList listOfVehicles;

    public int vehiclePointer = 0;
    // Start is called before the first frame update
    void Awake()
    {
        displayCabinet = GameObject.Find("environment");
        vehiclePointer = PlayerPrefs.GetInt("pointer");
        listOfVehicles = GameObject.Find("vehicleList").GetComponent<vehicleList>();

        GameObject childObject = Instantiate(listOfVehicles.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
        childObject.transform.parent = displayCabinet.transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        displayCabinet.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    public void rightButtonInvoke()
    {
        Debug.Log("rightBtnClick");
        if (vehiclePointer < listOfVehicles.vehicles.Length - 1)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            vehiclePointer++;
            PlayerPrefs.SetInt("pointer", vehiclePointer);
            GameObject childObject = Instantiate(listOfVehicles.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
            childObject.transform.parent = displayCabinet.transform;
        }
    }

    public void leftButtonInvoke()
    {
        if (vehiclePointer > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            vehiclePointer--;
            PlayerPrefs.SetInt("pointer", vehiclePointer);
            GameObject childObject = Instantiate(listOfVehicles.vehicles[vehiclePointer], Vector3.zero, Quaternion.identity) as GameObject;
            childObject.transform.parent = displayCabinet.transform;
        }
    }
}
