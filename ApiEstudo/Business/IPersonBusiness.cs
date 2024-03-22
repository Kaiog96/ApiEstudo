namespace ApiEstudo.Business
{
    using ApiEstudo.Data.VO;
    using ApiEstudo.Hypermedia.Utils;

    public interface IPersonBusiness
    {
        List<PersonVO> FindAll();

        PersonVO FindByID(long id);

        List<PersonVO> FindByName(string firtsName, string lastName);

        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);

        PersonVO Create(PersonVO personVO);

        PersonVO Update(PersonVO personVO);

        PersonVO Disable(long id);

        void Delete(long id);
    }
}
