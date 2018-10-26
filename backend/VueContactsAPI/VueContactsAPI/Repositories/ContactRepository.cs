using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VueContactsAPI.Entities;

namespace VueContactsAPI.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private MyContactsDbContext _context;
        private DbSet<Contact> _contactEntity;

        public ContactRepository(MyContactsDbContext context)
        {
            _context = context;
            _contactEntity = _context.Set<Contact>();
        }

        public void Delete(int id)
        {
            Contact contact = GetById(id);
            _contactEntity.Remove(contact);
            _context.SaveChanges();
        }

        public IEnumerable<Contact> GetAll()
        {
            return _contactEntity.AsEnumerable();
        }

        public Contact GetById(int id)
        {
            return _contactEntity.SingleOrDefault(x => x.Id == id);
        }

        public void Save(Contact contact)
        {
            _context.Entry(contact).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Update()
        {
            _context.SaveChanges();
        }
    }
}
