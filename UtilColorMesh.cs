using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class UtilColorMesh : GH_Component
    {
        public UtilColorMesh()
          : base("UtilColorMesh", "Color",
            "Color Mola mesh faces according to a value list",
            "Mola", "Utils")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Mesh", "M", "mesh to be converted", GH_ParamAccess.item);
            pManager.AddNumberParameter("Value list", "V", "a list of values to color all faces", GH_ParamAccess.list);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            List<float> valueList = new List<float>();
            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref valueList);

            UtilsFace.ColorFaceByValue(mMesh, valueList);

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
            get { return new Guid("fee14f0e-785d-4a7b-94c0-a8847c94c571"); }
        }
    }
}
