using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    public class User
    {
        public string ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
        public string Identity { get; set; }
        /// <summary>
        /// 平时发言次数
        /// </summary>
        public int NumberOfUsualSpeech { get; set; }
        /// <summary>
        /// 抽奖发言次数
        /// </summary>
        public int NumberOfLotterySpeech { get; set; }
        /// <summary>
        /// 最近五次不同的发言的频次
        /// </summary>
        public List<Dictionary<string, int>> RecentSpeech { get; set; }
        /// <summary>
        /// 活跃度
        /// </summary>
        public int Activity { get; set; }

        public User(string id, string name, string identity, int numberOfUsualSpeech, int numberOfLotterySpeech,int activity)
        {
            this.ID = id;
            this.Name = name;
            this.Identity = identity;
            this.NumberOfUsualSpeech = numberOfUsualSpeech;
            this.NumberOfLotterySpeech = numberOfLotterySpeech;
            this.Activity = activity;
         }

    }
}
