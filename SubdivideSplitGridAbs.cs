using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class SubdivideSplitGridAbs : GH_Component
    {
        public SubdivideSplitGridAbs()
          : base("Subdivide Split Grid Abs", "Split Grid Abs",
              "subdivide faces into cells with absolute size",
              "Mola", "2-Subdivisions")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddNumberParameter("USize", "U", "u direction cell size", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("VSize", "V", "v direction cell size", GH_ParamAccess.item, 1.0);
            pManager.AddIntegerParameter("Iteration", "I", "subdivide times", GH_ParamAccess.item, 1);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            double u = 1;
            double v = 1;
            int iteration = 1;

            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref u);
            DA.GetData(2, ref v);
            DA.GetData(3, ref iteration);

            for (int i = 0; i < iteration; i++)
            {
                mMesh = MeshSubdivision.GridAbs(mMesh, (float)u, (float)v);
            }

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.gridabs;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("070de0ea-21b9-4a46-a88a-4357a4039425"); }
        }
    }
}
