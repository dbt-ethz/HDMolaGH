using System;
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
            "Color Mola mesh faces",
            "Mola", "4-Utils")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.secondary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Mesh", "M", "mesh to be converted", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Mode", "Mode", "coloring method: 0 by single color, 1 by colors, 2 by a list of values", GH_ParamAccess.item, 0);
            pManager.AddColourParameter("Single Color", "C", "a single color to color all faces", GH_ParamAccess.item, System.Drawing.Color.White);
            pManager.AddColourParameter("Color list", "CList", "a list of colors to color all faces", GH_ParamAccess.list, new List<System.Drawing.Color>() { System.Drawing.Color.White });
            pManager.AddNumberParameter("Value list", "VList", "a list of values to color all faces", GH_ParamAccess.list, new List<double>() { 0});
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            int mode = 0;
            List<double> doubleList = new List<double>();
            List<System.Drawing.Color> rColorList = new List<System.Drawing.Color>();
            System.Drawing.Color rColor = new System.Drawing.Color();

            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref mode);
            DA.GetData(2, ref rColor);
            DA.GetDataList(3, rColorList);
            DA.GetDataList(4, doubleList);

            MolaMesh copyMesh = mMesh.Copy();

            switch (mode)
            {
                case 2:
                    if (doubleList.Count != copyMesh.FacesCount()) break;
                    List<float> floatList = new List<float>();
                    floatList = doubleList.Select(a => (float)a).ToList();
                    UtilsFace.ColorFaceByValue(copyMesh, floatList);
                    break;
                case 1:
                    if (rColorList.Count != copyMesh.FacesCount()) break;
                    List<Color> mColorList = new List<Color>();
                    mColorList = rColorList.Select(a => new Color((float)a.R / 255, (float)a.G / 255, (float)a.B / 255, (float)a.A / 255)).ToList(); 
                    for (int i = 0; i < copyMesh.FacesCount(); i++)
                    {
                        foreach (int v in copyMesh.Faces[i])
                        {
                            copyMesh.Colors[v] = mColorList[i];
                        }
                    }
                    break;
                case 0:
                    Color mColor = new Color((float)rColor.R / 255, (float)rColor.G / 255, (float)rColor.B / 255, (float)rColor.A / 255);
                    copyMesh.Colors = Enumerable.Repeat(mColor, copyMesh.VertexCount()).ToList();
                    break;
            }

            DA.SetData(0, copyMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.color;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("fee14f0e-785d-4a7b-94c0-a8847c94c571"); }
        }
    }
}
