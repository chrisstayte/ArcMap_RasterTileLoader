using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using Ookii.Dialogs;

namespace RasterTileLoader
{
    public partial class RasterTileLoader_Form : Form
    {
        #region Properties

        private static volatile RasterTileLoader_Form _instance;  // Instantiating A Singleton
        private static object _syncRoot = new object();
        private const string MB_TITLE = "RTL";
        private IApplication _application;
        private IMap _map;
        private IMxDocument _mxdocument;
        private Utilities _utilities;
        private IDictionary<String, Boolean> rasterList = new Dictionary<String, Boolean>();

        private string appDataFolder = String.Empty;
        private string saveFolder = String.Empty;
        private string saveFile = "history_filetype";
        private List<String> fileTypeHistory = new List<String>();

        #endregion

        #region Constructor

        public RasterTileLoader_Form(IApplication application)
        {
            _application = application;
            InitializeComponent();

            appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            saveFolder = System.IO.Path.Combine(appDataFolder, "RasterTileLoader");
        }

        public static RasterTileLoader_Form instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new RasterTileLoader_Form(ArcMap.Application);
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Events

        private void btnInit_Click(object sender, EventArgs e)
        {
            initialize();
        }

        private void btnLoadRaster_Click(object sender, EventArgs e)
        {

            if (checkRequirements())
            {
                generateRasterList();
                validateRasterList();
                loadRasterList();
            }
            
            //IRasterLayer rasterLayer = new RasterLayer();
            //rasterLayer.CreateFromFilePath("");
            //_mxdocument.AddLayer(rasterLayer);
        }

        private void cboTileIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboFieldName.Items.Clear();
            ILayer tileIndex = _utilities.Layer(cboTileIndex.Text);
            cboFieldName.Items.AddRange(_utilities.AllFields(tileIndex).ToArray());
        }

        private void btnSelectWorkspace_Click(object sender, EventArgs e)
        {
            string previousFolderDst = this.txbRasterWorkspace.Text;
            //FolderBrowserDialog fbd = new FolderBrowserDialog();
            //if (Directory.Exists(previousFolderDst))
            //    fbd.SelectedPath = previousFolderDst;
            //DialogResult result = new DialogResult();

            //result = fbd.ShowDialog();

            //if (result == DialogResult.OK)
            //     this.txbRasterWorkspace.Text = fbd.SelectedPath;

            var folderBrowserDialog = new VistaFolderBrowserDialog();

            if (Directory.Exists(previousFolderDst))
                folderBrowserDialog.SelectedPath = previousFolderDst;

            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
                MessageBox.Show(this, "Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
                this.txbRasterWorkspace.Text = folderBrowserDialog.SelectedPath;
        }

        private void cboExtension_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') e.Handled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
        #endregion

        #region Methods

        private void initialize()
        {
            _mxdocument = (IMxDocument)_application.Document;
            _map = _mxdocument.FocusMap;
            _utilities = new Utilities(_map);

            loadFileTypeList();

            cboTileIndex.Items.AddRange(_utilities.PolygonLayers().ToArray());
            if (cboTileIndex.Items.Count > 0)
            {
                cboTileIndex.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Add Some Polygon Layers To Arcmap", MB_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool checkRequirements()
        {
            if (String.IsNullOrEmpty(cboFieldName.Text) || String.IsNullOrEmpty(cboTileIndex.Text) || String.IsNullOrEmpty(txbRasterWorkspace.Text))
            {
                MessageBox.Show("initialize Tool Settings", MB_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Visible = true;
                return false;
            }
            try
            {
                IFeatureLayer featureLayer = _utilities.FeatureLayer(cboTileIndex.Text);
                if (featureLayer == null)
                    return false;

                IFeatureClass featureClass = featureLayer.FeatureClass;
                if (featureClass == null)
                    return false;

                IFields fields = featureClass.Fields;
                if (!(_utilities.FindField(featureClass, cboFieldName.Text) > -1))
                    return false;

                IFeatureSelection featureSelection = featureLayer as IFeatureSelection;

                if (rbtnSelected.Checked)
                {
                    if (featureSelection.SelectionSet.Count == 0)
                    {
                        showErrorBox("Select at least one feature.");
                        return false;
                    }
                }

                if (!Directory.Exists(txbRasterWorkspace.Text))
                {
                    showErrorBox("Raster Workspace Is Invalid");   
                    return false;
                }
                    
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void generateRasterList()
        {
            // Clear the Raster List
            rasterList.Clear();

            IFeatureLayer featureLayer = _utilities.FeatureLayer(cboTileIndex.Text);
            IFeatureClass featureClass = featureLayer.FeatureClass;

            if (rbtnAll.Checked)  // Grab All Of The Raster :) 
            {

                IFeatureCursor cursor = featureClass.Search(null, false);

                IFields fields = cursor.Fields;
                int fieldIndex = _utilities.FindField(featureClass, cboFieldName.Text);

                IFeature feature = cursor.NextFeature();

                while (feature != null)
                {
                    // Default To True I will validate After Adding All To Dictonary
                    if (!rasterList.ContainsKey(Convert.ToString(feature.get_Value(fieldIndex))))
                    {
                        rasterList.Add(Convert.ToString(feature.get_Value(fieldIndex)), true);
                    }
                    
                    feature = cursor.NextFeature();
                }
            }
            else // Grab Only The Selected Tiles
            {
                IFeatureSelection featureSelection = featureLayer as IFeatureSelection;
                IEnumIDs enumIDs = featureSelection.SelectionSet.IDs;
                enumIDs.Reset();
                int intOID = enumIDs.Next();
                while (intOID != -1)
                {
                    IFeature feature = featureClass.GetFeature(intOID);
                    if (feature != null)
                    {
                        int fieldIndex = _utilities.FindField(featureClass, cboFieldName.Text);
                        // Default To True I will validate After Adding All To Dictonary
                        if (!rasterList.ContainsKey(Convert.ToString(feature.get_Value(fieldIndex))))
                        {
                            rasterList.Add(Convert.ToString(feature.get_Value(fieldIndex)), true);
                        }
                    }
                    intOID = enumIDs.Next();
                }

            }
            
        }

        private void validateRasterList()
        {
            IDictionary<String, Boolean> newList = new Dictionary<String, Boolean>();

            foreach (KeyValuePair<String, Boolean> raster in rasterList) 
            {
                string filePath = txbRasterWorkspace.Text + "\\" + addPrefixAndSuffixToFileName(raster.Key) + getExtension();
                if (!File.Exists(filePath))
                    newList.Add(raster.Key, false);
                else
                    newList.Add(raster.Key, raster.Value);
                
            }
            rasterList = newList;
        }

        private void loadRasterList()
        {
            bool itWorked = false;
            foreach (KeyValuePair<String, Boolean> raster in rasterList)
            {
                if (raster.Value == true)
                {
                    if (!itWorked)
                        saveFileTypeList(getExtension());
                    itWorked = true;

                    string filePath = txbRasterWorkspace.Text + "\\" + addPrefixAndSuffixToFileName(raster.Key) + getExtension();
                    try
                    {
                        IRasterLayer rasterLayer = new RasterLayer();
                        rasterLayer.CreateFromFilePath(filePath);
                        _mxdocument.AddLayer(rasterLayer);
                    }
                    catch (Exception ex)
                    {
                        
                        // Just So ArcMap doesn't crash ;) 
                    }
                    
                }
            }
            if (itWorked)
            {
                _mxdocument.ActivatedView.Refresh();
            }

            rasterList.Clear();
        }

        private string getExtension()
        {
            string extension = String.Empty;

            if (cboExtension.Text != String.Empty)
            {
                extension = cboExtension.Text;
                if (extension.Contains("."))
                    extension = extension.Replace(".", "");
                return "." + extension;
            }
            return "NO EXTENSION LOADED";
        }

        private string addPrefixAndSuffixToFileName(string fileName)
        {
            string filename = String.Empty;
            string temp = String.Empty;

            if (!String.IsNullOrEmpty(txbPrefix.Text))
            {
                temp = txbPrefix.Text;
                temp = temp.Replace(" ", string.Empty);
                filename += temp;
            }

            filename += fileName;

            if (!String.IsNullOrEmpty(txbSuffix.Text))
            {
                temp = txbSuffix.Text;
                temp = temp.Replace(" ", string.Empty);
                filename += temp;
            }
            return filename;   
        }

        private void showErrorBox(String errorMessage)
        {
            MessageBox.Show(errorMessage, MB_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void loadFileTypeList()
        {
            cboExtension.Items.Clear();
            fileTypeHistory.Clear();

            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            if (File.Exists(saveFolder + "\\" + saveFile))
            {
                using (StreamReader sStreamReader = new StreamReader(saveFolder + "\\" + saveFile))
                {
                    string AllData = sStreamReader.ReadToEnd();
                    fileTypeHistory.AddRange(AllData.Split(",".ToCharArray()));
                    cboExtension.Items.AddRange(fileTypeHistory.ToArray());
                }
                
            }
        }

        private void saveFileTypeList(string type) 
        {
            if (!fileTypeHistory.Contains(type, StringComparer.OrdinalIgnoreCase) && !String.IsNullOrEmpty(type))
            {
                fileTypeHistory.Add(type);
            }
            using (TextWriter tw = new StreamWriter(saveFolder + "\\" + saveFile))
            {
                tw.Write(String.Join(",", fileTypeHistory));
            }
        }
        #endregion
    }
}


