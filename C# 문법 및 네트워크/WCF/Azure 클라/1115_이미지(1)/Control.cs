// Control.cs
using _1115_이미지_1_.ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace _1115_이미지_1_
{
    public class Control : IAzureCallback
    {
        private AzureClient fileclient = null;
        public Form1 fm = null;

        #region 싱글톤
        public static Control singleton { get; private set; } = null;

        static Control()
        {
            singleton = new Control();
        }
        private Control()
        {
            InstanceContext site = new InstanceContext(this);
            fileclient = new AzureClient(site);
        }
        #endregion

        #region 연결처리
        public void Load()
        {
            fileclient.AzureServiceLoad();
        }

        public void Close()
        {
            fileclient.AzureServiceClose();
        }
        #endregion

        #region 콜백 메서드
        public void AzureServiceLoadACK(bool reusult)
        { 
            try
            {
                if (reusult == true)
                {
                    Action updateText = () => { fm.Text = "연결 성공"; };
                    fm.Invoke(updateText);

                    fileclient.GetListSql();
                }
                else
                {
                    Action updateText = () => { fm.Text = "연결 실패"; };
                    fm.Invoke(updateText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AnalyzeACK(string analystrig)
        {
            try
            {
                string str = string.Format(analystrig);

                Action addText = () => { fm.textBox2.Text = str; };
                fm.textBox2.Invoke(addText);

                string imageFilePath = fm.pictureBox1.ImageLocation;
                fileclient.GetListSql();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ExtractTextACK(string analystrig)
        {
            try
            {
                string str = string.Format(analystrig);
                fm.textBox2.Text = str;

                string imageFilePath = fm.pictureBox1.ImageLocation;
                fileclient.GetListSql();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetListSqlACK(string packet)
        {
            try
            {
                Action clearList = () => { fm.listBox1.Items.Clear(); };
                fm.listBox1.Invoke(clearList);

                string[] sp = packet.Split('@');

                for (int i = 0; i < sp.Count() - 1; i++)
                {
                    string[] sp3 = sp[i].Split('#');

                    if (sp3.Length >= 3)
                    {
                        string image = sp3[0];

                        Action addList = () => { fm.listBox1.Items.Add(image); };
                        fm.listBox1.Invoke(addList);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetSelectItemACK(string packet)
        {
            try
            {
                Action clearText = () => { fm.textBox3.Text = string.Empty; };
                fm.textBox3.Invoke(clearText);

                string[] sp = packet.Split('#');

                if (sp.Length >= 3)
                {
                    string image = sp[1] + "\n\n" + sp[2];

                    Action addText = () => { fm.textBox3.Text = image; };
                    fm.textBox3.Invoke(addText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AzureServiceCloseACK(bool result)
        {
        }
        #endregion

        #region 이미지 분석, 글자 추출, 파일명 출력
        public void Analyze(string path, byte[] bytePic)
        {
            fileclient.Analyze(Path.GetFileName(path), bytePic);
        }

        public void ExtractText(string path, byte[] bytePic)
        {
            fileclient.ExtractText(Path.GetFileName(path), bytePic);
        }

        public void GetSelectItem(string name)
        {
            fileclient.GetSelectItem(name);
        }
        #endregion
    }
}
