using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class PrimitiveTetrahedron : GH_Component
    {
        public PrimitiveTetrahedron()
          : base("Mola Tetrahedron", "Tetrahedron",
            "create a Tetrahedron MolaMesh",
            "Mola", "1-Primitives")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.secondary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("x", "x1", "x of the center point", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("y", "y1", "y of the center point", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("z", "z1", "z of the center point", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("radius", "r", "radius of the Tetrahedron", GH_ParamAccess.item, 1.0);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Tetrahedron", "Tetrahedron", "a Tetrahedron MolaMesh", GH_ParamAccess.item);
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

            MolaMesh mMesh = MeshFactory.CreateTetrahedron((float)r, (float)x, (float)y, (float)z);

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.tetrahedron;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("5cfbcdd1-c29a-4c0f-92cc-95f6787d7c8c"); }
        }
    }
}
