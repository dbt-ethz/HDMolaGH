using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class PrimitiveCircle : GH_Component
    {
        public PrimitiveCircle()
          : base("Mola Circle", "Circle",
            "create a flat circle MolaMesh",
            "Mola", "1-Primitives")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.primary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("x", "x", "x coordinate of the center point", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("y", "y", "y coordinate of the center point", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("z", "z", "z coordinate of the center point", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("Radius", "R", "radius of the circle", GH_ParamAccess.item, 1.0);
            pManager.AddIntegerParameter("Segments", "N", "segments of the circle", GH_ParamAccess.item, 6);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Circle", "Circle", "a flat circle Mola mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double x = 0;
            double y = 0;
            double z = 0;
            double r = 1;
            int n = 6;

            DA.GetData(0, ref x);
            DA.GetData(1, ref y);
            DA.GetData(2, ref z);
            DA.GetData(3, ref r);
            DA.GetData(4, ref n);

            MolaMesh mMesh = MeshFactory.CreateCircle((float)x, (float)y, (float)z, (float)r, n);

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.circle;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("0c7adcbe-1cad-40ed-8299-003dce50ed98"); }
        }
    }
}
