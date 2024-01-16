using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihanLKS_chapter2
{
    public partial class print : Form
    {
        public print()
        {
            InitializeComponent();
        }

        private void print_Load(object sender, EventArgs e)
        {
            ReportDocument cr = new ReportDocument();
            cr.Load("C:\\Users\\Administrator\\source\\repos\\latihanLKS-chapter2\\latihanLKS-chapter2");
            crystalReportViewer1.ReportSource = cr;
            crystalReportViewer1.Refresh();
        }
    }
}
