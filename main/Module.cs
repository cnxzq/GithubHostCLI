using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace main
{
    public partial class Module : Form
    {
        private OpenFileDialog openFileDialog1;
        private Button button1;
        private TextBox textBox1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn name;
        private DataGridViewTextBoxColumn FullName;
        private Button button2;
        private Label label1;

        public Module()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(599, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(61, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(507, 23);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.FullName});
            this.dataGridView1.Location = new System.Drawing.Point(12, 45);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(662, 242);
            this.dataGridView1.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 307);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // FullName
            // 
            this.FullName.HeaderText = "类";
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            // 
            // Module
            // 
            this.ClientSize = new System.Drawing.Size(686, 398);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Module";
            this.Load += new System.EventHandler(this.Module_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string LastOpenDllPath = ConfigurationManager.AppSettings["LastOpenDllPath"];
         
            if (LastOpenDllPath == null) {
                LastOpenDllPath = System.Environment.CurrentDirectory.ToString();
            }

            openFileDialog1.Multiselect = true;//该值确定是否可以选择多个文件
            openFileDialog1.Title = "请选择DLL";
            openFileDialog1.Filter = "所有文件(*.dll)|*.dll";
            openFileDialog1.InitialDirectory = LastOpenDllPath;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                this.textBox1.Text = file;
                ConfigurationManager.AppSettings["LastOpenDllPath"] = file;

                Assembly assembly = Assembly.LoadFile(file);
                Type[] types = assembly.GetTypes();




                /*
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
                textboxcell.Value = "aaa";
                row.Cells.Add(textboxcell);
                DataGridViewComboBoxCell comboxcell = new DataGridViewComboBoxCell();
                row.Cells.Add(comboxcell);
                dataGridView1.Rows.Add(row);
                */
                this.dataGridView1.Rows.Clear();
                foreach (Type t in types)
                {
                    if (t.BaseType == typeof(Form))
                    {
                        int index = this.dataGridView1.Rows.Add();
                        this.dataGridView1.Rows[index].Cells[0].Value = t.Name;
                        this.dataGridView1.Rows[index].Cells[1].Value = t.FullName;
                    }
                }
            }
        }

        private void Module_Load(object sender, EventArgs e)
        {

        }
    }
}
