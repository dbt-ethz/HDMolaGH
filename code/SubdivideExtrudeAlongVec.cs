using System;
using System.Collections.Generic;
using Mola;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class SubdivideExtrudeAlongVec : GH_Component
    {
        public SubdivideExtrudeAlongVec()
          : base("Subdivide Extrude Along Vec", "ExtrudeAlong",
              "extrudes the all faces in a MolaMesh along a direction by distance height",
              "Mola", "2-Subdivisions")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.secondary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddVectorParameter("Direction", "D", "extrude direction", GH_ParamAccess.item, new Vector3d(0, 0, 1));
            pManager.AddNumberParameter("Height", "H", "extrude height", GH_ParamAccess.item, 1.0);
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
            Vector3d d = new Vector3d(0, 0, 1);
            double h = 0;
            bool c = true;
            int iteration = 1;

            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref d);
            DA.GetData(2, ref h);
            DA.GetData(3, ref c);
            DA.GetData(4, ref iteration);

            Vec3 molaVec = new Vec3((float)d.X, (float)d.Y, (float)d.Z);

            for (int i = 0; i < iteration; i++)
            {
                mMesh = MeshSubdivision.ExtrudeAlongVec(mMesh, molaVec, (float)h, c);
            }

            DA.SetData(0, mMesh);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.extrudealongvector;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("443C14C8-ED5E-4EE7-AACC-1F72601515E1"); }
        }
    }
}