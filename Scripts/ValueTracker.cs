using System;
using System.Collections.Generic;

namespace Vun.UnityUtils
{
    /// <summary>
    /// A class for a value that is needed to be keep track of over an interval.
    /// Support max value cap to total amount per interval, interval between event broadcasts, and broadcast value threshold.
    /// Doesn't support negative values
    /// </summary>
    public class ValueTracker
    {
        private static double IgnoreAddAmount(ValueTracker tracker, double amount)
        {
            return 0;
        }

        /// <summary>
        /// Return a new add amount that will not exceed <see cref="MaxTotalValuePerInterval"/>
        /// </summary>
        public static double CapAddAmount(ValueTracker tracker, double amount)
        {
            return tracker.MaxTotalValuePerInterval - tracker.TotalValue;
        }

        private struct ValueAddedRecord
        {
            public double Value;

            public double Timestamp;
        }

        /// <summary>
        /// Maximum <see cref="AccumulatedValue"/> during each <see cref="AccumulateInterval"/>
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

        /// <summary>
        /// The duration to keep track of accumulated values. In seconds
        /// </summary>
        public readonly double AccumulateInterval;

        /// <summary>
        /// Invoked in <see cref="Update"/> when the <see cref="MinBroadcastInterval"/> has passed and <see cref="MinBroadcastValue"/> is reached
        /// </summary>
        public event Action<double> OnProcessingAccumulatedValue;

        private readonly Queue<ValueAddedRecord> _recordQueue = new();

        private double _lastBroadcastTime;

        private readonly Func<double> _timeGetter;

        private readonly Func<ValueTracker, double, double> _addAmountProcessor;

        public double TotalValue { get; private set; }

        public double AccumulatedValue { get; private set; }

        /// <param name="timeGetter">Function to get the current timestamp</param>
        /// <param name="accumulateInterval">See <see cref="AccumulateInterval"/></param>
        /// <param name="maxTotalValuePerInterval">See <see cref="MaxTotalValuePerInterval"/></param>
        /// <param name="minBroadcastInterval">See <see cref="MinBroadcastInterval"/></param>
        /// <param name="minBroadcastValue">See <see cref="MinBroadcastValue"/></param>
        /// <param name="maxValueBehaviour">See <see cref="MaxValueAction"/></param>
        /// <param name="addAmountProcessor">
        /// Function to process the add amount in case it exceed <see cref="MaxTotalValuePerInterval"/>.
        /// The 1st param is this instance, the 2nd param is the add amount, return value is a new add amount.
        /// If null, the add amount will be ignored.
        /// <see cref="CapAddAmount"/> can be used here to reduce the add amount instead
        /// </param>
        public ValueTracker(
            Func<double> timeGetter,
            double accumulateInterval = 1,
            double maxTotalValuePerInterval = double.PositiveInfinity,
            double minBroadcastInterval = 1,
            double minBroadcastValue = 0,
            Func<ValueTracker, double, double> addAmountProcessor = null)
        {
            _timeGetter = timeGetter;
            AccumulateInterval = accumulateInterval;
            MaxTotalValuePerInterval = maxTotalValuePerInterval;
            MinBroadcastInterval = minBroadcastInterval;
            MinBroadcastValue = minBroadcastValue;
            _addAmountProcessor = addAmountProcessor ?? IgnoreAddAmount;
        }

        /// <summary>
        /// Add <c>amount</c> to total and keep track of this event. If this added amount would exceed <see cref="MaxTotalValuePerInterval"/>, nothing happen
        /// </summary>
        public void Accumulate(double amount)
        {
            if (TotalValue >= MaxTotalValuePerInterval)
            {
                return;
            }

            if (amount + TotalValue > MaxTotalValuePerInterval)
            {
                amount = _addAmountProcessor.Invoke(this, amount);
            }

            if (amount <= 0)
            {
                return;
            }

            TotalValue += amount;
            AccumulatedValue += amount;

            _recordQueue.Enqueue(new ValueAddedRecord
            {
                Value = amount,
                Timestamp = _timeGetter.Invoke()
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
            var boundary = currentTimestamp - AccumulateInterval;
            var addEvent = _recordQueue.Peek();

            while (addEvent.Timestamp <= boundary && _recordQueue.Count > 0)
            {
                _recordQueue.Dequeue();
                TotalValue -= addEvent.Value;

                if (_recordQueue.Count > 0)
                {
                    addEvent = _recordQueue.Peek();
                }
            }
        }

        private void CheckAccumulatedAmount(double timestamp)
        {
            if (AccumulatedValue < MinBroadcastValue || timestamp - _lastBroadcastTime < MinBroadcastInterval)
            {
                return;
            }

            OnProcessingAccumulatedValue?.Invoke(AccumulatedValue);
            AccumulatedValue = 0;
            _lastBroadcastTime = timestamp;
        }

        /// <summary>
        /// Revert this tracker to before any value is accumulated
        /// </summary>
        public void Reset()
        {
            _recordQueue.Clear();
            AccumulatedValue = 0;
            TotalValue = 0;
        }
    }
}