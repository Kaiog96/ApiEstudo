namespace ApiEstudo.Services
{
    using ApiEstudo.Model;

    public interface IPersonService
    {
        List<Person> FindAll();

        Person FindByID(long id);

        Person Create(Person person);

        Person Update(Person person);

        void Delete(long id);
    }
}
