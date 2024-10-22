using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public float fadeDuration = 1.0f;  // Time for one fade in or out cycle
    public bool fadeInAtStart = true;  // Should the UI elements start with fade in?
    private List<Graphic> uiElements = new List<Graphic>();  // List to hold all Image and Text components
    private List<TextMeshProUGUI> tmpElements = new List<TextMeshProUGUI>(); // List for TextMeshPro elements
    private Coroutine fadeCoroutine;

    void Start()
    {
        // Get all UI elements (Image, Text, and TextMeshProUGUI) even if the GameObjects are disabled
        GetUIElements();

        // Automatically start fading in or out depending on fadeInAtStart
        if (fadeInAtStart)
        {
            StartFade(true); // Fade in
        }
        else
        {
            StartFade(false); // Fade out
        }
    }

    // Method to start the fade in or fade out
    public void StartFade(bool fadeIn)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine); // Stop any ongoing fade coroutine
        }
        fadeCoroutine = StartCoroutine(FadeCoroutine(fadeIn));
    }

    // Coroutine to fade the UI elements
    private IEnumerator FadeCoroutine(bool fadeIn)
    {
        float startAlpha = fadeIn ? 0 : 1;
        float endAlpha = fadeIn ? 1 : 0;
        float timer = 0f;

        // Get the current alpha of the first element, assuming all have the same alpha
        if (uiElements.Count > 0)
        {
            startAlpha = uiElements[0].color.a;
        }
        else if (tmpElements.Count > 0)
        {
            startAlpha = tmpElements[0].color.a;
        }

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);

            // Set the new alpha for all UI elements
            SetAlpha(alpha);  

            yield return null; // Wait for the next frame
        }

        // Ensure the final alpha is set to the target value
        SetAlpha(endAlpha);
        if(ButtonManager.Instance.GetGameWon())
            ButtonManager.Instance.OpenWinScreen();
    }

    // Helper method to set the alpha for all Image, Text, and TextMeshProUGUI components
    private void SetAlpha(float alpha)
    {
        foreach (Graphic element in uiElements)
        {
            if (element != null)
            {
                Color color = element.color;
                color.a = alpha;
                element.color = color;
            }
        }

        foreach (TextMeshProUGUI tmpElement in tmpElements)
        {
            if (tmpElement != null)
            {
                Color color = tmpElement.color;
                color.a = alpha;
                tmpElement.color = color;
            }
        }
    }

    // Method to get all UI elements (Images, Text, TextMeshProUGUI) from both active and inactive GameObjects
    private void GetUIElements()
    {
        // Get all Graphic components (Image, Text) in children, including inactive GameObjects
        uiElements.AddRange(FindComponentsInInactiveChildren<Image>(gameObject));
        uiElements.AddRange(FindComponentsInInactiveChildren<Text>(gameObject));

        // Get all TextMeshProUGUI components in children, including inactive GameObjects
        tmpElements.AddRange(FindComponentsInInactiveChildren<TextMeshProUGUI>(gameObject));
    }

    // Helper method to find components in both active and inactive children
    private List<T> FindComponentsInInactiveChildren<T>(GameObject parent) where T : Component
    {
        List<T> results = new List<T>();
        T[] components = parent.GetComponentsInChildren<T>(true); // 'true' includes inactive children
        foreach (T component in components)
        {
            results.Add(component);
        }
        return results;
    }

    // Public method to trigger fade in/out manually
    public void TriggerFade()
    {
        bool isCurrentlyFadingOut = uiElements.Count > 0 ? uiElements[0].color.a == 1 : tmpElements[0].color.a == 1;
        StartFade(!isCurrentlyFadingOut); // Switch fade direction
    }
}
