using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class UtilMeshMerge : GH_Component
    {
        public UtilMeshMerge()
          : base("Mesh Merge", "Merge",
            "Merge a list of Mola Meshes into one",
            "Mola", "Utils")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Meshes", "M", "mesh to be merged", GH_ParamAccess.list);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<MolaMesh> mMeshList = new List<MolaMesh>();

            DA.GetData(0, ref mMeshList);

            MolaMesh mMesh = new MolaMesh();
            for (int i = 0; i < mMeshList.Count; i++)
            {
                mMesh.AddMesh(mMeshList[i]);
            }

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
            get { return new Guid("53d32542-fc19-46ca-b046-24923e137046"); }
        }
    }
}
