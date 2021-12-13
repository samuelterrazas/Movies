﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Mappings;
using Movies.Application.Common.Models;
using Movies.Application.Genres.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Genres.Queries.GetGenres
{
    public class GetGenresQuery : IRequest<PaginatedResponse<GenreDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, PaginatedResponse<GenreDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenresQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<PaginatedResponse<GenreDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Genres
                .AsNoTracking()
                .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
                .PaginatedResponseAsync(request.PageNumber, request.PageSize);
        }
    }
}