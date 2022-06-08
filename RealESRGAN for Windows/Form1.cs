using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Reflection;

namespace RealESRGAN_for_Windows
{
    public partial class Form1 : Form
    {
        public static string versionCode = "104";
        public static string cmdRadioN = "realesrgan-x4plus-anime";
        public static string selectedPic;
        public static string cmd;
        public static string realVersion;
        public static string realData;
        public static string realDownload;
        public static string newVersionInfo;
        public static string exeDownloadLink;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(Directory.GetCurrentDirectory() + @"\out_frames"))
            {
                System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\out_frames");

            }
            if (!System.IO.Directory.Exists(Directory.GetCurrentDirectory() + @"\input_frames"))
            {
                System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\input_frames");

            }
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send("114.114.114.114");
                if (reply.Status == IPStatus.Success)
                {
                    string xmlFileName = @"https://gitee.com/matsuzakasatou/my-static-resource/raw/master/Xml/realesrgan_info.xml";
                    XmlDocument doc = new XmlDocument();
                    doc.Load(xmlFileName);
                    XmlNodeList nodes = doc.SelectNodes("//item");
                    foreach (XmlNode node in nodes)
                    {
                        string newVersion = node.SelectSingleNode("exever").InnerText;
                        string newDownload = node.SelectSingleNode("download").InnerText;
                        newVersionInfo = node.SelectSingleNode("newVersionInfo").InnerText;
                        exeDownloadLink = node.SelectSingleNode("exeDownloadLink").InnerText;
                        realVersion = node.SelectSingleNode("realver").InnerText;
                        realData = node.SelectSingleNode("realdata").InnerText;
                        realDownload = node.SelectSingleNode("realdownload").InnerText;
                        this.label8.Text = "Real-ESRGAN最新版本:" + Form1.realVersion + " | 发行日期:" + Form1.realData + " | 点此进入GitHub(不提供自动更新,下载新版本后解压覆盖即可)";
                        int versionResult = string.Compare(newVersion, versionCode);
                        DialogResult updateOkResult;
                        if (versionResult == 1)
                        {
                            updateOkResult = MessageBox.Show("检测到新版本,请立即更新!(不包括Real-ESRGAN)" + "\r\n" + "更新日志：" + "\r\n" + Form1.newVersionInfo, "检测更新", MessageBoxButtons.OK);
                            if (updateOkResult == DialogResult.OK)
                            {
                                Process.Start(exeDownloadLink);
                            }
                        }
                        else
                        {
                            MessageBox.Show("当前是最新版本,无需更新!(不包括Real-ESRGAN)", "检测更新");
                        }
                    }
                }
                if (File.Exists(Directory.GetCurrentDirectory() + @"\realesrgan-ncnn-vulkan.exe"))
                {
                    this.label3.Text = "已检测到RealESRGAN本体,可以正常使用";
                    this.formatChecked.SelectedIndex = 0;
                    this.upscaleChecked.SelectedIndex = 3;
                }
                else
                {
                    this.label3.Text = "未检测到RealESRGAN本体,无法正常使用";
                    this.button1.Enabled = false;
                    this.button2.Enabled = false;
                    this.radioButtonAnime.Enabled = false;
                    this.radioButtonDefault.Enabled = false;
                    this.animeVideoX2.Enabled = false;
                    this.animeVideoX4.Enabled = false;
                    this.upscaleChecked.Enabled = false;
                    this.formatChecked.Enabled = false;
                    this.startButton.Enabled = false;
                    this.picPath.Text = "请检查当前目录下是否存在realesrgan-ncnn-vulkan.exe文件";
                    this.picPath.Enabled = false;
                    DialogResult realResult;
                    realResult = MessageBox.Show("未检测到Real-ESRGAN本体文件,是否立刻前往下载?", "Real-ESRGAN本体检测", MessageBoxButtons.OK);
                    if (realResult == DialogResult.OK)
                    {
                        Process.Start(realDownload);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("无网络连接,检测更新失败!", "检测更新");
                MessageBox.Show("无网络连接,获取Real-ESRGAN最新版本信息失败!", "Real-ESRGAN本体检测");
                if (File.Exists(Directory.GetCurrentDirectory() + @"\realesrgan-ncnn-vulkan.exe"))
                {
                    this.label3.Text = "已检测到RealESRGAN本体,可以正常使用";
                    this.formatChecked.SelectedIndex = 0;
                    this.upscaleChecked.SelectedIndex = 3;
                }
                else
                {
                    this.label3.Text = "未检测到RealESRGAN本体,无法正常使用";
                    this.button1.Enabled = false;
                    this.button2.Enabled = false;
                    this.radioButtonAnime.Enabled = false;
                    this.radioButtonDefault.Enabled = false;
                    this.animeVideoX2.Enabled = false;
                    this.animeVideoX4.Enabled = false;
                    this.upscaleChecked.Enabled = false;
                    this.formatChecked.Enabled = false;
                    this.startButton.Enabled = false;
                    this.picPath.Text = "请检查当前目录下是否存在realesrgan-ncnn-vulkan.exe文件";
                    this.picPath.Enabled = false;
                }
            }
            
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        System.Windows.Forms.ImageList imageList1 = new System.Windows.Forms.ImageList();
        System.Collections.ArrayList imgList = new System.Collections.ArrayList();
        private void button1_Click(object sender, EventArgs e)
        {
            selectedPic = "1";
            this.openFileDialog1.Filter = "jpg文件(*.jpg)|*.jpg|png文件(*.png)|*.png|jpeg文件(*.jpeg)|*.jpeg";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string PicFileName = this.openFileDialog1.FileName;
                this.imgList.Add(PicFileName);
                this.imageList1.Images.Add(Image.FromFile(PicFileName));
                this.picPath.Text = PicFileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedPic = "2";
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                this.picPath.Text = foldPath;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cmdRadioN = "RealESRGANv2-animevideo-xsx4";
            this.upscaleChecked.SelectedIndex = 1;
            this.formatChecked.SelectedIndex = 1;
            this.upscaleChecked.Enabled = false;
            this.formatChecked.Enabled = false;
        }

        private void radioButtonDefault_CheckedChanged(object sender, EventArgs e)
        {
            cmdRadioN = "realesrgan-x4plus";
            this.upscaleChecked.Enabled = true;
            this.formatChecked.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void animeVideoX2_CheckedChanged(object sender, EventArgs e)
        {
            cmdRadioN = "RealESRGANv2-animevideo-xsx2";
            this.upscaleChecked.SelectedIndex = 1;
            this.formatChecked.SelectedIndex = 1;
            this.upscaleChecked.Enabled = false;
            this.formatChecked.Enabled = false;
        }

        private void radioButtonAnime_CheckedChanged(object sender, EventArgs e)
        {
            cmdRadioN = "realesrgan-x4plus-anime";
            this.upscaleChecked.Enabled = true;
            this.formatChecked.Enabled = true;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (this.picPath.Text == string.Empty)
            {
                MessageBox.Show("你的老婆走丢啦,快去找回来!（；´д｀）ゞ", "老婆丢啦");
            }
            else
            {
                if (Form1.selectedPic == "1")
                {
                    File.Copy(this.picPath.Text, Directory.GetCurrentDirectory() + @"\input_frames\" + Path.GetFileName(picPath.Text), true);
                    cmd = "realesrgan-ncnn-vulkan.exe -i " + Directory.GetCurrentDirectory() + @"\input_frames\" + Path.GetFileName(picPath.Text) + " -o out_frames/" + Path.GetFileNameWithoutExtension(picPath.Text) + "." + this.formatChecked.Text + " -n " + Form1.cmdRadioN + " -s " + this.upscaleChecked.Text + " -f " + this.formatChecked.Text;
                }
                if (Form1.selectedPic == "2")
                {
                    cmd = "realesrgan-ncnn-vulkan.exe -i " + this.picPath.Text + " -o out_frames -n " + Form1.cmdRadioN + " -s " + this.upscaleChecked.Text + " -f " + this.formatChecked.Text;
                }

                FileInfo info = new FileInfo(Directory.GetCurrentDirectory() + "\\" + "temp.bat");
                if (info.Exists)
                {
                    File.Delete(Directory.GetCurrentDirectory() + "\\" + "temp.bat");
                    File.Create(Directory.GetCurrentDirectory() + "\\" + "temp.bat").Close();
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\" + "temp.bat", "@echo off" + "\r\n" + "title Real-ESRGAN GUI Command" + "\r\n" + cmd + "\r\n" + "pause");
                    info.Attributes = FileAttributes.Hidden;
                    System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "\\" + "temp.bat");
                }
                else
                {
                    File.Create(Directory.GetCurrentDirectory() + "\\" + "temp.bat").Close();
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\" + "temp.bat", "@echo off" + "\r\n" + "title Real-ESRGAN GUI Command" + "\r\n" + cmd + "\r\n" + "pause");
                    info.Attributes = FileAttributes.Hidden;
                    System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "\\" + "temp.bat");
                }
                /*using (System.Diagnostics.Process p = new System.Diagnostics.Process())
                {
                    p.StartInfo.FileName = "cmd.exe";
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = false;
                    p.StartInfo.RedirectStandardError = false;
                    p.StartInfo.CreateNoWindow = false;
                    p.Start();
                    p.StandardInput.WriteLine(Form1.cmd);
                    p.StandardInput.AutoFlush = true;
                    p.WaitForExit();
                    p.Close();
                }*/
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://space.bilibili.com/11997556");
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Process.Start(Form1.realDownload);
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileInfo info1 = new FileInfo(Directory.GetCurrentDirectory() + "\\" + "temp.bat");
            if (info1.Exists)
            {
                File.Delete(Directory.GetCurrentDirectory() + "\\" + "temp.bat");
            }
            if (System.IO.Directory.Exists(Directory.GetCurrentDirectory() + @"\input_frames"))
            {
                Directory.Delete(Directory.GetCurrentDirectory() + @"\input_frames", true);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/xinntao/Real-ESRGAN/blob/master/docs/anime_video_model.md");
        }
    }
}