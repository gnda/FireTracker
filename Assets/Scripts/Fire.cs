using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fire : MonoBehaviour
{
    [SerializeField] private float fireDuration = 5.0f;

    public float FireDuration
    {
        get => fireDuration;
        set => fireDuration = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (FindObjectOfType<GameManager>().currentGameMode == GameMode.Tracking 
            && other.GetComponent<PlayerGaze>())
            FindObjectOfType<Drone>().SeenFiresPosition.Add(transform.position);
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
