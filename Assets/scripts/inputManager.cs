using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
    public float vertical;
    public float horizontal;
    public bool handbrake;
    public bool boosting;

    public bool shiftUp;
    public bool shiftDown;
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        /*vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        print("vertical" + vertical + "horiztal" + horizontal);
        handbrake = (Input.GetAxis("Jump") != 0) ? true : false;
        if (Input.GetKey(KeyCode.LeftShift)) boosting = true; else boosting = false;
        shiftUp = Input.GetKeyDown(KeyCode.E);
        shiftDown= Input.GetKeyDown(KeyCode.Q);*/
    }
}
