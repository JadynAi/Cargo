using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LongPressButton : Button
{
    [DllImport("user32.dll", EntryPoint = "keybd_event")]

    public static extern void Keybd_event(

       byte bvk,//�����ֵ ESC����Ӧ����27

       byte bScan,//0

       int dwFlags,//0Ϊ���£�1��ס��2�ͷ�

       int dwExtraInfo//0

       );
    // ����
    public UnityEvent my_onLongPress;
  

    public UnityEvent my_onEventUp;
    
    private bool my_isStartPress = false;
    private float my_curPointDownTime = 0f;
    private float my_longPressTime = 0.6f;
    private bool my_longPressTrigger = false;

    [SerializeField] public byte keyCode;


    // Update is called once per frame
    void Update()
    {
        CheckIsLongPress();
    }

    private void CheckIsLongPress()
    {
        if (my_isStartPress && !my_longPressTrigger)
        {
            if (Time.time > my_curPointDownTime + my_longPressTime)
            {
                my_longPressTrigger = true;
                my_isStartPress = false;
                //Keybd_event(keyCode, 0, 1, 0);
    
            }
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        // ����ˢ�®�ǰ�r�g
        base.OnPointerDown(eventData);
        print("on pointer down");
        if (my_onLongPress != null)
        {
            my_onLongPress.Invoke();
        }
        my_curPointDownTime = Time.time;
        my_isStartPress = true;
        my_longPressTrigger = false;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        // ָᘔE�𣬽Y���_ʼ�L��
        base.OnPointerUp(eventData);
        my_isStartPress = false;
       // Keybd_event(keyCode, 0, 2, 0);
        if (my_onEventUp != null)
        {
            my_onEventUp.Invoke();
        }

    }



    public override void OnPointerExit(PointerEventData eventData)
    {
        // ָ��Ƴ����Y���_ʼ�L����Ӌ�r�L����־
        base.OnPointerExit(eventData);
        my_isStartPress = false;

    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        //(�����ѽ��c���M���L���󣬔E�����r)
        if (!my_longPressTrigger)
        {
            
            if (eventData.clickCount == 2)
            {

               //if (my_onDoubleClick != null)
                //{
                //    my_onDoubleClick.Invoke();
                //}

            }
            else if (eventData.clickCount == 1)
            {
                onClick.Invoke();
            }
        }
    }
   
}
