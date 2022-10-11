using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [SerializeField] public List<UI> UI_List = new List<UI>();
    public bool uiEnabled = true;
    public bool canSwipe = true;
    public bool keyDown = false;
    public bool externalListenerFired = false;

    [SerializeField] float doubleClickDelay;
    private Vector3 trace_pos = Vector3.zero;
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
                }
            }
        }
    }
}
