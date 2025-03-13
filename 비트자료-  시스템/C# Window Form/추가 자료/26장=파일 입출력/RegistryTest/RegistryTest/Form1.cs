using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace RegistryTest
{
    public partial class Form1 : Form
    {
        const string RegRoot = @"Software\MiyoungSoft\RegistryTest";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string Setting = RegRoot + @"\Setting";
            RegistryKey R = Registry.CurrentUser.OpenSubKey(Setting);
            if (R == null)
            {
                textBox1.Text = "첫 문자열";
                numericUpDown1.Value = 12;
            }
            else
            {
                textBox1.Text = (string)R.GetValue("Text");
                numericUpDown1.Value = (int)R.GetValue("Value");
                R.Close();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string Setting = RegRoot + @"\Setting";
            RegistryKey R = Registry.CurrentUser.OpenSubKey(Setting, true);
            if (R == null)
            {
                R = Registry.CurrentUser.CreateSubKey(Setting);
            }

            R.SetValue("Text", textBox1.Text);
            R.SetValue("Value", numericUpDown1.Value, RegistryValueKind.DWord);

            R.Close();
        }
    }
}