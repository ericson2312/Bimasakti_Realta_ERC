using FastReport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesignFormGS
{
    public partial class DesignReportGS : Form
    {
        private Report loReport;

        public DesignReportGS()
        {
            InitializeComponent();
        }

        private void DesignReportGS_Load(object sender, EventArgs e)
        {
            loReport = new Report();
        }

        private void GSM01500PrintCenter_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(GSM01500COMMON.Models.GSM01500PrintCenterModelDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }
    }
}
