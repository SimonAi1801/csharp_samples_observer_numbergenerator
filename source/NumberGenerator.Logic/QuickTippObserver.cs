﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Beobachter, welcher auf einen vollständigen Quick-Tipp wartet: 6 unterschiedliche Zahlen zw. 1 und 45.
    /// </summary>
    public class QuickTippObserver
    {
        #region Fields

        private int _tippCount = 0;

        #endregion

        #region Properties

        public List<int> QuickTippNumbers { get; private set; }
        public int CountOfNumbersReceived { get; private set; }

        #endregion

        #region Constructor

        public QuickTippObserver()
        {
            QuickTippNumbers = new List<int>();
        }

        #endregion

        #region Methods

        public void OnNextNumber(object sender, int number)
        {
            CountOfNumbersReceived++;
            if (number >= 1 && number <= 45 && !QuickTippNumbers.Contains(number))
            {
                QuickTippNumbers.Add(number);
                _tippCount++;
            }

            if (_tippCount == 6)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   >> {this.GetType().Name}: Got a full Quick-Tipp => I am not interested in new numbers anymore => Detach().");
                Console.ResetColor();
                (sender as RandomNumberGenerator).NumberChanged -= OnNextNumber;
            }
        }

        public override string ToString()
        {
            return $"{nameof(QuickTippNumbers)}: {QuickTippNumbers}, {nameof(QuickTippNumbers)}: {QuickTippNumbers[_tippCount]}";
        }
        #endregion
    }
}
