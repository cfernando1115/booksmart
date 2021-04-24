using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSmart.SeedData
{
    public interface IDbInitializer
    {
        void Initialize();
    }
}
