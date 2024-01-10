using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class AnalyzeFaceModulo : GH_Component
    {
        public AnalyzeFaceModulo()
          : base("AnalyzeFaceModulo", "Face Modulo",
            "get a boolean list of faces modolo",
            "Mola", "Analyze")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be analyzed", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Modulo", "Mo", "modulo number", GH_ParamAccess.item);
            pManager.AddIntegerParameter("N", "N", "every n item", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("BooleanList", "B", "a list of boolean values", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            int modulo = 4;
            int n = 0;
            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref modulo);
            DA.GetData(2, ref n);

            List<bool> boolList = new List<bool>(mMesh.FacesCount());
            for (int i = 0; i < mMesh.FacesCount(); i++)
            {
                if(i % modulo == n)
                {
                    boolList[i] = true;
                }
            }

            DA.SetData(0, boolList);
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
            get { return new Guid("a2195b9a-b0aa-4ab4-ab4f-cac7e4db28d0"); }
        }
    }
}
