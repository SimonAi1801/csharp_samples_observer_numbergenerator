
using NumberGenerator.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberGenerator.Ui
{
    class Program
    {
        static void Main()
        {
            // Zufallszahlengenerator erstelltn
            RandomNumberGenerator numberGenerator = new RandomNumberGenerator(250);

            // Beobachter erstellen
            BaseObserver baseObserver = new BaseObserver(10);
            StatisticsObserver statisticsObserver = new StatisticsObserver( 20);
            RangeObserver rangeObserver = new RangeObserver(5, 200, 300);
            QuickTippObserver quickTippObserver = new QuickTippObserver();


            numberGenerator.NumberChanged += baseObserver.OnNextNumber;
            numberGenerator.NumberChanged += statisticsObserver.OnNextNumber;
            numberGenerator.NumberChanged += rangeObserver.OnNextNumber;
            numberGenerator.NumberChanged += quickTippObserver.OnNextNumber;

            // Nummerngenerierung starten
            numberGenerator.StartNumberGeneration();
            int[] tipps = quickTippObserver.QuickTippNumbers.OrderBy(_=>_).ToArray();
            // Resultat ausgeben

            Console.WriteLine("--------------------------------------- Result -----------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{nameof(StatisticsObserver)}: Recevied {statisticsObserver.CountOfNumbersReceived:D5} " +
                $"numbers ===> {nameof(statisticsObserver.Min)}: '{statisticsObserver.Min}', " +
                $"{nameof(statisticsObserver.Max)}: '{statisticsObserver.Max}', {nameof(statisticsObserver.Sum)}: " +
                $"'{statisticsObserver.Sum}', {nameof(statisticsObserver.Avg)}: '{statisticsObserver.Avg}'");

            Console.WriteLine($"{nameof(RangeObserver)}:      Recevied {rangeObserver.CountOfNumbersReceived:D5} " +
                $"numbers ===> There were '{rangeObserver.NumbersInRange}' numbers between '{rangeObserver.LowerRange}' " +
                $"and '{rangeObserver.UpperRange}'.");

            Console.WriteLine($"{nameof(QuickTippObserver)}:  Recevied {quickTippObserver.CountOfNumbersReceived:D5} " +
                $"numbers ===> Quick-Tipp is {tipps[0]} {tipps[1]} {tipps[2]} {tipps[3]} {tipps[4]} {tipps[5]}.");

            Console.ResetColor();
        }
    }
}
