using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Microsoft.Win32;
using WpfApp1.ViewModel;
using WpfApp1.Controller;
using WpfApp1.Applier;
namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private string SelectedFilePath;
        Point startPosition;
        Point currentPosition;
        bool isDragging = false;

        private MainWindowViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel(new Controller.CameraController(camera), new MaterialApplier());

            foreach (var item in stackpanel1.Children)
            {
                if (item is Slider slider)
                {
                    slider.ValueChanged += Slider_ValueChanged;
                }
            }
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DirectionalLight light = (DirectionalLight)((ModelVisual3D)view3d.Children[1]).Content;
            light.Direction = new Vector3D(slider1.Value, slider2.Value, slider3.Value);
            logTextBlock.Text = $"Light Direction: {light.Direction}";
        }

        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            viewModel.AdjustCameraOnKeyDown(e.Key);
        }

        private void Button_ResetCamera_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ResetCamera();
        }

        private void Window_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
           viewModel.ZoomCamera(e.Delta);
        }
        private void mouseArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            currentPosition = e.GetPosition(this);
            startPosition = viewModel.CalculateStartPos(currentPosition);
            isDragging = true;
            Mouse.Capture(mouseArea); // захват мыши на прямоугольнике
        }

        private void mouseArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                currentPosition = e.GetPosition(this);
                viewModel.MoveCamera(startPosition, currentPosition);
            }
        }

        private void mouseArea_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            Mouse.Capture(null);
        }

        private void LoadModel(string path)
        {
                modelVisual.Model = viewModel.LoadModel(path);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    SelectedFilePath = openFileDialog.FileName;

                }
                LoadModel(SelectedFilePath);
            }
            catch (Exception b)
            {
                MessageBox.Show(b.Message);
            }

        }
    }
}
