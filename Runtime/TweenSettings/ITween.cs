using System;

namespace SP.AsyncTweener
{
    public interface ITween<T> where T : struct
    {
        T from { get; }
        T to { get; }
        float duration { get; }
        float delay { get; }
        bool ignoreTimeScale { get; }
        Func<T, T, float, T> interpolation { get; }
        Func<float, float> easing { get; }
        Action<T> onChanged { get; }
    }
}
