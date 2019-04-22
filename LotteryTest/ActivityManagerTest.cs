using System;
using System.Globalization;
using Lottery;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LotteryTest
{
    [TestClass]
    public class ActivityManagerTest
    { 
        [TestMethod]
        public void TestAddActivity()
        {
            ActivityManager manager = new ActivityManager();
            Activity activity = new Activity
            {
                KeyWord = "KeyWord",
                BeginTime = DateTime.ParseExact("2019-04-02 08:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                EndTime = DateTime.ParseExact("2019-04-03 08:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
            };

            manager.AddActivity(activity);

            Assert.AreEqual(1, manager.ActivityCount);
        }

        [TestMethod]
        public void TestAddInvalidActivityWithSameKeyword()
        {
            ActivityManager manager = new ActivityManager();
            Activity activity = new Activity();
            activity.KeyWord = "KeyWord";
            activity.BeginTime = DateTime.ParseExact("2019-04-02 08:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            activity.EndTime = DateTime.ParseExact("2019-04-03 08:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            manager.AddActivity(activity);

            Activity activityWithSameKeyword = new Activity
            {
                KeyWord = "KeyWord",
                BeginTime = DateTime.ParseExact("2019-04-05 08:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                EndTime = DateTime.ParseExact("2019-04-06 08:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
            };
            manager.AddActivity(activityWithSameKeyword);

            Assert.AreEqual(2, manager.ActivityCount);
        }

        [TestMethod]
        public void TestAddInvalidActivityWithInproperateTime()
        {
            ActivityManager manager = new ActivityManager();
            Activity activity = new Activity()
            {
                KeyWord = "KeyWord",
                BeginTime = DateTime.ParseExact("2019-04-02 08:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                EndTime = DateTime.ParseExact("2019-04-03 08:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
            };

            manager.AddActivity(activity);

            Activity invalidActivity = new Activity()
            {
                KeyWord = "KeyWord",
                BeginTime = DateTime.ParseExact("2019-04-01 08:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                EndTime = DateTime.ParseExact("2019-04-04 08:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
            };
            manager.AddActivity(invalidActivity);

            Assert.AreEqual(1, manager.ActivityCount);
        }

        [TestMethod]
        public void TestQuery()
        {
            ActivityManager manager = new ActivityManager();
            Activity activity = new Activity()
            {
                KeyWord = "Foo",
                BeginTime = Util.GetDateTime("2019-04-04 09:00:00"),
                EndTime = Util.GetDateTime("2019-04-05 09:00:00")
            };
            manager.AddActivity(activity);

            Activity activity2 = new Activity()
            {
                KeyWord = "Bar",
                BeginTime = Util.GetDateTime("2019-05-01 08:00:00"),
                EndTime = Util.GetDateTime("2019-05-02 09:00:00")
            };
            manager.AddActivity(activity2);

            Activity result = manager.Query("Foo", Util.GetDateTime("2019-04-01 00:00:00"), Util.GetDateTime("2019-04-10 00:00:00"));
            Assert.AreEqual(activity, result);
        }
    }
}
