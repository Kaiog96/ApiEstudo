namespace ApiEstudo.Repository.Implementations
{
    using ApiEstudo.Model;
    using ApiEstudo.Model.Context;
    using System;

    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MysqlContext _context;

        public PersonRepositoryImplementation(MysqlContext context)
        {
           _context = context;
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindByID(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return person;
        }

        public Person Update(Person person)
        {
            if(!Exists(person.Id)) return new Person();

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);

                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }           

            return person;
        }      

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);

                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
