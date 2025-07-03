using UnityEngine;

namespace Vun.UnityUtils
{
    public static class ColorUtils
    {
        #region Color

        /// <summary>
        /// Return a new <see cref="Color"/> with set <see cref="Color.r"/> value
        /// </summary>
        public static Color WithRed(this Color color, float r)
        {
            color.r = r;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color"/> with set <see cref="Color.g"/> value
        /// </summary>
        public static Color WithGreen(this Color color, float g)
        {
            color.g = g;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color"/> with set <see cref="Color.b"/> value
        /// </summary>
        public static Color WithBlue(this Color color, float b)
        {
            color.b = b;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color"/> with set <see cref="Color.a"/> value
        /// </summary>
        public static Color WithAlpha(this Color color, float a)
        {
            color.a = a;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color"/> with set <see cref="Color.r"/> and <see cref="Color.g"/> values
        /// </summary>
        public static Color WithRG(this Color color, float r, float g)
        {
            color.r = r;
            color.g = g;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color"/> with set <see cref="Color.r"/> and <see cref="Color.b"/> values
        /// </summary>
        public static Color WithRB(this Color color, float r, float b)
        {
            color.r = r;
            color.b = b;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color"/> with set <see cref="Color.r"/> and <see cref="Color.a"/> values
        /// </summary>
        public static Color WithRA(this Color color, float r, float a)
        {
            color.r = r;
            color.a = a;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color"/> with set <see cref="Color.g"/> and <see cref="Color.b"/> values
        /// </summary>
        public static Color WithGB(this Color color, float g, float b)
        {
            color.g = g;
            color.b = b;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color"/> with set <see cref="Color.g"/> and <see cref="Color.a"/> values
        /// </summary>
        public static Color WithGA(this Color color, float g, float a)
        {
            color.g = g;
            color.a = a;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color"/> with set <see cref="Color.b"/> and <see cref="Color.a"/> values
        /// </summary>
        public static Color WithBA(this Color color, float b, float a)
        {
            color.b = b;
            color.a = a;
            return color;
        }

        /// <summary>
        /// Compare 2 <see cref="Color"/> by each RGB value, with optional alpha value
        /// </summary>
        public static bool Approximately(this in Color a, in Color b, float epsilon = 0.001f, bool compareAlpha = true)
        {
            return Mathf.Abs(a.r - b.r) <= epsilon
                && Mathf.Abs(a.g - b.g) <= epsilon
                && Mathf.Abs(a.b - b.b) <= epsilon
                && (!compareAlpha || Mathf.Abs(a.a - b.a) <= epsilon);
        }

        #endregion

        #region Color32

        /// <summary>
        /// Return a new <see cref="Color32"/> with set <see cref="Color32.r"/> value
        /// </summary>
        public static Color32 WithRed(this Color32 color, byte r)
        {
            color.r = r;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color32"/> with set <see cref="Color32.g"/> value
        /// </summary>
        public static Color32 WithGreen(this Color32 color, byte g)
        {
            color.g = g;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color32"/> with set <see cref="Color32.b"/> value
        /// </summary>
        public static Color32 WithBlue(this Color32 color, byte b)
        {
            color.b = b;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color32"/> with set <see cref="Color32.a"/> value
        /// </summary>
        public static Color32 WithAlpha(this Color32 color, byte a)
        {
            color.a = a;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color32"/> with set <see cref="Color32.r"/> and <see cref="Color32.g"/> values
        /// </summary>
        public static Color32 WithRG(this Color32 color, byte r, byte g)
        {
            color.r = r;
            color.g = g;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color32"/> with set <see cref="Color32.r"/> and <see cref="Color32.b"/> values
        /// </summary>
        public static Color32 WithRB(this Color32 color, byte r, byte b)
        {
            color.r = r;
            color.b = b;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color32"/> with set <see cref="Color32.r"/> and <see cref="Color32.a"/> values
        /// </summary>
        public static Color32 WithRA(this Color32 color, byte r, byte a)
        {
            color.r = r;
            color.a = a;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color32"/> with set <see cref="Color32.g"/> and <see cref="Color32.b"/> values
        /// </summary>
        public static Color32 WithGB(this Color32 color, byte g, byte b)
        {
            color.g = g;
            color.b = b;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color32"/> with set <see cref="Color32.g"/> and <see cref="Color32.a"/> values
        /// </summary>
        public static Color32 WithGA(this Color32 color, byte g, byte a)
        {
            color.g = g;
            color.a = a;
            return color;
        }

        /// <summary>
        /// Return a new <see cref="Color32"/> with set <see cref="Color32.b"/> and <see cref="Color32.a"/> values
        /// </summary>
        public static Color32 WithBA(this Color32 color, byte b, byte a)
        {
            color.b = b;
            color.a = a;
            return color;
        }

        /// <summary>
        /// Compare 2 <see cref="Color32"/> by each RGB value, with optional alpha value
        /// </summary>
        public static bool Approximately(this in Color32 a, in Color32 b, byte epsilon = 0, bool compareAlpha = true)
        {
            return Mathf.Abs(a.r - b.r) <= epsilon
                && Mathf.Abs(a.g - b.g) <= epsilon
                && Mathf.Abs(a.b - b.b) <= epsilon
                && (!compareAlpha || Mathf.Abs(a.a - b.a) <= epsilon);
        }

        #endregion
    }
}