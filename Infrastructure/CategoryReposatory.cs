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
	public class CategoryReposatory : Reposatory<Category, int>, ICategoryReposatory
	{
		public CategoryReposatory(ApplicationContext context) : base(context)
		{
		}
	}
}
