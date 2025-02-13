using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class AnalyzeMolaMesh : GH_Component
    {
        public AnalyzeMolaMesh()
          : base("Analyze MolaMesh", "Analyze Mesh",
            "get Mola Mesh properties",
            "Mola", "3-Analysis")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.secondary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be analyzed", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Vertice Count", "V", "vertice count", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Face Count", "F", "face count", GH_ParamAccess.item);
            //pManager.AddIntegerParameter("Color Count", "C", "color count", GH_ParamAccess.item);
            //pManager.AddIntegerParameter("UV Count", "UV", "UV count", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            DA.GetData(0, ref mMesh);

            int vcount = mMesh.VertexCount();
            int fcount = mMesh.FacesCount();
            //int ccount = mMesh.Colors.Count;
            //int uvcount = mMesh.UVs.Count;

            DA.SetData(0, vcount);
            DA.SetData(1, fcount);
            //DA.SetData(2, ccount);
            //DA.SetData(3, uvcount);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.analyzemola;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("6e365d45-ff22-4d9f-b844-d9c4c64d187b"); }
        }
    }
}
