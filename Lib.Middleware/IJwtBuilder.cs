using System;

namespace Lib.Middleware
{
    public interface IJwtBuilder
    {
        string GetToken(Guid userId);
        Guid ValidateToken(string token);
    }
}
