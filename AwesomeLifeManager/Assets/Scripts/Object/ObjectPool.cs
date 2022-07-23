using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo{
    public GameObject goPrefab;
    public int count;
    public Transform tfPoolParent;
}

public class ObjectPool : MonoBehaviour
{
    [SerializeField] ObjectInfo[] objectInfo = null;

    public static ObjectPool instance;

    public Queue<GameObject> boxQueue = new Queue<GameObject>();

    void Start()
    {
        instance = this;
        boxQueue = InsertQueue(objectInfo[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Queue<GameObject> InsertQueue(ObjectInfo p_objectInfo){
        Queue<GameObject> t_queue = new Queue<GameObject>();
        for(int i = 0; i < p_objectInfo.count; i ++){
            GameObject t_clone = Instantiate(p_objectInfo.goPrefab, transform.position, Quaternion.identity);
            t_clone.SetActive(false);
            if(p_objectInfo.tfPoolParent != null)
                t_clone.transform.SetParent(p_objectInfo.tfPoolParent,false);
            else
                t_clone.transform.SetParent(this.transform,false);
            t_queue.Enqueue(t_clone); 
        }
        return t_queue;
    }
}
