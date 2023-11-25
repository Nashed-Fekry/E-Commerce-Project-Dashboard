using Application.Contracts;
using Application.Contracts;
using Context;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class CityReposatory : Reposatory<City, int>,ICityReposatory
    {
        private readonly ApplicationContext context;
        private readonly DbSet<City> dbset;
        public CityReposatory(ApplicationContext context) : base(context)
        {
            this.context = context;
            dbset = this.context.Set<City>();

        }

        public async Task<List<City>> GetCitiesbyCountry(int id)
        {
            return  dbset.Where(c => c.CountryId == id).ToList();
        }
    }
}
