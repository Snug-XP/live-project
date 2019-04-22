﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lottery
{
    public partial class Form1 : Form
    {
        UserList userList = new UserList();
        ActivityManager activityManager = new ActivityManager();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = textBox1.Text.Trim();
            if(!filePath.Equals(""))
            {
                MessageBox.Show("路径不得为空");
                return;
            }
            MessageParser p = new MessageParser(filePath);
            while (p.HasNextMessage())
            {
                MessageInfo info = p.NextMessage();
                
                if (!userList.IsExist(info.ID))
                {
                    //用户首次发言=进群，不计次数
                    User user = new User(info.ID, info.Name, "学生", 0, 0, 0);
                    userList.Add(user);
                }
                else
                {
                    //存在用户，判断发言类型并增加
                    User user = userList.GetUser(info.ID);
                    int lj =  (info.KeySet.Count == 0) ? (user.NumberOfUsualSpeech++) : (user.NumberOfLotterySpeech++);
                    user.WeedOut(info.MessageHashCode);
                    foreach (string code in info.KeySet)
                    {
                        //根据code查找active
                        //activity.AddParticipant(info.ID);
                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label11.Text = "";
            
            string keyword = textBox2.Text.Trim();//关键词
            string copyWriter = richTextBox1.Text.Trim();//文案
            DateTime startTime = dateTimePicker3.Value.Date;
            DateTime endTime = dateTimePicker4.Value.Date;
            if (!keyword.Equals(""))
            {
                label11.Text = "错误：关键词不得为空";
            }
            else if (startTime >= endTime)
            {
                label11.Text = "错误：请确保：开始时间 < 结束时间";
            }
            // 测试
            //Console.WriteLine(keyword + " " + filter + " " + copyWriter + " " + startTime.ToString() + " " + endTime.ToString() );

            Activity activity = new Activity();
            activity.KeyWord = keyword;//关键词
            activity.CopyWrite = copyWriter;//文案
            activity.BeginTime = startTime;
            activity.EndTime = endTime;

            //黑名单内所有id
            List<string> vs = new List<string>();
            int dataLine1 = dataGridView1.RowCount - 1;
            for (int i = 0; i < dataLine1; i++) {
                string id = dataGridView1.Rows[i].Cells[0].Value.ToString();//id
                Console.WriteLine(id);
                vs.Add(id);
            }
            //黑名单置入
            activity.BackIds = vs;
            try
            {
                
                //奖项爬取  奖励名，奖品信息 人数，你们自己封装成数据结构吧
                int dataLine2 = dataGridView2.RowCount - 1;
                for (int i = 0; i < dataLine2; i++)
                {
                    string awardName = dataGridView2.Rows[i].Cells[0].Value.ToString();//奖励名
                    string awardMessage = dataGridView2.Rows[i].Cells[1].Value.ToString();//奖品
                    int count = Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value);//人数
                    Award award = new Award(awardName, awardName, count);
                    //奖励名单置入
                    activity.AddAward(award);
                }
            }
            catch (FormatException err)
            {
                Console.WriteLine(err.Message);
                label11.Text = "错误：人数应为整数，请重新输入";
                //转换int的异常
            }
            catch (Exception err)
            {
                label11.Text = "错误：未知来源，请联系我们团队：1484643646@qq.com";
                Console.WriteLine(err.Message);
            }

        }

        private void DataGridView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                e.Row.HeaderCell.Value = string.Format("{0}", e.Row.Index + 1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string keyword = textBox5.Text.Trim();//关键词
            string filter = comboBox1.Text;//过滤程度
            DateTime sentTime = dateTimePicker2.Value.Date;
            if (!keyword.Equals(""))
            {
                MessageBox.Show("错误：关键字不得为空");
                return;
            }
            else if (true)
            {
                MessageBox.Show("错误：请确保：开始时间 < 结束时间");
            }
            //匹配活动
            //action = 

            //处理
            dataGridView3.Rows.Clear();
            int count = 5;//需要改
            for (int i = 0; i < count; ++i)
            {
                int index = dataGridView3.Rows.Add();
                dataGridView3.Rows[index].Cells[0].Value = keyword;
                dataGridView3.Rows[index].Cells[1].Value = "中奖ID";
                dataGridView3.Rows[index].Cells[2].Value = "中奖昵称";
                dataGridView3.Rows[index].Cells[3].Value = "奖励名称";
                dataGridView3.Rows[index].Cells[4].Value = "奖品信息";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "文本文件|*.txt|所有文件|*.*";
            if (DialogResult.OK == openDialog.ShowDialog())
            {
                string filename = openDialog.FileName;
                textBox1.Text = filename;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
