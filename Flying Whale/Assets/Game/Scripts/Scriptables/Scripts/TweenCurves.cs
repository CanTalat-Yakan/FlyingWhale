using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Tween Info", fileName = "Tween Curves", order = 2)]
public class TweenCurves : ScriptableObject
{
    public AnimationCurve SquishSquash;
    public AnimationCurve Log;
}
