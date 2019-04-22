using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class UserList
    {
        private List<User> userList;
 
        //创建类型为Person的对象集合
        List<UserList> persons = new List<UserList>();
        //将Person对象放入集合
        persons.Add(p1);
        persons.Add(p2);
        persons.Add(p3);
        //输出第2个人的姓名

        public int CompareTo(UserList p)
        {
            return this.id - p.id;
        }

        public int Compare(Person p1, Person p2)
        {
            return System.Collections.Comparer.Default.Compare(p1.Name, p2.Name);
        }
        public static bool MidAge(Person p)
        {

            if (p.Age >= 40)
                return true;
            else
                return false;
        }


    }
