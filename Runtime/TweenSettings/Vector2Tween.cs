using System;
using UnityEngine;

namespace SP.AsyncTweener
{
    public struct Vector2Tween : ITween<Vector2>
    {
        public Vector2 from { get; set; }
        public Vector2 to { get; set; }
        public float duration { get; set; }
        public float delay { get; set; }
        public bool ignoreTimeScale { get; set; }
        public Func<Vector2, Vector2, float, Vector2> interpolation => Vector2.LerpUnclamped;
        public Func<float, float> easing { get; set; }
        public Action<Vector2> onChanged { get; set; }
    }
}
