using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lottery
{
    /// <summary>
    /// MessageParser - 从聊天记录中获取有效信息
    /// </summary>
    public class MessageParser
    {
        // 匹配消息头部的 Regex
        private static readonly Regex HeaderRegex = new Regex(
            @"^(?<dateTime>\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2})\s(?<name>.*)(\((?<id>\d*)\)|<(?<id>[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+)>)$",
            RegexOptions.Compiled, TimeSpan.FromMilliseconds(200));
        // 日期格式
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        // 匹配中奖消息的 Regex
        private static readonly Regex LottoryCodeRegex = new Regex(
            @"#(?<code>[^\\#|.]+)#",
            RegexOptions.Compiled, TimeSpan.FromMilliseconds(200));

        private string firstLine;
        private string secondLine;
        private readonly StreamReader reader;

        /// <summary>
        /// 由给定文件创建 MessageParser 对象
        /// </summary>
        /// <param name="fileName">聊天记录的文件名</param>
        public MessageParser(string fileName)
        {
            reader = new StreamReader(fileName, Encoding.Default);
            firstLine = reader.ReadLine();
            secondLine = reader.ReadLine();
        }

        /// <summary>
        /// 判断是否还有下一条消息记录
        /// </summary>
        /// <returns>如果还有记录，则返回 true，否则返回 false</returns>
        public bool HasNextMessage()
        {
            return !string.IsNullOrEmpty(firstLine);
        }

        /// <summary>
        /// 解析并返回下一条消息
        /// </summary>
        /// <returns>下一条消息得到的 MessageInfo 对象</returns>
        public MessageInfo NextMessage()
        {
            MessageInfo result = new MessageInfo();
            if (IsHeader(firstLine))
            {
                DateTime sentTime;
                string id;
                string name;
                ParseHeader(firstLine, out sentTime, out id, out name);
                result.ID = id;
                result.SentTime = sentTime;
                result.Name = name;
            }
            ReadNext();
            result.KeySet = new HashSet<string>();
            StringBuilder builder = new StringBuilder();
            while (HasNextMessage() && !IsHeader(firstLine))
            {
                Match m = LottoryCodeRegex.Match(firstLine);
                while (m.Success)
                {
                    result.KeySet.Add(m.Result("${code}"));
                    m = m.NextMatch();
                }
                builder.Append(firstLine);
                ReadNext();
            }
            result.MessageHashCode = builder.ToString().GetHashCode();
            return result;
        }

        private bool IsHeader(string str)
        {
            return HeaderRegex.Match(str).Success;
        }

        private void ParseHeader(string str, out DateTime sentTime, out string id, out string name)
        {
            Match m = HeaderRegex.Match(str);
            if (m.Success)
            {
                sentTime = DateTime.ParseExact(m.Result("${dateTime}"), DateTimeFormat, CultureInfo.InvariantCulture);
                id = m.Result("${id}");
                name = m.Result("${name}");
            }
            else
            {
                // 注意，代码不应该运行到这里
                sentTime = DateTime.Now;
                id = string.Empty;
                name = string.Empty;
            }
        }

        private void ReadNext()
        {
            firstLine = secondLine;
            secondLine = reader.ReadLine();
        }

    }
}
