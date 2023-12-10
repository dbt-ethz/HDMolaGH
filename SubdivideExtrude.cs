﻿using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Mola;

namespace HDMolaGH
{
    public class SubdivideExtrude : GH_Component
    {
        public SubdivideExtrude()
          : base("SubdivideExtrude", "Extrude",
              "extrudes the all faces in a MolaMesh straight by distance height",
              "Mola", "Subdivision")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddNumberParameter("Height", "H", "extrude height", GH_ParamAccess.item, 1.0);
            pManager.AddBooleanParameter("Cap", "C", "wether cap the top", GH_ParamAccess.item, false);
            pManager.AddIntegerParameter("Iteration", "I", "subdivide times", GH_ParamAccess.item, 1);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh rMesh = new Mesh();
            double h = 0;
            bool c = false;
            int iteration = 1;

            DA.GetData(0, ref rMesh);
            DA.GetData(1, ref h);
            DA.GetData(2, ref c);
            DA.GetData(3, ref iteration);

            MolaMesh mMesh = HDMeshToRhino.FillMolaMesh(rMesh);
            for (int i = 0; i < iteration; i++)
            {
                mMesh = MeshSubdivision.Extrude(mMesh, (float)h, c);
            }

            rMesh = HDMeshToRhino.FillRhinoMesh(mMesh);
            DA.SetData(0, rMesh);
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
            get { return new Guid("7DFE7595-CA1E-4363-B0BA-8F9EB1277D26"); }
        }
    }
}