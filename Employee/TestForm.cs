using BIG.DataService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
  
namespace BIG.Present
{
    public partial class TestForm : Form
    {

         
        public TestForm()
        {
            InitializeComponent();
        } 
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cleardatabase();
                RunWorkerCompletedEventArgs taskResult = BusyForm.RunLongTask("Waiting for fingerprint ...", new DoWorkEventHandler(doEnroll),
                    false, null, new EventHandler(CancelScanningHandler));
                EnrollmentResult enrollmentResult = (EnrollmentResult)taskResult.Result;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }
        internal class EnrollmentResult
        { 
        };

        private void CancelScanningHandler(object sender, EventArgs e)
        { 
        }
        private void doEnroll(object sender, DoWorkEventArgs args)
        {
           
        }

        private void cleardatabase()
        {
              
        }
    }
}
