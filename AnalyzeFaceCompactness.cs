using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class AnalyzeFaceCompactness : GH_Component
    {
        public AnalyzeFaceCompactness()
          : base("Analyze Face Compactness", "Face Compactness",
            "get a list of face compactness",
            "Mola", "3-Analysis")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be analyzed", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("FaceCompactness", "C", "a list of face compactness", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            DA.GetData(0, ref mMesh);

            List<float> compactnessList = new List<float>();
            for (int i = 0; i < mMesh.FacesCount(); i++)
            {
                compactnessList.Add(mMesh.FaceCompactness(i));
            }

            DA.SetDataList(0, compactnessList);
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
            get { return new Guid("5a4d20e3-239b-44a5-a3db-62fdcd01f910"); }
        }
    }
}
