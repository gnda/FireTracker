using System;
using System.Collections;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private float moveDuration = 5.0f;
    public bool IsBusy { set; get; }
    public bool IsMoving { set; get; }
    public float WaitDuration { set; get; }

    private void Start()
    {
        IsBusy = false;
        IsMoving = false;
    }

    public void Move(Vector3 direc)
    {
        if (!IsBusy)
            StartCoroutine(TranslationCoroutine(
            transform.position, new Vector2(direc.x, direc.y), true));
    }

    private IEnumerator TranslationCoroutine(Vector2 startPos, Vector2 endPos, bool goBack = false)
    {
        float elapsedTime = 0;

        IsMoving = true;
        IsBusy = true;

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
        else
            yield return IsBusy = false;
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