using System.Linq;
using IliaskaWebSite.EfStuff;
using IliaskaWebSite.EfStuff.Model;

namespace SpaceWeb.EfStuff.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IliaskaDbContext iliaskaDbContext) : base(iliaskaDbContext)
        {
            
        }

        public User Get(string data)
        {
            return _dbSet.SingleOrDefault(x => x.Name == data
                                               || x.Login == data);
        }
    }
}