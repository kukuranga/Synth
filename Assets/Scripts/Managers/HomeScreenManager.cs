using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeScreenManager : MonoBehaviour
{
    public TextMeshProUGUI _ShinyAchievementText;

    // Speed of the color cycle
    public float cycleSpeed = 1f;

    private void Start()
    {
        AchievementManager.Instance.CheckAchievements(); 
        _ShinyAchievementText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (AchievementManager.Instance.HasPendingAchievements())
        {
            _ShinyAchievementText.gameObject.SetActive(true);

            // Calculate t value based on time and speed
            float t = Mathf.PingPong(Time.time * cycleSpeed, 1);

            // Define primary colors
            Color color1 = new Color(0.9f, 0f, 0.7f); // pink
            Color color2 = new Color(1f, 1f, 0f); // yellow
            Color color3 = new Color(0.9f, 0.5f, 0.1f); // Blue

            // Interpolate between colors
            Color topLeft = Color.Lerp(color1, color2, t);
            Color topRight = Color.Lerp(color2, color3, t);
            Color bottomLeft = Color.Lerp(color3, color1, t);
            Color bottomRight = Color.Lerp(topLeft, bottomLeft, t);

            // Set the color gradient
            VertexGradient gradient = new VertexGradient(topLeft, topRight, bottomLeft, bottomRight);
            _ShinyAchievementText.colorGradient = gradient;
        }
        else
        {
            _ShinyAchievementText.gameObject.SetActive(false);
        }
    }

    public void AchievementTabOpened()
    {
        AchievementManager.Instance.OpenAchievementsTab();
    }
}
