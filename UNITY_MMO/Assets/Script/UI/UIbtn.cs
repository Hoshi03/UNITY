using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIbtn : UI_Base
{
    //타입, 오브젝트를 가지는 딕셔너리 선언
    //Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    [SerializeField]
    Text _text;
    int score = 0;

    enum Btns
    {
        PointBtn
    }

    enum Texts
    {
        PointText,
        ScoreText
    }

    enum GameObjects
    {
        TestObject
    }

    private void Start()
    {
        Bind<Button>(typeof(Btns));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        //enum값에 접근하니까 int로 변환한듯
        Get<Text>((int)Texts.ScoreText).text = "bind test";
    }

    public void OnBtnClicked()
    {
        _text.text = $" 점수 :{++score} 점";
    }
}
