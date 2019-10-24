using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Drone : MonoBehaviour
{
    [SerializeField] private float moveDuration = 5.0f;
    public bool IsBusy { set; get; }
    public bool IsMoving { set; get; }
    public float WaitDuration { set; get; }

    public void Move(Vector3 direc)
    {
        StartCoroutine(TranslationCoroutine(
            transform.position, new Vector2(direc.x, direc.y), true));
    }

    private IEnumerator TranslationCoroutine(Vector2 startPos, Vector2 endPos, bool goBack = false)
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

        if (goBack)
            yield return StartCoroutine(WaitCoroutine(endPos, startPos));
    }
    
    private IEnumerator WaitCoroutine(Vector2 startPos, Vector2 endPos)
    {
        float elapsedTime = 0;

        while (elapsedTime < WaitDuration)
        {
            float elapsedTimePerc = elapsedTime / WaitDuration;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(TranslationCoroutine(startPos, endPos));
    }
}