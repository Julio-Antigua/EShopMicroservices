namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string mesaage) : base(mesaage)
        {
                
        }

        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
                
        }
    }
}
