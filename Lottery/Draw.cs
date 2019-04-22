﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class Draw
    {
        int nofilter = 0;
        int commonfilter = 0;
        int deepfilter = 0;
        
        int totalnum;
        List<T> h_act_List = new List<T>();//高活跃
        List<T> m_act_List = new List<T>();//中活跃
        List<T> l_act_List = new List<T>();//低活跃
        List<T> good_List = new List<T>();//中奖名单
        int[] array = new int[10];
        int a = totalnum /5; 
        int c = totalnum / 2;
        
        public void Settotalnum(int num)
        {
            totalnum = num; 
        }
        public void GetpList(List<T> totalpeople) //人数划分
        {

            T[] sortList = sort(totalpeople);//排序
          
            
            for(int i=0;i<a;i++) //高活跃
            {
                h_act_List.Add(h_act_List[i]);
            }
            for (int i =a ; i < c; i++) //中活跃
            {
                m_act_List.Add(h_act_List[i]);
            }
            for (int i = c; i < totalnum; i++) //低活跃
            {
                l_act_List.Add(h_act_List[i]);
            }
        }
       
        public static List<T> DeepFilterList<T>(int n) //n为所需中将人数
        {

            int iSeed = 10;
            Random ro = new Random(10);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            while (n > 0)
            {
                n--;//进入循环中奖人数减一
                int i; //第一层筛选
                i = ro.Next(0, 11);
                //从高活跃抽取
                if (i <= 5)
                {    
                    int flag = 1;//判断这个人是否可以中奖,1为可中将，0不行
                    int iResult;
                    iResult = ro.Next();
                    int goodnum = iResult % a; //获取中奖号码
                    for (遍历黑名单)
                    {
                        if (id == id)
                        {
                            flag = 0;
                            n++;//中奖人员数返回
                            break;
                        }
                    }
                    if(flag==1)
                    good_List.Add(h_act_List[goodnum]); //加入中奖队列
                }
                else if (i > 5 && i < 8)
                {
                    //从中活跃抽取
                    int flag = 1;//判断这个人是否可以中奖,1为可中将，0不行
                    int iResult;
                    iResult = ro.Next();
                    int goodnum = iResult % (c - a); //获取中奖号码
                    for (遍历黑名单)
                    {
                        if (id == id)
                        {
                            flag = 0;
                            n++;//中奖人员数返回
                            break;
                        }
                    }
                    if (flag == 1)
                        good_List.Add(m_act_List[goodnum]); //加入中奖队列
                }
                if (i > 7)
                {
                    //从低活跃抽取
                    
                    int flag = 1;//判断这个人是否可以中奖,1为可中将，0不行
                    int iResult;
                    iResult = ro.Next();
                    int goodnum = iResult % (totalnum - c); //获取中奖号码
                    for (遍历黑名单)
                    {
                        if (id == id)
                        {
                            flag = 0;
                            n++;//中奖人员数返回
                            break;
                        }
                    }
                    if (flag == 1)
                        good_List.Add(l_act_List[goodnum]); //加入中奖队列
                }
            }
            return good_List;
            //Copy to a array
         
        }
        public static List<T> noFilterList<T>(int n) //n为所需中将人数
        {

            int iSeed = 10;
            Random ro = new Random(10);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            while (n > 0)
            {
                n--;//进入循环中奖人数减一
                int i; //第一层筛选
                i = ro.Next(0, 11);
                //从高活跃抽取
                if (i <= 5)
                {
                   // int flag = 1;//判断这个人是否可以中奖,1为可中将，0不行
                    int iResult;
                    iResult = ro.Next();
                    int goodnum = iResult % a; //获取中奖号码
                   /* for (遍历黑名单)
                    {
                        if (id == id)
                        {
                            flag = 0;
                            n++;//中奖人员数返回
                            break;
                        }
                    }
                    if (flag == 1)*/
                        good_List.Add(h_act_List[goodnum]); //加入中奖队列
                }
                else if (i > 5 && i < 8)
                {
                    //从中活跃抽取
                    //int flag = 1;//判断这个人是否可以中奖,1为可中将，0不行
                    int iResult;
                    iResult = ro.Next();
                    int goodnum = iResult % (c - a); //获取中奖号码
                    /*for (遍历黑名单)
                    {
                        if (id == id)
                        {
                            flag = 0;
                            n++;//中奖人员数返回
                            break;
                        }
                    }
                    if (flag == 1)*/
                        good_List.Add(m_act_List[goodnum]); //加入中奖队列
                }
                if (i > 7)
                {
                    //从低活跃抽取

                    //int flag = 1;//判断这个人是否可以中奖,1为可中将，0不行
                    int iResult;
                    iResult = ro.Next();
                    int goodnum = iResult % (totalnum - c); //获取中奖号码
                    /*for (遍历黑名单)
                    {
                        if (id == id)
                        {
                            flag = 0;
                            n++;//中奖人员数返回
                            break;
                        }
                    }
                    if (flag == 1)*/
                        good_List.Add(l_act_List[goodnum]); //加入中奖队列
                }
            }
            return good_List;
            //Copy to a array

        }
    }
}