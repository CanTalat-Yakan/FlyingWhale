using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishScale : MonoBehaviour
{


    internal static IEnumerator Play(GameObject _go, AnimationCurve _curve, float _duration, float _factor, bool _delay = true)
    {
        Vector3 scale = _go.transform.localScale;
        float timer = 0;

        if (_delay)
            yield return new WaitForSeconds(Random.value * 0.1f);

        while (timer < _duration)
        {
            _go.transform.localScale = new Vector3(
                scale.x, 
                scale.y, 
                scale.z + _curve.Evaluate(timer.Remap(0, _duration, 0, 1)) * _factor);

            timer += Time.deltaTime;
            yield return null;
        }

        _go.transform.localScale = scale;


        yield return null;
    }
}
