using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Mola;

namespace HDMolaGH
{
    public class FilterIndex : GH_Component
    {
        public FilterIndex()
          : base("FilterByIndex", "ByIndex",
              "filter result faces into two meshes by face index",
              "Mola", "Filter")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Index", "I", "index of faces to be extracted", GH_ParamAccess.item, 0);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh1", "M1", "positive result mesh", GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh2", "M2", "negative result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh rMesh = new Mesh();
            int index = 0;

            DA.GetData(0, ref rMesh);
            DA.GetData(1, ref index);

            MolaMesh mMesh = HDMeshToRhino.FillMolaMesh(rMesh);
            MolaMesh m1 = mMesh.CopySubMesh(index);
            MolaMesh m2 = mMesh.CopySubMesh(index, true);

            Mesh rm1 = HDMeshToRhino.FillRhinoMesh(m1);
            Mesh rm2 = HDMeshToRhino.FillRhinoMesh(m2);

            DA.SetData(0, rm1);
            DA.SetData(1, rm2);
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

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("D37E420C-D55F-424C-A247-1DA229AAFABA"); }
        }
    }
}