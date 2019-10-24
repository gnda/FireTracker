using System;
using System.Collections;
using System.Collections.Generic;
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
        if (FindObjectOfType<GameManager>().currentGameMode == GameMode.Tracking &&
            other.GetComponent<PlayerGaze>() && 
            !FindObjectOfType<Drone>().SeenFiresPosition.Contains(transform.position)) {
            FindObjectOfType<Drone>().SeenFiresPosition.Add(transform.position);
        }
    }

    private void OnParticleTrigger()
    {
        Debug.Log("TEST");
        ParticleSystem ps = GetComponent<ParticleSystem>();

        if (FindObjectOfType<GameManager>().currentGameMode == GameMode.Watching
            && ps.trigger.GetCollider(0).GetComponent<Smoke>()) {
            Debug.Log("TEST");
            Destroy(gameObject);
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
