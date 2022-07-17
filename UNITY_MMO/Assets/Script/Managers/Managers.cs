using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //매니저 선언, 유일성 보장
    static Managers s_instance;

    //유일한 매니저 가져오기
    static Managers instance { get { Init(); return s_instance; } }

    //inputmanager 추가
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();

    public static UIManager UI { get { return instance._ui; } }
    public static InputManager Input { get { return instance._input ; } }
    public static ResourceManager resource { get { return instance._resource; } }
    void Start()
    {
        Init();
        
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("Manager");
            if(go == null)
            {
                //게임 오브젝트 만들어주고 이름 붙이기
                go = new GameObject { name = "Manager" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

        }
    }
}
