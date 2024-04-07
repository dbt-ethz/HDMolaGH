using System;
using System.Collections.Generic;
using Mola;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class GridToMolaMesh : GH_Component
    {
        public GridToMolaMesh()
          : base("Grid To MolaMesh", "Grid To Mola",
            "convert a mola grid to a mola mesh",
            "Mola", "5-Voxel")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Grid", "G", "grid to be converted", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Mesh", "M", "the result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaGrid<bool> grid = new MolaGrid<bool>(0, 0, 0);
            DA.GetData(0, ref grid);

            MolaMesh mesh = UtilsGrid.VoxelMesh(grid);

            DA.SetData(0, mesh);
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
            get { return new Guid("1080664B-D654-4EE7-B592-014C99761617"); }
        }
    }
}