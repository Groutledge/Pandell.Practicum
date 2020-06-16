using System;

namespace Pandell.Practicum.App.Services
{
    public interface IIdService
    {
        Guid GenerateId();
    }
    
    public class IdService : IIdService
    {
        public Guid GenerateId()
        {
            return Guid.NewGuid();
        }
    }
}