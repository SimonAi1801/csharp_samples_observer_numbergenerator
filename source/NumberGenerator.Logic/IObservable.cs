namespace NumberGenerator.Logic
{
    public interface IObservable
    {
        delegate void NextNumberHandler(int nextNumber);

        public NextNumberHandler NumberChanged { get; set; }
    }
}