﻿namespace ApiEstudo.Data.Converter.Implementations
{
    using ApiEstudo.Data.Converter.Contract;
    using ApiEstudo.Data.VO;
    using ApiEstudo.Model;
    using System.Collections.Generic;

    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }

        public Person Parse(PersonVO origin)
        {
            if (origin == null) return null;

            return new Person
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender,
            };
        }

        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }

        public PersonVO Parse(Person origin)
        {
            if (origin == null) return null;

            return new PersonVO
            {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender,
            };
        }
    }
}
