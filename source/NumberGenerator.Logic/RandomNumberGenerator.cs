﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NumberGenerator.Logic
{
    /// <summary>
    /// Implementiert einen Nummern-Generator, welcher Zufallszahlen generiert.
    /// Es können sich Beobachter registrieren, welche über eine neu generierte Zufallszahl benachrichtigt werden.
    /// Zwischen der Generierung der einzelnen Zufallsnzahlen erfolgt jeweils eine Pause.
    /// Die Generierung erfolgt so lange, solange Beobachter registriert sind.
    /// </summary>
    public class RandomNumberGenerator : IObservable
    {
        #region Constants

        private static readonly int DEFAULT_SEED = DateTime.Now.Millisecond;
        private const int DEFAULT_DELAY = 500;
        private const int RANDOM_MIN_VALUE = 1;
        private const int RANDOM_MAX_VALUE = 1000;

        #endregion

        #region Fields
        private readonly Random _random;
        private readonly IList<IObserver> _observers;
        private int _delay;
        #endregion

        #region Constructors

        /// <summary>
        /// Initialisiert eine neue Instanz eines NumberGenerator-Objekts
        /// </summary>
        public RandomNumberGenerator() : this(DEFAULT_DELAY, DEFAULT_SEED)
        {

        }

        /// <summary>
        /// Initialisiert eine neue Instanz eines NumberGenerator-Objekts
        /// </summary>
        /// <param name="delay">Enthält die Zeit zw. zwei Generierungen in Millisekunden.</param>
        public RandomNumberGenerator(int delay) : this(delay, DEFAULT_SEED)
        {

        }

        /// <summary>
        /// Initialisiert eine neue Instanz eines NumberGenerator-Objekts
        /// </summary>
        /// <param name="delay">Enthält die Zeit zw. zwei Generierungen in Millisekunden.</param>
        /// <param name="seed">Enthält die Initialisierung der Zufallszahlengenerierung.</param>
        public RandomNumberGenerator(int delay, int seed)
        {
            _observers = new List<IObserver>();
            _random = new Random(seed);
            _delay = delay;
        }

        #endregion

        #region Methods

        #region IObservable Members

        /// <summary>
        /// Fügt einen Beobachter hinzu.
        /// </summary>
        /// <param name="observer">Der Beobachter, welcher benachricht werden möchte.</param>
        public void Attach(IObserver observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            if (_observers.Contains(observer))
            {
                throw new InvalidOperationException();
            }

            _observers.Add(observer);
        }

        /// <summary>
        /// Entfernt einen Beobachter.
        /// </summary>
        /// <param name="observer">Der Beobachter, welcher nicht mehr benachrichtigt werden möchte</param>
        public void Detach(IObserver observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException();
            }

            if (!_observers.Contains(observer))
            {
                throw new InvalidOperationException();
            }

            _observers.Remove(observer);
        }

        /// <summary>
        /// Benachrichtigt die registrierten Beobachter, dass eine neue Zahl generiert wurde.
        /// </summary>
        /// <param name="number">Die generierte Zahl.</param>
        public void NotifyObservers(int number)
        {
            for (int i = 0; i < _observers.Count; i++)
            {
                _observers[i].OnNextNumber(number);
            }
        }

        #endregion

        public override string ToString()
        {
            return $"{nameof(RandomNumberGenerator)}";
        }

        /// <summary>
        /// Started the Nummer-Generierung.
        /// Diese läuft so lange, solange interessierte Beobachter registriert sind (=>Attach()).
        /// </summary>
        public void StartNumberGeneration()
        {
            while (_observers.Count > 0)
            {
                int number = _random.Next(RANDOM_MIN_VALUE, RANDOM_MAX_VALUE);
                NotifyObservers(number);
                Task.Delay(_delay);
            }
        }
    }

    #endregion
}

