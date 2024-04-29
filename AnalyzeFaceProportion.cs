using System;
using System.Collections.Generic;
using Mola;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class AnalyzeFaceProportion : GH_Component
    {
        public AnalyzeFaceProportion()
          : base("Analyze Face Proportion", "Face Proportion",
              "get a list of face proportions",
              "Mola", "3-Analysis")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be analyzed", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("FaceProportion", "P", "a list of face proportions", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            DA.GetData(0, ref mMesh);

            List<float> proportionList = new List<float>();
            for (int i = 0; i < mMesh.FacesCount(); i++)
            {
                proportionList.Add(mMesh.FaceProportion(i));
            }

            DA.SetDataList(0, proportionList);

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

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("42F2358D-C693-4A5B-9665-E7D05F8CB5ED"); }
        }
    }
}