
using HelixToolkit.Wpf;
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
    public class CameraController : ICameraController
    {
        private readonly PerspectiveCamera _camera;


        private double _theta;
        private double _phi;
        private const double MIN_PHI = 0.01;
        private const double MAX_PHI = 3;
        private double _radius;
        private double _optimalRadius;
        public double OptimalRadius
        {
            get { return _radius; }
            set
            {
                _optimalRadius = value;
                _radius = value;
                UpdateCameraPosition();
            }
        }
        private double mouseSensitivity = 0.01;

        private Point3D _modelCenter;
        public Point3D ModelCenter
        {
            get { return _modelCenter; }
            set
            {
                _modelCenter = value;
                UpdateCameraPosition();
            }
        }


        public CameraController(PerspectiveCamera camera)
        {
            _camera = camera;
            _theta = 0;
            _phi = 1;
            _radius = 0;
        }

        public void UpdateCameraPosition()
        {
            if (_phi < MIN_PHI) _phi = MIN_PHI;
            else if (_phi > MAX_PHI) _phi = MAX_PHI;
            if (_radius <= 0) _radius = 0.01;

            double x = _modelCenter.X + _radius * Math.Sin(_phi) * Math.Cos(_theta);
            double y = _modelCenter.Y + _radius * Math.Cos(_phi);
            double z = _modelCenter.Z + _radius * Math.Sin(_phi) * Math.Sin(_theta);

            _camera.Position = new Point3D(x, y, z);
            _camera.LookDirection = new Vector3D(_modelCenter.X - x, _modelCenter.Y - y, _modelCenter.Z - z);
        }

        public void MoveCamera(Point startPosition, Point currentPosition)
        {
            double x = currentPosition.X - startPosition.X;
            double y = startPosition.Y - currentPosition.Y;

            _theta = x * mouseSensitivity;
            _phi = y * mouseSensitivity;

            UpdateCameraPosition();
        }

        public void ResetCamera()
        {
            _theta = 0;
            _phi = 1;
            _radius = _optimalRadius;
            UpdateCameraPosition();
        }

        public void AdjustCameraOnKeyDown(Key key)
        {
            switch (key)
            {
                case Key.W: _phi -= 0.1; break;
                case Key.S: _phi += 0.1; break;
                case Key.A: _theta += 0.1; break;
                case Key.D: _theta -= 0.1; break;
                case Key.Up: _radius -= 0.5; break;
                case Key.Down: _radius += 0.5; break;
            }
            UpdateCameraPosition();
        }

        public Point CalculateStartPos(Point currentPosition)
        {
            return new Point(currentPosition.X - (_theta / mouseSensitivity), currentPosition.Y + (_phi / mouseSensitivity));
        }

        public void ZoomCamera(double delta)
        {
            _radius += (delta > 0) ? -0.5 : 0.5;
            UpdateCameraPosition();
        }
    }
}
