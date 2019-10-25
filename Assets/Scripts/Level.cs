using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Level : MonoBehaviour
{
    [Header("Level Settings")] [SerializeField]
    private float levelDuration = 10f;
    public float LevelDuration => levelDuration;

    [Header("Level Prefabs")]
    [SerializeField] GameObject[] firePrefabs;
    [SerializeField] GameObject treePrefab;
    [SerializeField] string difficulty;

    private GameObject firesGo;

    private void Start()
    {
        firesGo = new GameObject("Fires");
        firesGo.transform.SetParent(transform);
    }

    public void InitLevel(bool spawnTrees)
    {
        if (spawnTrees) SpawnTrees();
        FindObjectOfType<GameManager>().FirstGame = false;
        SpawnFires();
    }

    private TileBase[] GetAllTiles()
    {
        Tilemap tilemap = FindObjectOfType<Tilemap>();
        BoundsInt bounds = tilemap.cellBounds;

        return tilemap.GetTilesBlock(bounds);
    }

    private void SpawnTrees()
    {
        Tilemap tilemap = FindObjectOfType<Tilemap>();
        BoundsInt bounds = tilemap.cellBounds;
        GameObject treesGo = new GameObject("Trees");
        treesGo.transform.SetParent(transform);

        for (int x = 0; x < bounds.size.x; x++)
            for (int y = 0; y < bounds.size.y; y++)
            {
                GameObject treeGo = Instantiate(treePrefab, treesGo.transform);
                treeGo.transform.position = new Vector2(x + 0.5f, y + 0.5f);
            }
    }

    private void SpawnFires()
    {
        if (difficulty == "Easy")
            for (int i = 0; i < Random.Range(8,15); i++)
                StartCoroutine(StartBurningCoroutine(Random.Range(0f, 10f)));
        if (difficulty == "Normal")
            for (int i = 0; i < Random.Range(15,25); i++)
                StartCoroutine(StartBurningCoroutine(Random.Range(0f, 5f)));
        if (difficulty == "Hard")
            for (int i = 0; i < Random.Range(25,40); i++)
                StartCoroutine(StartBurningCoroutine(Random.Range(0f, 3f)));
    }

    IEnumerator StartBurningCoroutine(float tempsEnSecondes)
    {
        yield return new WaitForSeconds(tempsEnSecondes);
        
        float posX, posY;
        bool fireExists = false;
        bool treeExists = false;

        // On cherche une case sans feu
        do
        {
            posX = Random.Range(0, 12) + 0.5f;
            posY = Random.Range(0, 6) + 0.5f;
            
            foreach (var f in FindObjectsOfType<Fire>())
                if (f.transform.position.x == posX &&
                    f.transform.position.y == posY ) 
                    fireExists = true;
            
            foreach (var t in FindObjectsOfType<ForestTree>())
                if (t.transform.position.x == posX &&
                    t.transform.position.y == posY ) 
                    treeExists = true;

            yield return null;
        } while (fireExists || !treeExists);

        //Un type de feu parmi nos prefabs
        GameObject firePrefab = firePrefabs[Random.Range(0, firePrefabs.Length)];

        GameObject fireGo = Instantiate(firePrefab, firesGo.transform);
        fireGo.transform.position = new Vector2(posX, posY);
    }
}
