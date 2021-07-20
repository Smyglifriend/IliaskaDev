using Microsoft.EntityFrameworkCore;
using SpaceWeb.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IliaskaWebSite.EfStuff;
using Microsoft.AspNetCore.Mvc;

namespace SpaceWeb.EfStuff.Repositories
{
    public abstract class BaseRepository<ModelType>
        : IBaseRepository<ModelType> where ModelType : BaseModel
    {
        protected IliaskaDbContext _iliaskaDbContext;
        protected DbSet<ModelType> _dbSet;

        public BaseRepository(IliaskaDbContext iliaskaDbContext)
        {
            _iliaskaDbContext = iliaskaDbContext;
            _dbSet = _iliaskaDbContext.Set<ModelType>();
        }

        public ModelType Get(long id)
        {
            return _dbSet.SingleOrDefault(x => x.Id == id);
        }

        public virtual List<ModelType> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Remove(long id)
        {
            var model = Get(id);
            Remove(model);
        }

        public void Remove(IEnumerable<long> id)
        {
            foreach (var userid in id)
            {
                Remove(userid);
            }
        }

        public void Remove(ModelType model)
        {
            _iliaskaDbContext.Remove(model);
            _iliaskaDbContext.SaveChanges();
        }

        public void Save(ModelType model)
        {
            if (model.Id > 0)
            {
                _dbSet.Update(model);
            }
            else
            {
                _dbSet.Add(model);
            }

            _iliaskaDbContext.SaveChanges();
        }
    }
}
