using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBMoveRocks : MonoBehaviour
{
    public List<GameObject> _Rocks;
    public List<GameObject> _SpawnLocations;
    public GameObject _ScreenCentre;
    public float _MinSpeed;
    public float _MaxSpeed;
    public float _Duration;
    public int _Rate;

    public float _SpawnDelay = 0.3f; // Delay between each rock starting

    private void Start()
    {
        StartCoroutine(LoopRocksMovement());
    }

    IEnumerator LoopRocksMovement()
    {
        while(true)
        {
            MoveRocks();
            yield return new WaitForSeconds(_Rate);
        }
        yield return null;
    }

    public void MoveRocks()
    {
        StartCoroutine(MoveRocksRoutine());
    }

    IEnumerator MoveRocksRoutine()
    {
        if (_Rocks.Count == 0 || _SpawnLocations.Count == 0) yield break;

        int rockCount = Random.Range(1, _Rocks.Count + 1);

        List<GameObject> availableRocks = new List<GameObject>(_Rocks);

        for (int i = 0; i < rockCount; i++)
        {
            int rockIndex = Random.Range(0, availableRocks.Count);
            GameObject rock = availableRocks[rockIndex];
            availableRocks.RemoveAt(rockIndex);

            int spawnIndex = Random.Range(0, _SpawnLocations.Count);
            Transform spawn = _SpawnLocations[spawnIndex].transform;

            rock.transform.position = spawn.position;
            rock.SetActive(true);

            float speed = Random.Range(_MinSpeed, _MaxSpeed);

            StartCoroutine(MoveRockCoroutine(rock, speed));

            // Delay before spawning the next rock
            yield return new WaitForSeconds(_SpawnDelay);
        }
    }

    IEnumerator MoveRockCoroutine(GameObject rock, float speed)
    {
        float timer = 0f;

        Vector3 direction = (_ScreenCentre.transform.position - rock.transform.position).normalized;

        while (timer < _Duration && rock != null)
        {
            // Constant speed movement
            rock.transform.position += direction * speed * Time.deltaTime;

            timer += Time.deltaTime;
            yield return null;
        }
    }
}
