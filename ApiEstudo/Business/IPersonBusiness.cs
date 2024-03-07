namespace ApiEstudo.Business
{
    using ApiEstudo.Model;

    public interface IPersonBusiness
    {
        List<Person> FindAll();

        Person FindByID(long id);

        Person Create(Person person);

        Person Update(Person person);

        void Delete(long id);
    }
}
