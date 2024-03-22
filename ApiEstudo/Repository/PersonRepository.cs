namespace ApiEstudo.Repository
{
    using ApiEstudo.Model;
    using ApiEstudo.Model.Context;
    using ApiEstudo.Repository.Generic;
    using System.Collections.Generic;

    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MysqlContext context) : base(context) {}

        public Person Disable(long id)
        {
            if (!this._context.Persons.Any(p => p.Id.Equals(id))) return null;

            var user = this._context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (user != null) 
            {
                user.Enabled = false;

                try
                {
                    this._context.Entry(user).CurrentValues.SetValues(user);

                    this._context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return user;
        }

        public List<Person> FindByName(string firtsName, string lastName)
        {
            if (!string.IsNullOrEmpty(firtsName) && !string.IsNullOrEmpty(lastName))
            {
                return this._context.Persons.Where(p => p.FirstName.Contains(firtsName) && p.LastName.Contains(lastName)).ToList();
            }            
            else if (string.IsNullOrEmpty(firtsName) && !string.IsNullOrEmpty(lastName))
            {
                return this._context.Persons.Where(p => p.LastName.Contains(lastName)).ToList();
            }
            else if (!string.IsNullOrEmpty(firtsName) && string.IsNullOrEmpty(lastName))
            {
                return this._context.Persons.Where(p => p.FirstName.Contains(firtsName)).ToList();
            }

            return null;
        }
    }
}
