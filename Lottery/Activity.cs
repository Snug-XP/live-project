using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    /// <summary>
    /// 活动类，用于存储活动相关信息
    /// </summary>
    public class Activity
    {
        /// <summary>
        /// 活动的关键词（消息中出现该关键词表示用户参与该活动）
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 活动的开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 活动的结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 活动的文案
        /// </summary>
        public string CopyWrite { get; set; }
    }
}
