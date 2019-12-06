using System;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher einfache Statistiken bereit stellt (Min, Max, Sum, Avg).
    /// </summary>
    public class StatisticsObserver : BaseObserver
    {
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Enthält das Minimum der generierten Zahlen.
        /// </summary>
        public int Min { get; private set; } = int.MaxValue;

        /// <summary>
        /// Enthält das Maximum der generierten Zahlen.
        /// </summary>
        public int Max { get; private set; } = int.MinValue;

        /// <summary>
        /// Enthält die Summe der generierten Zahlen.
        /// </summary>
        public int Sum { get; private set; }

        /// <summary>
        /// Enthält den Durchschnitt der generierten Zahlen.
        /// </summary>
        public int Avg
        {
            get
            {
                if (CountOfNumbersReceived == 0)
                {
                    throw new DivideByZeroException();
                }
                return Sum / CountOfNumbersReceived;
            }
        }

        #endregion

        #region Constructors

        public StatisticsObserver(IObservable numberGenerator, int countOfNumbersToWaitFor) : base(numberGenerator, countOfNumbersToWaitFor)
        {

        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{nameof(BaseObserver)} [CountOfNumbersReceived='{CountOfNumbersReceived}', CountOfNumbersToWaitFor='{CountOfNumbersToWaitFor}'] => {nameof(StatisticsObserver)} [Min='{Min}', Max='{Max}', Sum='{Sum}', Avg='{Avg}']";
        }
        public override void OnNextNumber(int number)
        {
            if (number <= Min)
            {
                Min = number;
            }
            if (number > Max)
            {
                Max = number;
            }
            Sum += number;
            base.OnNextNumber(number);
        }

        #endregion
    }
}
