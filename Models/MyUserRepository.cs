using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIuser.Models
{
    public class MyUserRepository
    {
        private SecureDBEntities db = new SecureDBEntities();

        public MyUser loadUserByUserNamePassword(String username,String password)
        {
            return db.MyUsers.Where(x => x.Name == username && x.Password == password).FirstOrDefault();
        }
    }
}