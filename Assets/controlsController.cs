using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlsController : MonoBehaviour
{
    public string up;
    public string lt;
    public string rt;
    public string dw;

    private void Awake()
    {
        Debug.Log(Input.GetAxis( "Horizontal" ).ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
