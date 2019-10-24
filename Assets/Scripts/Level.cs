using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Level : MonoBehaviour
{
    [field: Header("Level Settings")]
   public float LevelDuration { get; } = 4;

    [Header("Level Prefabs")]
    [SerializeField] GameObject firePrefab;
    // [SerializeField] GameObject treePrefab;
    [SerializeField] string difficulty;

    [Header("Type of Fire")]
    [SerializeField] int timeBlue;
    [SerializeField] int timeGreen;
    [SerializeField] int timeRed;


    Color couleur;
    //Vector2[] cellBurned;
    //Vector3[] cellBurning;
    List<Vector2> cellBurned;
    List<Vector3> cellBurning;
   
    int currentlyCellBurn = 0;

    TileBase tile;

    int typeOfFire;
    int timerFire;

    int posX;
    int posY;
    bool boolSortie = true;

    bool inFirst = false;

    private void Start()
    {

        cellBurning = new List<Vector3>();
        cellBurned = new List<Vector2>();
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
            //Debug.Log(difficulty);
            StartCoroutine(BURN(Random.Range(0f, 20f)));
            //StartCoroutine(WaitingForBURN());
        }
        if (difficulty == "Normal")
        {
            //Debug.Log(difficulty);
            StartCoroutine(BURN(Random.Range(0f, 15f)));
            //StartCoroutine(WaitingForBURN());
        }
        if (difficulty == "Hard")
        {
            //Debug.Log(difficulty);
            StartCoroutine(BURN(Random.Range(0f, 7f)));
            //StartCoroutine(WaitingForBURN());
        }
    }

    private void Update()
    {
        SpawnFire();
    //    Debug.Log(cellBurning[0].z);
       // FireAwaitingToDiseapear();
    }

    //private void FireAwaitingToDiseapear()
    //{
    //        for (int z = 0; z <= cellBurning.Count; z++)
    //        {

    //            cellBurned.Add(new Vector2(cellBurning[z].x, cellBurning[z].y));
    //            StartCoroutine(Propagation(cellBurning[z].x, cellBurning[z].y, cellBurning[z].z, z));
    //        }

    //}

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
    IEnumerator Propagation(float posX, float posY, float tempsEnSecondes)
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
    }

    IEnumerator BURN(float tempsEnSecondes)
    {
        if (inFirst == false)
        {
            inFirst = true;
            yield return new WaitForSeconds(tempsEnSecondes);
            posX = Random.Range(0, 12);
            posY = Random.Range(0, 6);

            typeOfFire = Random.Range(0, 3);
           
           
            //Debug.Log(posX);
            //Debug.Log(posY);
          //  Debug.Log(typeOfFire);

            cellBurning.Add(new Vector3(posX, posY, timerFire));

            StartCoroutine(Propagation(posX, posY, timerFire));

            // var foo = cellBurning.Skip(currentlyCellBurn).First();
            //  currentlyCellBurn += 1;
            //Debug.Log(cellBurning[0]);
            //Debug.Log(cellBurning[1]);

            var position = transform.position;
            GameObject fireGo = Instantiate(firePrefab, new Vector2(posX + 0.5f, posY + 0.5f),
                Quaternion.identity);
            fireGo.GetComponent<SpriteRenderer>().color = SelectColor(typeOfFire);
            inFirst = false;
        }
        //fireGo.transform.SetParent(transform);
        //print(Time.time);
    }
    //IEnumerator WaitingForBURN()
    //{
    //    while (inFirst == true)
    //        yield return new WaitForSeconds(0.1f);
    //    print("Do stuff.");
    //}


    private Color SelectColor(float color)
    {
        if (typeOfFire == 0)
        {
            timerFire = timeBlue;
            couleur = new Color(0, 255, 255, 255);
        }
        if (typeOfFire == 1)
        {
            timerFire = timeGreen;
            couleur = new Color(116, 255, 0, 255);
        }
        if (typeOfFire == 2)
        {
            timerFire = timeRed;
            couleur = new Color(255, 166, 0, 255);
        }

        return couleur;
    }
}
