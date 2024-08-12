using System;
using System.Collections.Generic;
using Mola;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class SubdivideGrid : GH_Component
    {
        public SubdivideGrid()
          : base("Subdivide Grid", "Grid",
              "splits all triangle or quad faces in a MolaMesh into regular grids",
              "Mola", "2-Subdivisions")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.primary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddIntegerParameter("U", "U", "u direction division number", GH_ParamAccess.item, 2);
            pManager.AddIntegerParameter("V", "V", "v direction division number", GH_ParamAccess.item, 2);
            pManager.AddIntegerParameter("Iteration", "I", "subdivide times", GH_ParamAccess.item, 1);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            int u = 2;
            int v = 2;
            int iteration = 1;

            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref u);
            DA.GetData(2, ref v);
            DA.GetData(3, ref iteration);

            for (int i = 0; i < iteration; i++)
            {
                mMesh = MeshSubdivision.Grid(mMesh, u, v);
            }

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.grid;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("4D58DB06-45F8-4244-BCF6-BBD4AAFE0E2D"); }
        }
    }
}