using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Mola;

namespace HDMolaGH
{
    public class FilterModulo : GH_Component
    {
        public FilterModulo()
          : base("FilterByModulo", "ByModulo",
              "filter result faces into two meshes by face index modulo",
              "Mola", "Filter")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Index", "I", "index of faces to be extracted", GH_ParamAccess.item, 0);
            pManager.AddIntegerParameter("Modulo", "Mo", "modulo of each group of faces", GH_ParamAccess.item, 5);
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
            int modu = 5;

            DA.GetData(0, ref rMesh);
            DA.GetData(1, ref index);
            DA.GetData(2, ref modu);

            MolaMesh mMesh = HDMeshToRhino.FillMolaMesh(rMesh);
            MolaMesh m1 = mMesh.CopySubMeshByModulo(index, modu);
            MolaMesh m2 = mMesh.CopySubMeshByModulo(index, modu, true);

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

        public override Guid ComponentGuid
        {
            get { return new Guid("CC81962C-FDCA-4B7E-9F64-FF541A0531AE"); }
        }
    }
}