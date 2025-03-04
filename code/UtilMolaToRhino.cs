﻿using System;
using System.Collections.Generic;
using Mola;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Linq;

namespace HDMolaGH
{
    public class UtilMolaToRhino : GH_Component
    {
        public UtilMolaToRhino()
          : base("Mola to Rhino", "To Rhino",
              "convert a mola mesh to a rhino mesh",
              "Mola", "4-Utils")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.tertiary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Mesh", "M", "mesh to be converted", GH_ParamAccess.item);
            pManager.AddBooleanParameter("With Color", "C", "convert mola color to rhino color", GH_ParamAccess.item, false);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            bool withColor = false;
            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref withColor);

            MolaMesh meshCopy = mMesh.Copy();
            if (!withColor)
            {
                meshCopy.Colors = new List<Color>();
            }
            else
            {
                if (meshCopy.Colors.Count != meshCopy.VertexCount())
                {
                    meshCopy.Colors = Enumerable.Repeat(Color.white, meshCopy.VertexCount()).ToList();
                }
            }

            Mesh rMesh = HDMeshConverter.FillRhinoMesh(meshCopy);

            DA.SetData(0, rMesh);
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.molatorhino;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("0132E621-E581-422D-8750-148037B6FBE3"); }
        }
    }
}