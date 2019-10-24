using Tobii.Gaming;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject m_selectDrone;
    [SerializeField] List<GameObject> m_placeholderDrones;

    private int _last;
    private GazePoint _lastGazePoint = GazePoint.Invalid;

    // Update is called once per frame
    void Update()
    {
        GazePoint gazePoint = TobiiAPI.GetGazePoint();

        if (gazePoint.IsRecent()
            && gazePoint.Timestamp > (_lastGazePoint.Timestamp + float.Epsilon))
        {
            //Debug.Log((int) gazePoint.Screen.x);
            //Debug.Log((int) gazePoint.Screen.y);
        }

        if (m_selectDrone != null && m_placeholderDrones.Any())
        {

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Vector3 placeholderDrone1 = m_placeholderDrones.First().transform.position;
                m_selectDrone.transform.position = placeholderDrone1;
            }
            else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Vector3 placeholderDrone2 = m_placeholderDrones.Skip(1).First().transform.position;
                m_selectDrone.transform.position = placeholderDrone2;
            }
            else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Vector3 placeholderDrone3 = m_placeholderDrones.Last().transform.position;
                m_selectDrone.transform.position = placeholderDrone3;
            }
        }


    }
}
