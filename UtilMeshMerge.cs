using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;

namespace HDMolaGH
{
    public class UtilMeshJoin : GH_Component
    {
        public UtilMeshJoin()
          : base("Mesh Join", "Join",
            "Join a list of Mola Meshes into one",
            "Mola", "4-Utils")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Meshes", "M", "mesh to be joined", GH_ParamAccess.list);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<MolaMesh> mMeshList = new List<MolaMesh>();
            DA.GetDataList(0, mMeshList);

            MolaMesh mMesh = new MolaMesh();
            for (int i = 0; i < mMeshList.Count; i++)
            {
                mMesh.AddMesh(mMeshList[i]);
            }

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.meshjoin;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("53d32542-fc19-46ca-b046-24923e137046"); }
        }
    }
}
