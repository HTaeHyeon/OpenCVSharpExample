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

        private void LoadImageBtn_Click(object sender, RoutedEventArgs e)
        {
            string fileName = string.Empty;
            FileOpen(ref fileName);

            Mat image;
            image = Cv2.ImRead(fileName, ImreadModes.Color);

            if (image.Empty())
            {
                return;
            }

            WriteableBitmap wb = new WriteableBitmap(image.Width, image.Height, 96, 96, PixelFormats.Bgr24, null);
            WriteableBitmapConverter.ToWriteableBitmap(image, wb);
            ShowImage.Source = wb;
        }
    }
}
