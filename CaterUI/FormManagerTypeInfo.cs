using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaterBll;

namespace CaterUI
{
    public partial class FormManagerTypeInfo : Form
    {
        public FormManagerTypeInfo()
        {
            InitializeComponent();
            this.dgvList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvList.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void ManagerTypeInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        VIPTypeInfoBll bll=new VIPTypeInfoBll();

        void LoadList()
        {
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = bll.GetList();
        }
    }
}
