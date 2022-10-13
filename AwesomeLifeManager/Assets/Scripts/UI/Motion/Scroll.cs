using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

//스크롤 기능을 미리구현해둔 UI입니다.
//이 클래스를 상속받으면 y축 스크롤기능을 사용할 수 있게 됩니다.
public abstract class Scroll : UI
{
    //스크롤할 목록을 묶는 변수입니다. 하이어라키창에서 반드시 설정해주어야하며 
    //상위에 Mask 컴포넌트를 사용하여 스크롤 창을 구현할 시에 이 오브젝트의 크기를 Mask컴포넌트가 달리 오브젝트의 크기와 같게 설정해주시는 편이 좋습니다.
    public RectTransform objGroup;
    //objGroup하위의 목록들을 저장할 변수입니다.
    public RectTransform[] objs;
    //스크롤 시작과 끝을 감지할 변수
    public bool startSwipe = false;
    //다른 UI와 동시에 실행될 수 있는지 여부를 나타냅니다.
    public bool rejactSameTimeActing = true;

    private void Awake()
    {
        updateObjs();
    }

    //objs를 업데이트합니다. objGroup하위에 목록을 추가한후 반드시 실행시켜주어야합니다.
    public void updateObjs()
    {
        objs = objGroup.GetComponentsInChildren<RectTransform>();
    }

    public override bool onClickDown(Vector2 clickPos)
    {
        return false;
    }

    public override bool onClickUp(float dragDis, Vector2 clickPos)
    {
        if (objGroup.anchoredPosition.y <= 0.2)
        {
            objGroup.anchoredPosition = Vector2.zero;
            if (startSwipe)
            {
                startSwipe = false;
                onEndSwipe();
            }
        }
        return false;
    }

    public override bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp)
    {
        Vector2[] _end = {Vector2.zero,Vector2.zero};
        for (int i = 0; i < objs.Length; i++) {
            if (_end[0].y > objs[i].anchoredPosition.y)
            {
                _end[0] = objs[i].anchoredPosition;
                _end[1] = objs[i].sizeDelta;
            }
        }
        if (objGroup.anchoredPosition.y - (swipeStartp.y - swipeEndp.y) >= 0 &&
            -1 * (objGroup.anchoredPosition.y - (swipeStartp.y - swipeEndp.y)) >= (_end[0].y + _end[1].y / 2 - 25))
        {
            objGroup.anchoredPosition += new Vector2(0, -1.5f * (swipeStartp.y - swipeEndp.y));
            if (!startSwipe)
            {
                startSwipe = true;
                onStartSwipe();
            }
            return rejactSameTimeActing;
        }
        else if (objGroup.anchoredPosition.y - (swipeStartp.y - swipeEndp.y) < 0)
        {
            objGroup.anchoredPosition = Vector2.zero;
            return rejactSameTimeActing;
        }
        else return false;
    }

    //스크롤 시작시에 실행될 함수
    public abstract void onStartSwipe();

    //스크롤이 끝났을때 실행할 함수
    public abstract void onEndSwipe();
}
