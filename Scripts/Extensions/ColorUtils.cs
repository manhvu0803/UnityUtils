using UnityEngine;

namespace Vun.UnityUtils
{
    public static class ColorUtils
    {
        /// <summary>
        /// Return a new <c>Color</c> with set red value
        /// </summary>
        public static Color WithR(this Color color, float r)
        {
            color.r = r;
            return color;
        }

        /// <summary>
        /// Return a new <c>Color</c> with set green value
        /// </summary>
        public static Color WithG(this Color color, float g)
        {
            color.g = g;
            return color;
        }

        /// <summary>
        /// Return a new <c>Color</c> with set blue value
        /// </summary>
        public static Color WithB(this Color color, float b)
        {
            color.b = b;
            return color;
        }

        /// <summary>
        /// Return a new <c>Color</c> with set alpha value
        /// </summary>
        public static Color WithA(this Color color, float a)
        {
            color.a = a;
            return color;
        }

        /// <summary>
        /// Return a new <c>Color</c> with set alpha value
        /// </summary>
        public static Color WithRG(this Color color, float r, float g)
        {
            color.r = r;
            color.g = g;
            return color;
        }

        /// <summary>
        /// Return a new <c>Color</c> with set alpha value
        /// </summary>
        public static Color WithRB(this Color color, float r, float b)
        {
            color.r = r;
            color.b = b;
            return color;
        }

        /// <summary>
        /// Return a new <c>Color</c> with set alpha value
        /// </summary>
        public static Color WithRA(this Color color, float r, float a)
        {
            color.r = r;
            color.a = a;
            return color;
        }

        /// <summary>
        /// Return a new <c>Color</c> with set alpha value
        /// </summary>
        public static Color WithGB(this Color color, float g, float b)
        {
            color.g = g;
            color.b = b;
            return color;
        }

        /// <summary>
        /// Return a new <c>Color</c> with set alpha value
        /// </summary>
        public static Color WithGA(this Color color, float g, float a)
        {
            color.g = g;
            color.a = a;
            return color;
        }

        /// <summary>
        /// Return a new <c>Color</c> with set alpha value
        /// </summary>
        public static Color WithBA(this Color color, float b, float a)
        {
            color.b = b;
            color.a = a;
            return color;
        }

        /// <summary>
        /// Compare 2 <c>Color</c> by each RGBA value
        /// </summary>
        public static bool Approximately(this Color a, Color b, float epsilon = 0.001f, bool compareAlpha = true)
        {
            return Mathf.Abs(a.r - b.r) <= epsilon
                && Mathf.Abs(a.g - b.g) <= epsilon
                && Mathf.Abs(a.b - b.b) <= epsilon
                && (!compareAlpha || Mathf.Abs(a.a - b.a) <= epsilon);
        }
    }
}