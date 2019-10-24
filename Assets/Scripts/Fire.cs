using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fire : MonoBehaviour
{
    public GameObject gameManager;
    [SerializeField] private float fireDuration = 5.0f;
    [field: Header("Score")]
    public int Score { get; set; } = 0;
    private Drone lastDrone;

    private void Start()
    {
        gameManager = GameObject.Find("/Managers/GameManager");
    }

    public float FireDuration
    {
        get => fireDuration;
        set => fireDuration = value;
    }

    private void Update()
    {
        Debug.Log(Score);
        if (lastDrone && lastDrone.GetComponent<Drone>() && !lastDrone.GetComponent<Drone>().IsMoving)
        {
            gameManager.GetComponent<GameManager>().Scoring();
            Destroy(gameObject);
        }
           
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
            gameManager.GetComponent<GameManager>().Scoring();
            Debug.Log("+100");
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            GameManager.SelectionCursor = CursorSelection.Nothing;
        }
    }
}
