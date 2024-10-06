using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace WpfApp1.Controller
{
    public interface ICameraController
    {
        double OptimalRadius { get; set; }
        Point3D ModelCenter { get; set; }
        void UpdateCameraPosition();
        void ResetCamera();
        void AdjustCameraOnKeyDown(Key key);
        void MoveCamera(Point startPosition, Point currentPosition);
        Point CalculateStartPos(Point currentPosition);
        void ZoomCamera(double delta);
    }


}
