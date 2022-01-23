using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContacts
{
   
    public partial class Form1 : Form
    {
        IContactRepository repository;
        public Form1()
        {
            InitializeComponent();
            repository = new ContactRepository();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            DgContact.AutoGenerateColumns = false;
            DgContact.DataSource = repository.SeleectAll();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            FrmAddOrEdit frm = new FrmAddOrEdit();
            frm.ShowDialog();
            if(DialogResult==DialogResult.OK)
            {
                BindGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if(DgContact.CurrentRow!=null)
            {
                string Name = DgContact.CurrentRow.Cells[1].Value.ToString();
                string Family = DgContact.CurrentRow.Cells[2].Value.ToString();
                string FollName = Name + " " + Family;
                if (MessageBox.Show($"آیا از حذف {FollName} اطمینان دارید...؟", "توجه !", MessageBoxButtons.YesNo)==DialogResult.Yes) 
                {
                    int ContactId = int.Parse(DgContact.CurrentRow.Cells[0].Value.ToString());
                    repository.delete(ContactId);
                    BindGrid();
                }
            }
            else
            {
                MessageBox.Show("لطفاً یک شخص رااز لیست انتخاب نمایید");
            }
        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            if(DgContact.CurrentRow!=null)
            {
                int ContactId = int.Parse(DgContact.CurrentRow.Cells[0].Value.ToString());
                FrmAddOrEdit frm = new FrmAddOrEdit();
                frm.ContactId = ContactId;
                if(frm.ShowDialog()==DialogResult.OK)
                {
                    BindGrid();
                }
            }
        }

        private void txtSerch_TextChanged(object sender, EventArgs e)
        {
            DgContact.DataSource = repository.Search(txtSearch.Text);
        }
    }
}
