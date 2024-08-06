using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Mola;

namespace HDMolaGH
{
    public class SubdivideExtrudeTapered : GH_Component
    {
        public SubdivideExtrudeTapered()
          : base("Subdivide Extrude Tapered", "Extrude Tapered",
              "Extrudes all face in a MolaMesh tapered like a window by creating an offset face and quads between every original edge and the corresponding new edge",
              "Mola", "2-Subdivisions")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddNumberParameter("Height", "H", "extrude height", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("Fraction", "F", "fraction between 0 and 1", GH_ParamAccess.item, 0.5);
            pManager.AddBooleanParameter("Cap", "C", "wether cap the top", GH_ParamAccess.item, true);
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
            double f = 0.5;
            bool c = true;
            int iteration = 1;

            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref h);
            DA.GetData(2, ref f);
            DA.GetData(3, ref c);
            DA.GetData(4, ref iteration);

            for (int i = 0; i < iteration; i++)
            {
                mMesh = MeshSubdivision.ExtrudeTapered(mMesh, (float)h, (float)f, c);
            }

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.extrudetapered;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("6566106F-0D04-47FB-BF98-8D7EE8EC8EF4"); }
        }
    }
}