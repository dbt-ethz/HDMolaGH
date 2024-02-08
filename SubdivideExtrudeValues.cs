using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Linq;

namespace HDMolaGH
{
    public class SubdivideExtrudeValues : GH_Component
    {
        public SubdivideExtrudeValues()
          : base("Subdivide Extrude Values", "Extrude Values",
              "extrudes the all faces in a MolaMesh straight by a list of distance heights",
              "Mola", "2-Subdivisions")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddNumberParameter("Height values", "Hs", "a list of height values", GH_ParamAccess.list);
            pManager.AddBooleanParameter("Cap", "C", "wether cap the top", GH_ParamAccess.list);
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
            List<bool> cList = new List<bool>();
            int iteration = 1;

            DA.GetData(0, ref mMesh);
            DA.GetDataList(1, doubleList);
            DA.GetDataList(2, cList);
            DA.GetData(3, ref iteration);

            floatList = doubleList.Select(a => (float)a).ToList();

            for (int i = 0; i < iteration; i++)
            {
                mMesh = MeshSubdivision.Extrude(mMesh, floatList, cList);
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
        public override Guid ComponentGuid
        {
            get { return new Guid("70ece2d0-eb18-4c05-9cc8-2f3c44108bd4"); }
        }
    }
}
