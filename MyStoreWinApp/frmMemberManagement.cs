using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomobileWinApp
{
    public partial class frmMemberManagement : Form
    {
        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        public frmMemberManagement()
        {
            InitializeComponent();
        }

        private void frmMemberManagement_Load(object sender, EventArgs e)
        {
            LoadMemberList();
        }

        private void ClearTest()
        {
            txtMemberID.Text = string.Empty;
            txtMemberName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCountry.Text = string.Empty;
        }
        
        private void LoadMemberList()
        {
            var members = memberRepository.GetMembers();
            try
            {
                source = new BindingSource();
                source.DataSource = members;
                dgvMemberList.DataSource = source;
                btnDelete.Enabled = false;
                ClearTest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MemberObject member = null;
            try
            {
                member = new MemberObject
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                };
            memberRepository.UpdateMember(member);
            LoadMemberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update a member");
            }
}

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            MemberObject member = null;
            try
            {
                member = new MemberObject
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                };
                memberRepository.InsertMember(member);
                LoadMemberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add a new member");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                memberRepository.DeleteMember(int.Parse(txtMemberID.Text));
                LoadMemberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a car");
            }
        }
        int position = -1;
        private void dgvMemberList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            position = e.RowIndex;
            if (position == -1) return;
            btnDelete.Enabled = true;
            DataGridViewRow row = dgvMemberList.Rows[position];
            txtMemberID.Text = row.Cells[0].Value.ToString();
            txtMemberName.Text = row.Cells[1].Value.ToString();
            txtEmail.Text = row.Cells[2].Value.ToString();
            txtPassword.Text = row.Cells[3].Value.ToString();
            txtCity.Text = row.Cells[4].Value.ToString();
            txtCountry.Text = row.Cells[5].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvMemberList.DataSource = memberRepository.GetMembersByName(txtSearch.Text.ToLower());
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMemberList.DataSource = memberRepository.SortMembers(cboFilter.Text);
        }
    }
}
