using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageScroll : MonoBehaviour
{
    //this script will scroll the page to the right every 

    public int _TotalNumberOfSlides = 0;      // Total number of pages/slides
    public GameObject _Page;                  // The page GameObject (UI element or normal GameObject)
    private int _CurrentPage = 0;             // Current page index
    public List<GameObject> _Pages;           // Move Pages to the correct position using this

    private float _screenWidth;
    private bool isMoving = false;            // To prevent multiple movements at the same time
    public float moveDuration = 0.5f;         // Duration of the sliding effect

    private void Start()
    {
        // Get the width of the screen (useful for the sliding effect)
        _screenWidth = Screen.width;
        int i = 0;
        foreach (var page in _Pages)
        {
            page.transform.position = _Pages[0].transform.position + new Vector3((_screenWidth * i) * 1.454f, 0);
            i++;
        }
    }

    // Move one screen width to the right
    public void MoveToNextPage()
    {
        // Prevent method call if a movement is already happening
        if (!isMoving && _CurrentPage < _TotalNumberOfSlides - 1)
        {
            _CurrentPage++;
            StartCoroutine(MovePage(-_screenWidth));  // Smooth movement to the right
        }
    }

    // Move one screen width to the left
    public void MoveToPreviousPage()
    {
        // Prevent method call if a movement is already happening
        if (!isMoving && _CurrentPage > 0)
        {
            _CurrentPage--;
            StartCoroutine(MovePage(_screenWidth));   // Smooth movement to the left
        }
    }

    // Coroutine for smooth movement
    private IEnumerator MovePage(float offset)
    {
        isMoving = true;  // Set the flag to indicate that movement is happening

        RectTransform rectTransform = _Page.GetComponent<RectTransform>();
        Vector2 initialPosition;
        Vector2 targetPosition;

        if (rectTransform != null)
        {
            // UI element: move its anchoredPosition
            initialPosition = rectTransform.anchoredPosition;
            targetPosition = initialPosition + new Vector2(offset, 0);
        }
        else
        {
            // Regular GameObject: move its position in world space
            initialPosition = _Page.transform.position;
            targetPosition = initialPosition + new Vector2(offset, 0);
        }

        float elapsedTime = 0f;

        // Smooth movement over the given duration
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration); // Normalized time (0 to 1)

            if (rectTransform != null)
            {
                // Lerp for smooth UI movement
                rectTransform.anchoredPosition = Vector2.Lerp(initialPosition, targetPosition, t);
            }
            else
            {
                // Lerp for smooth world-space movement
                _Page.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            }

            yield return null;  // Wait until the next frame
        }

        // Ensure final position is accurate
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = targetPosition;
        }
        else
        {
            _Page.transform.position = targetPosition;
        }

        isMoving = false;  // Reset the flag when movement is complete
    }

}
