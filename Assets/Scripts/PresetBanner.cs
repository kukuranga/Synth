using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PresetBanner : MonoBehaviour
{
    public TextMeshProUGUI _Text;

    public GameObject _PosStart;
    public GameObject _PosMiddle;
    public GameObject _PosEnd;


    private void Start()
    {
        if (GameManager.Instance._levelPreSet != LevelPreSet.Normal)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        } 

        switch (GameManager.Instance._levelPreSet)
        {
            case LevelPreSet.Rain:
                _Text.text = "Rain!!";
                break;
            case LevelPreSet.Dust:
                _Text.text = "Dust!!";
                break;
            case LevelPreSet.lava:
                _Text.text = "Lava!!";
                break;
            case LevelPreSet.gold:
                _Text.text = "Gold!!";
                break;
        }
    }

    //Called after the scene is loaded
    public void StartScene()
    {
        if (GameManager.Instance._levelPreSet != LevelPreSet.Normal)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        MoveObjectSequence();
    }

    public void MoveObjectSequence()
    {
        StartCoroutine(MoveSequenceCoroutine());
    }

    private IEnumerator MoveSequenceCoroutine()
    {
        // Move to start position instantly
        transform.position = _PosStart.transform.position;
        yield return new WaitForSeconds(3f); // Pause briefly if needed

        // Quickly move to the middle position
        yield return StartCoroutine(SmoothMove(transform, _PosMiddle.transform.position, 0.2f));

        // Slowly move for 2 seconds
        AudioManager.Instance.PlaySound("Terrain");
        yield return StartCoroutine(SmoothMove(transform, _PosMiddle.transform.position, 0.5f));

        // Quickly move to the end position
        yield return StartCoroutine(SmoothMove(transform, _PosEnd.transform.position, 0.2f));
    }

    private IEnumerator SmoothMove(Transform obj, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = obj.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            obj.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }

        obj.position = targetPosition; // Ensure the final position is exact
    }
}
