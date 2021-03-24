using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Schulcast.Server.Models;

namespace Schulcast.Server.Helpers
{
	public static class Token
	{
		private static string Issue(TimeSpan expiration, ICollection<(string key, string value)> pairs)
		{
			List<Claim> claims = new List<Claim>() {
				new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString("N")),
			};

			foreach (var pair in pairs)
			{
				if (pair.value != null)
				{
					claims.Add(new Claim(pair.key, pair.value));
				}
			}

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
			StringBuilder sb = new StringBuilder();
			foreach (byte b in hashedArr) sb.Append(b.ToString("X2"));
			return sb.ToString();
		}
	}
}