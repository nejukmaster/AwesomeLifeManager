using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//세세한 UI나 모션 커스텀을 위한 추상클래스
//여러 모션이나 클릭등을 미리구현하여 상속받아 쓸 수 있게 합니다.
public abstract class UI : MonoBehaviour
{
    
    public UIManager theUIManager;
    //UI의 동작 여부, false일경우 이 UI는 작동하지 않습니다.
    public bool active = true;
    //더블클릭 구현을 위해 클릭사이의 간격을 저장할 변수
    public float clickedDelay;

    //터치/마우스를 눌렀을때 실행되는 함수
    public abstract bool onClickDown(Vector2 clickPos);

    //터치/마우스를 뗐을때 실행되는 함수
    public abstract bool onClickUp(float dragDis, Vector2 clickPos);

    //스와이프, 즉 터치/마우스를 누른채로 드래그시 실행되는 함수
    public abstract bool onSwipe(Vector2 swipeStartp, Vector2 swipeEndp);

    //더블클릭시 실행되는 함수
    //이 함수의 반환값이 false일경우 더블클릭 기능을 사용하지 않겠다는 의미입니다.
    //또한 더블클릭기능을 사용하려면 더블클릭시 실행될 코드를 isActive가 true일 경우 발생하도록 짜야합니다.
    public virtual bool onDoubleClick(Vector2 clickPos, bool isActivate)
    {
        return false;
    }

    void Awake()
    {
        theUIManager = UIManager.instance;
    }

    //p_obj의 하위 UI의 active값을 전부 p_bool로 바꾸는 정적함수입니다.
    public static void ToggleSubUI(GameObject p_obj, bool p_bool)
    {
        foreach(UI ui in p_obj.GetComponentsInChildren<UI>())
        {
            ui.active = p_bool;
        }
    }
}
