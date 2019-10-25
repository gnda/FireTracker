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
            for (int i = 0; i < Random.Range(15,30); i++)
                StartCoroutine(StartBurningCoroutine(Random.Range(0f, 10f)));
        if (difficulty == "Normal")
            for (int i = 0; i < Random.Range(30,60); i++)
                StartCoroutine(StartBurningCoroutine(Random.Range(0f, 5f)));
        if (difficulty == "Hard")
            for (int i = 0; i < Random.Range(40,71); i++)
                StartCoroutine(StartBurningCoroutine(Random.Range(0f, 3f)));
    }

    //private void PropagationFire(float posX, float posY, int color)
    //{
    //    GameObject fireHaut = Instantiate(firePrefab, new Vector3(posX + 0.5f, posY + 1.5f, color),
    //    Quaternion.identity);
    //    colorOfFire.Add(timerFire);

    //    cellBurning.Add(new Vector3(posX, posY+1, timerFire));
    //    GameObject fireBas = Instantiate(firePrefab, new Vector3(posX + 0.5f, posY - 1.5f, color),
    //    Quaternion.identity);
    //    colorOfFire.Add(timerFire);

    //    cellBurning.Add(new Vector3(posX, posY-1, timerFire));
    //    GameObject fireDroite = Instantiate(firePrefab, new Vector3(posX + 1.5f, posY + 0.5f,color),
    //    Quaternion.identity);
    //    colorOfFire.Add(timerFire);

    //    cellBurning.Add(new Vector3(posX+1, posY, timerFire));
    //    GameObject fireGauche = Instantiate(firePrefab, new Vector3(posX - 1.5f, posY + 0.5f,color),
    //    Quaternion.identity);
    //    colorOfFire.Add(timerFire);

    //}
    
    /*IEnumerator Propagation(float posX, float posY, float tempsEnSecondes)
    {
        yield return new WaitForSeconds(tempsEnSecondes);

        cellBurned.Add(new Vector2(posX, posY));

        cellBurning.Add(new Vector3(posX, posY + 1, timerFire));
        GameObject fireHaut = Instantiate(firePrefab, new Vector3(posX + 0.5f, posY + 1.5f, tempsEnSecondes),
        Quaternion.identity);
        fireHaut.GetComponent<SpriteRenderer>().color = SelectColor(typeOfFire);

        cellBurning.Add(new Vector3(posX, posY - 1, timerFire));
        GameObject fireBas = Instantiate(firePrefab, new Vector3(posX + 0.5f, posY - 0.5f, tempsEnSecondes),
        Quaternion.identity);
        fireBas.GetComponent<SpriteRenderer>().color = SelectColor(typeOfFire);

        cellBurning.Add(new Vector3(posX + 1, posY, timerFire));
        GameObject fireDroite = Instantiate(firePrefab, new Vector3(posX + 1.5f, posY + 0.5f, tempsEnSecondes),
        Quaternion.identity);
        fireDroite.GetComponent<SpriteRenderer>().color = SelectColor(typeOfFire);

        cellBurning.Add(new Vector3(posX - 1, posY, timerFire));
        GameObject fireGauche = Instantiate(firePrefab, new Vector3(posX - 0.5f, posY + 0.5f, tempsEnSecondes),
        Quaternion.identity);
        fireGauche.GetComponent<SpriteRenderer>().color = SelectColor(typeOfFire);
    }*/

    IEnumerator StartBurningCoroutine(float tempsEnSecondes)
    {
        yield return new WaitForSeconds(tempsEnSecondes);
        
        float posX, posY;
        bool fireExists = false;

        // On cherche une case sans feu
        do
        {
            posX = Random.Range(0, 12) + 0.5f;
            posY = Random.Range(0, 6) + 0.5f;
            
            foreach (var f in FindObjectsOfType<Fire>())
                if (f.transform.position.x == posX &&
                    f.transform.position.y == posY ) 
                    fireExists = true;

            yield return null;
        } while (fireExists);
        
        //Un type de feu parmi nos prefabs
        GameObject firePrefab = firePrefabs[Random.Range(0, firePrefabs.Length)];

        GameObject fireGo = Instantiate(firePrefab, firesGo.transform);
        fireGo.transform.position = new Vector2(posX, posY);
    }
}
