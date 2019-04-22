using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    /// <summary>
    /// 包含每条记录解析出来的数据
    /// </summary>
    public class MessageInfo
    {
        /// <summary>
        /// 消息发送者的 ID（可能是 QQ 号或者邮箱账号）
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 消息发送的日期时间
        /// </summary>
        public DateTime SentTime { get; set; }
        /// <summary>
        /// 包含参与抽奖信息的列表
        /// </summary>
        public ISet<string> KeySet { get; set; }
        /// <summary>
        /// 消息内容的 HashCode
        /// </summary>
        public int MessageHashCode { get; set; }
    }
}
