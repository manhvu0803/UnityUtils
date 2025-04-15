﻿using System;
using System.Collections.Generic;

namespace Vun.UnityUtils
{
    /// <summary>
    /// A tracker for a value that is needed to be keep track of over an interval.
    /// Support max value cap to total amount per interval, interval between event broadcasts, and broadcast value threshold
    /// </summary>
    public class ValueTracker
    {
        /// <summary>
        /// Behaviour of <see cref="ValueTracker"/> when <see cref="ValueTracker.Accumulate"/> is called with an amount that exceed <see cref="ValueTracker.MaxTotalValuePerInterval"/>
        /// </summary>
        public enum MaxValueOption : byte
        {
            /// <summary>
            /// Ignore the added amount
            /// </summary>
            Ignore,

            /// <summary>
            /// Reduce the added amount so that <see cref="ValueTracker.AccumulatedValue"/> not exceed <see cref="ValueTracker.MaxTotalValuePerInterval"/>
            /// </summary>
            AddToCap
        }

        private struct ValueAddedRecord
        {
            public double Value;

            public double Timestamp;
        }

        /// <summary>
        /// Maximum <see cref="AccumulatedValue"/> between each <see cref="OnProcessingAccumulatedValue"/> broadcasts
        /// </summary>
        public readonly double MaxTotalValuePerInterval;

        /// <summary>
        /// The minimum amount of time in seconds between <see cref="OnProcessingAccumulatedValue"/> broadcasts
        /// </summary>
        public readonly double MinBroadcastInterval;

        /// <summary>
        /// The minimum value need to accumulate for <see cref="OnProcessingAccumulatedValue"/> broadcasts
        /// </summary>
        public readonly double MinBroadcastValue;

        public readonly MaxValueOption MaxValueBehaviour;

        private readonly Func<double> _timeGetter;

        /// <summary>
        /// Invoked in <see cref="Update"/> when the <see cref="MinBroadcastInterval"/> has passed and <see cref="MinBroadcastValue"/> is reached
        /// </summary>
        public event Action<double> OnProcessingAccumulatedValue;

        private readonly Queue<ValueAddedRecord> _recordQueue = new();

        private double _totalValue;

        private double _lastBroadcastTime;

        public double AccumulatedValue { get; private set; }

        /// <param name="timeGetter">Function to get the current timestamp</param>
        /// <param name="maxTotalValuePerInterval">See <see cref="MaxTotalValuePerInterval"/></param>
        /// <param name="minBroadcastInterval">See <see cref="MinBroadcastInterval"/></param>
        /// <param name="minBroadcastValue">See <see cref="MinBroadcastValue"/></param>
        /// <param name="maxValueBehaviour">See <see cref="MaxValueOption"/></param>
        public ValueTracker(
            Func<double> timeGetter,
            double maxTotalValuePerInterval = double.PositiveInfinity,
            double minBroadcastInterval = 1,
            double minBroadcastValue = 0,
            MaxValueOption maxValueBehaviour = MaxValueOption.Ignore)
        {
            _timeGetter = timeGetter;
            MaxTotalValuePerInterval = maxTotalValuePerInterval;
            MinBroadcastInterval = minBroadcastInterval;
            MinBroadcastValue = minBroadcastValue;
            MaxValueBehaviour = maxValueBehaviour;
        }

        /// <summary>
        /// Add <c>amount</c> to total and keep track of this event. If this added amount would exceed <see cref="MaxTotalValuePerInterval"/>, nothing happen
        /// </summary>
        public void Accumulate(double amount)
        {
            if (amount == 0 || _totalValue >= MaxTotalValuePerInterval)
            {
                return;
            }

            // Cap added amount to not exceed MaxTotalValuePerInterval
            if (_totalValue + amount >= MaxTotalValuePerInterval)
            {
                if (MaxValueBehaviour == MaxValueOption.Ignore)
                {
                    return;
                }

                amount = MaxTotalValuePerInterval - _totalValue;
            }

            _totalValue += amount;
            AccumulatedValue += amount;

            _recordQueue.Enqueue(new ValueAddedRecord
            {
                Value = amount,
                Timestamp = _timeGetter()
            });
        }

        /// <summary>
        /// Update the tracker and broadcast event if all condition is met (see <see cref="OnProcessingAccumulatedValue"/>)
        /// </summary>
        public void Update()
        {
            if (_recordQueue.Count <= 0)
            {
                return;
            }

            var currentTimestamp = _timeGetter();
            CheckAccumulatedAmount(currentTimestamp);
            var boundary = currentTimestamp - 1;
            var addEvent = _recordQueue.Peek();

            while (addEvent.Timestamp <= boundary && _recordQueue.Count > 0)
            {
                _recordQueue.Dequeue();
                _totalValue -= addEvent.Value;

                if (_recordQueue.Count > 0)
                {
                    addEvent = _recordQueue.Peek();
                }
            }
        }

        private void CheckAccumulatedAmount(double timestamp)
        {
            if (AccumulatedValue <= MinBroadcastValue || timestamp - _lastBroadcastTime < MinBroadcastInterval)
            {
                return;
            }

            OnProcessingAccumulatedValue?.Invoke(AccumulatedValue);
            AccumulatedValue = 0;
            _lastBroadcastTime = timestamp;
        }

        public void Reset()
        {
            _recordQueue.Clear();
            AccumulatedValue = 0;
            _totalValue = 0;
        }
    }
}