using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAuth.Domain.Entities;

namespace UserAuth.Service.Helpers.Jwt;

public interface IJwtProvider
{
    string GenerateToken(User user);
}
