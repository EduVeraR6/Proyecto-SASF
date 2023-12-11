using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AGE.Utils.Paginacion
{
    public class Paginator<TEntity, TDao>
    {

        public static async Task<Page<TDao>> Paginar(
            IQueryable<TEntity> Query, 
            Pageable Pageable,
            Func<TEntity, TDao> FromEntityToDAO)
        {

            int TotalRecords = await Query.CountAsync();
            IOrderedQueryable<TEntity> SortedQuery;

            List<Sort<TDao>> SortList = Pageable.getSortList<TDao>();
            Sort<TDao> FirstSort = SortList.First();

            bool IsAscending = FirstSort.Direction.Equals("ASC", StringComparison.OrdinalIgnoreCase);
            var FirstOrderByLambda = GetOrderByLambda<TEntity>(FirstSort.FieldName);

            SortedQuery = IsAscending
                            ? Query.OrderBy(FirstOrderByLambda)
                            : Query.OrderByDescending(FirstOrderByLambda);


            foreach (Sort<TDao> SortField in SortList.Skip(1))
            {

                IsAscending = SortField.Direction.Equals("ASC", StringComparison.OrdinalIgnoreCase);
                var OrderByLambda = GetOrderByLambda<TEntity>(SortField.FieldName);

                SortedQuery = IsAscending
                                ? SortedQuery.ThenBy(OrderByLambda)
                                : SortedQuery.ThenByDescending(OrderByLambda);

            }


            List<TEntity> RecordsList = await SortedQuery
                                        .Skip(Pageable.Page * Pageable.Size)
                                        .Take(Pageable.Size)
                                        .ToListAsync();

            return new Page<TDao>(Pageable, 
                ConversorEntityToDAOList<TEntity,TDao>.FromEntityToDAOList(RecordsList, FromEntityToDAO), 
                TotalRecords);
        }



        public static Page<TDao> Paginar(
            List<TDao> RecordsList,
            Pageable Pageable)
        {
            int TotalRecords = RecordsList.Count;

            RecordsList = RecordsList.Skip(Pageable.Page * Pageable.Size)
                               .Take(Pageable.Size).ToList();

            return new Page<TDao>(Pageable, RecordsList, TotalRecords);
        }



        private static Expression<Func<TEntity, object>> GetOrderByLambda<TEntity>(string FieldName)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "s");
            var property = Expression.Property(parameter, FieldName);
            return Expression.Lambda<Func<TEntity, object>>(Expression.Convert(property, typeof(object)), parameter);
        }

    }
}
