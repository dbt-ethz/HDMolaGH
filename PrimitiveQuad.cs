using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Mola;

namespace HDMolaGH
{
    public class PrimitiveQuad : GH_Component
    {
        public PrimitiveQuad()
          : base("Mola Quad", "Quad",
            "create a quad face mesh",
            "Mola", "1-Primitives")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("x1", "x1", "x of the lower left coner", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("y1", "y1", "y of the lower left coner", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("z1", "z1", "z of the lower left coner", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("x2", "x2", "x of the lower right coner", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("y2", "y2", "y of the lower right coner", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("z2", "z2", "z of the lower right coner", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("x3", "x3", "x of the upper right coner", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("y3", "y3", "y of the upper right coner", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("z3", "z3", "z of the upper right coner", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("x4", "x4", "x of the upper left coner", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("y4", "y4", "y of the upper left coner", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("z4", "z4", "z of the upper left coner", GH_ParamAccess.item, 0.0);

        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Quad", "Quad", "a quad face Mola mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double x1 = 0;
            double y1 = 0;
            double z1 = 0;
            double x2 = 0;
            double y2 = 0;
            double z2 = 0;
            double x3 = 0;
            double y3 = 0;
            double z3 = 0;
            double x4 = 0;
            double y4 = 0;
            double z4 = 0;

            DA.GetData(0, ref x1);
            DA.GetData(1, ref y1);
            DA.GetData(2, ref z1);
            DA.GetData(3, ref x2);
            DA.GetData(4, ref y2);
            DA.GetData(5, ref z2);
            DA.GetData(6, ref x3);
            DA.GetData(7, ref y3);
            DA.GetData(8, ref z3);
            DA.GetData(9, ref x4);
            DA.GetData(10, ref y4);
            DA.GetData(11, ref z4);

            MolaMesh mMesh = new MolaMesh();
            MeshFactory.AddQuad(mMesh, (float)x1, (float)y1, (float)z1, (float)x2, (float)y2, (float)z2,
                (float)x3, (float)y3, (float)z3, (float)x4, (float)y4, (float)z4, Color.red);

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("67C08B50-7101-481E-8238-9637FFAE18BE"); }
        }
    }
}