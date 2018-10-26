using System.Collections.Generic;
using VueContactsAPI.Entities;

namespace VueContactsAPI.Repositories
{
    public interface ILoginRepository
    {
        void Save(Login login);
        IEnumerable<Login> GetAll();
        Login GetById(int id);
        void Delete(int id);
        void Update();
    }
}
