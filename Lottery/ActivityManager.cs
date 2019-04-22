using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    /// <summary>
    /// 活动管理类，对已有活动进行管理
    /// </summary>
    public class ActivityManager
    {
        public ActivityManager()
        {
            activities = new List<Activity>();
        }

        /// <summary>
        /// 添加一个新的活动
        /// 新活动只有满足以下条件才可加入
        /// 1. 在时间段内没有相同关键词的活动
        /// </summary>
        /// <param name="activity">要传入的活动</param>
        /// <returns>插入成功时返回 true，否则返回 false</returns>
        public bool AddActivity(Activity activity)
        {
            if (IsValidActivity(activity))
            {
                activities.Add(activity);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据用户提供的条件查找活动
        /// </summary>
        /// <param name="keyWord">活动的关键字</param>
        /// <param name="beginTime">活动的开始时间</param>
        /// <param name="endTime">活动的结束时间</param>
        /// <returns>
        /// 如果用户给定的关键字在活动列表里存在，且给定范围的时间落在活动的开始时间与结束时间内（包含两个边界）
        /// 则返回该活动，否则返回 null
        /// </returns>
        public Activity Query(string keyWord, DateTime beginTime, DateTime endTime)
        {
            var result = from activity in activities
                         where activity.KeyWord == keyWord && beginTime >= activity.BeginTime && activity.EndTime >= endTime
                         select activity;
            return result.FirstOrDefault();
        }

        /// <summary>
        /// 获取活动的总数
        /// </summary>
        public int ActivityCount
        {
            get { return activities.Count; }
        }


        private bool IsValidActivity(Activity activity)
        {
            return Query(activity.KeyWord, activity.BeginTime, activity.EndTime) == null;
        }

        private List<Activity> activities;
    }
}
