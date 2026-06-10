namespace Consumer.Worker.Data.Interfaces
{
    public interface IAccidentByStateRepository
    {
        public void Upsert(string state, int severity);
    }
}
