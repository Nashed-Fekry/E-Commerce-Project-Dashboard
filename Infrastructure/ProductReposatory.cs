﻿using Application.Contracts;
using Context;
using Domain;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ProductReposatory : Reposatory<Product, int>, IProductReposatory
    {
        ApplicationContext ApplicationContext;
        public ProductReposatory(ApplicationContext context) : base(context)
        {
            ApplicationContext = context;
        }

        public async Task<bool> DeleteProductSoftly(int id)
        {
            var res = ApplicationContext.Products.FirstOrDefault(p => p.Id == id);
            //if (res != null)
            //{
            //    res.Status = false;
            //    ApplicationContext.Products.Update(res);
            //    ApplicationContext.SaveChanges();
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            //return !(res != null);
            res.Status = !(res.Status);
            ApplicationContext.Products.Update(res);
            ApplicationContext.SaveChanges();
            return res.Status;
        }
    }
}
