namespace ApiEstudo.Business.Implementations
{
    using ApiEstudo.Data.Converter.Implementations;
    using ApiEstudo.Data.VO;
    using ApiEstudo.Model;
    using ApiEstudo.Repository;

    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IRepository<Person> _personRepository;

        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_personRepository.FindAll());
        }

        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_personRepository.FindByID(id));
        }

        public PersonVO Create(PersonVO personVO)
        {
            var personEntity = _converter.Parse(personVO);

            personEntity = _personRepository.Create(personEntity);

            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO personVO)
        {
            var personEntity = _converter.Parse(personVO);

            personEntity = _personRepository.Update(personEntity);

            return _converter.Parse(personEntity);
        }      

        public void Delete(long id)
        {
            _personRepository.Delete(id);
        }
    }
}
