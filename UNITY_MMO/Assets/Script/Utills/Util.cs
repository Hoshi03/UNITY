using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    //기능성 함수들을 넣어놓는 스크립트

    //게임오브젝트는 컴포넌트가 아니기에 따로 받아서 트랜스폼을 리턴하는 findchild로 해주고 transform의 게임오브젝트를 리턴해줌
    public static GameObject Findchild(GameObject go, string name = null, bool recursive = false) 
    {
        Transform transform = Findchild<Transform>(go, name, recursive);
        if (transform == null)
            return null;
        return transform.gameObject;

    }

    //해당 타입의 게임 오브젝트, 이름을 넣고,recursive로 자식까지 쭉 찾을지 부모만 보고 멈출지 설정해줌, 유니티 오브젝트만 찾아주게 설정
    public static T Findchild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        //널체크
        if (go == null)
            return null;

        if(recursive == false)
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }

            }
        }

        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                //이름이 비었거나 넣어준 것과 찾은 것의 이름이 같으면 
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }

        return null;

    }

}
