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

       byte bvk,//虚拟键值 ESC键对应的是27

       byte bScan,//0

       int dwFlags,//0为按下，1按住，2释放

       int dwExtraInfo//0

       );
    // 长按
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
        // 按下刷新當前時間
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
        // 指針擡起，結束開始長按
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
        // 指針移出，結束開始長按，計時長按標志
        base.OnPointerExit(eventData);
        my_isStartPress = false;

    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        //(避免已經點擊進入長按后，擡起的情況)
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
