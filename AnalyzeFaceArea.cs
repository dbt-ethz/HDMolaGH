using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class AnalyzeFaceArea : GH_Component
    {
        public AnalyzeFaceArea()
          : base("Analyze Face Area", "Face Area",
            "get a list of face areas",
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
            pManager.AddNumberParameter("FaceAreas", "F", "a list of face areas", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            DA.GetData(0, ref mMesh);

            List<float> areaList = new List<float>();
            for (int i = 0; i < mMesh.FacesCount(); i++)
            {
                areaList.Add(mMesh.FaceArea(i));
            }

            DA.SetDataList(0, areaList);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.area;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("5dfed65f-5256-41cc-8355-b6df8d3a9147"); }
        }
    }
}
