using System;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using System.Windows.Media;

namespace WpfApp1.ModelManager3D
{
    public static class ModelManager
    {
        public static Model3DGroup LoadModel(string filePath)
        {
            var importer = new ModelImporter();
            var model = importer.Load(filePath) as Model3DGroup;
            if (model == null)
                throw new Exception("Ошибка при загрузке модели.");

            PositionModelAtOrigin(model);
            ScaleModel(model, 0.1, 0.1, 0.1);
            return model;
        }

        public static void ApplyMaterial(Model3D model, Material material)
        {
            if (model is GeometryModel3D geometryModel)
            {
                geometryModel.Material = material;
            }
            else if (model is Model3DGroup modelGroup)
            {
                foreach (var child in modelGroup.Children)
                {
                    ApplyMaterial(child, material);
                }
            }
        }

        private static void PositionModelAtOrigin(Model3DGroup modelGroup)
        {
            Point3D center = GetModelCenter(modelGroup);
            Vector3D offset = new Vector3D(-center.X, -center.Y, -center.Z);
            Transform3D transform = new TranslateTransform3D(offset);
            modelGroup.Transform = transform;
        }

        private static void ScaleModel(Model3DGroup modelGroup, double scaleX, double scaleY, double scaleZ)
        {
            var scaleTransform = new ScaleTransform3D(scaleX, scaleY, scaleZ);
            modelGroup.Transform = scaleTransform;
        }

        public static Point3D GetModelCenter(Model3DGroup modelGroup)
        {
            Rect3D bounds = modelGroup.Bounds;
            return new Point3D(
                bounds.X + bounds.SizeX / 2,
                bounds.Y + bounds.SizeY / 2,
                bounds.Z + bounds.SizeZ / 2
            );
        }

        public static double CalculateOptimalRadius(Model3D model, double zoomFactor = 2.5)
        {
            if (model != null)
            {
                Rect3D bounds = model.Bounds;
                Vector3D diagonal = new Vector3D(bounds.SizeX, bounds.SizeY, bounds.SizeZ);

                //длина диагонали
                double diagonalLength = diagonal.Length;
                return diagonalLength * zoomFactor;
            }

            return 0;
        }

    }
}
