using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GazeController : MonoBehaviour
{
    public Sprite gazePointRed;
    public Sprite gazePointGreen;
    public Sprite gazePointBlue;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<GazePlotterCustom>().PointSprite = gazePointRed;
        } else if (Input.GetKeyDown(KeyCode.Z))
        {
            GetComponent<GazePlotterCustom>().PointSprite = gazePointGreen;
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<GazePlotterCustom>().PointSprite = gazePointBlue;
        }
    }
}
