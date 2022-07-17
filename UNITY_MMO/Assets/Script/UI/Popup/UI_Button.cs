using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    //타입, 오브젝트를 가지는 딕셔너리 선언
    //Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    [SerializeField]
    Text _text;
    int score = 0;

    enum Btns
    {
        PointButton
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

    enum Images
    {
        ItemIcon,
    }

    private void Start()
    {
        init();
    }

    public override void init()
    {
        base.init();
        Bind<Button>(typeof(Btns));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        //enum값에 접근하니까 int로 변환한듯
        //Get<Text>((int)Texts.ScoreText).text = "bind test";

        //enum에 넣어둔 오브젝트 가져와서
        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        //이벤트핸들러 추출하고 
        //UI_EventHandler evt = go.GetComponent<UI_EventHandler>();1
        //액션에 구독해줌
        //evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; });

        //위에 것을 UI_Base에 만들어둔 함수하나로 처리하게 해줌
        AddUIEvent(go, (PointerEventData data) => { go.gameObject.transform.position = data.position; }, Define.UIEvent.Drag);

        GetButton((int)Btns.PointButton).gameObject.AddUIEvent(OnBtnClicked);
    }

    public void OnBtnClicked(PointerEventData data)
    {
        score++;
        Get<Text>((int)Texts.ScoreText).text = $"점수 : {score}";
    }
}
