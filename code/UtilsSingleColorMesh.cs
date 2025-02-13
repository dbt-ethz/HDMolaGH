using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Linq;

namespace HDMolaGH
{
    public class UtilsSingleColorMesh : GH_Component
    {
        public UtilsSingleColorMesh()
          : base("Single Color Mesh", "Single Color",
            "Color Mola mesh faces according to a single color",
            "Mola", "4-Utils")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Mesh", "M", "mesh to be converted", GH_ParamAccess.item);
            pManager.AddColourParameter("Single Color", "C", "a single color to color all faces", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            System.Drawing.Color rColor = new System.Drawing.Color();

            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref rColor);

            Color mColor = new Color((float)rColor.R / 255, (float)rColor.G / 255, (float)rColor.B / 255, (float)rColor.A / 255);

            MolaMesh copyMesh = mMesh.Copy();
            copyMesh.Colors = Enumerable.Repeat(mColor, copyMesh.VertexCount()).ToList();

            DA.SetData(0, copyMesh);
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
            get { return new Guid("2f278eb9-3661-4c8b-b1c6-2ff7688abd11"); }
        }
    }
}
