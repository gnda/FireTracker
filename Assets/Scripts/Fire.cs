using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fire : MonoBehaviour
{
    [SerializeField] private float fireDuration = 5.0f;
    [field: Header("Score")]
    public int Score { get; set; } = 0;

    private void Start()
    {
        StartCoroutine(BurnCoroutine());
    }

    public float FireDuration
    {
        get => fireDuration;
        set => fireDuration = value;
    }
    
    private IEnumerator BurnCoroutine()
    {
        yield return new WaitForSeconds(fireDuration);
        
        foreach (var f in FindObjectsOfType<ForestTree>())
            if (f.transform.position == transform.position)
                Destroy(f.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(transform.position);
        if (FindObjectOfType<GameManager>().currentGameMode == GameMode.Tracking &&
            other.GetComponent<PlayerGaze>() && 
            !FindObjectOfType<Drone>().SeenFiresPosition.Contains(transform.position) &&
            gameObject.activeInHierarchy) {
            FindObjectOfType<Drone>().SeenFiresPosition.Add(transform.position);
        }
    }

    private void OnMouseDown()
    {
        if(GameManager.SelectionCursor == CursorSelection.Bucket)
        {
            Destroy(this.gameObject);
            FindObjectOfType<GameManager>().Scoring();
            Debug.Log("+100");
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            GameManager.SelectionCursor = CursorSelection.Nothing;
        }
    }
}
