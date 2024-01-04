using FastReport;
using System;
using System.Collections;
using APT00100COMMON.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesignFormAP
{
    public partial class DesignReportAP : Form
    {
        private Report loReport;

        public DesignReportAP()
        {
            InitializeComponent();
        }

        private void DesignReportAP_Load(object sender, EventArgs e)
        {
            loReport = new Report();
        }

        private void APT00110PrintReport_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(APT00110PrintReportModelDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }
    }
}
