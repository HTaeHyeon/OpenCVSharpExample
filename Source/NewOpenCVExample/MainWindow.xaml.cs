using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace NewOpenCVExample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FileOpen(ref string fileName)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files (*.*)|*.*";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                fileName = dlg.FileName;
            }
            else
            {
                fileName = "";
            }
        }

        private void FileSave(ref string fileName)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files (*.*)|*.*";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                fileName = dlg.FileName;
            }
            else
            {
                fileName = "";
            }
        }

        private void LoadImageBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = string.Empty;
            FileOpen(ref fileName);

            if (fileName == string.Empty)
                return;

            Mat image;
            image = Cv2.ImRead(fileName, ImreadModes.Color);

            if (image.Empty())
            {
                return;
            }

            DisplayImage(image);
        }

        private void SaveImage(Mat image)
        {
            string fileName = string.Empty;
            FileSave(ref fileName);
            if (fileName == string.Empty)
                return;

            Cv2.ImWrite(fileName, image);
        }

        private void DisplayImage(Mat image)
        {
            WriteableBitmap wb = new WriteableBitmap(image.Width, image.Height, 96, 96, PixelFormats.Bgr24, null);
            WriteableBitmapConverter.ToWriteableBitmap(image, wb);
            ShowImage.Source = wb;
        }

        private void SaveImageBtn_Click(object sender, RoutedEventArgs e)
        {
            WriteableBitmap wb = ShowImage.Source as WriteableBitmap;
            Mat image = wb.ToMat();
            if(image != null)
                SaveImage(image);
        }

        private void GaussianBlur()
        {
            WriteableBitmap wb = ShowImage.Source as WriteableBitmap;
            Mat image = wb.ToMat();

            if (image == null)
            {
                return;
            }

            for (int i = 1; i < 31; i = i + 2)
            {
                OpenCvSharp.Size size = new OpenCvSharp.Size(i, i);
                Cv2.GaussianBlur(image, image, size, 0, 0);
            }
            DisplayImage(image);
        }

        private void Sharpen(Mat image)
        {
            int channels = image.Channels();
            Mat sharpImage = new Mat();
            sharpImage.Create(image.Size(), image.Channels());

            for (int i = 1; i < image.Rows - 1; ++i)
            {
                IntPtr previous = image.Ptr(i - 1);
                IntPtr current = image.Ptr(i);
                IntPtr next = image.Ptr(i + 1);

                IntPtr output = sharpImage.Ptr(i);

                for (int j = channels; i < channels * (image.Cols - 1); i++)
                {
                    
                }
            }
        }

        private void GaussianBlurBtn_Click(object sender, RoutedEventArgs e)
        {
            GaussianBlur();
        }

        private void Logo()
        {
            WriteableBitmap wb = ShowImage.Source as WriteableBitmap;
            Mat image = wb.ToMat();

            string strFile = string.Empty;

            FileOpen(ref strFile);

            if (strFile == string.Empty)
            {
                return;
            }

            if (image == null)
            {
                return;
            }

            Mat logo = new Mat(strFile, ImreadModes.Color);

            Mat imageLogo = new Mat(image, new OpenCvSharp.Rect(0, 0, logo.Cols, logo.Rows));
            Cv2.AddWeighted(imageLogo, 1.0, logo, 0.3, 0.1, imageLogo);
            //logo.CopyTo(imageLogo);
            using (OpenCvSharp.Window test = new OpenCvSharp.Window("str"))
            {
                Cv2.ImShow("str", imageLogo);
                Cv2.WaitKey(0);
            }
            //Cv2.AddWeighted(image, 1.0, imageLogo, 0.3, 0.1, image);

            DisplayImage(image);
        }

        private void LogoBtn_Click(object sender, RoutedEventArgs e)
        {
            Logo();
        }
    }
}
