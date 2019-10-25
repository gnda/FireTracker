using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    [SerializeField] private float smokeDuration = 1.0f;
    [SerializeField] AudioClip[] smokeSFX;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.RandomizeSfx(smokeSFX);
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator PlayRandomly()
    {
        Animator animator = gameObject.GetComponentInChildren<Animator>();
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        while (true)
        {
            var randInd = Random.Range(0, clips.Length);

            var randClip = clips[randInd];

            animator.Play(randClip.name);

            // Wait until animation finished than pick the next one
            yield return new WaitForSeconds(randClip.length);

        }
    }

    private IEnumerator DestroyCoroutine()
    {
        foreach (var f in FindObjectsOfType<Fire>())
        {
            if (f.transform.position == transform.position)
            {
                FindObjectOfType<GameManager>().Scoring();

                Destroy(f.gameObject);
            }
        }

        yield return new WaitForSeconds(smokeDuration);
        Destroy(gameObject);
        //StartCoroutine(PlayRandomly());

        
    }
}
