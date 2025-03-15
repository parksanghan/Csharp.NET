using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ListViewData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        struct City
        {
            public string name;
            public int area;
            public int popu;
            public City(string aname, int aarea, int apopu)
            {
                name = aname; area = aarea; popu = apopu;
            }
        }

        City[] arCity = {
            new City("서울", 605, 1026),
            new City("부산", 758, 381),
            new City("용인", 591, 583),
            new City("춘천", 1116, 25),
            new City("홍천", 1817, 7),
        };

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (City C in arCity)
            {
                listView2.Items.Add(new ListViewItem(new string[] { C.name, 
                    C.area.ToString(), C.popu.ToString() }));
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                Text = "선택된 항목이 없습니다.";
            }
            else
            {
                Text = listView1.SelectedItems[0].Text;
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            MessageBox.Show(listView1.SelectedItems[0].Text + "에 대한 정보를 인쇄합니다.");
        }
    }
}