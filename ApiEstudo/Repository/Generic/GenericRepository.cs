
namespace ApiEstudo.Repository.Generic
{
    using ApiEstudo.Model.Base;
    using ApiEstudo.Model.Context;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MysqlContext _context;

        private DbSet<T> dataSet;

        public GenericRepository(MysqlContext context)
        {
            this._context = context;
            dataSet = _context.Set<T>();
        }

        public List<T> FindAll()
        {
            return dataSet.ToList();
        }

        public T FindByID(long id)
        {
            return dataSet.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Create(T item)
        {
            try
            {
                dataSet.Add(item);

                this._context.SaveChanges();

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Update(T item)
        {
            var result = dataSet.SingleOrDefault(p => p.Id.Equals(item.Id));

            if (result != null)
            {
                try
                {
                    this._context.Entry(result).CurrentValues.SetValues(item);

                    this._context.SaveChanges();

                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } else
            {
                return null;
            }
        }

        public void Delete(long id)
        {
            var result = dataSet.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    dataSet.Remove(result);

                    this._context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Exists(long id)
        {
            return dataSet.Any(p => p.Id.Equals(id));
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return dataSet.FromSqlRaw<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            var result = "";

            using (var connection = this._context.Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    result = command.ExecuteScalar().ToString();
                }
            }

            return int.Parse(result);
        }
    }
}
