using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField] private float smokeDuration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        foreach (var f in FindObjectsOfType<Fire>())
        {
            if (f.transform.position == transform.position) {
                FindObjectOfType<GameManager>().Scoring();
                Destroy(f.gameObject);
            }
        }
        
        yield return new WaitForSeconds(smokeDuration);
        Destroy(gameObject);
    }
}
