using Movies.Application.Common.Mappings;
using Movies.Domain.Entities;

namespace Movies.Application.Persons.DTOs
{
    public class PersonDto : IMapFrom<Person>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
