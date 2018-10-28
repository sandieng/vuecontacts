using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VueContactsAPI.Entities;
using VueContactsAPI.Infrastructures;

namespace VueContactsAPI.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private MyContactsDbContext _context;
        private DbSet<Login> _loginEntity;

        public LoginRepository(MyContactsDbContext context)
        {
            _context = context;
            _loginEntity = _context.Set<Login>();
        }

        public void Delete(int id)
        {
            Login login = GetById(id);
            _loginEntity.Remove(login);
            _context.SaveChanges();
        }

        public IEnumerable<Login> GetAll()
        {
            return _loginEntity.AsEnumerable();
        }

        public Login GetById(int id)
        {
            return _loginEntity.SingleOrDefault(x => x.Id == id);
        }

        public void Save(Login login)
        {
            _context.Entry(login).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Update()
        {
            _context.SaveChanges();
        }
    }
}
