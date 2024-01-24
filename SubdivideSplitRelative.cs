using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class SubdivideSplitRelative : GH_Component
    {
        public SubdivideSplitRelative()
          : base("Subdivide Relative", "Relative",
              "split MolaMesh faces according to relative proportion",
              "Mola", "2-Subdivisions")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Start diriection", "S", "from u or v direction to split", GH_ParamAccess.item, 0);
            pManager.AddNumberParameter("Min split 1", "Min1", "minimal split value 1", GH_ParamAccess.item, 0.1);
            pManager.AddNumberParameter("Max split 1", "Max1", "maximal split value 1", GH_ParamAccess.item, 0.9);
            pManager.AddNumberParameter("Min split 2", "Min2", "minimal split value 2", GH_ParamAccess.item, 0.1);
            pManager.AddNumberParameter("Max split 2", "Max2", "maximal split value 2", GH_ParamAccess.item, 0.9);
            pManager.AddIntegerParameter("Iteration", "I", "subdivide times", GH_ParamAccess.item, 1);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            int d = 0;
            double min1 = 0.1f;
            double max1 = 0.9f;
            double min2 = 0.1f;
            double max2 = 0.9f;
            int iteration = 1;

            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref d);
            DA.GetData(2, ref min1);
            DA.GetData(3, ref max1);
            DA.GetData(4, ref min2);
            DA.GetData(5, ref max2);
            DA.GetData(6, ref iteration);

            for (int i = 0; i < iteration; i++)
            {
                mMesh = MeshSubdivision.SplitRelative(mMesh, d, (float)min1, (float)max1, (float)min2, (float)max2);
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
            get { return new Guid("ea41f0b3-869f-4e25-8697-60559bfa15b7"); }
        }
    }
}
