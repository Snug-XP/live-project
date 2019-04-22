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
        /// 活跃度
        /// </summary>
        public int Activity { get; set; }
        /// <summary>
        /// 最近五次不同的发言的频次
        /// </summary>

        public int[] Key = { -1,-1,-1,-1,-1 };
        public int[] Value = { 0, 0, 0, 0, 0 };
        private int index = 0;


        public User(string id, string name, string identity, int numberOfUsualSpeech, int numberOfLotterySpeech, int activity)
        {
            this.ID = id;
            this.Name = name;
            this.Identity = identity;
            this.NumberOfUsualSpeech = numberOfUsualSpeech;
            this.NumberOfLotterySpeech = numberOfLotterySpeech;
            this.Activity = activity;

        }


        public int GetSumActivity()
        {
            return (2 * NumberOfUsualSpeech + NumberOfLotterySpeech + Activity);
        }
        /// <summary>
        /// 淘汰算法
        /// </summary>
        /// <param name="hashcode"></param>
        public void  WeedOut (int hashcode)
        {
            for (int i = 0; i < 5; i++)
            {
                //键存在，值+1
                if (hashcode == Key[index])
                {
                    Value[index]++;
                    if (Value[index] / 2 == 10) Activity -= 50;
                    else if (Value[index] > 20 && Value[index] <= 100)
                    {
                        Activity -= 3;
                        Identity = "预警者";
                    }
                    else if (Value[index] > 100)
                    {
                        Activity -= 1008611;
                        Identity = "危险者";
                    }
                    //活跃度<-1500000后不再减少（防止溢出）
                    else if (Value[index] < -1500000) { };
                    return;
                }
                else
                {
                    index++;
                    if (index >= 5) index = 0;
                }
            }
            //只比对四次-1，第五次直接替换
            for (int i = 0; i < 4; i++)
            {
                if (-1 == Key[index])
                {
                    Key[index] = hashcode;
                    Value[index] = 0;
                    return;
                }
                else
                {
                    index++;
                    if (index >= 5) index = 0;
                }
            }
            Key[index] = hashcode;
            Value[index] = 0;
        }
    }
}
