using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CTDL_GT
{
    public partial class Form1 : Form
    {
        private ArrayList TextList = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            TextList.Clear();
            ArrayList partList = Split(textBox1.Text);
            handleShow(partList);
        }
        private ArrayList Split(string temp)
        {
            string test = "";
            ArrayList arr = new ArrayList();
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == '<')
                {
                    if (test != "")
                    {
                        arr.Add(test);
                    }
                    test = temp[i].ToString();
                }
                else if (temp[i] == '>')
                {
                    arr.Add(test + temp[i]);
                    test = "";
                }
                else if (temp[i] != '\n' && temp[i] != '\r')
                {
                    test += temp[i];
                }
            }
            return arr;
        }
        private bool IsOpeningTag(string tag)
        {
            return ((tag[0] == '<') && (tag[1] != '/') && (tag[tag.Length - 1] == '>'));
        }
        private bool IsEndingTag(string tag)
        {
            return ((tag[0] == '<') && (tag[1] == '/') && (tag[tag.Length - 1] == '>'));
        }
        private bool matchingTag(string tag1, string tag2)
        {
            return tag1.Substring(1, tag1.Length - 1) == tag2.Substring(2, tag2.Length - 2);
        }
        static void QueueReverse(Queue q, string s)
        {
            Queue temp = new Queue();
            temp.Enqueue(s);
            while (q.Count > 0)
            {
                temp.Enqueue(q.Dequeue());
            }
            while (temp.Count > 0)
            {
                q.Enqueue(temp.Dequeue());
            }

        }
        private bool Check(ArrayList tag)
        {
            Queue q = new Queue();
            for (int i = 0; i < tag.Count; i++)
            {
                Console.WriteLine($"tag {tag[i]}");
                if (IsOpeningTag((string)tag[i]))
                {
                    QueueReverse(q, (string)tag[i]);
                }
                else if (IsEndingTag((string)tag[i]))
                {
                    if (q.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (!matchingTag((string)q.Dequeue(), (string)tag[i]))
                        {
                            return false;
                        }
                    }
                }
                else if ((string)tag[i] != " ")
                {
                    TextList.Add(tag[i]);
                }
            }
            if (q.Count == 0)
            {
                return true;
            }
            else { return false; }
        }
        private void handleShow(ArrayList a)
        {
            if (a.Count < 1)
            {
                textBox3.AppendText("Chưa nhập tệp HTML!");
                textBox3.ForeColor = Color.MediumOrchid;
            }
            else if (Check(a))
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < TextList.Count; i++)
                {
                    sb.AppendLine(TextList[i].ToString());
                }
                textBox2.AppendText(sb.ToString());
                textBox3.AppendText("Chạy thành công!");
                textBox3.ForeColor = Color.SeaGreen;
            }
            else
            {
                textBox3.AppendText("Có thẻ bị lỗi!");
                textBox3.ForeColor = Color.DarkRed;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
