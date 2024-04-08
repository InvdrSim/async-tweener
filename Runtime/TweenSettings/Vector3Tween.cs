using System;
using UnityEngine;

namespace SP.AsyncTweener
{
    public struct Vector3Tween : ITween<Vector3>
    {
        public Vector3 from { get; set; }
        public Vector3 to { get; set; }
        public float duration { get; set; }
        public float delay { get; set; }
        public bool ignoreTimeScale { get; set; }
        public Func<Vector3, Vector3, float, Vector3> interpolation => Vector3.LerpUnclamped;
        public Func<float, float> easing { get; set; }
        public Action<Vector3> onChanged { get; set; }
    }
}
