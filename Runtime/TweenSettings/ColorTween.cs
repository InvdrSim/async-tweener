using System;
using UnityEngine;

namespace SP.AsyncTweener
{
    public struct ColorTween : ITween<Color>
    {
        public Color from { get; set; }
        public Color to { get; set; }
        public float duration { get; set; }
        public float delay { get; set; }
        public bool ignoreTimeScale { get; set; }

        public Func<Color, Color, float, Color> interpolation =>
            mode switch
            {
                Mode.RGB => LerpRGB,
                Mode.Alpha => LerpAlpha,
                _ => Color.LerpUnclamped
            };


        public Func<float, float> easing { get; set; }
        public Action<Color> onChanged { get; set; }
        public Mode mode { get; set; }

        private static Color LerpRGB(Color from, Color to, float amount)
        {
            var result = Color.LerpUnclamped(from, to, amount);
            result.a = from.a;
            return result;
        }

        private static Color LerpAlpha(Color from, Color to, float amount)
        {
            float alpha = Mathf.LerpUnclamped(from.a, to.a, amount);
            from.a = alpha; // can change here because Color is a struct and passed by value
            return from;
        }

        public enum Mode
        {
            All,
            RGB,
            Alpha
        }
    }
}
