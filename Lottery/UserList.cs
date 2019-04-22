using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class UserList
    {
        public List<User> userList = new List<User>();



        public void Add(User user)
        {
            userList.Add(user);
        }

        public User GetUser(string userID)
        {
            foreach (User p in userList)
            {
                if (userID.CompareTo(p.ID) == 0)
                    return p;
            }
            return null;
        }

        public int GetTotal()
        {
            return userList.ToArray().Length;
        }

        public bool IsExist(string userID)
        {
            foreach(User p in userList)
            {
                if (userID.CompareTo(p.ID)==0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 按活跃度降序排序
        /// </summary>
        public void Sort()
        {
            userList.Sort(delegate (User x, User y) { return y.GetActivity().CompareTo(x.GetActivity()); });
        }


    }
}

