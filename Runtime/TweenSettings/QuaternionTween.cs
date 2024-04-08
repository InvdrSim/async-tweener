using System;
using UnityEngine;

namespace SP.AsyncTweener
{
    public class QuaternionTween : ITween<Quaternion>
    {
        public Quaternion from { get; set; }
        public Quaternion to { get; set; }
        public float duration { get; set; }
        public float delay { get; set; }
        public bool ignoreTimeScale { get; set; }

        public Func<Quaternion, Quaternion, float, Quaternion> interpolation =>
            useSlerp ? Quaternion.SlerpUnclamped : Quaternion.LerpUnclamped;

        public Func<float, float> easing { get; set; }
        public Action<Quaternion> onChanged { get; set; }
        public bool useSlerp { get; set; }
    }
}
