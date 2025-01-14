using ABCEvoEscritorio.BDD;
using ABCEvoEscritorio.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ABCEvoEscritorio.Pantallas
{
    public partial class Frm_members : Form
    {
        MariaDB db = new MariaDB();
        List<filaMember> mems = new List<filaMember>();
        List<member> memsl = new List<member>();

        public Frm_members()
        {
            InitializeComponent();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)  // para jalar el Escape
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }



        #region BARRA
        public void sstProgreso()
        {
            tstProgreso.PerformStep();
        }

        public void sstDatos(string texto, int maximo)
        {
            tslabel.Text = texto;
            tstProgreso.Value = 0;
            tstProgreso.Maximum = maximo;
            sst.Refresh();
        }

        private void sstInicie()
        {
            panel3.Visible = true;
            sst.Visible = true;
            tstProgreso.Visible = true;
            tstProgreso.Value = 0;
            tstProgreso.Step = 1;
            tslabel.Visible = true;
            tslabel.Text = "";
            sst.Refresh();
        }

        private void sstFinalice()
        {
            sst.Visible = false;
            tslabel.Visible = false;
            tstProgreso.Visible = false;
            panel3.Visible = false;
        }
        #endregion



        private void Frm_members_Load(object sender, EventArgs e)
        {
            Cargue();
        }

        private void Cargue()
        {
            memsl.Clear();
            memsl = db.GetMembersl();

            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = true;
            dgv.Rows.Clear();
            if (memsl.Count > 0)
            {
                foreach (member emp in memsl)
                {

                    DataGridViewRow row = (DataGridViewRow)dgv.Rows[0].Clone();
                    row.Cells[0].Value = emp.firstName;
                    row.Cells[1].Value = emp.lastName;
                    row.Cells[2].Value = emp.branchName;
                    row.Cells[3].Value = emp.document;
                    row.Cells[4].Value = emp.email;
                    row.Cells[5].Value = emp.address;
                    row.Cells[6].Value = emp.idMember;


                    dgv.Rows.Add(row);
                }
                dgv.AllowUserToAddRows = false;
                dgv.Sort(dgv.Columns[0], ListSortDirection.Ascending);

            }

        }

        private void btExcel_Click(object sender, EventArgs e)
        {
            sstInicie();
            Excel_Letras let = new Excel_Letras();

            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Workbooks.Add();
            Microsoft.Office.Interop.Excel._Worksheet ht = (Microsoft.Office.Interop.Excel.Worksheet)excelApp.ActiveSheet;

            int NFI = 1;
            String uc = "AH";

            ht.Cells[NFI, 1] = "CLIENTES EVO";
            string ER = "A1:" + uc + "1";

            ht.Range[ER].Font.Bold = true;
            NFI++;
            ht.Cells[NFI, 1] = DateTime.Now.ToString("d");

            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                ht.Cells[NFI, i + 1] = dgv.Columns[i].HeaderText;
            }
            ER = "A2:" + uc + "2";
            ht.Range[ER].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = 7;
            ht.Range[ER].Font.Bold = true;


            NFI++;
            sstDatos("Copiando a Excel", dgv.Rows.Count);

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                DataGridViewRow presu = dgv.Rows[i];

                for (int j = 0; j < dgv.ColumnCount; j++)
                {
                    if (presu.Cells[j].Value != null)
                    {
                        ht.Cells[NFI, j + 1] = presu.Cells[j].Value.ToString();

                    }

                }
                ER = "A" + NFI.ToString() + ":" + uc + NFI.ToString();
                ht.Range[ER].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = 7;
                NFI++;
                sstProgreso();
            }

            ER = "A" + NFI.ToString() + ":" + uc + NFI.ToString();
            ht.Range[ER].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = 7;


            ////LINEAS VERTICALES
            //NFI = NFI - 1;
            //for (int i = 1; i < 69; i++)
            //{
            //    ER = let.letra(i) + "2:" + let.letra(i) + NFI.ToString();
            //    ht.Range[ER].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 7;

            //}

            //ht.Columns.Range["A:A"].ColumnWidth = 5;
            ER = "A2:" + uc + NFI.ToString();
            ht.Rows.Range[ER].RowHeight = 15;

            excelApp.Columns.AutoFit();
            sstFinalice();

            excelApp.Visible = true;

            MessageBox.Show("Información fue pasada a Excel", "Tenga la bondad", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

    }
}
