using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZolieArcenciel : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textms;

    void Update()
    {
        float h, s, v;
        Color.RGBToHSV( textms.color, out h, out s, out v );

        textms.color = Color.HSVToRGB( h + Time.deltaTime * .25f, s, v );
    }
}
