using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class SubdivideLinearSplit : GH_Component
    {
        public SubdivideLinearSplit()
          : base("Subdivide Linear Split", "Linear Split",
              "split faces liearly from border",
              "Mola", "2-Subdivisions")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.primary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddNumberParameter("Border Width 1", "W1", "border width 1", GH_ParamAccess.item, 1);
            pManager.AddNumberParameter("Border Width 2", "W2", "border width 2", GH_ParamAccess.item, 1);
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
                mMesh = MeshSubdivision.LinearSplitQuad(mMesh, (float)w1, (float)w2, d);
            }

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.splitlinear;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("2bf86542-77bf-4cad-91d6-157645519339"); }
        }
    }
}
