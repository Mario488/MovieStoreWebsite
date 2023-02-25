using Microsoft.EntityFrameworkCore.Diagnostics;
using move_store_app.Models;
namespace move_store_app.data
{
    public class Reg_Log_Repo
    {
        public readonly MoviesDbContext context;
        public Reg_Log_Repo(MoviesDbContext _context)
        {
            context = _context;
        }
        public void AddReg(Registration_Login reg)
        {
            context.Add(reg);
            context.SaveChanges();
        }
        public void DeleteAcc(int Id)
        {
            var accToDelete = context.Reg_Log.Find(Id);
            if(accToDelete != null)
            {
                context.Reg_Log.Remove(accToDelete);
                context.SaveChanges();
            }
        }
        public List<Registration_Login> GetAllReg()
        {
            return context.Reg_Log.ToList();
        }   
    }
}
