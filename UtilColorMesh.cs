﻿using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Linq;

namespace HDMolaGH
{
    public class UtilColorMesh : GH_Component
    {
        public UtilColorMesh()
          : base("Color Mesh", "Color",
            "Color Mola mesh faces according to a value list",
            "Mola", "4-Utils")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Mesh", "M", "mesh to be converted", GH_ParamAccess.item);
            pManager.AddNumberParameter("Value list", "VList", "a list of values to color all faces", GH_ParamAccess.list);
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
            DA.GetData(0, ref mMesh);
            DA.GetDataList(1, doubleList);

            floatList = doubleList.Select(a => (float)a).ToList();

            MolaMesh copyMesh = mMesh.Copy();
            // temp. make sure color list is same with vertices to be colored by value
            copyMesh.Colors = Enumerable.Repeat(Color.black, copyMesh.VertexCount()).ToList();
            UtilsFace.ColorFaceByValue(copyMesh, floatList);

            DA.SetData(0, copyMesh);
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
            get { return new Guid("fee14f0e-785d-4a7b-94c0-a8847c94c571"); }
        }
    }
}
