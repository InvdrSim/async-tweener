using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SP.AsyncTweener
{
    public static class Tweener
    {
        public static UniTask TweenAsync<T>(
            ITween<T> settings,
            CancellationToken cancellationToken = default)
            where T : struct
        {
            return TweenAsync(
                settings.from, settings.to,
                settings.duration, settings.delay, settings.ignoreTimeScale,
                settings.interpolation, settings.easing,
                settings.onChanged,
                cancellationToken);
        }

        public static async UniTask TweenAsync<T>(
            T from, T to,
            float duration, float delay, bool ignoreTimeScale,
            Func<T, T, float, T> interpolation, Func<float, float> easing,
            Action<T> onChanged,
            CancellationToken cancellationToken = default)
        {
            await UniTask.WaitForSeconds(delay, ignoreTimeScale, cancellationToken: cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            if (duration > 0)
            {
                easing ??= Interpolations.Linear;
                float elapsedTime = 0f;
                while (elapsedTime < duration)
                {
                    elapsedTime += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
                    float percent = Mathf.Clamp01(elapsedTime / duration);
                    float amount = easing(percent);
                    T value = interpolation(from, to, amount);
                    onChanged?.Invoke(value);
                    await UniTask.Yield(cancellationToken);
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
            else
            {
                onChanged?.Invoke(to);
            }
        }
    }
}
