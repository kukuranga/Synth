using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBCheckConditionsItem : MonoBehaviour
{
    public bool _IsYellow; // if false is green

    [Header("Movement")]
    [SerializeField] private float _moveDuration = 0.6f;
    [SerializeField] private float _arcHeight = 1.2f;
    [SerializeField] private AnimationCurve _moveEase = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Destroy Pop")]
    [SerializeField] private float _popDuration = 0.15f;

    private void Start()
    {
        Transform target = _IsYellow
            ? VFX2Manager.Instance._yelowWaypoint
            : VFX2Manager.Instance._GreenWaypoint;

        StartCoroutine(MoveToTarget(target));
    }

    private IEnumerator MoveToTarget(Transform target)
    {
        Vector3 start = transform.position;
        Vector3 end = target.position;

        // slight random arc side/height so multiple items don't overlap perfectly
        Vector3 mid = Vector3.Lerp(start, end, 0.5f)
                       + Vector3.up * _arcHeight
                       + Vector3.right * Random.Range(-0.5f, 0.5f);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / _moveDuration;
            float e = _moveEase.Evaluate(Mathf.Clamp01(t));

            // quadratic bezier
            Vector3 a = Vector3.Lerp(start, mid, e);
            Vector3 b = Vector3.Lerp(mid, end, e);
            transform.position = Vector3.Lerp(a, b, e);

            yield return null;
        }

        transform.position = end;
        yield return StartCoroutine(PopAndDestroy());
    }

    private IEnumerator PopAndDestroy()
    {
        Vector3 startScale = transform.localScale;
        Vector3 bigScale = startScale * 1.3f;
        float half = _popDuration * 0.5f;

        for (float t = 0; t < half; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(startScale, bigScale, t / half);
            yield return null;
        }
        for (float t = 0; t < half; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(bigScale, Vector3.zero, t / half);
            yield return null;
        }

        // TODO: hook this up to your actual resource system
        if (_IsYellow)
            Game2Manager.Instance.AddYellowResource(1);
        else
            Game2Manager.Instance.AddGreenResource(1);


        Destroy(gameObject);
    }
}