using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CatAnimator : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 100;
    private List<Vector2> _queue;
    
    private Animator _animator;

    [SerializeField] private CatPath testPath;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
    }

    public void Start()
    {
        PlayPath(testPath);
    }

    public void PlayPath(CatPath path)
    {
        RectTransform rect = (RectTransform)transform;
        _queue = path.queue;
        rect.anchoredPosition = _queue[0];
        _animator.SetBool("isWalking", true);
        
        StartCoroutine(WalkRoutine());
    }

    private IEnumerator WalkRoutine()
    {
        int i = 1;
        
        // Sound
        AudioPlayScript.PlaySound(AudioPlayScript.SoundClip.Cat);
        
        while (true)
        {
            if (i >= _queue.Count) break;
            RectTransform rect = (RectTransform)transform;
            
            var dir = (_queue[i] - new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y));
            dir = dir.normalized;
            if (dir.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (dir.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            

            rect.anchoredPosition += (dir * (walkSpeed * Time.deltaTime));
            
            var dist = (_queue[i] - new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y));

            if (dist.magnitude < 1f)
            {
                i++;
                //Debug.Break();
                _animator.SetBool("isWalking", false);
                // Manchmal Sound
                int rnd = Random.Range(0, 3);
                if(rnd==0) AudioPlayScript.PlaySound(AudioPlayScript.SoundClip.Cat);
                yield return new WaitForSeconds(Random.Range(0f, 2f));
                _animator.SetBool("isWalking", true);
            }
            else
            {
                yield return null;
            }
            
        }
        
        _animator.SetBool("isWalking", false);
        
    }
}