using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rename;

namespace main
{
    public class Page {
        public Type FormType = null;
        public string Name {
            get{ return this.FormType.Name; }
        }
        public string FullName
        {
            get { return this.FormType.FullName; }
        }
        public String Title = "";
        public String DLL = "";
        public Page(Type form,String title,string DLL = "") {
            this.FormType = form;
            this.Title = title;
            this.DLL = DLL;
        }
    }

    public partial class Main : Form
    {
        private Dictionary<String, Form> Forms = new Dictionary<String, Form>();
        private Form activeForm = null;

        private IList<Page> pages = new List<Page>();

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pages.Add(new Page(typeof(Home), "首页"));
            this.pages.Add(new Page(typeof(Rename.Form1),"重命名"));
            this.pages.Add(new Page(typeof(Module), "页面管理"));
            this.pages.Add(new Page(typeof(About), "关于"));
            this.updateMenu();
            this.showHomeForm();
        }

        private void updateMenu() {
            this.listView1.Items.Clear();
            foreach(Page page in this.pages)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Name = page.FormType.Name;
                lvi.Text = page.Title;
                this.listView1.Items.Add(lvi);
            }
        }

        private void showHomeForm() {
            this.showForm("Home");
        }

        private Page getPage(string name) {
            return this.pages.First<Page>(p => p.Name == name);
        }

        private void showForm(string id) {
            Type formType = getPage(id).FormType;
            Form frm = null;
            if (this.Forms.ContainsKey(id))
            {
                this.Forms.TryGetValue(id,out frm);
                frm.Show();
            }
            else {
                if (typeof(Form).IsAssignableFrom(formType)) {
                    frm = (Form)Activator.CreateInstance(formType);
                    frm.MdiParent = this;
                    frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    this.Forms.Add(id, frm);
                }
            }
            if (this.activeForm != null)
            {
                this.activeForm.Hide();
            }
            if (frm!=null)
            {
                this.activeForm = frm;
                frm.Show();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            if (lv.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lv.SelectedItems[0];
                this.showForm(lvi.Name);
            }
        }
    }
}
