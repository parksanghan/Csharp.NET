using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TreeViewData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView2.Nodes.Add("대한민국");
            treeView2.Nodes[0].Nodes.Add("서울특별시");
            treeView2.Nodes[0].Nodes.Add("경기도");
            treeView2.Nodes[0].Nodes.Add("충청도");
            treeView2.Nodes[0].Nodes[0].Nodes.Add("은평구");
            treeView2.Nodes[0].Nodes[1].Nodes.Add("수원시");
            treeView2.Nodes[0].Nodes[1].Nodes.Add("용인시");
            treeView2.Nodes[0].Nodes[1].Nodes[1].Nodes.Add("죽전동");
            treeView2.Nodes[0].Nodes[2].Nodes.Add("당진군");

            treeView3.Nodes.Add(arCity[0].Name);
            AddCity(treeView3.Nodes[0], 0);
        }

        struct City
        {
            public int Parent;
            public string Name;
            public City(int aParent, string aName)
            {
                Parent = aParent;
                Name = aName;
            }
        }

        City[] arCity = {
            new City(-1, "대한민국"),
            new City(0, "서울특별시"),
            new City(0, "경기도"),
            new City(0, "충청도"),
            new City(1, "은평구"),
            new City(2, "수원시"),
            new City(2, "용인시"),
            new City(6, "죽전동"),
            new City(3, "당진군"),
        };

        private bool HaveChild(int idx)
        {
            for (int i = 0; i < arCity.Length; i++)
            {
                if (arCity[i].Parent == idx)
                {
                    return true;
                }
            }
            return false;
        }

        private void AddCity(TreeNode Node, int Parent)
        {
            TreeNode NewNode;
            for (int i = 0; i < arCity.Length; i++)
            {
                if (arCity[i].Parent == Parent)
                {
                    NewNode = Node.Nodes.Add(arCity[i].Name);
                    if (HaveChild(i))
                    {
                        AddCity(NewNode, i);
                    }
                }
            }
        }
    }
}