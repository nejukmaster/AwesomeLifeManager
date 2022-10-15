using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerContainer : MonoBehaviour
{
    public List<Viewer> viewer = new List<Viewer>();

    public void Init()
    {
        viewer.Add(this.GetComponent<StatusViewer>());
    }
}
