using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SP.AsyncTweener
{
    public static class Extensions
    {
        private static readonly Dictionary<Component, Dictionary<string, CancellationTokenSource>> k_Tweens = new();
        private static readonly object k_TweensLock = new();

        /// <summary>
        ///     Tweens the position of a transform to the specified position over the given duration.
        /// </summary>
        /// <param name="t">The transform to tween.</param>
        /// <param name="to">The target position to tween to.</param>
        /// <param name="duration">The duration of the tween.</param>
        /// <param name="delay">The delay before starting the tween.</param>
        /// <param name="ignoreTimeScale">Whether to ignore the timescale.</param>
        /// <param name="easing">The easing function to use for the tween.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A UniTask that represents the asynchronous operation.</returns>
        public static UniTask TweenPositionAsync(this Transform t, Vector3 to, float duration, float delay = 0,
            bool ignoreTimeScale = false, Func<float, float> easing = null,
            CancellationToken cancellationToken = default)
        {
            easing ??= Easings.Linear;
            return TweenInternalAsync(t, "Position", new Vector3Tween
            {
                from = t.position,
                to = to,
                duration = duration,
                delay = delay,
                ignoreTimeScale = ignoreTimeScale,
                easing = easing,
                onChanged = v => t.position = v
            }, cancellationToken);
        }

        /// <summary>
        ///     Tweens the rotation of a transform to the specified rotation over the given duration.
        /// </summary>
        /// <param name="t">The transform to tween.</param>
        /// <param name="to">The target rotation to tween to.</param>
        /// <param name="duration">The duration of the tween.</param>
        /// <param name="delay">The delay before starting the tween.</param>
        /// <param name="ignoreTimeScale">Whether to ignore the timescale.</param>
        /// <param name="easing">The easing function to use for the tween.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A UniTask that represents the asynchronous operation.</returns>
        public static UniTask TweenRotationAsync(this Transform t, Quaternion to, float duration, float delay = 0,
            bool ignoreTimeScale = false, Func<float, float> easing = null,
            CancellationToken cancellationToken = default)
        {
            easing ??= Easings.Linear;
            return TweenInternalAsync(t, "Rotation", new QuaternionTween
            {
                from = t.rotation,
                to = to,
                duration = duration,
                delay = delay,
                ignoreTimeScale = ignoreTimeScale,
                easing = easing,
                onChanged = v => t.rotation = v
            }, cancellationToken);
        }

        /// <summary>
        ///     Tweens the alpha value of a canvas group to the specified value over the given duration.
        /// </summary>
        /// <param name="canvasGroup">The canvas group to tween.</param>
        /// <param name="to">The target alpha value to tween to.</param>
        /// <param name="duration">The duration of the tween.</param>
        /// <param name="delay">The delay before starting the tween.</param>
        /// <param name="ignoreTimeScale">Whether to ignore the timescale.</param>
        /// <param name="easing">The easing function to use for the tween.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A UniTask that represents the asynchronous operation.</returns>
        public static UniTask TweenAlphaAsync(this CanvasGroup canvasGroup, float to, float duration, float delay = 0,
            bool ignoreTimeScale = false, Func<float, float> easing = null,
            CancellationToken cancellationToken = default)
        {
            easing ??= Easings.Linear;
            return TweenInternalAsync(canvasGroup, "Alpha",
                new FloatTween
                {
                    from = canvasGroup.alpha,
                    to = to,
                    duration = duration,
                    delay = delay,
                    ignoreTimeScale = ignoreTimeScale,
                    easing = easing,
                    onChanged = v => canvasGroup.alpha = v
                }, cancellationToken);
        }

        /// <summary>
        ///     Tweens the color of a graphic to the specified color over the given duration.
        /// </summary>
        /// <param name="graphic">The graphic whose color will be tweened.</param>
        /// <param name="to">The target color to tween to.</param>
        /// <param name="duration">The duration of the tween.</param>
        /// <param name="delay">The delay before starting the tween.</param>
        /// <param name="ignoreTimeScale">Whether to ignore the timescale.</param>
        /// <param name="easing">The easing function to use for the tween.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A UniTask that represents the asynchronous operation.</returns>
        public static UniTask TweenColorAsync(this Graphic graphic, Color to, float duration, float delay = 0,
            bool ignoreTimeScale = false, Func<float, float> easing = null,
            CancellationToken cancellationToken = default)
        {
            easing ??= Easings.Linear;
            return TweenInternalAsync(graphic, "Color",
                new ColorTween
                {
                    from = graphic.color,
                    to = to,
                    duration = duration,
                    delay = delay,
                    ignoreTimeScale = ignoreTimeScale,
                    easing = easing,
                    onChanged = v => graphic.color = v
                }, cancellationToken);
        }

        /// <summary>
        ///     Tweens the alpha component of a graphic's color to the specified value over the given duration.
        /// </summary>
        /// <param name="graphic">The graphic whose alpha you want to tween.</param>
        /// <param name="to">The target alpha value to tween to.</param>
        /// <param name="duration">The duration of the tween.</param>
        /// <param name="delay">The delay before starting the tween.</param>
        /// <param name="ignoreTimeScale">Whether to ignore the timescale.</param>
        /// <param name="easing">The easing function to use for the tween.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A UniTask that represents the asynchronous operation.</returns>
        public static UniTask TweenAlphaAsync(this Graphic graphic, float to, float duration, float delay = 0,
            bool ignoreTimeScale = false, Func<float, float> easing = null,
            CancellationToken cancellationToken = default)
        {
            easing ??= Easings.Linear;
            return TweenInternalAsync(graphic, "Color", new FloatTween
            {
                from = graphic.color.a,
                to = to,
                duration = duration,
                delay = delay,
                ignoreTimeScale = ignoreTimeScale,
                easing = easing,
                onChanged = v =>
                {
                    var color = graphic.color;
                    color.a = v;
                    graphic.color = color;
                }
            }, cancellationToken);
        }

        /// <summary>
        ///     Executes a tween on the specified property of a component.
        /// </summary>
        /// <typeparam name="T">The type of the value being tweened.</typeparam>
        /// <param name="component">The component to apply the tween to.</param>
        /// <param name="property">The property of the component to tween.</param>
        /// <param name="tween">The tween animation to execute.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A UniTask that represents the asynchronous operation.</returns>
        private static async UniTask TweenInternalAsync<T>(Component component, string property, ITween<T> tween,
            CancellationToken cancellationToken)
            where T : struct
        {
            StopTween(component, property);
            // then we create a new CTS and linked CTS to handle both internal and external cancellation
            // the linked/combined token is then passed to TweenAsync below
            using (var linkedCts = CreateLinkedCancellationSource(component, property, cancellationToken))
            {
                await Tweener.TweenAsync(tween, linkedCts.Token);
                StopTween(component, property);
            }
        }

        /// <summary>
        ///     Stops the tween associated with a specific component and property.
        /// </summary>
        /// <param name="component">The component for which the tween needs to be stopped.</param>
        /// <param name="property">The property associated with the tween to stop.</param>
        private static void StopTween(Component component, string property)
        {
            lock (k_TweensLock)
            {
                if (k_Tweens.TryGetValue(component, out var propertyToToken))
                {
                    if (propertyToToken.Remove(property, out var cts))
                    {
                        cts.Cancel();
                        cts.Dispose();
                        Debug.Log($"{component} => '{property}' => Tween stopped");
                    }
                    else
                    {
                        Debug.LogWarning($"{component} => '{property}' => no CTS");
                    }
                }
                else
                {
                    Debug.LogWarning($"{component} => no property '{property}'");
                }
            }
        }

        /// <summary>
        ///     Creates or retrieves a cancellation token source for a specific component and property pair.
        /// </summary>
        /// <param name="component">The component associated with the tween operation.</param>
        /// <param name="property">The property being tweened.</param>
        /// <returns>A cancellation token source that can be used to cancel the tween operation.</returns>
        private static CancellationTokenSource CreateTweenCancellationTokenSource(Component component, string property)
        {
            lock (k_TweensLock)
            {
                if (!k_Tweens.TryGetValue(component, out var propertyToCts))
                {
                    propertyToCts = new Dictionary<string, CancellationTokenSource>();
                    k_Tweens.Add(component, new Dictionary<string, CancellationTokenSource>());
                }

                if (!propertyToCts.TryGetValue(property, out var internalSource))
                {
                    internalSource = new CancellationTokenSource();
                    propertyToCts.Add(property, internalSource);
                }

                return internalSource;
            }
        }

        private static CancellationTokenSource CreateLinkedCancellationSource(Component component, string property,
            CancellationToken otherToken)
        {
            var internalToken = CreateTweenCancellationTokenSource(component, property);
            return CancellationTokenSource.CreateLinkedTokenSource(internalToken.Token, otherToken);
        }
    }
}