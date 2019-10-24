using Tobii.Gaming;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int _last;
    private GazePoint _lastGazePoint = GazePoint.Invalid;

    // Update is called once per frame
    void Update()
    {
        GazePoint gazePoint = TobiiAPI.GetGazePoint();

        if (gazePoint.IsRecent()
            && gazePoint.Timestamp > (_lastGazePoint.Timestamp + float.Epsilon))
        {
            Debug.Log((int) gazePoint.Screen.x);
            Debug.Log((int) gazePoint.Screen.y);
        }
    }
}
