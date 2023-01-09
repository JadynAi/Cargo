using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAdapt : MonoBehaviour
{
    private inputManager IM;
    // Start is called before the first frame update
    void Start()
    {
        IM = GameObject.FindGameObjectWithTag("Player").GetComponent<inputManager>();
    }

    public void setVertical(float value)
    {
        IM.vertical= value;
        print("setVerical" + value);
    }

    public void setHorizontal(float value)
    {
        IM.horizontal = value;
    }

    public void setHandbrake(bool value)
    {
        IM.handbrake = value;
    }

    public void setBoosting(bool value)
    {
        IM.boosting = value;
    }
}
