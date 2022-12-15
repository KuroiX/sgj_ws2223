using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimatedPlayButton : MonoBehaviour
{
    public float Strength;
    public float Delay;

    void Start()
    {
        StartCoroutine(Pulse());
    }

    IEnumerator Pulse()
    {
        // Loops forever
        while (true)
        {
            float timer = 0f;
            float originalSize = transform.localScale.x;

            // Heart beat twice
            for (int i = 0; i < 2; i++)
            {
                // Zoom in
                while (timer < Delay * 1f / 10f)
                {
                    yield return new WaitForEndOfFrame();
                    timer += Time.deltaTime;

                    transform.localScale = new Vector3
                    (
                        transform.localScale.x + (Time.deltaTime * Strength * 2),
                        transform.localScale.y + (Time.deltaTime * Strength * 2)
                    );
                }
            }

            // Return to normal
            while (transform.localScale.x < originalSize)
            {
                yield return new WaitForEndOfFrame();

                transform.localScale = new Vector3
                (
                    transform.localScale.x - Time.deltaTime * Strength,
                    transform.localScale.y - Time.deltaTime * Strength
                );
            }

            transform.localScale = new Vector3(originalSize, originalSize);

            yield return new WaitForSeconds(Delay * 9f / 10f);
        }
    }
}
