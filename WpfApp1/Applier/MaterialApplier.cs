using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfApp1.Applier
{
    public class MaterialApplier : IMaterialApplier
    {
        public void ApplyMaterial(Model3D model, Material material)
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
    }

}
