using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Linq;

namespace HDMolaGH
{
    public class SubdivideExtrudToPointWithValues : GH_Component
    {
        public SubdivideExtrudToPointWithValues()
          : base("Subdivide Extrude to Point Center with Values", "Extrude Point Center with Values",
             "extrudes the all faces their center point moved by hieght normal",
             "Mola", "2-Subdivisions")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.secondary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddNumberParameter("Height values", "Hs", "a list of extrude height", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Iteration", "I", "subdivide times", GH_ParamAccess.item, 1);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            List<double> doubleList = new List<double>();
            List<float> floatList = new List<float>();
            int iteration = 1;

            DA.GetData(0, ref mMesh);
            DA.GetDataList(1, doubleList);
            DA.GetData(2, ref iteration);

            floatList = doubleList.Select(a => (float)a).ToList();

            for (int i = 0; i < iteration; i++)
            {
                mMesh = MeshSubdivision.ExtrudeToPointCenter(mMesh, floatList);
            }

            DA.SetData(0, mMesh);
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

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("c55b14b3-7d6b-45f5-b349-6ad6be1c78a0"); }
        }
    }
}
