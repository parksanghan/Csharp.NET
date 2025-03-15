using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Imaging;  // 이미지 코덱 


namespace ImageCodecExam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImageCodecInfo[] Codecs;
            Codecs = ImageCodecInfo.GetImageEncoders();
            int num = Codecs.Length;
            string CodecInfo = null;

            for (int i = 0; i < num; i++)
            {
                CodecInfo += "Codec Nmae = " + Codecs[i].CodecName + "\n";
                CodecInfo += "Class ID   = " + Codecs[i].Clsid.ToString() + "\n";
                CodecInfo += "FileName Extension = " + Codecs[i].FilenameExtension + "\n";
                CodecInfo += "Flag = " + Codecs[i].Flags.ToString() + "\n";
                CodecInfo += "GUID = " + Codecs[i].FormatID.ToString() + "\n";
                CodecInfo += "MimeType = " + Codecs[i].MimeType.ToString() + "\n";
                CodecInfo += "Version = " + Codecs[i].Version.ToString() + "\n\n";
            }
            MessageBox.Show(CodecInfo, "설치된 Encoder Codec 정보");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImageCodecInfo[] Codecs;
            Codecs = ImageCodecInfo.GetImageDecoders();
            int num = Codecs.Length;
            string CodecInfo = null;

            for (int i = 0; i < num; i++)
            {
                CodecInfo += "Codec Nmae = " + Codecs[i].CodecName + "\n";
                CodecInfo += "Class ID   = " + Codecs[i].Clsid.ToString() + "\n";
                CodecInfo += "FileName Extension = " + Codecs[i].FilenameExtension + "\n";
                CodecInfo += "Flag = " + Codecs[i].Flags.ToString() + "\n";
                CodecInfo += "GUID = " + Codecs[i].FormatID.ToString() + "\n";
                CodecInfo += "MimeType = " + Codecs[i].MimeType.ToString() + "\n";
                CodecInfo += "Version = " + Codecs[i].Version.ToString() + "\n\n";
            }
            MessageBox.Show(CodecInfo, "설치된 Decoder Codec 정보");
        }
    }
}