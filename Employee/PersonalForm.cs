﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BIG.DataService;
using BIG.Model;
using System.IO;

namespace BIG.Present
{
    public partial class PersonalForm : Form
    {

        public PersonalForm()
        {
            InitializeComponent();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }
        private const int BUTTON_VIEW_COLUMN_INDEX = 9;//temp
        private const int BUTTON_EDIT_COLUMN_INDEX = 0;
        private const int BUTTON_PRINT_COLUMN_INDEX = 1;

        public bool isSearchbyDate { get; set; }
        private void PersonalForm_Load(object sender, EventArgs e)
        {
            InitialSites();
            if (txt_name_sname.Text != "")
            {
                BindDataGrid(txt_name_sname, false);
            }
            else if (txt_pid.Text != "")
            {
                BindDataGrid(txt_pid, false);
            }
            else if (txt_area.Text != "")
            {
                BindDataGridByArea(txt_area);
            }
            else
            {
                BindDataGrid(txt_name_sname, true);
            }
        }

        private void SetColumnHeader()
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                //if (col.Index == 0)
                //{
                //    col.Width = 80;  
                //    col.HeaderText = "View";
                //}
                if (col.Index == 0)
                {
                    col.Width = 80;
                    col.HeaderText = "View/Edit";
                }
                if (col.Index == 1)
                {
                    col.Width = 80;
                    col.HeaderText = "Print";
                }
                if (col.Index == 2)
                {
                    col.Width = 35;
                    col.HeaderText = "No";
                }
                if (col.Index == 3)
                {
                    col.Width = 100;
                    col.HeaderText = "รหัสพนักงาน";

                }
                if (col.Index == 4)
                {
                    col.Width = 100;
                    col.HeaderText = "หมายเลขบัตรประชาชน";

                }
                if (col.Index == 5)
                {
                    col.Width = 150;
                    col.HeaderText = "ชื่อ(TH)";

                }
                if (col.Index == 6)
                {
                    col.Width = 150;
                    col.HeaderText = "นามสกุล(TH)";

                }
                if (col.Index == 7)
                {
                    col.Width = 100;
                    col.HeaderText = "หมายเลขโทรศัพท์";

                }
                if (col.Index == 8)
                {
                    col.Width = 70;
                    col.HeaderText = "วันที่เริ่มงาน";

                }
                if (col.Index == 9)
                {
                    col.Width = 70;
                    col.HeaderText = "เขต";

                }
                if (col.Index == 10)
                {
                    col.Width = 100;
                    col.HeaderText = "สถานะ";
                }
            }
        }

        private void SetButtonCommand()
        {
            try
            {
                var editcol = new DataGridViewButtonColumn();
                editcol.Name = "Edit";
                editcol.Text = "แก้ไข";
                var printcol = new DataGridViewButtonColumn();
                printcol.Name = "Print";
                printcol.Text = "พิมพ์รายงาน";
                if (dataGridView1.Columns["Edit"] == null)
                {
                    dataGridView1.Columns.Insert(BUTTON_EDIT_COLUMN_INDEX, editcol);
                }
                if (dataGridView1.Columns["Print"] == null)
                {
                    dataGridView1.Columns.Insert(BUTTON_PRINT_COLUMN_INDEX, printcol);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error SetButtonCommand : " + ex.Message);
            }
        }

        private void SetButtonValue()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //DataGridViewCell view = row.Cells[BUTTON_VIEW_COLUMN_INDEX];//Column Index
                //view.Value = "ดูข้อมูล";
                DataGridViewCell edit = row.Cells[BUTTON_EDIT_COLUMN_INDEX];//Column Index
                edit.Value = "แก้ไข";
                DataGridViewCell print = row.Cells[BUTTON_PRINT_COLUMN_INDEX];//Column Index
                print.Value = "พิมพ์รายงาน";
            }
        }
        private void BindDataGrid(TextBox tx, bool Site)
        {
            if (!Site)
            {
                var i = 1;
                var lst = EmployeeServices.GetAll();
                var ds = lst.Where(x => x.FIRSTNAME_TH.Contains(txt_name_sname.Text) || x.FIRSTNAME_EN.Contains(txt_name_sname.Text) || x.LASTNAME_TH.Contains(txt_name_sname.Text) || x.LASTNAME_EN.Contains(txt_name_sname.Text)).Select(x =>
                    new { No = i++, x.EMP_ID, x.ID_CARD, x.FIRSTNAME_TH, x.LASTNAME_TH, x.MOBILE, x.DATESTARTWORK, x.AREA, x.STATUS }).ToList();

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = ds;
                dataGridView1.Refresh();
            }
            else
            {
                if (cbo_site.SelectedIndex != 0)
                {
                    var i = 1;
                    var lst = EmployeeServices.GetBySite(cbo_site.SelectedItem.ToString());
                    var ds = lst.Select(x =>
                    new { No = i++, x.EMP_ID, x.ID_CARD, x.FIRSTNAME_TH, x.LASTNAME_TH, x.MOBILE, x.DATESTARTWORK, x.AREA, x.STATUS }).ToList();

                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = ds;
                    dataGridView1.Refresh();
                }
                else
                {
                    var lst = EmployeeServices.GetAll();
                    var i = 1;
                    var ds = lst.Where(x => x.FIRSTNAME_TH.Contains(txt_name_sname.Text) || x.FIRSTNAME_EN.Contains(txt_name_sname.Text) || x.LASTNAME_TH.Contains(txt_name_sname.Text) || x.LASTNAME_EN.Contains(txt_name_sname.Text)).Select(x =>
                    new { No = i++, x.EMP_ID, x.ID_CARD, x.FIRSTNAME_TH, x.LASTNAME_TH, x.MOBILE, x.DATESTARTWORK, x.AREA, x.STATUS }).ToList();
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = ds;
                    dataGridView1.Refresh();
                }
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dataGridView1.Columns[column.Name].SortMode = DataGridViewColumnSortMode.Automatic;
            }

            SetButtonCommand();
            SetColumnHeader();
            SetButtonValue();

        }

        private void BindDataGridByArea(TextBox are)
        {
            int i = 1;
            var lst = EmployeeServices.GetByArea(are.Text);
            var ds = lst.Select(x => new { No = i++, x.EMP_ID, x.ID_CARD, x.FIRSTNAME_TH, x.LASTNAME_TH, x.MOBILE, x.DATESTARTWORK, x.AREA, x.STATUS }).ToList();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ds;
            dataGridView1.Refresh();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dataGridView1.Columns[column.Name].SortMode = DataGridViewColumnSortMode.Automatic;
            }

            SetButtonCommand();
            SetColumnHeader();
            SetButtonValue();
        }

        private void BindDataGridByStartDate(DateTimePicker date)
        {
            int i = 1;
            var lst = EmployeeServices.GetByStartDate(date.Value);
            var ds = lst.Select(x => new { No = i++, x.EMP_ID, x.ID_CARD, x.FIRSTNAME_TH, x.LASTNAME_TH, x.MOBILE, x.DATESTARTWORK, x.AREA, x.STATUS }).ToList();
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ds;
            dataGridView1.Refresh();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dataGridView1.Columns[column.Name].SortMode = DataGridViewColumnSortMode.Automatic;
            }

            SetButtonCommand();
            SetColumnHeader();
            SetButtonValue();
        }

        public void InitialSites()
        {
            cbo_site.Items.Clear();
            var lstSite = DataService.SitesDataService.GetListSiteLocation().OrderByDescending(x => x.CREATE_DATE).ToList();
            cbo_site.Items.Add("==ทั้งหมด==");
            foreach (var item in lstSite)
            {
                cbo_site.Items.Add(item.NAME);
            }
            if (cbo_site.Items.Count > 0)
            {
                cbo_site.SelectedIndex = 0;
            }
        }
        private void btn_search_Click_1(object sender, EventArgs e)
        {
            if (txt_name_sname.Text != "")
            {
                BindDataGrid(txt_name_sname, false);
            }
            else if (txt_pid.Text != "")
            {
                BindDataGrid(txt_pid, false);
            }
            else if (txt_area.Text != "")
            {
                BindDataGridByArea(txt_area);
            }
            else if ((cbo_site.SelectedIndex == -1) && (txt_name_sname.Text == "") && txt_pid.Text == "" && txt_area.Text == "")
            {
                BindDataGridByStartDate(dt_startwork);
            }
            else
            {
                BindDataGrid(txt_name_sname, true);
            }
        }

        private void btn_load_pid_Click(object sender, EventArgs e)
        {

            var empfrm = new EmployeeForm();
            empfrm.Show();
            this.Hide();
        }

        private void left_menu1_Click(object sender, EventArgs e)
        {

        }

        private void ribbonOrbMenuItem2_Click(object sender, EventArgs e)
        {
            var emp = new EmployeeForm();
            emp.Show();
            this.Hide();
        }

        private void rb_new_emp_Click(object sender, EventArgs e)
        {

        }

        private void rb_load_emp_Click(object sender, EventArgs e)
        {


        }

        private void rb_home_Click(object sender, EventArgs e)
        {
            Hide();
            var mainform = new MainForm();
            mainform.Show();
        }

        private void rb_load_pid_Click(object sender, EventArgs e)
        {

        }

        private void rb_new_Click(object sender, EventArgs e)
        {

            try
            {
                var emp = new EmployeeForm();
                emp.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("An error has occured: {0}", ex.Message), "Nffv C# Sample",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_load_Click(object sender, EventArgs e)
        {
            if (txt_pid.Text != "")
            {
                var emp = new EmployeeForm(txt_pid.Text);
                emp.Show();
                this.Hide();
            }
            else
            {
                var emp = new EmployeeForm();
                emp.Show();
                this.Hide();
            }
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            var mainform = new MainForm();
            mainform.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Hide();
            var mainform = new MainForm();
            mainform.Show();
        }

        private void ribbonButton20_Click(object sender, EventArgs e)
        {

        }

        private void ribbonButton21_Click(object sender, EventArgs e)
        {

        }

        private void ribbonButton14_Click(object sender, EventArgs e)
        {
            txt_name_sname.Focus();
        }

        private void ribbonButton15_Click(object sender, EventArgs e)
        {
            this.Hide();
            var pform = new ReportEmployee();
            pform.Show();
        }

        private void rb_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new LoginForm();
            form.Show();
        }

        private void rb_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == BUTTON_EDIT_COLUMN_INDEX && e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[3].Value != null)
                    {
                        var emp_id = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                        e_id = emp_id;
                        var emp = new EmployeeForm(e_id, "Edit");
                        emp.Show();
                        this.Hide();
                    }
                }
            }
            else if (e.ColumnIndex == BUTTON_PRINT_COLUMN_INDEX && e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[3].Value != null)
                    {
                        var emp_id = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                        var form = new ReportEmployee(emp_id);
                        form.Show();
                        this.Hide();
                    }
                }
            }
            else if ((e.ColumnIndex == 2 && e.RowIndex >= 0) || (e.ColumnIndex == 3 && e.RowIndex >= 0) || (e.ColumnIndex == 4 && e.RowIndex >= 0) || (e.ColumnIndex == 5 && e.RowIndex >= 0) || (e.ColumnIndex == 6 && e.RowIndex >= 0) || (e.ColumnIndex == 7 && e.RowIndex >= 0))
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[3].Value != null)
                    {
                        var emp_id = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                        var profile = ProfileImageDataService.GetByEmployeeID(emp_id);
                        if (profile != null)
                        {
                            pic_emp.Image = GetImage(profile.PHOTO, 190, 170);
                        }
                    }
                }
            }
        }

        private Image GetImage(byte[] PHOTO, int width, int height)
        {
            var myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            var myBitmap = new Bitmap(byteArrayToImage(PHOTO));
            var myThumbnail = myBitmap.GetThumbnailImage(width, height, myCallback, IntPtr.Zero);
            return myThumbnail;
        }
        private System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }
        public bool ThumbnailCallback()
        {
            return false;
        }
        public string e_id { get; set; }
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                var emp = new EmployeeForm(e_id, "View");
                emp.Show();
            }));
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Hide();
        }

        private void worker_search_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void worker_search_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void rb_setting_company_Click(object sender, EventArgs e)
        {
            var frm = new CompanyInfoForm();
            frm.Show();
            Hide();
        }

        private void dt_startwork_ValueChanged(object sender, EventArgs e)
        {
            cbo_site.SelectedIndex = -1;
            txt_name_sname.Text = "";
            txt_area.Text = "";
            txt_pid.Text = "";
        }

        private void cbo_order_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_order.SelectedIndex == 1)
            {
                var ds = dataGridView1.DataSource;
            }
            else if (cbo_order.SelectedIndex == 2)
            {

            }
            else if (cbo_order.SelectedIndex == 3)
            {

            }
            else if (cbo_order.SelectedIndex == 4)
            {

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            var form = new CompanyInfoForm();
            form.Show();

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            var form = new CompanyInfoForm();
            form.Show();

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Hide();
            var form = new EmployeeForm();
            form.Show();

        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            var form = new EmployeeForm();
            form.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Hide();
            var form = new ReportEmployee();
            form.Show();
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            var form = new ReportEmployee();
            form.Show();
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 2 && e.RowIndex >= 0) || (e.ColumnIndex == 3 && e.RowIndex >= 0) || (e.ColumnIndex == 4 && e.RowIndex >= 0) || (e.ColumnIndex == 5 && e.RowIndex >= 0) || (e.ColumnIndex == 6 && e.RowIndex >= 0) || (e.ColumnIndex == 7 && e.RowIndex >= 0))
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[2].Value != null)
                    {
                        var emp_id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                        var profile = ProfileImageDataService.GetByEmployeeID(emp_id);
                        if (profile != null)
                        {
                            pic_emp.Image = GetImage(profile.PHOTO, 190, 170);
                        }
                    }
                }
            }
        }

        private void ribbonAllEmployee_Click(object sender, EventArgs e)
        {
            Hide();
            var form = new ReportAllEmployeeForm();
            form.Show();
        }

        private void rb_employee_Click(object sender, EventArgs e)
        {
            Hide();
            var form = new ReportEmployee();
            form.Show();
        }

        private void PersonalForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            var form = new MainForm();
            form.Show();
        }
    }
}
