namespace ApiEstudo.Business.Implementations
{
    using ApiEstudo.Data.Converter.Implementations;
    using ApiEstudo.Data.VO;
    using ApiEstudo.Hypermedia.Utils;
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

        public List<PersonVO> FindByName(string firtsName, string lastName)
        {
            return this._converter.Parse(_personRepository.FindByName(firtsName, lastName));
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && sortDirection.Equals("desc") ? "asc" : "desc";

            var size = (pageSize < 1) ? 10 : pageSize;

            var offSet = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from person p where 1 = 1";
            if (!string.IsNullOrEmpty(name)) query = query + $" and p.first_name like '%{name}%' ";
            query += $" order by p.first_name {sort} limit {size} offset {offSet}";

            string countQuery = @"select count(*) from person p where 1 = 1";
            if (!string.IsNullOrEmpty(name)) countQuery = countQuery + $" and p.first_name like '%{name}%' ";

            var persons = this._personRepository.FindWithPagedSearch(query);

            int totalResults = this._personRepository.GetCount(countQuery);

            return new PagedSearchVO<PersonVO> 
            { 
                CurrentPage = page,
                List = this._converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults,            
            };
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
