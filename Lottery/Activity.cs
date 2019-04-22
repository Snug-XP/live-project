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

        /// <summary>
        /// 给活动添加奖项
        /// </summary>
        /// <param name="award">要添加的奖项</param>
        public void AddAward(Award award)
        {
            awardList.Add(award);
        }

        /// <summary>
        /// 添加参与者的 ID 到参与人员列表中
        /// </summary>
        /// <param name="userId">参与人员的 ID</param>
        public void AddParticipant(string userId)
        {
            participantIds.Add(userId);
        }

        /// <summary>
        /// 检查一条消息记录是否属于该活动的有效发言
        /// 判断条件有两个
        /// 1. 消息内包含参与活动的关键字
        /// 2. 消息的发送时间在活动时间之内
        /// </summary>
        /// <param name="info">由聊天记录解析到的消息数据</param>
        /// <returns>发言有效则返回 true，否则返回 false</returns>
        public bool IsValidMessage(MessageInfo info)
        {
            return info.KeySet.Contains(KeyWord) &&
                BeginTime <= info.SentTime && info.SentTime <= EndTime;
        }

        /// <summary>
        /// 获取参与人员的 ID 列表
        /// </summary>
        public List<string> Participants
        {
            get
            {
                return participantIds;
            }
        }

        /// <summary>
        /// 获取奖品列表
        /// </summary>
        public List<Award> Awards
        {
            get
            {
                return awardList;
            }
        }

        private List<Award> awardList = new List<Award>();

        private List<string> participantIds = new List<string>();

    }
}
