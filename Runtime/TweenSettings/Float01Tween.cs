using System;
using UnityEngine;

namespace SP.AsyncTweener
{
    public struct Float01Tween : ITween<float>
    {
        public float from => 0;
        public float to => 1;
        public float duration { get; set; }
        public float delay { get; set; }
        public bool ignoreTimeScale { get; set; }
        public Func<float, float, float, float> interpolation => Mathf.LerpUnclamped;
        public Func<float, float> easing { get; set; }
        public Action<float> onChanged { get; set; }
    }
}
