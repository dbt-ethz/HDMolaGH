﻿using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace HDMolaGH
{
    public class HDMolaGHInfo : GH_AssemblyInfo
    {
        public override string Name => "HDMolaGH";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("2E68F54B-8589-4E08-A2E3-C150AD0FDB3D");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}