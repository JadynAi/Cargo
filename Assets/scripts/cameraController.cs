using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    private GameObject Player;
    private controller Controller;
    private GameObject cameraLooAt, cameraPos;
    private float speed;
    private float defaultFOV = 0, desiredFOV = 0;
    [Range(0, 50)] public float smoothTime = 8;

    // Start is called before the first frame update
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Controller = Player.GetComponent<controller>();
        cameraPos = Player.transform.Find("camera constraint").gameObject;
        cameraLooAt = Player.transform.Find("camera lookAt").gameObject;

        defaultFOV = Camera.main.fieldOfView;
        desiredFOV = defaultFOV + 15;
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        follow();
        boostFOV();
    }

    private void boostFOV()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, desiredFOV, Time.deltaTime * 5);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, defaultFOV, Time.deltaTime * 5);
        }
       
    }

    private void follow()
    {
        speed = Controller.KPH / smoothTime;
        gameObject.transform.position = Vector3.Lerp(transform.position, cameraPos.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(cameraLooAt.gameObject.transform.position);
    }
}

  
 
