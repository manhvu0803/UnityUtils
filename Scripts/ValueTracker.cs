using System;
using System.Collections.Generic;

namespace Vun.UnityUtils
{
    /// <summary>
    /// A tracker for a value that is needed to be keep track of over an interval.
    /// Support max value cap to total amount per interval, interval between event broadcasts, and broadcast value threshold
    /// </summary>
    public class ValueTracker
    {
        private struct AddEvent
        {
            public double Value;

            public double Timestamp;
        }

        public readonly double MaxTotalValuePerInterval;

        /// <summary>
        /// The minimum amount of time between process event broadcasts
        /// </summary>
        public readonly double MinBroadcastInterval;

        /// <summary>
        /// The minimum value need to accumulate for process event
        /// </summary>
        public readonly double MinBroadcastValue;

        private readonly Func<double> _timeGetter;

        /// <summary>
        /// Invoked in <see cref="Update"/> when the <see cref="MinBroadcastInterval"/> has passed and <see cref="MinBroadcastValue"/> is reached
        /// </summary>
        public event Action<double> OnProcessAccumulatedValue;

        private readonly Queue<AddEvent> _eventQueue = new();

        private double _totalValue;

        private double _lastBroadcastTime;

        public double AccumulatedValue { get; private set; }

        /// <param name="timeGetter">Function to get the current timestamp</param>
        /// <param name="maxTotalValuePerInterval">See <see cref="MaxTotalValuePerInterval"/></param>
        /// <param name="minBroadcastInterval">See <see cref="MinBroadcastInterval"/></param>
        /// <param name="minBroadcastValue">See <see cref="MinBroadcastValue"/></param>
        public ValueTracker(Func<double> timeGetter, double maxTotalValuePerInterval = double.MaxValue, double minBroadcastInterval = 1, double minBroadcastValue = 0)
        {
            MaxTotalValuePerInterval = maxTotalValuePerInterval;
            MinBroadcastInterval = minBroadcastInterval;
            MinBroadcastValue = minBroadcastValue;
            _timeGetter = timeGetter;
        }

        /// <summary>
        /// Add <c>amount</c> to total and keep track of the event. If this added amount would exceed <see cref="MaxTotalValuePerInterval"/>, nothing happen
        /// </summary>
        public void Accumulate(double amount)
        {
            if (_totalValue + amount >= MaxTotalValuePerInterval)
            {
                return;
            }

            _totalValue += amount;
            AccumulatedValue += amount;

            _eventQueue.Enqueue(new AddEvent
            {
                Value = amount,
                Timestamp = _timeGetter()
            });
        }

        /// <summary>
        /// Update the tracker and broadcast event if all condition is met (see <see cref="OnProcessAccumulatedValue"/>)
        /// </summary>
        public void Update()
        {
            if (_eventQueue.Count <= 0)
            {
                return;
            }

            var currentTimestamp = _timeGetter();
            CheckAccumulatedAmount(currentTimestamp);
            var boundary = currentTimestamp - 1;
            var addEvent = _eventQueue.Peek();

            while (addEvent.Timestamp <= boundary && _eventQueue.Count > 0)
            {
                _eventQueue.Dequeue();
                _totalValue -= addEvent.Value;

                if (_eventQueue.Count > 0)
                {
                    addEvent = _eventQueue.Peek();
                }
            }
        }

        private void CheckAccumulatedAmount(double timestamp)
        {
            if (AccumulatedValue <= MinBroadcastValue || timestamp - _lastBroadcastTime < MinBroadcastInterval)
            {
                return;
            }

            OnProcessAccumulatedValue?.Invoke(AccumulatedValue);
            AccumulatedValue = 0;
            _lastBroadcastTime = timestamp;
        }

        public void Reset()
        {
            _eventQueue.Clear();
            AccumulatedValue = 0;
            _totalValue = 0;
        }
    }
}