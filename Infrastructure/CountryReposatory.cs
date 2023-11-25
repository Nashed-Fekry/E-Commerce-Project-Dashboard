using Application.Contracts;
using Context;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class CountryReposatory : Reposatory<Country, int>, IcountryReposatory
    {
        public CountryReposatory(ApplicationContext context) : base(context)
        {
        }
    }
}
