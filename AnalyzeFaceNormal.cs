using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class AnalyzeFaceNormal : GH_Component
    {
        public AnalyzeFaceNormal()
          : base("Analyze Face Normal", "Face Normal",
            "get a list of face mnormals",
            "Mola", "3-Analysis")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.primary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be analyzed", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddVectorParameter("FaceNormals", "N", "a list of face normals", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            DA.GetData(0, ref mMesh);

            List<Vector3f> normalList = new List<Vector3f>();
            for (int i = 0; i < mMesh.FacesCount(); i++)
            {
                normalList.Add(new Vector3f(mMesh.FaceNormal(i).x, mMesh.FaceNormal(i).y, mMesh.FaceNormal(i).z));
            }

            DA.SetDataList(0, normalList);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.normal;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("eb8efa7a-0aba-4501-bf56-11bcdec989d9"); }
        }
    }
}
