namespace ApiEstudo.Business
{
    using ApiEstudo.Data.VO;

    public interface IPersonBusiness
    {
        List<PersonVO> FindAll();

        PersonVO FindByID(long id);

        PersonVO Create(PersonVO personVO);

        PersonVO Update(PersonVO personVO);

        void Delete(long id);
    }
}
