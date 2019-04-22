using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class Draw
    {
        public int nofilter = 0;
        public int commonfilter = 0;
        public int deepfilter = 0;

        public List<User> userList;
        public List<User> h_act_List = new List<User>();//高活跃
        public List<User> m_act_List = new List<User>();//中活跃
        public List<User> l_act_List = new List<User>();//低活跃
        public List<User> good_List = new List<User>();//中奖名单

        Activity activity;

        public int[] array = new int[10];
        public int totalnum;
        public int a;
        public int c;

        public Draw(UserList userList,Activity activity)
        {
            userList.Sort();//排序
            totalnum = userList.GetTotal();
            a = totalnum / 5;
            c = totalnum / 2;
            this.userList = userList.userList;
            this.activity = activity;
            
            GetpList();
        }

        public void Settotalnum(int num)
        {
            totalnum = num; 
        }
        public void GetpList() //人数划分
        {
            for(int i=0;i<a;i++) //高活跃
            {
                h_act_List.Add(userList[i]);
            }
            for (int i =a ; i < c; i++) //中活跃
            {
                m_act_List.Add(userList[i]);
            }
            for (int i = c; i < totalnum; i++) //低活跃
            {
                l_act_List.Add(userList[i]);
            }
        }
       
        public List<Lottery.User> DeepFilterList(int n) //n为所需中奖人数
        {

            Random ro = new Random(10);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            while (n > 0)
            {
                
                int i; //第一层筛选
                i = ro.Next(0, 11);
                //从高活跃抽取
                if (i <= 5)
                {    
                    int flag = 1;//判断这个人是否可以中奖,1为可中奖，0不行
                    int iResult;
                    iResult = ro.Next();
                    int goodnum = iResult % a; //获取中奖号码
                    string goodID = h_act_List[goodnum].ID;
                    foreach (string userID in activity.GetParticipantIds())
                    {
                        if (goodID.CompareTo(userID) == 0)  //该User在当前活动的参与者中
                        {
                            foreach (string userId in activity.GetBlackList())
                            {
                                if (userId.CompareTo(goodID) == 0) //这个人在黑名单
                                {
                                    flag = 0;
                                   
                                    break;
                                }
                            }

                            if (flag == 1)
                            {
                                good_List.Add(h_act_List[goodnum]); //加入中奖队列!!!!
                                n--;//中奖人数减一
                                break;
                            }
                            else
                                break;
                        }
                      
                    }
                    
                }
                else if (i > 5 && i < 8)
                {
                    //从中活跃抽取
                    int flag = 1;//判断这个人是否可以中奖,1为可中将，0不行
                    int iResult;
                    iResult = ro.Next();
                    int goodnum = iResult % (c - a); //获取中奖号码

                    string goodID = m_act_List[goodnum].ID;
                    foreach (string userID in activity.GetParticipantIds())
                    {
                        if (goodID.CompareTo(userID) == 0)  //该User在当前活动的参与者中
                        {
                            foreach (string userId in activity.GetBlackList())
                            {
                                if (userId.CompareTo(goodID) == 0) //这个人在黑名单
                                {
                                    flag = 0;
                                    break;
                                }
                            }

                            if (flag == 1)
                            {
                                good_List.Add(m_act_List[goodnum]); //加入中奖队列!!!!
                                n--;//中奖人数减一
                                break;
                            }
                            else
                                break;
                        }
                    }
                }
                else if (i > 7)
                {
                    //从低活跃抽取
                    
                    int flag = 1;//判断这个人是否可以中奖,1为可中将，0不行
                    int iResult;
                    iResult = ro.Next();
                    int goodnum = iResult % (totalnum - c); //获取中奖号码

                    string goodID = l_act_List[goodnum].ID;
                    foreach (string userID in activity.GetParticipantIds())
                    {
                        if (goodID.CompareTo(userID) == 0)  //该User在当前活动的参与者中
                        {
                            foreach (string userId in activity.GetBlackList())
                            {
                                if (userId.CompareTo(goodID) == 0) //这个人在黑名单
                                {
                                    flag = 0;
                                    break;
                                }
                            }

                            if (flag == 1)
                            {
                                good_List.Add(l_act_List[goodnum]); //加入中奖队列!!!!
                                n--;//中奖人数减一
                                break;
                            }
                            else
                                break;
                        }
                    }
                }
            }
           
            return good_List;
            //Copy to a array
         
        }

        public List<Lottery.User> CommonFilterList(int n) //n为所需中将人数
        {
            ///if(activity.Participants.ToArray().Length<0)

            Random ro = new Random(10);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            while (n > 0)
            {
                int i; //第一层筛选
                i = ro.Next(0, 11);
                //从高活跃抽取
                if (i <= 5)
                {
                   // int flag = 1;//判断这个人是否可以中奖,1为可中将，0不行
                    int iResult;
                    iResult = ro.Next();
                    int goodnum = iResult % a; //获取中奖号码
                    string goodID = h_act_List[goodnum].ID;
                    foreach (string userID in activity.GetParticipantIds())
                    {
                        if (goodID.CompareTo(userID) == 0)  //该User在当前活动的参与者中
                        {
                            good_List.Add(h_act_List[goodnum]); //加入中奖队列
                            n--;//中奖人数减一
                            break;
                        }
                    }
                }
                else if (i > 5 && i < 8)
                {
                    //从中活跃抽取
                    //int flag = 1;//判断这个人是否可以中奖,1为可中将，0不行
                    int iResult;
                    iResult = ro.Next();
                    int goodnum = iResult % (c - a); //获取中奖号码
                    string goodID = m_act_List[goodnum].ID;
                    foreach (string userID in activity.GetParticipantIds())
                    {
                        if (goodID.CompareTo(userID) == 0)  //该User在当前活动的参与者中
                        {
                            good_List.Add(m_act_List[goodnum]); //加入中奖队列
                            n--;//中奖人数减一
                            break;
                        }
                    }
                }
                if (i > 7)
                {
                    //从低活跃抽取

                    //int flag = 1;//判断这个人是否可以中奖,1为可中将，0不行
                    int iResult;
                    iResult = ro.Next();
                    int goodnum = iResult % (totalnum - c-1); //获取中奖号码
                    string goodID = l_act_List[goodnum].ID;
                    foreach (string userID in activity.GetParticipantIds())
                    {
                        if (goodID.CompareTo(userID) == 0)  //该User在当前活动的参与者中
                        {
                            good_List.Add(l_act_List[goodnum]); //加入中奖队列
                            n--;//中奖人数减一
                            break;
                        }
                    }
                }
            }
            return good_List;
            //Copy to a array
        }



    }
}
