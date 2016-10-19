﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.ArcMapUI;
using System.Collections;
using System.Windows.Forms;


namespace RasterTileLoader
{
    class Utilities
    {
        #region Field(s)

        IMap m_pMap = null;
        private bool m_suppressmessage = false;

        public enum DomainItemType
        {
            Value = 0, Name, Combined
        };

        #endregion

        #region Properties

        /// <summary>
        /// <para> FocusMap property </para>
        /// <para> initialize this class with a Map object to use the majority of its methods </para>
        /// </summary>
        private IMap FocusMap
        {
            get
            {
                return m_pMap;
            }
        }

        /// <summary>
        /// Set true to suppress the appearance of a message box on error otherwise set to false.
        /// </summary>
        public bool SupressMessaging
        {
            get { return m_suppressmessage; }
            set { m_suppressmessage = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor overloded class
        /// </summary>
        public Utilities()
        {
        }

        /// <summary> Constructor overloaded class </summary>
        /// <param name="pMapControl"> ESRI's IMap object (IMapControl4) </param>
        public Utilities(IMap pMap)
        {
            m_pMap = pMap;
        }
        #endregion

        #region Method(s)

        /// <summary> The polygon feature layers in current map </summary>
        /// <returns> Function to retrieve a list of polygon feature layers in current map </returns>
        public ArrayList PolygonLayers()
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                UID pID = new UIDClass();
                pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //GeoFeatureLayer
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    IFeatureLayer2 pFLayer = (IFeatureLayer2)pLayer;
                    if (pFLayer.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        if (pFLayer.FeatureClass != null)
                            pList.Add(pLayer.Name);
                    }
                    pLayer = pEnumLayer.Next();
                }
                return pList;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Polygon Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        /// <summary> The feature layers in current map </summary>
        /// <returns> Function to retrieve a list of all feature layers in current map </returns>
        public ArrayList FeatureLayers()
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                UID pID = new UIDClass();
                pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //GeoFeatureLayer
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    if (pLayer is IFeatureLayer)
                        pList.Add(pLayer.Name);
                    pLayer = pEnumLayer.Next();
                }
                return pList;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        /// <summary> Returns a list of feature layers based on the geometry type </summary>
        /// <param name="geometryType">Geometry type.  Expected values "Point", "Line" or "Polygon".</param>
        /// <returns>Arraylist</returns>
        public ArrayList FeatureLayers(string geometryType)
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                UID pID = new UIDClass();
                pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //GeoFeatureLayer
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    if (pLayer is IFeatureLayer)
                    {
                        IFeatureLayer featurelayer = (IFeatureLayer)pLayer;
                        if (string.Compare(geometryType, "Point", true) == 0)
                        {
                            if ((featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint) || (featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPoint))
                                pList.Add(pLayer.Name);
                        }
                        if (string.Compare(geometryType, "Line", true) == 0)
                        {
                            if ((featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline) || (featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryLine))
                                pList.Add(pLayer.Name);
                        }
                        if (string.Compare(geometryType, "Polygon", true) == 0)
                        {
                            if (featurelayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                                pList.Add(pLayer.Name);
                        }
                    }
                    pLayer = pEnumLayer.Next();
                }
                return pList;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        /// <summary> The selectable layers in current map </summary>
        /// <returns> Function to retrieve a list of all selectable layers in current map </returns>
        public ArrayList SelectableLayers()
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                UID pID = new UIDClass();
                pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //GeoFeatureLayer
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    IFeatureLayer2 pFLayer = (IFeatureLayer2)pLayer;
                    if (pFLayer.Selectable == true)
                        pList.Add(pLayer.Name);
                    pLayer = pEnumLayer.Next();
                }
                return pList;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        /// <summary> The point feature layers in current map </summary>
        /// <returns> Function to retrieve a list of all point feature layers in current map </returns>
        public ArrayList PointLayers()
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                UID pID = new UIDClass();
                pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //GeoFeatureLayer
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    IFeatureLayer2 pFLayer = (IFeatureLayer2)pLayer;
                    if (pFLayer.ShapeType == esriGeometryType.esriGeometryMultipoint || pFLayer.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        pList.Add(pLayer.Name);
                    }
                    pLayer = pEnumLayer.Next();
                }
                return pList;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Point Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        /// <summary> The polyline feature layers in current map </summary>
        /// <returns> Function to retrieve a list of polyline feature layers in current map </returns>
        public ArrayList PolylineLayers()
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                UID pID = new UIDClass();
                pID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; //GeoFeatureLayer
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    IFeatureLayer2 pFLayer = (IFeatureLayer2)pLayer;
                    if (pFLayer.ShapeType == esriGeometryType.esriGeometryLine || pFLayer.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        pList.Add(pLayer.Name);
                    }
                    pLayer = pEnumLayer.Next();
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Polyline Layers", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The specified layer </summary>
        /// <param name="LayerName"> The layer be retrieved </param>
        ///<returns> Function to retrieve a layer by name </returns>
        public ILayer Layer(string LayerName)
        {
            if (FocusMap == null)
                return null;
            if (FocusMap.LayerCount == 0)
                return null;
            try
            {
                UID pID = new UIDClass();
                pID.Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}";
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    if (string.Compare(pLayer.Name, LayerName, true) == 0)
                        return pLayer;
                    pLayer = pEnumLayer.Next();
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return null;
        }

        ///<summary> The specified featurelayer </summary>
        /// <param name="LayerName"> The feature layer be retrieved </param>
        ///<returns> Function to retrieve a layer by name </returns>
        public IFeatureLayer FeatureLayer(string LayerName)
        {
            if (FocusMap == null)
                return null;
            if (FocusMap.LayerCount == 0)
                return null;
            try
            {
                UID pID = new UIDClass();
                pID.Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}";
                IEnumLayer pEnumLayer = FocusMap.get_Layers(pID, true);
                pEnumLayer.Reset();
                ILayer pLayer = pEnumLayer.Next();
                while (!(pLayer == null))
                {
                    if (string.Compare(pLayer.Name, LayerName, true) == 0)
                        return pLayer as IFeatureLayer;
                    pLayer = pEnumLayer.Next();
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return null;
        }

        /// <summary>
        /// Returns the editor
        /// </summary>
        /// <param name="mxApplication"></param>
        /// <returns>the editor</returns>
        public IEditor3 GetEditorFromArcMap(IMxApplication mxApplication)
        {
            if (mxApplication == null)
            {
                return null;
            }
            ESRI.ArcGIS.esriSystem.UID uid = new ESRI.ArcGIS.esriSystem.UIDClass();
            uid.Value = "{F8842F20-BB23-11D0-802B-0000F8037368}";
            ESRI.ArcGIS.Framework.IApplication application = mxApplication as ESRI.ArcGIS.Framework.IApplication; // Dynamic Cast
            ESRI.ArcGIS.esriSystem.IExtension extension = application.FindExtensionByCLSID(uid);
            ESRI.ArcGIS.Editor.IEditor3 editor3 = extension as ESRI.ArcGIS.Editor.IEditor3; // Dynamic Cast

            return editor3;
        }

        /// <summary>
        /// Returns a list of all the coded domains: 0 - Value, 1 - Name, 2 - Combined "Value (name)"
        /// </summary>
        /// <param name="feature_layer"></param>
        /// <param name="attribute_field"></param>
        /// <param name="domainitemtype"></param>
        /// <returns></returns>
        public IList<string> GetCodedDomainItems(IFeatureLayer feature_layer, string attribute_field, DomainItemType domainitemtype)
        {
            IList<string> domain_list = new List<string>();


            IFields fields = feature_layer.FeatureClass.Fields;

            if (fields.FieldCount == 0)
                throw new Exception("Feature Class has no fields");

            int field_index = fields.FindField(attribute_field);

            if (field_index > -1)
            {
                IDomain domain = fields.get_Field(field_index).Domain;
                if (domain == null)
                    throw new Exception("Attribute field '" + attribute_field + "' has no domain");

                if (domain.Type == esriDomainType.esriDTCodedValue)
                {
                    ICodedValueDomain codedvaluedomain = (ICodedValueDomain)domain;
                    int codecount = codedvaluedomain.CodeCount;
                    if (domainitemtype == DomainItemType.Name)
                    {
                        for (int i = 0; i < codecount; i++)
                            domain_list.Add(codedvaluedomain.get_Name(i).ToString());
                    }
                    if (domainitemtype == DomainItemType.Value)
                    {
                        for (int i = 0; i < codecount; i++)
                            domain_list.Add(codedvaluedomain.get_Value(i).ToString());
                    }
                    if (domainitemtype == DomainItemType.Combined)
                    {
                        string name = String.Empty;
                        string value = String.Empty;
                        for (int i = 0; i < codecount; i++)
                        {
                            name = codedvaluedomain.get_Name(i).ToString();
                            value = codedvaluedomain.get_Value(i).ToString();
                            domain_list.Add(name + " (" + value + ")");
                        }
                    }
                }
            }

            return domain_list;

        }

        /// <summary>Returns the index of a particular field given the Name or Alias of a given field.</summary>
        /// <param name="o">Object that could either be a layer, featurelayer, featureclass, feature, row, table, standalone table or tablefields</param>
        /// <param name="FieldName"> Field name to search</param>
        /// <returns>Integer denoting the field index</returns>
        public int FindField(object o, string FieldName)
        {
            try
            {
                if (o is ILayer)
                {
                    ILayer layer = (ILayer)o;
                    if (!(layer == null))
                    {
                        IFeatureLayer featurelayer = (IFeatureLayer)layer;
                        if (!(featurelayer == null))
                        {
                            int i = featurelayer.FeatureClass.Fields.FindField(FieldName);
                            if (i > -1)
                                return i;
                            else
                            {
                                for (int j = 0; j < featurelayer.FeatureClass.Fields.FieldCount; j++)
                                {
                                    if (FieldName.CompareTo(featurelayer.FeatureClass.Fields.get_Field(j).AliasName) == 0)
                                        return j;
                                }
                            }
                        }
                    }
                }
                if (o is IFeatureLayer)
                {
                    IFeatureLayer featurelayer = (IFeatureLayer)o;
                    if (!(featurelayer == null))
                    {
                        int i = featurelayer.FeatureClass.Fields.FindField(FieldName);
                        if (i > -1)
                            return i;
                        else
                        {
                            for (int j = 0; j < featurelayer.FeatureClass.Fields.FieldCount; j++)
                            {
                                if (FieldName.CompareTo(featurelayer.FeatureClass.Fields.get_Field(j).AliasName) == 0)
                                    return j;
                            }
                        }
                    }
                }
                if (o is IFeatureClass)
                {
                    IFeatureClass featureclass = (IFeatureClass)o;
                    if (!(featureclass == null))
                    {
                        int i = featureclass.Fields.FindField(FieldName);
                        if (i > -1)
                            return i;
                        else
                        {
                            for (int j = 0; j < featureclass.Fields.FieldCount; j++)
                            {
                                if (FieldName.CompareTo(featureclass.Fields.get_Field(j).AliasName) == 0)
                                    return j;
                            }
                        }
                    }
                }
                if (o is IFeature)
                {
                    IFeature feature = (IFeature)o;
                    if (!(feature == null))
                    {
                        int i = feature.Fields.FindField(FieldName);
                        if (i > -1)
                            return i;
                        else
                        {
                            for (int j = 0; j < feature.Fields.FieldCount; j++)
                            {
                                if (FieldName.CompareTo(feature.Fields.get_Field(j).AliasName) == 0)
                                    return j;
                            }
                        }
                    }
                }
                if (o is IRow)
                {
                    IRow row = (IRow)o;
                    if (!(row == null))
                    {
                        int i = row.Fields.FindField(FieldName);
                        if (i > -1)
                            return i;
                        else
                        {
                            for (int j = 0; j < row.Fields.FieldCount; j++)
                            {
                                if (FieldName.CompareTo(row.Fields.get_Field(j).AliasName) == 0)
                                    return j;
                            }
                        }
                    }
                }
                if (o is ITable)
                {
                    ITable table = (ITable)o;
                    if (!(table == null))
                    {
                        int i = table.FindField(FieldName);
                        if (i > -1)
                            return i;
                        else
                        {
                            for (int j = 0; j < table.Fields.FieldCount; j++)
                            {
                                if (FieldName.CompareTo(table.Fields.get_Field(j).AliasName) == 0)
                                    return j;
                            }
                        }
                    }
                }
                if (o is IStandaloneTable)
                {
                    IStandaloneTable standalonetable = (IStandaloneTable)o;
                    int i = standalonetable.Table.FindField(FieldName);
                    if (i > -1)
                        return i;
                    else
                    {
                        for (int j = 0; j < standalonetable.Table.Fields.FieldCount; j++)
                        {
                            if (FieldName.CompareTo(standalonetable.Table.Fields.get_Field(j).AliasName) == 0)
                                return j;
                        }
                    }
                }
                if (o is ITableFields)
                {
                    ITableFields tablefields = (ITableFields)o;
                    int i = tablefields.FindField(FieldName);
                    if (i > -1)
                        return i;
                    else
                    {
                        for (int j = 0; j < tablefields.FieldCount; j++)
                        {
                            if (FieldName.CompareTo(tablefields.get_FieldInfo(j).Alias) == 0)
                                return j;
                        }
                    }
                }
                if (o is IGeoFeatureLayer)
                {
                    IGeoFeatureLayer geofeaturelayer = (IGeoFeatureLayer)o;
                    if (!(geofeaturelayer == null))
                    {
                        IFeatureLayer featurelayer = (IFeatureLayer)geofeaturelayer;
                        if (!(featurelayer == null))
                        {
                            int i = featurelayer.FeatureClass.Fields.FindField(FieldName);
                            if (i > -1)
                                return i;
                            else
                            {
                                for (int j = 0; j < featurelayer.FeatureClass.Fields.FieldCount; j++)
                                {
                                    if (FieldName.CompareTo(featurelayer.FeatureClass.Fields.get_Field(j).AliasName) == 0)
                                        return j;
                                }
                            }
                        }
                    }
                }
                if (o is IFields)
                {
                    IFields fields = (IFields)o;
                    if (!(fields == null))
                    {
                        int i = fields.FindField(FieldName);
                        if (i > -1)
                            return i;
                        else
                        {
                            for (int j = 0; j < fields.FieldCount; j++)
                            {
                                if (FieldName.CompareTo(fields.get_Field(j).AliasName) == 0)
                                    return j;
                            }
                        }
                    }
                }

                return -1;
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "Release COM Objects", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return -1;
        }

        /// <summary>
        /// Returns the current date in 'MM/DD/YYYY' format
        /// </summary>
        /// <returns>string</returns>
        public string GetCurrrentDate()
        {
            DateTime datetime = DateTime.Now;
            string year = datetime.Year.ToString();
            string month = datetime.Month.ToString();
            string day = datetime.Day.ToString();
            return month + "/" + day + "/" + year;
        }

        /// <summary>
        /// Will write a value when given: Feature, field_index, valueobject
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="field_index"></param>
        /// <param name="valueobject"></param>
        public void WriteValue(IEditor3 editor, IFeature feature, int field_index, object valueobject)
        {
            string valueobjectstring = valueobject.ToString();
            if (field_index > -1)
            {
                IField field = feature.Fields.get_Field(field_index);
                editor.StartOperation();
                switch (field.Type)
                {
                    case esriFieldType.esriFieldTypeDate:
                        DateTime datetime = Convert.ToDateTime(valueobjectstring);
                        string year = datetime.Year.ToString();
                        string month = datetime.Month.ToString();
                        string day = datetime.Day.ToString();
                        valueobjectstring = month + "/" + day + "/" + year;
                        feature.set_Value(field_index, valueobjectstring);
                        break;
                    case esriFieldType.esriFieldTypeDouble:
                        double valueobjectdouble = Convert.ToDouble(valueobjectstring);
                        System.Math.Round(valueobjectdouble, 8);
                        feature.set_Value(field_index, valueobjectdouble);
                        break;
                    case esriFieldType.esriFieldTypeGeometry:
                        break;
                    case esriFieldType.esriFieldTypeGlobalID:
                        if (field.Length > valueobjectstring.Length)
                            feature.set_Value(field_index, valueobject);
                        break;
                    case esriFieldType.esriFieldTypeGUID:
                        if (field.Length > valueobjectstring.Length)
                            feature.set_Value(field_index, valueobject);
                        break;
                    case esriFieldType.esriFieldTypeInteger:
                        feature.set_Value(field_index, Convert.ToInt64(valueobject));
                        break;
                    case esriFieldType.esriFieldTypeOID:
                        if (field.Length > valueobjectstring.Length)
                            feature.set_Value(field_index, Convert.ToInt64(valueobject));
                        break;
                    case esriFieldType.esriFieldTypeRaster:
                        break;
                    case esriFieldType.esriFieldTypeSingle:
                        if (field.Length > valueobjectstring.Length)
                            feature.set_Value(field_index, Convert.ToSingle(valueobject));
                        break;
                    case esriFieldType.esriFieldTypeSmallInteger:
                        if (field.Length > valueobjectstring.Length)
                            feature.set_Value(field_index, Convert.ToInt16(valueobject));
                        break;
                    case esriFieldType.esriFieldTypeString:
                        if (field.Length < valueobjectstring.Length) valueobjectstring = valueobjectstring.Substring(0, field.Length - 1);
                        feature.set_Value(field_index, valueobject.ToString());
                        break;
                    case esriFieldType.esriFieldTypeXML:
                        break;
                }
                editor.StopOperation("Status updated!" + feature.OID);
                feature.Store();
            }
        }

        ///<summary> The fields of a specified layer </summary>
        /// <param name="pLayer"> The layer from which a list of fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all fields in a given layer </returns>
        public ArrayList AllFields(ILayer pLayer)
        {
            ArrayList pList = new ArrayList();
            if (pLayer == null)
                return pList;
            try
            {
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        pList.Add(pFields.get_Field(i).AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Fields - By Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The fields of a specified layer </summary>
        /// <param name="LayerName"> The layer from which a list of fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all fields in a given layer </returns>
        public ArrayList AllFields(string LayerName)
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                ILayer pLayer = Layer(LayerName);
                if (pLayer == null)
                    return pList;
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        pList.Add(pFields.get_Field(i).AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Fields - By Layer Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The text fields of a specified layer </summary>
        /// <param name="pLayer"> The layer from which a list of text fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all text fields in a given layer </returns>
        public ArrayList TextFields(ILayer pLayer)
        {
            ArrayList pList = new ArrayList();
            if (pLayer == null)
                return pList;
            try
            {
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        IField pField = pFields.get_Field(i);
                        if (pField.Type == esriFieldType.esriFieldTypeString)
                            pList.Add(pField.AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Text Fields - By Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The text fields of a specified layer </summary>
        /// <param name="LayerName"> The layer from which a list of text fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all text fields in a given layer </returns>
        public ArrayList TextFields(string LayerName)
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                ILayer pLayer = Layer(LayerName);
                if (pLayer == null)
                    return pList;
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        IField pField = pFields.get_Field(i);
                        if (pField.Type == esriFieldType.esriFieldTypeString)
                            pList.Add(pField.AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Text Fields - By Layer Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The number fields of a specified layer </summary>
        /// <param name="pLayer"> The layer from which a list of number fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all number fields in a given layer </returns>
        public ArrayList NumberFields(ILayer pLayer)
        {
            ArrayList pList = new ArrayList();
            if (pLayer == null)
                return pList;
            try
            {
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        IField pField = pFields.get_Field(i);
                        if (pField.Type == esriFieldType.esriFieldTypeDouble || pField.Type == esriFieldType.esriFieldTypeInteger || pField.Type == esriFieldType.esriFieldTypeSingle || pField.Type == esriFieldType.esriFieldTypeSmallInteger)
                            pList.Add(pField.AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Number Fields - By Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The number fields of a specified layer </summary>
        /// <param name="LayerName"> The layer from which a list of number fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all number fields in a given layer </returns>
        public ArrayList NumberFields(string LayerName)
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                ILayer pLayer = Layer(LayerName);
                if (pLayer == null)
                    return pList;
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        IField pField = pFields.get_Field(i);
                        if (pField.Type == esriFieldType.esriFieldTypeDouble || pField.Type == esriFieldType.esriFieldTypeInteger || pField.Type == esriFieldType.esriFieldTypeSingle || pField.Type == esriFieldType.esriFieldTypeSmallInteger)
                            pList.Add(pField.AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Number Fields - By Layer Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The date fields of a specified layer </summary>
        /// <param name="pLayer"> The layer from which a list of date fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all data fields in a given layer </returns>
        public ArrayList DateFields(ILayer pLayer)
        {
            ArrayList pList = new ArrayList();
            if (pLayer == null)
                return pList;
            try
            {
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        IField pField = pFields.get_Field(i);
                        if (pField.Type == esriFieldType.esriFieldTypeDate)
                            pList.Add(pField.AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Date Fields - By Layer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        ///<summary> The date fields of a specified layer </summary>
        /// <param name="LayerName"> The layer from which a list of date fields is to be fetched </param>
        ///<returns> Function to retrieve a list of all data fields in a given layer </returns>
        public ArrayList DateFields(string LayerName)
        {
            ArrayList pList = new ArrayList();
            if (FocusMap == null)
                return pList;
            if (FocusMap.LayerCount == 0)
                return pList;

            try
            {
                ILayer pLayer = Layer(LayerName);
                if (pLayer == null)
                    return pList;
                if (pLayer is IGeoFeatureLayer)
                {
                    IFeatureLayer pFlayer = (IFeatureLayer)pLayer;
                    IDisplayTable pDisplayTable = (IDisplayTable)pFlayer;
                    IFields pFields = pDisplayTable.DisplayTable.Fields;
                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        IField pField = pFields.get_Field(i);
                        if (pField.Type == esriFieldType.esriFieldTypeDate)
                            pList.Add(pField.AliasName);
                    }
                }
            }
            catch (Exception ex)
            {
                if (SupressMessaging == false)
                    MessageBox.Show(ex.Message, "All Date Fields - By Layer Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return pList;
        }

        #endregion

        #region Destructor
        ///<summary> Destroy </summary>
        ~Utilities()
        {
        }

        #endregion
    }
}
