using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class PrimitiveDodecahedron : GH_Component
    {
        public PrimitiveDodecahedron()
          : base("Mola Dodecahedron", "Dodecahedron",
            "create a Dodecahedron MolaMesh",
            "Mola", "1-Primitives")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("x", "x1", "x of the center point", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("y", "y1", "y of the center point", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("z", "z1", "z of the center point", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("radius", "r", "radius of the dodecahedron", GH_ParamAccess.item, 1.0);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Dodecahedron", "Dodecahedron", "a Dodecahedron MolaMesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double x = 0;
            double y = 0;
            double z = 0;
            double r = 1;

            DA.GetData(0, ref x);
            DA.GetData(1, ref y);
            DA.GetData(2, ref z);
            DA.GetData(3, ref r);

            MolaMesh mMesh = MeshFactory.CreateDodecahedron((float)r, (float)x, (float)y, (float)z);

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.dodecahedron;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("47429f34-0cce-41fb-b10d-1ad52119c823"); }
        }
    }
}
