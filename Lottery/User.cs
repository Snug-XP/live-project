using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class User
    {
        private string id { get; set; }
        private string name { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
        private string identity { get; set; }
        /// <summary>
        /// 平时发言次数
        /// </summary>
        private int numberOfUsualSpeech { get; set; }
        /// <summary>
        /// 抽奖发言次数
        /// </summary>
        private int numberOfLotterySpeech { get; set; }
        /// <summary>
        /// 最近五次不同的发言的频次
        /// </summary>
        public List<Dictionary<string, int>> RecentSpeech { get; set; }
        /// <summary>
        /// 活跃度
        /// </summary>
        private int activity { get; set; }

        User(string id, string name, string identity, int numberOfUsualSpeech, int numberOfLotterySpeech,int activity)
        {
            this.id = id;
            this.name = name;
            this.identity = identity;
            this.numberOfUsualSpeech = numberOfUsualSpeech;
            this.numberOfLotterySpeech = numberOfLotterySpeech;
            this.activity = activity;
         }

    }
}
