using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //매니저를 유일한 전역 변수로 선언
    static Managers s_instance;

    //매니저 가져오기
    public static Managers instance { get { Init();  return s_instance; } }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
