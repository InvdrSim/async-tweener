using System;
using UnityEngine;

namespace SP.AsyncTweener
{
    /// <summary>
    ///     Contains different interpolation ease functions.
    /// </summary>
    public static class Easings
    {
        public enum Direction
        {
            In,
            Out,
            InOut
        }

        public enum Mode
        {
            Linear,
            Smoothstep,
            Quadratic,
            Cubic,
            Quartic,
            Quintic,
            Sinusoidal,
            Exponential,
            Circular,
            Elastic,
            Back,
            Bounce
        }

        public static Func<float, float> Get(Mode method, Direction direction = Direction.InOut)
        {
            switch (method)
            {
                case Mode.Linear:
                default:
                    return Linear;
                case Mode.Smoothstep:
                    return Smoothstep;
                case Mode.Quadratic:
                    switch (direction)
                    {
                        case Direction.In:
                            return Quadratic.In;
                        case Direction.Out:
                            return Quadratic.Out;
                        case Direction.InOut:
                        default:
                            return Quadratic.InOut;
                    }
                case Mode.Cubic:
                    switch (direction)
                    {
                        case Direction.In:
                            return Cubic.In;
                        case Direction.Out:
                            return Cubic.Out;
                        case Direction.InOut:
                        default:
                            return Cubic.InOut;
                    }
                case Mode.Quartic:
                    switch (direction)
                    {
                        case Direction.In:
                            return Quartic.In;
                        case Direction.Out:
                            return Quartic.Out;
                        case Direction.InOut:
                        default:
                            return Quartic.InOut;
                    }
                case Mode.Quintic:
                    switch (direction)
                    {
                        case Direction.In:
                            return Quintic.In;
                        case Direction.Out:
                            return Quintic.Out;
                        case Direction.InOut:
                        default:
                            return Quintic.InOut;
                    }
                case Mode.Sinusoidal:
                    switch (direction)
                    {
                        case Direction.In:
                            return Sinusoidal.In;
                        case Direction.Out:
                            return Sinusoidal.Out;
                        case Direction.InOut:
                        default:
                            return Sinusoidal.InOut;
                    }
                case Mode.Exponential:
                    switch (direction)
                    {
                        case Direction.In:
                            return Exponential.In;
                        case Direction.Out:
                            return Exponential.Out;
                        case Direction.InOut:
                        default:
                            return Exponential.InOut;
                    }
                case Mode.Circular:
                    switch (direction)
                    {
                        case Direction.In:
                            return Circular.In;
                        case Direction.Out:
                            return Circular.Out;
                        case Direction.InOut:
                        default:
                            return Circular.InOut;
                    }
                case Mode.Elastic:
                    switch (direction)
                    {
                        case Direction.In:
                            return Elastic.In;
                        case Direction.Out:
                            return Elastic.Out;
                        case Direction.InOut:
                        default:
                            return Elastic.InOut;
                    }
                case Mode.Back:
                    switch (direction)
                    {
                        case Direction.In:
                            return Back.In;
                        case Direction.Out:
                            return Back.Out;
                        case Direction.InOut:
                        default:
                            return Back.InOut;
                    }
                case Mode.Bounce:
                    switch (direction)
                    {
                        case Direction.In:
                            return Bounce.In;
                        case Direction.Out:
                            return Bounce.Out;
                        case Direction.InOut:
                        default:
                            return Bounce.InOut;
                    }
            }
        }

        public static Func<float, float, float, float> FromToInterpolationFunc(Func<float, float> interp)
        {
            return (from, to, amt) => { return Mathf.Lerp(from, to, interp(amt)); };
        }

        public static Func<float, float> Combine(Mode methodIn, Direction directionIn, Mode methodOut,
            Direction directionOut, Mode methodCombine, Direction directionCombine)
        {
            var inFunc = Get(methodIn, directionIn);
            var outFunc = Get(methodOut, directionOut);
            var combineFunc = FromToInterpolationFunc(Get(methodCombine, directionCombine));
            return amt => combineFunc(inFunc(amt), outFunc(amt), amt);
        }

        public static float Linear(float t)
        {
            return t;
        }

        public static float Smoothstep(float t)
        {
            return 3 * t * t - 2 * t * t * t;
        }

        public static class Quadratic
        {
            public static float In(float t)
            {
                return t * t;
            }

            public static float Out(float t)
            {
                return t * (2 - t);
            }

            public static float InOut(float t)
            {
                if ((t *= 2) < 1)
                    return 0.5f * t * t;
                return -0.5f * (--t * (t - 2) - 1);
            }
        }

        public static class Cubic
        {
            public static float In(float t)
            {
                return t * t * t;
            }

            public static float Out(float t)
            {
                return --t * t * t + 1;
            }

            public static float InOut(float t)
            {
                if ((t *= 2) < 1)
                    return 0.5f * t * t * t;
                return 0.5f * ((t -= 2) * t * t + 2);
            }
        }

        public static class Quartic
        {
            public static float In(float t)
            {
                return t * t * t * t;
            }

            public static float Out(float t)
            {
                return 1 - --t * t * t * t;
            }

            public static float InOut(float t)
            {
                if ((t *= 2) < 1)
                    return 0.5f * t * t * t * t;
                return -0.5f * ((t -= 2) * t * t * t - 2);
            }
        }

        public static class Quintic
        {
            public static float In(float t)
            {
                return t * t * t * t * t;
            }

            public static float Out(float t)
            {
                return --t * t * t * t * t + 1;
            }

            public static float InOut(float t)
            {
                if ((t *= 2) < 1)
                    return 0.5f * t * t * t * t * t;
                return 0.5f * ((t -= 2) * t * t * t * t + 2);
            }
        }

        public static class Sinusoidal
        {
            public static float In(float t)
            {
                return 1 - Mathf.Cos(t * Mathf.PI / 2);
            }

            public static float Out(float t)
            {
                return Mathf.Sin(t * Mathf.PI / 2);
            }

            public static float InOut(float t)
            {
                return 0.5f * (1 - Mathf.Cos(Mathf.PI * t));
            }
        }

        public static class Exponential
        {
            public static float In(float t)
            {
                return t == 0 ? 0 : Mathf.Pow(1024, t - 1);
            }

            public static float Out(float t)
            {
                return t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);
            }

            public static float InOut(float t)
            {
                if (t == 0)
                    return 0;
                if (t == 1)
                    return 1;
                if ((t *= 2) < 1)
                    return 0.5f * Mathf.Pow(1024, t - 1);
                return 0.5f * (-Mathf.Pow(2, -10 * (t - 1)) + 2);
            }
        }

        public static class Circular
        {
            public static float In(float t)
            {
                return 1 - Mathf.Sqrt(1 - t * t);
            }

            public static float Out(float t)
            {
                return Mathf.Sqrt(1 - --t * t);
            }

            public static float InOut(float t)
            {
                if ((t *= 2) < 1)
                    return -0.5f * (Mathf.Sqrt(1 - t * t) - 1);
                return 0.5f * (Mathf.Sqrt(1 - (t -= 2) * t) + 1);
            }
        }

        public static class Elastic
        {
            public static float In(float t)
            {
                if (t == 0)
                    return 0;
                if (t == 1)
                    return 1;
                return -Mathf.Pow(2, 10 * (t - 1)) * Mathf.Sin((t - 1.1f) * 5 * Mathf.PI);
            }

            public static float Out(float t)
            {
                if (t == 0)
                    return 0;
                if (t == 1)
                    return 1;
                return Mathf.Pow(2, -10 * t) * Mathf.Sin((t - 0.1f) * 5 * Mathf.PI) + 1;
            }

            public static float InOut(float t)
            {
                if (t == 0)
                    return 0;
                if (t == 1)
                    return 1;
                if ((t *= 2) < 1)
                    return -0.5f * Mathf.Pow(2, 10 * (t - 1)) * Mathf.Sin((t - 1.1f) * 5 * Mathf.PI);
                return 0.5f * Mathf.Pow(2, -10 * (t - 1)) * Mathf.Sin((t - 1.1f) * 5 * Mathf.PI) + 1;
            }
        }

        public static class Back
        {
            private const float s   = 1.70158f;
            private const float sio = s * 1.525f;

            public static float In(float t)
            {
                return t * t * ((s + 1) * t - s);
            }

            public static float Out(float t)
            {
                return --t * t * ((s + 1) * t + s) + 1;
            }

            public static float InOut(float t)
            {
                if ((t *= 2) < 1)
                    return 0.5f * (t * t * ((sio + 1) * t - sio));
                return 0.5f * ((t -= 2) * t * ((sio + 1) * t + sio) + 2);
            }
        }

        public static class Bounce
        {
            private const float tc1 = 1f / 2.75f;
            private const float tc2 = 2f / 2.75f;
            private const float tc3 = 2.5f / 2.75f;
            private const float ts2 = 1.5f / 2.75f;
            private const float ts3 = 2.25f / 2.75f;
            private const float ts4 = 2.625f / 2.75f;
            private const float ta2 = 0.75f;
            private const float ta3 = 0.9375f;
            private const float ta4 = 0.984375f;
            private const float f   = 7.5625f;

            public static float In(float t)
            {
                return 1 - Out(1 - t);
            }

            public static float Out(float t)
            {
                if (t < tc1)
                    return f * t * t;
                if (t < tc2)
                    return f * (t -= ts2) * t + ta2;
                if (t < tc3)
                    return f * (t -= ts3) * t + ta3;
                return f * (t -= ts4) * t + ta4;
            }

            public static float InOut(float t)
            {
                if (t < 0.5f)
                    return In(t * 2) * 0.5f;
                return Out(t * 2 - 1) * 0.5f + 0.5f;
            }
        }
    }
}
