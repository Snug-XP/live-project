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
        /// 根据爬取的每一条信息，判断其参与了哪些活动
        /// </summary>
        /// <param name="message">从聊天记录里得到的 MessageInfo</param>
        /// <returns>一个列表，包含其参与的所有活动，如果其没有参与任何活动，返回一个空列表</returns>
        public List<Activity> Query(MessageInfo message)
        {
            List<Activity> participated = new List<Activity>();
            foreach (Activity activity in activities)
            {
                if (activity.IsValidMessage(message))
                    participated.Add(activity);
            }
            return participated;
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
            // 从已有活动里筛选出 keyword 与该 activity 相同的所有活动
            var activitiesWithSameKeyword = from a in activities where a.KeyWord == activity.KeyWord select a;
            // 如果该活动的关键词从未出现过
            if (activitiesWithSameKeyword.Count() == 0)
                return true;
            // 否则，检测该活动的日期是否与其它活动的日期有交集
            foreach (Activity a in activitiesWithSameKeyword)
            {
                if (IsTwoTimePeriodIntersect(a.BeginTime, a.EndTime, activity.BeginTime, activity.EndTime))
                    return false;
            }
            return true;
        }

        private bool IsTwoTimePeriodIntersect(DateTime beginTimeA, DateTime endTimeA, DateTime beginTimeB, DateTime endTimeB)
        {
            if (beginTimeA < beginTimeB)
                return beginTimeB < endTimeA;
            else
                return beginTimeA < endTimeB;
        }

        private List<Activity> activities;
    }
}
