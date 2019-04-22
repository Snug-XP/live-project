using System;
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
            if(filePath.Equals(""))
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
                    //获取参与活动列表
                    List<Activity> ans = activityManager.Query(info);
                    foreach (Activity a in ans)
                    {
                        a.Participants.Remove(user.ID);
                        a.AddParticipant(info.ID);
                    }
                }
            }
            MessageBox.Show("导入完成");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //默认
            Activity activity1 = new Activity();
            activity1.KeyWord = "我要红包";
            activity1.BeginTime = dateTimePicker3.Value.Date.AddYears(-1);
            activity1.EndTime = dateTimePicker3.Value.Date.AddYears(10);
            activity1.AddAward(new Award("奖励一", "无", 10));
            activityManager.AddActivity(activity1);

            label11.Text = "";
            
            string keyword = textBox2.Text.Trim();//关键词
            string copyWriter = richTextBox1.Text.Trim();//文案
            DateTime startTime = dateTimePicker3.Value.Date;
            DateTime endTime = dateTimePicker4.Value.Date;
            if (keyword.Equals(""))
            {
                label11.Text = "错误：关键词不得为空";
                return;
            }
            else if (startTime >= endTime)
            {
                label11.Text = "错误：请确保：开始时间 < 结束时间";
                return;
            }
            // 测试
            //Console.WriteLine(keyword + " " + filter + " " + copyWriter + " " + startTime.ToString() + " " + endTime.ToString() );

            Activity activity = new Activity();
            activity.KeyWord = keyword;//关键词
            activity.CopyWrite = copyWriter;//文案
            activity.BeginTime = startTime;
            activity.EndTime = endTime;
            
            //黑名单内所有id
            int dataLine1 = dataGridView1.RowCount - 1;
            for (int i = 0; i < dataLine1; i++) {
                string id = dataGridView1.Rows[i].Cells[0].Value.ToString();//id
                //Console.WriteLine(id);
                //黑名单置入
                activity.AddBlockedParticipant(id);
            }
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
                //往活动管理中添加活动
                if (!activityManager.AddActivity(activity))
                {
                    label11.Text = "错误：在此时间段已有相同关键词活动存在";
                    return;
                }
            }
            catch (FormatException err)
            {
                Console.WriteLine(err.Message);
                label11.Text = "错误：人数应为整数，请重新输入";
                return;
                //转换int的异常
            }
            catch (Exception err)
            {
                label11.Text = "错误：未知来源，请联系我们团队：1484643646@qq.com";
                Console.WriteLine(err.Message);
                return;
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
            int filter = comboBox1.SelectedIndex;//过滤程度
            DateTime sentTime = dateTimePicker2.Value.Date;
            
            if (keyword.Equals(""))
            {
                MessageBox.Show("错误：关键字不得为空");
                return;
            }
            //构造消息
            MessageInfo info = new MessageInfo();
            info.KeySet = new HashSet<string>();
            info.KeySet.Add(keyword);
            info.SentTime = sentTime;
            //查询列表
            List<Activity> list = activityManager.Query(info);

            if (list.Count == 0 )
            {
                MessageBox.Show("错误：未匹配到活动");
            }
            else//处理
            {
                Activity activity = list[0];
                //抽奖
                Draw draw = new Draw(userList, activity);
                dataGridView3.Rows.Clear();
                foreach (Award award in activity.Awards)
                {
                    List<Lottery.User> listUser;
                    if (filter < 2)
                        listUser = draw.CommonFilterList(award.Count);
                    else
                        listUser = draw.DeepFilterList(award.Count);
                    foreach(Lottery.User user in listUser)
                    {
                        int index = dataGridView3.Rows.Add();
                        dataGridView3.Rows[index].Cells[0].Value = keyword;
                        dataGridView3.Rows[index].Cells[1].Value = user.ID;
                        dataGridView3.Rows[index].Cells[2].Value = user.Name;
                        dataGridView3.Rows[index].Cells[3].Value = award.AwardName;
                        dataGridView3.Rows[index].Cells[4].Value = award.AwardMessage;
                    }
                }
                MessageBox.Show("生成结束");
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
    }
}