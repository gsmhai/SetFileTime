using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SetFileTime
{
    public partial class Form1 : Form
    {
        public DateTime CreateDateTime, AccessDateTime, ModifyDateTime;
        public long CreateTimeStamp, AccessTimeStamp, ModifyTimeStamp;
        public Form1()
        {
            InitializeComponent();
        }

        public long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) ;
            return t;
        }

        public string GetTimeStamp(System.DateTime time)
        {
            long ts = ConvertDateTimeToInt(time);
            return ts.ToString();
        }

        public DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp);
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        public DateTime ConvertTimeStampToDateTime(long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = timeStamp;
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        } 

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = @".//";
                openFileDialog.Title = "请选择要打开的文件";
                openFileDialog.Filter = "所有文件|*.*";
                string filepath = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filepath = openFileDialog.FileName.ToString();
                    textBox1.Text = filepath;
                    FileInfo fileInfo = new FileInfo(filepath);
                    CreateDateTime = fileInfo.CreationTime;
                    AccessDateTime = fileInfo.LastAccessTime;
                    ModifyDateTime = fileInfo.LastWriteTime;

                    textBox2.Text = CreateDateTime.ToString();
                    dateTimePicker1.Text = textBox2.Text;

                    CreateTimeStamp = ConvertDateTimeToInt(CreateDateTime);
                    AccessTimeStamp = ConvertDateTimeToInt(AccessDateTime);
                    ModifyTimeStamp = ConvertDateTimeToInt(ModifyDateTime);

                    textBox3.Text = AccessDateTime.ToString();
                    dateTimePicker2.Text = textBox3.Text;
                    textBox4.Text = ModifyDateTime.ToString();
                    dateTimePicker3.Text = textBox4.Text;

                }
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("打开失败，请检查权限");
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filepath = textBox1.Text.ToString();
            if (filepath != string.Empty)
            {
                string settimestr = dateTimePicker1.Text.ToString();                
                DateTime dt = Convert.ToDateTime(settimestr);
                long ts =  ConvertDateTimeToInt(dt);
                ts += CreateTimeStamp % 10000000;
                dt = ConvertStringToDateTime(ts.ToString());
                try
                {
                    File.SetCreationTime(filepath, dt);
                    //MessageBox.Show("修改成功");
                }
                catch (System.UnauthorizedAccessException)
                {
                    MessageBox.Show("修改失败,权限问题,并且请检查文件的只读属性");
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.ToString());
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filepath = textBox1.Text.ToString();
            if (filepath != string.Empty)
            {
                string settimestr = dateTimePicker2.Text.ToString();
                DateTime dt = Convert.ToDateTime(settimestr);
                long ts = ConvertDateTimeToInt(dt);
                ts += CreateTimeStamp % 10000000;
                dt = ConvertStringToDateTime(ts.ToString());
                try
                {
                    File.SetLastAccessTime(filepath, dt);
                    //MessageBox.Show("修改成功");
                }
                catch (System.UnauthorizedAccessException)
                {
                    MessageBox.Show("修改失败,权限问题,并且请检查文件的只读属性");
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.ToString());
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string filepath = textBox1.Text.ToString();
            if (filepath != string.Empty)
            {
                string settimestr = dateTimePicker3.Text.ToString();
                DateTime dt = Convert.ToDateTime(settimestr);
                long ts = ConvertDateTimeToInt(dt);
                ts += CreateTimeStamp % 10000000;
                dt = ConvertStringToDateTime(ts.ToString());
                try
                {
                    File.SetLastWriteTime(filepath, dt);
                    //MessageBox.Show("修改成功");
                }
                catch (System.UnauthorizedAccessException)
                {
                    MessageBox.Show("修改失败,权限问题,并且请检查文件的只读属性");
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.ToString());
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
            button3_Click(sender, e);
            button4_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string filepath = textBox1.Text;
            textBox1.Text = filepath;
            FileInfo fileInfo = new FileInfo(filepath);
            CreateDateTime = fileInfo.CreationTime;
            AccessDateTime = fileInfo.LastAccessTime;
            ModifyDateTime = fileInfo.LastWriteTime;

            textBox2.Text = CreateDateTime.ToString();
            dateTimePicker1.Text = textBox2.Text;

            CreateTimeStamp = ConvertDateTimeToInt(CreateDateTime);
            AccessTimeStamp = ConvertDateTimeToInt(AccessDateTime);
            ModifyTimeStamp = ConvertDateTimeToInt(ModifyDateTime);

            textBox3.Text = AccessDateTime.ToString();
            dateTimePicker2.Text = textBox3.Text;
            textBox4.Text = ModifyDateTime.ToString();
            dateTimePicker3.Text = textBox4.Text;
        }


    }
}
