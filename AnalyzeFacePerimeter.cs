using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class AnalyzeFacePerimeter : GH_Component
    {
        public AnalyzeFacePerimeter()
          : base("Analyze Face Edge Perimeter", "Face Perimeter",
            "get a list of face perimeters",
            "Mola", "3-Analysis")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be analyzed", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("FaceEdgeLength", "L", "a list of face perimeters", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            DA.GetData(0, ref mMesh);

            List<float> perimeterList = new List<float>();
            for (int i = 0; i < mMesh.FacesCount(); i++)
            {
                perimeterList.Add(mMesh.FacePerimeter(i));
            }

            DA.SetDataList(0, perimeterList);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.perimetor;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("9b62919a-02d9-4d81-8792-1fc9b9b954f9"); }
        }
    }
}
