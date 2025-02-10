using System;
using System.Collections.Generic;
using Mola;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class SubdivideExtrudeTaperedAlongVec : GH_Component
    {
        public SubdivideExtrudeTaperedAlongVec()
          : base("Subdivide Extrude Tapered Along Vec", "ExtrudeTaperedAlongVec",
              "Extrudes all face in a MolaMesh tapered along a direction by height",
              "Mola", "2-Subdivisions")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.secondary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddVectorParameter("Direction", "D", "extrude direction", GH_ParamAccess.item, new Vector3d(0, 0, 1));
            pManager.AddNumberParameter("Height", "H", "extrude height", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("Fraction", "F", "fraction between 0 and 1", GH_ParamAccess.item, 0.5);
            pManager.AddBooleanParameter("Cap", "C", "wether cap the top", GH_ParamAccess.item, true);
            pManager.AddIntegerParameter("Iteration", "I", "subdivide times", GH_ParamAccess.item, 1);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            double h = 0;
            Vector3d d = new Vector3d(0, 0, 1);
            double f = 0.5;
            bool c = true;
            int iteration = 1;

            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref d);
            DA.GetData(2, ref h);
            DA.GetData(3, ref f);
            DA.GetData(4, ref c);
            DA.GetData(5, ref iteration);

            Vec3 molaVec = new Vec3((float)d.X, (float)d.Y, (float)d.Z);

            for (int i = 0; i < iteration; i++)
            {
                mMesh = MeshSubdivision.ExtrudeTapered(mMesh, molaVec, (float)h, (float)f, c);
            }

            DA.SetData(0, mMesh);
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

        public override Guid ComponentGuid
        {
            get { return new Guid("76678FA8-16FA-450D-AF62-A007702E3201"); }
        }
    }
}