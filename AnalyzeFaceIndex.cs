using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Linq;

namespace HDMolaGH
{
    public class AnalyzeFaceIndex : GH_Component
    {
        public AnalyzeFaceIndex()
          : base("Analyze Face Index", "Face Index",
            "get a list of face index",
            "Mola", "3-Analysis")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be analyzed", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("FaceIndex", "I", "a list of face indexs", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            DA.GetData(0, ref mMesh);

            int fcount = mMesh.FacesCount();
            List<int> indexList = Enumerable.Range(0, fcount).ToList();

            DA.SetDataList(0, indexList);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.index;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("29236dea-c29f-44d6-9b10-7a494e4bff0f"); }
        }
    }
}
