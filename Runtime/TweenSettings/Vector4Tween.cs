using System;
using UnityEngine;

namespace SP.AsyncTweener
{
    public class Vector4Tween : ITween<Vector4>
    {
        public Vector4 from { get; set; }
        public Vector4 to { get; set; }
        public float duration { get; set; }
        public float delay { get; set; }
        public bool ignoreTimeScale { get; set; }
        public Func<Vector4, Vector4, float, Vector4> interpolation => Vector4.LerpUnclamped;
        public Func<float, float> easing { get; set; }
        public Action<Vector4> onChanged { get; set; }
    }
}
