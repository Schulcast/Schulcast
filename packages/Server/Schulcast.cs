global using System;
global using System.Security;
global using System.Collections.Generic;
global using System.ComponentModel.DataAnnotations;
global using System.IdentityModel.Tokens.Jwt;
global using System.Linq;
global using System.Net;
global using System.Net.Http.Headers;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
global using System.Reflection;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Xml.Serialization;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Globalization;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.EntityFrameworkCore.Metadata;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Schulcast;
global using Schulcast.Application;
global using Schulcast.Application.Abstractions;
global using Schulcast.Application.Content;
global using Schulcast.Application.Files;
global using Schulcast.Application.Members;
global using Schulcast.Application.Slides;
global using Schulcast.Application.Tasks;
global using Schulcast.Infrastructure;
global using Schulcast.Infrastructure.Identity;
global using Microsoft.EntityFrameworkCore.Migrations;
global using Schulcast.Infrastructure.Persistence;
global using Newtonsoft.Json;
global using Microsoft.Extensions.Caching.Memory;

WebApplication.CreateBuilder(args)
	.InstallSchulcast().Build()
	.ConfigureSchulcast().Run();