using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAnime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(AnimeCo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AnimeCo()
    {
        Material mat = this.GetComponent<Image>().material;
        while (mat.GetFloat("_CellSize") < 500)
        {
            mat.SetFloat("_CellSize", mat.GetFloat("_CellSize") + 15f * Time.deltaTime);
            yield return null;
        }
        mat.SetFloat("_CellSize", 500);
    }
}
