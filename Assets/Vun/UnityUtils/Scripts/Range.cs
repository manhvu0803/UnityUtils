using System;
using UnityEngine;

namespace Vun.UnityUtils
{
    /// <summary>
    /// A struct that ensure <see cref="Min"/> is always less than or equal to <see cref="Max"/>,
    /// have a custom drawer,
    /// and is interchangeable with <see cref="Vector2"/>
    /// </summary>
    [Serializable]
    public struct Range : IRange<float>
    {
        [SerializeField]
        private float _min;

        [SerializeField]
        private float _max;

        /// <summary>
        /// Get and set min value. If set value is larger than <see cref="Max"/>, also set <see cref="Max"/> to value
        /// </summary>
        public float Min
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
        public float Max
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
        public Range(float min, float max)
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

        public static implicit operator Vector2(in Range range)
        {
            return new Vector2(range.Min, range.Max);
        }

        public static implicit operator Range(in Vector2 vector)
        {
            return new Range(vector.x, vector.y);
        }

        public static explicit operator Vector2Int(in Range range)
        {
            return new Vector2Int((int)range.Min, (int)range.Max);
        }

        public static implicit operator Range(in Vector2Int vector)
        {
            return new Range(vector.x, vector.y);
        }

        #endregion
    }
}