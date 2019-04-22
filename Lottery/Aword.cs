using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    public class Award
    {
        private string awardName;
        private string awardMessage;
        private int count;
        private List<string> id = new List<string>();

        public Award() { }
        public Award(string aName,string aMessage, int num)
        {
            AwardName = aName;
            AwardMessage = aMessage;
            Count = num;
        }

        public string AwardName { get => awardName; set => awardName = value; }
        public string AwardMessage { get => awardMessage; set => awardMessage = value; }
        public int Count { get => count; set => count = value; }

        public void AddUser(string awardID) {
            if(id.Count <= count)
                id.Add(awardID);
        }

        public string PushUser(string awardID)
        {
            string temp = null;
            if (id.Count > 0)
            {
                temp = id[0];
                id.RemoveAt(0);
            }
            return temp;
        }
    }
}
