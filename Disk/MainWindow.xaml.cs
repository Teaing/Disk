using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Disk
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public const double zm = 7.84423828125;

        private void calcfat32()
        {
            if ((bool)TB.IsChecked)
            {
                MessageBox.Show("不支持TB运算!容量超过了FAT32文件系统允许的最大容量32GB!");
                return;
            }
            else
            {
                if (double.Parse(Variations.Text.ToString()) <= 0)
                {
                    MessageBox.Show("输入非法值!");
                    return;
                }
                if (double.Parse(Variations.Text.ToString()) > 32)
                {
                    MessageBox.Show("无法计算!容量超过了FAT32文件系统允许的最大容量32GB!");
                    return;
                }
            }
            double sr;
            sr = double.Parse(Variations.Text.ToString());
            Result.Text = ((sr - 1) * 4 + 1024 * sr).ToString();
        }

        private void calcntfs()
        {
            double sr = double.Parse(Variations.Text);
            if (double.Parse(Variations.Text.ToString()) <= 0)
            {
                MessageBox.Show("输入非法值!");
                return;
            }
            if ((bool)TB.IsChecked)
            {
                if (double.Parse(Variations.Text.ToString()) > 2)
                {
                    MessageBox.Show("无法计算!容量超过了NTFS文件系统允许的最大容量2TB!");
                    return;
                }
                sr = sr * 1024;
            }

            if ((bool)GB.IsChecked)
            {
                if (double.Parse(Variations.Text.ToString()) > 2048)
                {
                    MessageBox.Show("无法计算!容量超过了NTFS文件系统允许的最大容量2TB!");
                    return;
                }
            }
            double temp;
            temp = sr * 1024;
            temp = temp / zm;
            temp = Math.Floor(temp);
            temp = temp + 1;
            temp = temp * zm;
            temp = Math.Floor(temp);//Math.Floor取整，不进位
            temp = temp + 1;
            Result.Text = temp.ToString();

        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if ((bool)FAT32.IsChecked)
                {
                    calcfat32();
                    return;
                }
                if ((bool)NTFS.IsChecked)
                {
                    calcntfs();
                    return;
                }
            }
            catch (Exception caught)//异常捕获，避免程序崩溃.
            {
                Variations.Text = string.Empty;
                //Result.Text = caught.Message;
                MessageBox.Show(caught.Message);
                return;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Variations.Text = string.Empty;
            Result.Text = string.Empty;
        }
    }
}