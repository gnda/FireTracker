using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class Level : MonoBehaviour
{
    [field: Header("Level Settings")]
    public float LevelDuration { get; } = 30f;

    [Header("Level Prefabs")]
    [SerializeField] GameObject firePrefab;
    // [SerializeField] GameObject treePrefab;
    [SerializeField] string difficulty;

    TileBase tile;

    int posX;
    int posY;

    bool inFirst = false;

    private void Start()
    {
        GetAllTiles();
    }

    private void GetAllTiles()
    {
        Tilemap tilemap = FindObjectOfType<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                tile = allTiles[x + y * bounds.size.x];
            }
        }

        //if (tile != null)
        //        {
        //            var position = transform.position;
        //            GameObject fireGo = Instantiate(firePrefab, new Vector2(x-0.5f, y-0.5f), 
        //                Quaternion.identity);
        //            fireGo.transform.SetParent(transform);
        //        }

    }
    private void SpawnFire()
    {
        if (difficulty == "Easy")
        {
            Debug.Log(difficulty);
            StartCoroutine(BURN(15f));
            //StartCoroutine(WaitingForBURN());
        }
        if (difficulty == "Normal")
        {
            Debug.Log(difficulty);
            StartCoroutine(BURN(10f));
            //StartCoroutine(WaitingForBURN());
        }
        if (difficulty == "Hard")
        {
            Debug.Log(difficulty);
            StartCoroutine(BURN(5f));
            //StartCoroutine(WaitingForBURN());
        }
    }

    private void Update()
    {
        SpawnFire();  
    }

    IEnumerator BURN(float tempsEnSecondes)
    {
        if (inFirst == false)
        {
            inFirst = true;
            yield return new WaitForSeconds(tempsEnSecondes);
            posX = Random.Range(0, 12);
            posY = Random.Range(0, 6);
            var position = transform.position;
            GameObject fireGo = Instantiate(firePrefab, new Vector2(posX + 0.5f, posY + 0.5f),
                Quaternion.identity);
            inFirst = false;
        }
        // fireGo.transform.SetParent(transform);
        //print(Time.time);
    }
    //IEnumerator WaitingForBURN()
    //{
    //    while (inFirst == true)
    //        yield return new WaitForSeconds(0.1f);
    //    print("Do stuff.");
    //}
}
