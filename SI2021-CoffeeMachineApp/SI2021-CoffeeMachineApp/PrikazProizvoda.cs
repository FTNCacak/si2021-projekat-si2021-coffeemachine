using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SI2021_CoffeeMachineApp
{
    public partial class PrikazProizvoda : Form
    {
        public PrikazProizvoda()
        {
            InitializeComponent();
        }

        private void PrikazProizvoda_Load(object sender, EventArgs e)
        {
            /*DataGridViewImageColumn dgvimgcol = new DataGridViewImageColumn();
            dgvimgcol.HeaderText = "C:\\Users\\sasha\\Desktop\\Asajment.jpg";
            dgvimgcol.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.Columns.Add(dgvimgcol);

            dataGridView1.RowTemplate.Height = 250;

            dataGridView1.AllowUserToAddRows = false;*/
            DataGridViewImageColumn kolona = new DataGridViewImageColumn();
            kolona.HeaderText = "DN";
            kolona.ImageLayout = DataGridViewImageCellLayout.Stretch;
            kolona.Name = "DEEZ";
            dataGridView1.Columns.Insert(0, kolona);
            Bitmap pb = new Bitmap("C:\\Users\\sasha\\Desktop\\Asajment.jpg");
            ((DataGridViewImageCell)dataGridView1.Rows[0].Cells[0]).Value = pb;
        }
    }
}
