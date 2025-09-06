using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DragDrop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            DragDropEffects effect;

            int index = listBox1.IndexFromPoint(e.X, e.Y);
            if (index != ListBox.NoMatches)
            {
                string item = (string)listBox1.Items[index];
                effect = DoDragDrop(item, DragDropEffects.Copy | DragDropEffects.Move);
                if (effect == DragDropEffects.Move)
                {
                    listBox1.Items.RemoveAt(index);
                }
            }
        }

        private void listBox1_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (e.EscapePressed)
            {
                e.Action = DragAction.Cancel;
            }
        }

        private void listBox2_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                if ((e.KeyState & 8) != 0)
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                listBox2.Items.Add(e.Data.GetData(DataFormats.StringFormat));
            }
        }
    }
}