namespace ApiEstudo.Business.Implementations
{
    using ApiEstudo.Data.Converter.Implementations;
    using ApiEstudo.Data.VO;
    using ApiEstudo.Model;
    using ApiEstudo.Repository;

    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _personRepository;

        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository personRepository)
        {
            this._personRepository = personRepository;
            this._converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return this._converter.Parse(_personRepository.FindAll());
        }

        public PersonVO FindByID(long id)
        {
            return this._converter.Parse(_personRepository.FindByID(id));
        }

        public PersonVO Create(PersonVO personVO)
        {
            var personEntity = this._converter.Parse(personVO);

            personEntity = this._personRepository.Create(personEntity);

            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO personVO)
        {
            var personEntity = this._converter.Parse(personVO);
             
            personEntity = this._personRepository.Update(personEntity);

            return _converter.Parse(personEntity);
        }

        public PersonVO Disable(long id)
        {
            var personEntity = _personRepository.Disable(id);

            return this._converter.Parse((personEntity));
        }

        public void Delete(long id)
        {
            this._personRepository.Delete(id);
        }       
    }
}
