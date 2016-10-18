using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RasterTileLoader
{
    public class RasterTileLoader : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public RasterTileLoader()
        {
        }

        protected override void OnClick()
        {
            RasterTileLoader_Form form = RasterTileLoader_Form.instance;

            if (form == null)
                return;
            if (form.Visible == false)
            {
                form.Visible = true;
                return;
            }
            form.Visible = false;

            
        }
        protected override void OnUpdate()
        {
        }
    }

}
