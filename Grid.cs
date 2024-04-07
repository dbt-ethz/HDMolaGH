using System;
using System.Collections.Generic;
using Mola;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class Grid : GH_Component
    {
        public Grid()
          : base("Grid", "Grid",
            "create a 3D grid of bool or float",
            "Mola", "5-Voxel")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("X", "X", "x dimention of the grid", GH_ParamAccess.item, 10);
            pManager.AddIntegerParameter("Y", "Y", "y dimention of the grid", GH_ParamAccess.item, 10);
            pManager.AddIntegerParameter("Z", "Z", "z dimention of the grid", GH_ParamAccess.item, 10);
            pManager.AddIntegerParameter("Type", "T", "data type, 0 for bool and 1 for float", GH_ParamAccess.item, 0);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Grid", "Grid", "a Mola Grid", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int X = 10;
            int Y = 10;
            int Z = 10;
            int T = 0;

            DA.GetData(0, ref X);
            DA.GetData(1, ref Y);
            DA.GetData(2, ref Z);
            DA.GetData(3, ref T);

            if (T == 0)
            {
                MolaGrid<bool> grid = new MolaGrid<bool>(X, Y, Z);
                DA.SetData(0, grid);
            }
            else if(T == 1)
            {
                MolaGrid<float> grid = new MolaGrid<float>(X, Y, Z);
                DA.SetData(0, grid);
            } 
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

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("B6B84576-FF3A-4E9C-9007-BB5103A94FEE"); }
        }
    }
}