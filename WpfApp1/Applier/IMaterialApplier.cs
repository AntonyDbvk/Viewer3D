using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfApp1.Applier
{
    public interface IMaterialApplier
    {
        void ApplyMaterial(Model3D model, Material material);
    }

}
