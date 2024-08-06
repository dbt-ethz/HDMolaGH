using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class SubdivideSplitRoof : GH_Component
    {
        public SubdivideSplitRoof()
         : base("Subdivide Split Roof", "Split Roof",
             "extrudes a pitched roof",
             "Mola", "2-Subdivisions")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddNumberParameter("Height", "H", "extrude height", GH_ParamAccess.item, 1.0);
            pManager.AddIntegerParameter("Iteration", "I", "subdivide times", GH_ParamAccess.item, 1);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            double h = 0;
            int iteration = 1;

            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref h);
            DA.GetData(2, ref iteration);

            for (int i = 0; i < iteration; i++)
            {
                mMesh = MeshSubdivision.SplitRoof(mMesh, (float)h);
            }

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.roof;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("e57b1ad1-cfc6-452f-930c-1f6b9b7dd73b"); }
        }
    }
}
