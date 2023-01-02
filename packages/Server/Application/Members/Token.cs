namespace Schulcast.Application.Members;

public static class Token
{
	public static string Issue(TimeSpan expiration, ICollection<(string key, string value)> pairs)
	{
		var claims = new List<Claim> {
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
		};
		claims.AddRange(pairs.Where(pair => pair.value != null).Select(pair => new Claim(pair.key, pair.value)));
		var token = new JwtSecurityToken(
			issuer: "Schulcast",
			audience: "Schulcast",
			claims: claims.ToArray(),
			expires: DateTime.Now.Add(expiration),
			signingCredentials: new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.JWT_SECRET)),
				SecurityAlgorithms.HmacSha256
			)
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
		var hashedArr = SHA256.HashData(Encoding.UTF8.GetBytes(inputString));
		var sb = new StringBuilder();
		foreach (var b in hashedArr)
		{
			sb.Append(b.ToString("X2"));
		}
		return sb.ToString();
	}
}