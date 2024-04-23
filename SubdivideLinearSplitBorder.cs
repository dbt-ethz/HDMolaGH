using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class SubdivideLinearSplitBorder : GH_Component
    {
        public SubdivideLinearSplitBorder()
          : base("Subdivide Linear Split Border", "Linear Split Border",
              "split faces liearl..",
              "Mola", "2-Subdivisions")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddNumberParameter("Border width1", "W1", "Border width1", GH_ParamAccess.item, 1);
            pManager.AddNumberParameter("Border width2", "W2", "mBorder width2", GH_ParamAccess.item, 1);
            pManager.AddIntegerParameter("Split direction", "D", "split direction", GH_ParamAccess.item, 0);
            pManager.AddIntegerParameter("Iteration", "I", "subdivide times", GH_ParamAccess.item, 1);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            double w1 = 1;
            double w2 = 1;
            int d = 0;
            int iteration = 1;

            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref w1);
            DA.GetData(2, ref w2);
            DA.GetData(3, ref d);
            DA.GetData(4, ref iteration);

            for (int i = 0; i < iteration; i++)
            {
                mMesh = MeshSubdivision.LinearSplitBorder(mMesh, (float)w1, (float)w2, d);
            }

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
            get { return new Guid("383e845f-64ca-4ae4-a854-23d6bdeaa6fd"); }
        }
    }
}
