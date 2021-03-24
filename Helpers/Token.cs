using Microsoft.IdentityModel.Tokens;
using Schulcast.Server.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace Schulcast.Server.Helpers
{
	public static class Token
	{
		static string Issue(TimeSpan expiration, ICollection<(string key, string value)> pairs)
		{
			var claims = new List<Claim>() {
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString("N")),
			};
			claims.AddRange(pairs.Where(pair => pair.value != null).Select(pair => new Claim(pair.key, pair.value)));
			var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Program.Configuration["Jwt:SecretKey"])), SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				issuer: Program.Configuration["Jwt:Issuer"],
				audience: Program.Configuration["Jwt:Audience"],
				claims: claims.ToArray(),
				expires: DateTime.Now.Add(expiration),
				signingCredentials: credentials
			);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public static string IssueAccountAccess(TimeSpan expiration, Member member)
		{
			return Issue(expiration,
				new[] {
					(JwtRegisteredClaimNames.Sub, member.Id.ToString()),
					(JwtRegisteredClaimNames.NameId, member.Nickname),
					(ClaimTypes.Name, member.Nickname),
					(ClaimTypes.Role, member.Role)
				}
			);
		}
	}

	public static class Sha256
	{
		public static string GetHash(string inputString)
		{
			HashAlgorithm algorithm = SHA256.Create();
			var hashedArr = algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
			var sb = new StringBuilder();
			foreach (var b in hashedArr)
			{
				_ = sb.Append(b.ToString("X2"));
			}

			return sb.ToString();
		}
	}
}