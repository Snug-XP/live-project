using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class UserList
    {
        private List<User> userList = new List<User>();



        public void Add(User user)
        {
            userList.Add(user);
        }

        public bool IsExist(string userID)
        {
            foreach(User p in userList)
            {
                if (userID.CompareTo(p.ID)!=0)
                    return true;
            }
            return false;
        }



    }