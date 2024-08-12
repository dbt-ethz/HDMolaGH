using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Mola;

namespace HDMolaGH
{
    public class PrimitiveBox : GH_Component
    {
        public PrimitiveBox()
          : base("Mola Box", "Box",
            "create a box",
            "Mola", "1-Primitives")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.secondary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("x1", "x1", "x of the lower left coner", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("y1", "y1", "y of the lower left coner", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("z1", "z1", "z of the lower left coner", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("x2", "x2", "x of the upper right coner", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("y2", "y2", "y of the upper right coner", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("z2", "z2", "z of the upper right coner", GH_ParamAccess.item, 1.0);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Box", "Box", "a box Mola mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double x1 = 0;
            double y1 = 0;
            double z1 = 0;
            double x2 = 0;
            double y2 = 0;
            double z2 = 0;

            DA.GetData(0, ref x1);
            DA.GetData(1, ref y1);
            DA.GetData(2, ref z1);
            DA.GetData(3, ref x2);
            DA.GetData(4, ref y2);
            DA.GetData(5, ref z2);

            MolaMesh mMesh = new MolaMesh();
            MeshFactory.AddBox(mMesh, (float)x1, (float)y1, (float)z1, (float)x2, (float)y2, (float)z2);

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon => Properties.Resources.box;
        public override Guid ComponentGuid => new Guid("3C05489B-F189-4C78-B8D5-0B9E4A4020DF");
    }
}