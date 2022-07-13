using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    //마우스 이벤트
    public Action<Define.MouseEvent> MouseAction = null;
    //키 이벤트
    public Action keyaction = null;
    //마우스 눌리거나 땟을때 상태를 저장하는 bool 변수
    bool _pressed = false;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey && keyaction != null)
            keyaction.Invoke();

        //getmousebutton - 누르고 있을때, getmousebuttondown - 처음에만 
        if (Input.GetMouseButton(0))
        {
            MouseAction.Invoke(Define.MouseEvent.Press);
            _pressed = true;
        }
        else
        {
            //기존에 마우스를 눌럿으면
            if (_pressed)
                MouseAction.Invoke(Define.MouseEvent.Click);
            _pressed = false;
        }


        
    }
}
