using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOnActivate : MonoBehaviour
{
    public Image image;
    public float fadeDuration = 1.0f;
    public float Delay = 0f;

    private void OnEnable()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = image.color;

        // Ensure the image starts fully transparent
        color.a = 0f;
        image.color = color;

        // Delay before starting the fade
        while (elapsedTime < Delay)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        // Fade in over fadeDuration
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            image.color = color;
            yield return null;
        }

        // Ensure the image ends fully opaque
        color.a = 1f;
        image.color = color;
    }
}
