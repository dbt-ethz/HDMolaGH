using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class SubdivideCatmullClark : GH_Component
    {
        public SubdivideCatmullClark()
          : base("Subdivide CatmullClark", "CatmullClark",
              "apply CatmullClark algorithm to a MolaMesh",
              "Mola", "2-Subdivisions")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.primary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Iteration", "I", "subdivide times", GH_ParamAccess.item, 1);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            int iteration = 1;

            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref iteration);

            mMesh.WeldVertices();
            mMesh.UpdateTopology();

            for (int i = 0; i < iteration; i++)
            {
                mMesh = MeshSubdivision.CatmullClark(mMesh);
            }
            mMesh.WeldVertices();
            mMesh.UpdateTopology();

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.catmullclark;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("99ff0c51-47f9-4104-80a3-1905c702714a"); }
        }
    }
}
