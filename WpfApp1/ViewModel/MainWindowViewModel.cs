using System.Windows.Media.Media3D;
using System.Windows.Media;
using WpfApp1.Controller;
using WpfApp1.Applier;
using System.Windows.Input;
using System.Windows;
using WpfApp1.ModelManager3D;

namespace WpfApp1.ViewModel
{
    public class MainWindowViewModel
    {
        private readonly ICameraController _cameraController;
        private readonly IMaterialApplier _materialApplier;


        public MainWindowViewModel(ICameraController cameraController, IMaterialApplier materialApplier)
        {
            _cameraController = cameraController;
            _materialApplier = materialApplier;
        }

        public Model3DGroup LoadModel(string filePath)
        {
            var modelGroup = ModelManager.LoadModel(filePath);
            //var material = new DiffuseMaterial(new SolidColorBrush(Colors.Gray));
            //ModelManager.ApplyMaterial(modelGroup, material);
            _cameraController.ModelCenter = ModelManager.GetModelCenter(modelGroup);
            _cameraController.OptimalRadius = ModelManager.CalculateOptimalRadius(modelGroup);
            return modelGroup;
        }

        public void ZoomCamera(double delta)
        {
            _cameraController.ZoomCamera(delta);
        }

        public void AdjustCameraOnKeyDown(Key key)
        {
            _cameraController.AdjustCameraOnKeyDown(key);
        }

        public void MoveCamera(Point startPosition, Point currentPosition)
        {
            _cameraController.MoveCamera(startPosition, currentPosition);
        }

        public Point CalculateStartPos(Point currentPosition)
        {
            return _cameraController.CalculateStartPos(currentPosition);
        }

        public void ResetCamera()
        {
            _cameraController.ResetCamera();
        }
    }
}
