using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class PrimitiveCone : GH_Component
    {
        public PrimitiveCone()
          : base("Mola Cone", "Cone",
            "create a cone",
            "Mola", "1-Primitives")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("z1", "z1", "z coordinate of the lower center", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("z2", "z2", "z coordinate of the top center", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("r1", "r1", "radius of the lower center", GH_ParamAccess.item, 2.0);
            pManager.AddNumberParameter("r2", "r2", "radius of the top center", GH_ParamAccess.item, 1.0);
            pManager.AddIntegerParameter("segments", "N", "segments of cone", GH_ParamAccess.item, 6);
            pManager.AddBooleanParameter("cap bottom", "c1", "z of the upper right coner", GH_ParamAccess.item, true);
            pManager.AddBooleanParameter("cap top", "c2", "z of the upper right coner", GH_ParamAccess.item, true);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Cone", "Cone", "a cone Mola mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double z1 = 0;
            double z2 = 1;
            double r1 = 2;
            double r2 = 1;
            int n = 6;
            bool c1 = true;
            bool c2 = true;

            DA.GetData(0, ref z1);
            DA.GetData(1, ref z2);
            DA.GetData(2, ref r1);
            DA.GetData(3, ref r2);
            DA.GetData(4, ref n);
            DA.GetData(5, ref c1);
            DA.GetData(6, ref c2);

            MolaMesh mMesh = MeshFactory.CreateCone((float)z1, (float)z2, (float)r1, (float)r2, n, c1, c2);

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.cone;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("b316ba0e-0e66-4831-b935-19a059ed44c0"); }
        }
    }
}
