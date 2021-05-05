using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Rename
{
    public class FileGridView : DataGridView {
        public void CheckAll()
        {
            foreach (DataGridViewRow row in this.Rows)
            {
                if ((Convert.ToBoolean(row.Cells[0].Value) == false))
                {
                    row.Cells[0].Value = "True";
                }
                else
                    continue;

            }

        }
        public FileGridView() { 
            
        }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string dirPath = this.folderBrowserDialog1.SelectedPath;
                this.textBox1.Text = dirPath;
                this.loaddir(dirPath);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in this.dataGridView1.Rows)
            {
                if (dgvr.Cells[2].Value == dgvr.Cells[3].Value)
                {
                    continue;
                }
                string fullname = (string)dgvr.Cells[1].Value;
                FileInfo fi = new FileInfo(fullname);
                string newname = fi.DirectoryName + "/" + dgvr.Cells[3].Value;
                fi.MoveTo(newname);
                FileInfo newfi = new FileInfo(newname);
                this.setRowData(dgvr, newfi);
            }
        }

        private void setRowData(DataGridViewRow dgvr, FileSystemInfo fi)
        {
            dgvr.Cells[1].Value = fi.FullName;
            dgvr.Cells[2].Value = fi.Name;
            dgvr.Cells[3].Value = fi.Name;
            dgvr.Cells[4].Value = Path.GetExtension(fi.Name);
            dgvr.Cells[5].Value = Path.GetExtension(fi.Name);
        }

        private void loaddir(string dirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            // string[] files = Directory.GetDirectories(dirPath, "*", System.IO.SearchOption.AllDirectories);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
            this.dataGridView1.Rows.Clear();

            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo) //判断是否文件夹
                {
                    //DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                    //subdir.Delete(true); //删除子目录和文件
                }
                else
                {
                    int index = this.dataGridView1.Rows.Add();
                    DataGridViewRow dgrv = this.dataGridView1.Rows[index];
                    this.setRowData(this.dataGridView1.Rows[index], i);
                    //File.Delete(i.FullName); //删除指定文件
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.loaddir(this.textBox1.Text);
        }


        private void selectAll()
        {
            this.dataGridView1.CheckAll();
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            this.selectAll();
        }
    }
}
