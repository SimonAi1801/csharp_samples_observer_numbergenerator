using System;


namespace NumberGenerator.Logic
{
    public interface IObservable
    {
        public event EventHandler<int> NumberChanged;
    }
}