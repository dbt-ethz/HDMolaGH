using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class PrimitiveTorus : GH_Component
    {
        public PrimitiveTorus()
          : base("Mola Torus", "Torus",
            "create a Torus MolaMesh",
            "Mola", "1-Primitives")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("r1", "r1", "radius of the ring", GH_ParamAccess.item, 3);
            pManager.AddNumberParameter("r2", "r2", "radius of the tube", GH_ParamAccess.item, 1);
            pManager.AddIntegerParameter("ringN", "rN", "segments of the ring", GH_ParamAccess.item, 6);
            pManager.AddIntegerParameter("tubeN", "tN", "segments of the tube", GH_ParamAccess.item, 6);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Torus", "Torus", "a Torus MolaMesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double r1 = 3;
            double r2 = 1;
            int n1 = 6;
            int n2 = 6;

            DA.GetData(0, ref r1);
            DA.GetData(1, ref r2);
            DA.GetData(2, ref n1);
            DA.GetData(3, ref n2);

            MolaMesh mMesh = MeshFactory.CreateTorus((float)r1, (float)r2, n1, n2);

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("866bc903-1716-4f74-a449-f91578e95e97"); }
        }
    }
}
