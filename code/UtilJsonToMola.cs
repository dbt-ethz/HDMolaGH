using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class UtilJsonToMola : GH_Component
    {
        public UtilJsonToMola()
          : base("JSON to Mola", "From JSON",
            "construct a mola mesh from JSON",
            "Mola", "4-Utils")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.tertiary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("JSON", "J", "result JSON", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Mesh", "M", "mesh to be converted", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string json = "";
            DA.GetData(0, ref json);

            MolaMesh mMesh = MolaMesh.FromJson(json);

            DA.SetData(0, mMesh);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("1ad2ded1-38e5-4ebc-bf46-34172489f476"); }
        }
    }
}
