using System;
using UnityEngine;

namespace SP.AsyncTweener
{
    public struct FloatTween : ITween<float>
    {
        public float from { get; set; }
        public float to { get; set; }
        public float duration { get; set; }
        public float delay { get; set; }
        public bool ignoreTimeScale { get; set; }

        public Func<float, float, float, float> interpolation =>
            isAngle ? Mathf.LerpAngle : Mathf.LerpUnclamped;

        public Func<float, float> easing { get; set; }
        public Action<float> onChanged { get; set; }
        public bool isAngle { get; set; }
    }
}
