using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerContainer : MonoBehaviour
{
    public List<Viewer> viewer = new List<Viewer>();
    public List<GameObject> viewerObj = new List<GameObject>();

    public void Init()
    {
        viewer.Add(viewerObj[0].GetComponent<StatusViewer>());
        viewer.Add(viewerObj[1].GetComponent<PersonalityViewer>());
    }
}
