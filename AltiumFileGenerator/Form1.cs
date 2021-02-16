using AltiumFileGenerator.Services;
using AltiumFileGenerator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltiumFileGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            fileGenerator = new FileGenerator(prgBarCreation.Maximum);
            fileGenerator.OnDataPartCreated += FileGenerator_OnPartChanged;
        }

        private readonly IFileGenerator fileGenerator;

        private async void btnOk_Click(object sender, EventArgs e)
        {
            DisableControlsAfterStart();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                var fileSizeInGb = (ushort)nudFileSize.Value;

                await Task.Run(() => fileGenerator.GenerateFile(fileSizeInGb));

                stopwatch.Stop();
                MessageBox.Show($"File is created! Elapsed time is: {stopwatch.Elapsed}");
            }
            catch (Exception x)
            {
                MessageBox.Show($"Create file error: {x.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControlsAfterFinish();
            }
        }

        private void DisableControlsAfterStart()
        {
            nudFileSize.Enabled = false;
            prgBarCreation.Visible = true;
            prgBarCreation.Value = 0;
            btnOk.Enabled = false;
        }

        private void EnableControlsAfterFinish()
        {
            nudFileSize.Enabled = true;
            prgBarCreation.Visible = false;
            btnOk.Enabled = true;
        }

        private void FileGenerator_OnPartChanged(object sender, EventArgs e)
        {
            prgBarCreation.Invoke(new MethodInvoker(() => prgBarCreation.PerformStep()));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
