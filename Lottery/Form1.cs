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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string filePath = textBox1.Text.Trim();
            MessageParser p = new MessageParser(filePath);
            while (p.HasNextMessage())
            {
                MessageInfo info = p.NextMessage();
            }
            Console.WriteLine(filePath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //需要补上类型监测和时间类转化
            string keyword = textBox2.Text.Trim();//关键词
            string filter = comboBox1.Text;//过滤程度
            string copyWriter = richTextBox1.Text.Trim();//文案
            DateTime startTime = dateTimePicker3.Value.Date;
            DateTime endTime = dateTimePicker4.Value.Date;
            // 测试
            Console.WriteLine(keyword + " " + filter + " " + copyWriter + " " + startTime.ToString() + " " + endTime.ToString() );
            //黑名单内所有id
            List<string> vs = new List<string>();
            int dataLine1 = dataGridView1.RowCount - 1;
            for (int i = 0; i < dataLine1; i++) {
                string id = dataGridView1.Rows[i].Cells[0].Value.ToString();//id
                Console.WriteLine(id);
                vs.Add(id);
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
                    Console.WriteLine(awardName + " " + awardMessage + "" + count);
                    //此处封装成数据结构加入到list中
                }
            }
            catch (FormatException err)
            {
                Console.WriteLine(err.Message);
                //转换int的异常
            }
            catch (Exception err)
            {
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
            //需要补上类型监测和时间类转化
            string keyword = textBox5.Text.Trim();//关键词
            DateTime startTime = dateTimePicker1.Value.Date;
            DateTime endTime = dateTimePicker2.Value.Date;
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
    }
}
