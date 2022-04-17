﻿global using System.Net.Mime;
global using System.Diagnostics;
global using System.Text;
global using System.Text.Json;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Authorization;
global using MediatR;
global using FluentValidation.AspNetCore;
global using NSwag;
global using NSwag.Generation.Processors.Security;
global using Movies.Common.Wrappers;
global using Movies.Common.Exceptions;
global using Movies.Application;
global using Movies.Infrastructure;
global using Movies.Infrastructure.Identity;
global using Movies.Infrastructure.Persistence;
global using Movies.WebAPI;
global using Movies.WebAPI.Filters;
global using Movies.WebAPI.Middlewares;
global using Movies.Application.Auth.Commands.Login;
global using Movies.Application.Auth.Commands.Signup;
global using Movies.Application.Genres.Commands.CreateGenre;
global using Movies.Application.Genres.Commands.UpdateGenre;
global using Movies.Application.Genres.Commands.DeleteGenre;
global using Movies.Application.Genres.Queries.GetGenres;
global using Movies.Application.Movies.Commands.CreateMovie;
global using Movies.Application.Movies.Commands.UpdateMovie;
global using Movies.Application.Movies.Commands.DeleteMovie;
global using Movies.Application.Movies.Queries.GetMovies;
global using Movies.Application.Movies.Queries.GetMovieDetails;
global using Movies.Application.Persons.Commands.CreatePerson;
global using Movies.Application.Persons.Commands.UpdatePerson;
global using Movies.Application.Persons.Commands.DeletePerson;
global using Movies.Application.Persons.Queries.GetPersons;
global using Movies.Application.Persons.Queries.GetPersonDetails;
global using Movies.Application.Images.Commands.UploadImage;
global using Movies.Application.Images.Commands.DeleteImage;
global using Movies.Application.Images.Commands.UpdateImage;