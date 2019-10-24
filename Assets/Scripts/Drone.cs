using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private float moveDuration = 1.0f;
    [SerializeField] private float rotationDuration = 1.0f;
    
    [SerializeField] private GameObject smokePrefab;
    
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
            yield return StartCoroutine(MoveCoroutine(pos));
        yield return StartCoroutine(
            MoveCoroutine(anchorPosition));
    }
    
    private IEnumerator MoveCoroutine(Vector2 endPos)
    {
        yield return StartCoroutine(RotationCoroutine(endPos));
        yield return StartCoroutine(TranslationCoroutine(transform.position, endPos));
        
        if (seenFiresPosition.Count > 0 && 
            anchorPosition != endPos) {
            GameObject smokeGo = Instantiate(smokePrefab);
            smokeGo.transform.position = endPos;
        }
    }

    private IEnumerator RotationCoroutine(Vector2 endPos)
    {
        Vector2 dir = endPos - (Vector2) transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        Quaternion qAngle = Quaternion.AngleAxis(angle, Vector3.forward); 
        Quaternion qEnd = Quaternion.Euler (qAngle.eulerAngles.x,
            qAngle.eulerAngles.y, qAngle.eulerAngles.z - 90);

        float elapsedTime = 0;
        
        IsMoving = true;
        
        while (elapsedTime < rotationDuration)
        {
            float elapsedTimePerc = elapsedTime / rotationDuration;
            transform.rotation = Quaternion.Slerp(
                transform.rotation, qEnd, elapsedTimePerc);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.rotation = qEnd;
        
        IsMoving = false;
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