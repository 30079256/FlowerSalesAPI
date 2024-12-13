using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace FlowerSales.API.Models
{
    public static class MongoExtensions
    {
        public static IFindFluent<TEntity, TEntity> OrderByCustom<TEntity>(this IFindFluent<TEntity, TEntity> findFluent, string sortBy, string sortOrder)
        {
            SortDefinition<TEntity> sortDefinition;

            
            if (sortOrder == "desc")
                sortDefinition = Builders<TEntity>.Sort.Descending(sortBy);
            else
                sortDefinition = Builders<TEntity>.Sort.Ascending(sortBy);

            return findFluent.Sort(sortDefinition);
        }
    }
}
