using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class UtilMolsToJson : GH_Component
    {
        public UtilMolsToJson()
          : base("Mola to JSON", "To JSON",
              "convert a mola mesh to JSON",
              "Mola", "4-Utils")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.tertiary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Mesh", "M", "mesh to be converted", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("JSON", "J", "result JSON", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            DA.GetData(0, ref mMesh);

            string json = mMesh.ToJson();
            DA.SetData(0, json);
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
            get { return new Guid("9316a8fb-3a6f-474c-b78f-1091e7fe36fa"); }
        }
    }
}
