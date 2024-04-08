using System;

namespace SP.AsyncTweener
{
    public struct Tween<T> : ITween<T> where T : struct
    {
        public T from { get; set; }
        public T to { get; set; }
        public float duration { get; set; }
        public float delay { get; set; }
        public bool ignoreTimeScale { get; set; }
        public Func<T, T, float, T> interpolation { get; set; }
        public Func<float, float> easing { get; set; }
        public Action<T> onChanged { get; set; }
        public Action<T> onFinished { get; set; }
    }
}
