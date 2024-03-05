using System;
using UnityEngine;

namespace SP.AsyncTweener
{
    public struct FloatTweenSettings : ITweenSettings<float>
    {
        public float from { get; set; }
        public float to { get; set; }
        public float duration { get; set; }
        public float delay { get; set; }
        public bool ignoreTimeScale { get; set; }

        public Func<float, float, float, float> interpolation
        {
            get
            {
                if (isAngle)
                    return Mathf.LerpAngle;
                return Mathf.LerpUnclamped;
            }
        }

        public Func<float, float> easing { get; set; }
        public Action<float> onChanged { get; set; }

        public bool isAngle { get; set; }
    }

    public struct Float01TweenSettings : ITweenSettings<float>
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
