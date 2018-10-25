using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueContactsAPI.Entities;

namespace VueContactsAPI.Repositories
{
    public interface IContactRepository
    {
        void Save(Contact contact);
        IEnumerable<Contact> GetAll();
        Contact GetById(int id);
        void Delete(int id);
        void Update(Contact contact);
    }
}
