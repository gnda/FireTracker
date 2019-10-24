using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fire : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.GetComponentInParent<PlayerView>())
        {
            Vector2 position = other.transform.position;
            Tilemap tilemap = FindObjectOfType<Tilemap>();
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
            
            //TileBase test = allTiles[(int) position.x + (int) position.y * bounds.size.x];
            Debug.Log("Eyes position");
            Debug.Log(new Vector2((int) position.x, (int) position.y));
            Debug.Log("Fires position");
            foreach (Fire f in FindObjectsOfType<Fire>())
            {
                Debug.Log(f.transform.position);
                if (f.transform.position == )
            }
            Debug.Log("------");
        }*/
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("TEST");
    }
}
