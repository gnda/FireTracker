using System;
using System.Collections.Generic;
using System.Linq;
using Tobii.Gaming;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject m_selectDrone;
    [SerializeField] List<GameObject> m_placeholderDrones;

    private Drone lastSelectedDrone;
    public Fire LastSeenFire { get; set; }
    
    private int _last;
    private GazePoint _lastGazePoint = GazePoint.Invalid;

    private void Start()
    {
        lastSelectedDrone = FindObjectsOfType<Drone>()[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (m_selectDrone != null && m_placeholderDrones.Any())
        {

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Vector3 placeholderDrone1 = m_placeholderDrones.First().transform.position;
                m_selectDrone.transform.position = placeholderDrone1;
                lastSelectedDrone = FindObjectsOfType<Drone>()[2];
            }
            else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Vector3 placeholderDrone2 = m_placeholderDrones.Skip(1).First().transform.position;
                m_selectDrone.transform.position = placeholderDrone2;
                lastSelectedDrone = FindObjectsOfType<Drone>()[1];
            }
            else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Vector3 placeholderDrone3 = m_placeholderDrones.Last().transform.position;
                m_selectDrone.transform.position = placeholderDrone3;
                lastSelectedDrone = FindObjectsOfType<Drone>()[0];
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                GazePoint gazePoint = TobiiAPI.GetGazePoint();

                if (gazePoint.IsRecent()
                    && gazePoint.Timestamp > (_lastGazePoint.Timestamp + float.Epsilon))
                {
                    lastSelectedDrone.IsBusy = true;
                    lastSelectedDrone.WaitDuration = LastSeenFire.FireDuration;
                    lastSelectedDrone.Move(LastSeenFire.transform.position);
                }
            }
        }
    }
}
