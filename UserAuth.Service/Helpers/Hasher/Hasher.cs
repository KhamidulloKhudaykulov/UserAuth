using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAuth.Service.Helpers.Hasher;

public abstract class Hasher
{
    public static string Hash(string input)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(11);
        return BCrypt.Net.BCrypt.HashPassword(input, salt);
    }

    public static bool Verify(string input, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(input, hash);
    }
}
