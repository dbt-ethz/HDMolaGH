using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Linq;

namespace HDMolaGH
{
    public class UtilsMeshSplit : GH_Component
    {
        public UtilsMeshSplit()
          : base("Mesh Split", "Split",
            "Split a Mola mesh according to a boolean list",
            "Mola", "4-Utils")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Mesh", "M", "mesh to be converted", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Boolean List", "Booleans", "a list of boolean values to split mesh", GH_ParamAccess.list);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh1", "M1", "positive result mesh", GH_ParamAccess.item);
            pManager.AddGenericParameter("MolaMesh2", "M2", "negative result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            List<bool> maskList = new List<bool>();

            DA.GetData(0, ref mMesh);
            DA.GetDataList(1, maskList);

            if(maskList.Count > mMesh.FacesCount())
            {
                maskList.RemoveRange(mMesh.FacesCount(), maskList.Count - mMesh.FacesCount());
            }
            else if(maskList.Count < mMesh.FacesCount())
            {
                List<bool> newList = Enumerable.Repeat(false, mMesh.FacesCount() - maskList.Count).ToList();
                maskList.AddRange(newList);
            }
            bool[] maskArray = maskList.ToArray();

            MolaMesh m1 = mMesh.CopySubMesh(maskArray);
            maskArray = maskArray.Select(b => !b).ToArray();
            MolaMesh m2 = mMesh.CopySubMesh(maskArray);

            DA.SetData(0, m1);
            DA.SetData(1, m2);
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
            get { return new Guid("c07d83d3-6184-446c-8209-afa3dee6327d"); }
        }
    }
}
