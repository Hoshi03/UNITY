using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        //enum에 있는 것들 이름 가져오기
        string[] names = Enum.GetNames(type);

        //만들어둔 딕셔너리에 키와 벨류 넣어주기
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];

        _objects.Add(typeof(T), objects);


        for (int i = 0; i < names.Length; i++)
        {
            //만약 타입이 게임오브젝트면 
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.Findchild(gameObject, names[i], true);
            else
                objects[i] = Util.Findchild<T>(gameObject, names[i], true);
        }

    }


    //잘 이해가 안되는 부분
    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        //오브젝트 형 배열 만들고 안에다가 딕셔너리 밸류 넣어주기
        UnityEngine.Object[] objects = null;
        //키값 가져오기
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;
        else

            return objects[index] as T;
    }

    protected Text GetText(int index)
    {
        return Get<Text>(index);
    }

    protected Button GetButton(int index)
    {
        return Get<Button>(index);
    }

    protected Image GetImage(int index)
    {
        return Get<Image>(index);
    }

    public static void AddUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
      
        
        //컴포넌트를 안넣어줬거나 아예 없으면 만들어서 넣어줌
        UI_EventHandler evt = Util.GetorAddComponet<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;

            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;


        }
    }
}
