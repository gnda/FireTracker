using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private float moveDuration = 5.0f;
    
    private Vector2 anchorPosition;
    private List<Vector2> seenFiresPosition;
    public bool IsMoving { set; get; }
    public List<Vector2> SeenFiresPosition => seenFiresPosition;

    private void Start()
    {
        anchorPosition = transform.position;
        seenFiresPosition = new List<Vector2>();
        IsMoving = false;
    }

    public void Move()
    {
        StartCoroutine(MoveToFiresCoroutine());
    }

    private IEnumerator MoveToFiresCoroutine()
    {
        foreach (Vector2 pos in seenFiresPosition)
            yield return StartCoroutine(TranslationCoroutine(transform.position, pos));
        yield return StartCoroutine(
            TranslationCoroutine(transform.position, anchorPosition));
    }
    
    private IEnumerator TranslationCoroutine(Vector2 startPos, Vector2 endPos)
    {
        float elapsedTime = 0;

        IsMoving = true;

        while (elapsedTime < moveDuration)
        {
            float elapsedTimePerc = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPos, endPos, elapsedTimePerc);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
    
        IsMoving = false;
    }
}