using System;
using UnityEngine;

namespace Vun.UnityUtils
{
    /// <summary>
    /// A struct that ensure <see cref="Min"/> is always less than or equal to <see cref="Max"/>,
    /// have a custom drawer, and is interchangeable with <see cref="Vector2Int"/>
    /// </summary>
    [Serializable]
    public struct IntRange : IRange<int>
    {
        private int _min;

        private int _max;

        /// <summary>
        /// Get and set min value. If set value is larger than <see cref="Max"/>, also set <see cref="Max"/> to value
        /// </summary>
        public int Min
        {
            get => _min;
            set
            {
                _min = value;

                if (value > _max)
                {
                    _max = value;
                }
            }
        }

        /// <summary>
        /// Get and set max value. If set value is smaller than <see cref="Min"/>, also set <see cref="Min"/> to value
        /// </summary>
        public int Max
        {
            get => _max;
            set
            {
                _max = value;

                if (value < _min)
                {
                    _min = value;
                }
            }
        }

        /// <summary>
        /// If <c>min</c> is greater than <c>max</c>, they're swapped
        /// </summary>
        public IntRange(int min, int max)
        {
            if (min > max)
            {
                (min, max) = (max, min);
            }

            _min = min;
            _max = max;
        }

        public override string ToString() => $"{_min} : {_max}";

        #region Converters

        public static implicit operator Range(in IntRange range)
        {
            return new Range(range.Min, range.Max);
        }

        public static implicit operator Vector2(in IntRange range)
        {
            return new Vector2(range.Min, range.Max);
        }

        public static explicit operator IntRange(in Vector2 vector)
        {
            return new IntRange((int)vector.x, (int)vector.y);
        }

        public static explicit operator IntRange(in Range range)
        {
            return new IntRange((int)range.Min, (int)range.Max);
        }

        public static implicit operator Vector2Int(in IntRange range)
        {
            return new Vector2Int(range.Min, range.Max);
        }

        public static implicit operator IntRange(in Vector2Int vector)
        {
            return new IntRange(vector.x, vector.y);
        }

        #endregion
    }
}