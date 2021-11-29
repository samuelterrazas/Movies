using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Mappings;
using Movies.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Movies.Application.Persons.DTOs;

namespace Movies.Application.Persons.Queries.GetPersons
{
    public class GetPersonsQuery : IRequest<PaginatedResponse<PersonDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Name { get; set; }
    }

    public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, PaginatedResponse<PersonDto>>
    {
        private readonly IApplicationDbContext _dbContext; 
        private readonly IMapper _mapper;

        public GetPersonsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<PaginatedResponse<PersonDto>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            var persons = _dbContext.Persons.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
                persons = persons.Where(p => p.FullName.Contains(request.Name));
            
            return await persons
                .AsNoTracking()
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .PaginatedResponseAsync(request.PageNumber, request.PageSize);
        }
    }
}
