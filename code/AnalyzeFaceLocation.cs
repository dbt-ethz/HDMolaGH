using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class AnalyzeFaceLocation : GH_Component
    {
        public AnalyzeFaceLocation()
          : base("Analyze Face Location", "Face Location",
            "get a list of face center locations",
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

            List<Vector3f> locationList = new List<Vector3f>();
            for (int i = 0; i < mMesh.FacesCount(); i++)
            {
                locationList.Add(new Vector3f(mMesh.FaceCenter(i).x, mMesh.FaceCenter(i).y, mMesh.FaceCenter(i).z));
            }

            DA.SetDataList(0, locationList);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.location;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("44324e87-e367-46ec-9e98-2690cf751f91"); }
        }
    }
}
