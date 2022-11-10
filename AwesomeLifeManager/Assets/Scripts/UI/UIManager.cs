using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

//구현한 UI를 작동시킬 UIManager클래스
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //구현한 UI의 리스트를 받습니다.
    //UI를 구현한후 반드시 하이어라키창에 끌어서 추가해주어야합니다.
    [SerializeField] public List<UI> UI_List = new List<UI>();
    public SpriteAtlas UIAtlas;
    public SpriteAtlas IconAtlas;
    //UI의 작동여부, false일경우 모든 UI가 작동을 하지 않습니다.
    public bool uiEnabled = true;
    //Swipe기능의 작동여부.
    public bool canSwipe = true;
    //swipe기능을 위해 터치/마우스가 눌렸는지 여부를 저장할 변수
    public bool keyDown = false;
    //유니티의 Button의 동작을 감지해 Button과 UI가 동시에 실행되는 일을 방지하기위한 변수
    //만약에 Button과 UI가 동시에 실행되는 일을 막고싶다면, Button의 리스너에 UIManager의 instance를 참조하여 이 함수를 true로 만드는 작업이 반드시 필요합니다.
    public bool externalListenerFired = false;

    //더블클릭이 인식될 클릭과 클릭사이의 간격, 단위 초
    [SerializeField] float doubleClickDelay;
    //터치/마우스를 누른채의 움직임을 감지하기 위한 변수
    private Vector3 trace_pos = Vector3.zero;
    //총 드래그 길이를 저장할 변수
    private float total_drag_distance = 0f;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (uiEnabled)
        {
            //UI.onClickDown함수를 처리
            if (Input.GetMouseButtonDown(0))
            {
                trace_pos = Input.mousePosition;
                keyDown = true;
                for (int i = 0; i < UI_List.Count; i++)
                {
                    if (!UI_List[i].gameObject.activeInHierarchy) continue;
                    if (!UI_List[i].active) continue;
                    if (UI_List[i].onClickDown(Input.mousePosition))
                        break;
                }
            }
            //UI.onClickUp함수를 처리
            if (Input.GetMouseButtonUp(0))
            {
                keyDown = false;
                for (int i = 0; i < UI_List.Count; i++)
                {
                    if (!UI_List[i].gameObject.activeInHierarchy) continue;
                    if (!UI_List[i].active) continue;
                    if (externalListenerFired)
                    {
                        externalListenerFired = false;
                        break;
                    }
                    //UI.onDoubleClick함수를 처리
                    if (UI_List[i].onDoubleClick(Input.mousePosition, false))
                    {
                        if (UI_List[i].clickedDelay == 0) UI_List[i].clickedDelay = 0.01f;
                        else
                        {
                            if (UI_List[i].clickedDelay < doubleClickDelay)
                            {
                                UI_List[i].onDoubleClick(Input.mousePosition, true);
                                UI_List[i].clickedDelay = 0;
                                break;
                            }
                            UI_List[i].clickedDelay = 0;
                        }
                    }
                    if (UI_List[i].onClickUp(total_drag_distance, Input.mousePosition))
                    {
                        break;
                    }
                }
                total_drag_distance = 0;
            }
            //UI.onSwipe함수를 처리
            if (keyDown)
            {
                float dis = Vector2.Distance(trace_pos, Input.mousePosition);
                total_drag_distance += dis;
                for (int i = 0; i < UI_List.Count; i++)
                {
                    if (!UI_List[i].gameObject.activeInHierarchy) continue;
                    if (!UI_List[i].active) continue;
                    if (UI_List[i].onSwipe(trace_pos, Input.mousePosition))
                        break;
                }
                trace_pos = Input.mousePosition;
            }
            for (int i = 0; i < UI_List.Count; i++)
            {
                if (UI_List[i].clickedDelay > 0)
                {
                    UI_List[i].clickedDelay += Time.deltaTime;
                    if (UI_List[i].clickedDelay > doubleClickDelay)
                        UI_List[i].clickedDelay = 0;
                }
            }
        }
    }
}
