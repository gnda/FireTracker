using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fire : MonoBehaviour
{
    [SerializeField] private float fireDuration = 5.0f;

    private Drone lastDrone;

    public float FireDuration
    {
        get => fireDuration;
        set => fireDuration = value;
    }

    private void Update()
    {
        if (lastDrone &&
            lastDrone.GetComponent<Drone>() && 
            !lastDrone.GetComponent<Drone>().IsMoving)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<PlayerController>().LastSeenFire = this;
        if (other.GetComponent<Drone>()) {
            lastDrone = other.GetComponent<Drone>();
        }
    }

    private void OnMouseDown()
    {
        if(GameManager.SelectionCursor == CursorSelection.Bucket)
        {
            Destroy(this.gameObject);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            GameManager.SelectionCursor = CursorSelection.Nothing;
        }
    }
}
