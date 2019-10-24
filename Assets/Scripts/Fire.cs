using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fire : MonoBehaviour
{
    public GameObject gameManager;
    [SerializeField] private float fireDuration = 5.0f;
    [field: Header("Score")]
    public int Score { get; set; } = 0;
    private void Start()
    {
        gameManager = GameObject.Find("/Managers/GameManager");
    }

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

    private void OnMouseDown()
    {
        if(GameManager.SelectionCursor == CursorSelection.Bucket)
        {
            Destroy(this.gameObject);
            gameManager.GetComponent<GameManager>().Scoring();
            Debug.Log("+100");
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            GameManager.SelectionCursor = CursorSelection.Nothing;
        }
    }
}
