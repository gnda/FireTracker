using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    [field: Header("Level Settings")]
    public float LevelDuration { get; } = 30f;

    [Header("Level Prefabs")] 
    [SerializeField] GameObject firePrefab;
    [SerializeField] GameObject treePrefab;

    private void Start()
    {
        SpawnFires();
    }

    private void SpawnFires()
    {
        Tilemap tilemap = FindObjectOfType<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    var position = transform.position;
                    GameObject fireGo = Instantiate(firePrefab, new Vector2(x-0.5f, y-0.5f), 
                        Quaternion.identity);
                    fireGo.transform.SetParent(transform);
                }
            }
        }
    }
}
