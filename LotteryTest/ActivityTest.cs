using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lottery;
using System.Globalization;
using System.Collections.Generic;

namespace LotteryTest
{
    [TestClass]
    public class ActivityTest
    {
        private Activity activity;

        [TestInitialize]
        public void Init()
        {
            activity = new Activity()
            {
                KeyWord = "Keyword",
                BeginTime = Util.GetDateTime("2019-04-02 08:00:00"),
                EndTime = Util.GetDateTime("2019-04-04 08:00:00"),
            };
        }

        [TestMethod]
        public void TestValidMessage()
        {
            MessageInfo info = new MessageInfo()
            {
                ID = "foo@bar.com",
                Name = "Foo",
                SentTime = Util.GetDateTime("2019-04-03 08:00:00"),
                KeySet = new HashSet<string>() { "Keyword" },
            };

            Assert.IsTrue(activity.IsValidMessage(info));

            info.SentTime = Util.GetDateTime("2019-04-02 08:00:00");
            Assert.IsTrue(activity.IsValidMessage(info));
            info.SentTime = Util.GetDateTime("2019-04-04 08:00:00");
            Assert.IsTrue(activity.IsValidMessage(info));
        }

        [TestMethod]
        public void TestInvalidMessage()
        {
            MessageInfo info = new MessageInfo()
            {
                ID = "foo@bar.com",
                Name = "Foo",
                SentTime = Util.GetDateTime("2019-04-05 08:00:00"),
                KeySet = new HashSet<string>() { "Keyword" },
            };

            Assert.IsFalse(activity.IsValidMessage(info));

            info.SentTime = Util.GetDateTime("2019-04-03 08:00:00");
            info.KeySet = new HashSet<string>() { "foobar" };
            Assert.IsFalse(activity.IsValidMessage(info));
        }

        [TestMethod]
        public void TestValidMessageByKeywordAndSentTime()
        {
            Assert.IsTrue(activity.IsValidMessage("Keyword", Util.GetDateTime("2019-04-03 00:00:00")));
            Assert.IsTrue(activity.IsValidMessage("Keyword", Util.GetDateTime("2019-04-02 08:00:00")));
            Assert.IsTrue(activity.IsValidMessage("Keyword", Util.GetDateTime("2019-04-04 08:00:00")));
        }

        [TestMethod]
        public void TestInvalidMessageByKeywordAndSentTime()
        {
            Assert.IsFalse(activity.IsValidMessage("Foo", Util.GetDateTime("2019-04-03 08:00:00")));
            Assert.IsFalse(activity.IsValidMessage("Keyword", Util.GetDateTime("2019-01-01 00:00:00")));
            Assert.IsFalse(activity.IsValidMessage("Keyword", Util.GetDateTime("2020-01-01 00:00:00")));
        }
    }
}
