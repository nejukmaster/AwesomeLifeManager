using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  이 클래스에선 오브젝트 풀링을 담당한답니다.
    게임에 오브젝트가 사용하고 사용하지 않을때마다 
    생성하고 삭제하면 리소스를 너무 많이 잡아먹게되므로, 
    미리 생성해놓고 코드로 활성화/비활성화 및 배치만 하는것을 
    오브젝트 풀링이라고 해요.   */

/*  미리 생성할 객체의 프리팹과 수등의 정보를 
    저장하여 등록할 클래스를 선언해요.  */
[System.Serializable]
public class ObjectInfo{
    public GameObject goPrefab;
    public int count;
    public Transform tfPoolParent;
    public bool restore_world_position;
}

/*  오브젝트 풀링을 작동할 클래스예요. 
    객체를 생성하고, 큐에 담은후 출고/반납을 담당하죠.  */
public class ObjectPool : MonoBehaviour
{
    [SerializeField] ObjectInfo[] objectInfo = null;

    public static ObjectPool instance;

    public Queue<GameObject> calenderCellQueue = new Queue<GameObject>();

    void Start()
    {
        instance = this;
        calenderCellQueue = InsertQueue(objectInfo[0]);
    }

    Queue<GameObject> InsertQueue(ObjectInfo p_objectInfo){
        Queue<GameObject> t_queue = new Queue<GameObject>();
        for(int i = 0; i < p_objectInfo.count; i ++){
            GameObject t_clone = Instantiate(p_objectInfo.goPrefab, transform.position, Quaternion.identity);
            t_clone.SetActive(false);
            if(p_objectInfo.tfPoolParent != null)
                t_clone.transform.SetParent(p_objectInfo.tfPoolParent,p_objectInfo.restore_world_position);
            else
                t_clone.transform.SetParent(this.transform,p_objectInfo.restore_world_position);
            t_queue.Enqueue(t_clone); 
        }
        return t_queue;
    }
}
