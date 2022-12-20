using UnityEngine;
using System.Collections;

public class ShakeEffect: MonoBehaviour
{
    [Header("Info")]
    private Vector3 _startPos;
    private float _timer;
    private Vector3 _randomPos;

    [Header("Settings")]
    [Range(0f, 2f)]
    private float _time = 0.2f;
    [Range(0f, 2f)]
    private float _distance = 0.1f;
    [Range(0f, 0.1f)]
    private float _delayBetweenShakes = 0f;

    private void Awake()
    {
        _startPos = transform.position;
    }

    private void OnValidate()
    {
        if (_delayBetweenShakes > _time)
            _delayBetweenShakes = _time;
    }

    [ContextMenu("Beginl")]
    public void Begin()
    {
        StopAllCoroutines();
        StartCoroutine(Shake());
    }
    

    public void StartShake(float _time, float _distance, float _delayetweenShakes)
    {
        StopAllCoroutines();
        StartCoroutine(Shake(_time,_distance,_delayetweenShakes));
    }

    private IEnumerator Shake(float _time,float _distance,float _delayetweenShakes)
    {
        _timer = 0f;

        while (_timer < _time)
        {
            _timer += Time.deltaTime;

            _randomPos = _startPos + (Random.insideUnitSphere * _distance);

            transform.position = _randomPos;

            if (_delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(_delayBetweenShakes);
            }
            else
            {
                yield return null;
            }
        }

        transform.position = _startPos;
    }
    private IEnumerator Shake()
    {
        _timer = 0f;

        while (_timer < _time)
        {
            _timer += Time.deltaTime;

            _randomPos = _startPos + (Random.insideUnitSphere * _distance);

            transform.position = _randomPos;

            if (_delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(_delayBetweenShakes);
            }
            else
            {
                yield return null;
            }
        }

        transform.position = _startPos;
    }

}