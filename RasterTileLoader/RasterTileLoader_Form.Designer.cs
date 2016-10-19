namespace RasterTileLoader
{
    partial class RasterTileLoader_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboTileIndex = new System.Windows.Forms.ComboBox();
            this.cboFieldName = new System.Windows.Forms.ComboBox();
            this.txbPrefix = new System.Windows.Forms.TextBox();
            this.txbSuffix = new System.Windows.Forms.TextBox();
            this.txbRasterWorkspace = new System.Windows.Forms.TextBox();
            this.btnInit = new System.Windows.Forms.Button();
            this.btnLoadRaster = new System.Windows.Forms.Button();
            this.rbtnAll = new System.Windows.Forms.RadioButton();
            this.rbtnSelected = new System.Windows.Forms.RadioButton();
            this.lblTileIndex = new System.Windows.Forms.Label();
            this.lblTileNameField = new System.Windows.Forms.Label();
            this.lblExtension = new System.Windows.Forms.Label();
            this.lblSuffix = new System.Windows.Forms.Label();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.lblRasterWorkspace = new System.Windows.Forms.Label();
            this.lblLoadingRasterMethod = new System.Windows.Forms.Label();
            this.btnSelectWorkspace = new System.Windows.Forms.Button();
            this.cboExtension = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboTileIndex
            // 
            this.cboTileIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTileIndex.FormattingEnabled = true;
            this.cboTileIndex.Location = new System.Drawing.Point(12, 68);
            this.cboTileIndex.Name = "cboTileIndex";
            this.cboTileIndex.Size = new System.Drawing.Size(150, 21);
            this.cboTileIndex.TabIndex = 0;
            this.cboTileIndex.SelectedIndexChanged += new System.EventHandler(this.cboTileIndex_SelectedIndexChanged);
            // 
            // cboFieldName
            // 
            this.cboFieldName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFieldName.FormattingEnabled = true;
            this.cboFieldName.Location = new System.Drawing.Point(176, 68);
            this.cboFieldName.Name = "cboFieldName";
            this.cboFieldName.Size = new System.Drawing.Size(148, 21);
            this.cboFieldName.TabIndex = 1;
            // 
            // txbPrefix
            // 
            this.txbPrefix.Location = new System.Drawing.Point(12, 128);
            this.txbPrefix.Name = "txbPrefix";
            this.txbPrefix.Size = new System.Drawing.Size(100, 22);
            this.txbPrefix.TabIndex = 3;
            // 
            // txbSuffix
            // 
            this.txbSuffix.Location = new System.Drawing.Point(118, 128);
            this.txbSuffix.Name = "txbSuffix";
            this.txbSuffix.Size = new System.Drawing.Size(100, 22);
            this.txbSuffix.TabIndex = 4;
            // 
            // txbRasterWorkspace
            // 
            this.txbRasterWorkspace.Location = new System.Drawing.Point(12, 183);
            this.txbRasterWorkspace.Name = "txbRasterWorkspace";
            this.txbRasterWorkspace.ReadOnly = true;
            this.txbRasterWorkspace.Size = new System.Drawing.Size(282, 22);
            this.txbRasterWorkspace.TabIndex = 5;
            // 
            // btnInit
            // 
            this.btnInit.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInit.Location = new System.Drawing.Point(12, 12);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 6;
            this.btnInit.Text = "Initialize";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnLoadRaster
            // 
            this.btnLoadRaster.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadRaster.Location = new System.Drawing.Point(249, 230);
            this.btnLoadRaster.Name = "btnLoadRaster";
            this.btnLoadRaster.Size = new System.Drawing.Size(75, 23);
            this.btnLoadRaster.TabIndex = 7;
            this.btnLoadRaster.Text = "LoadRaster";
            this.btnLoadRaster.UseVisualStyleBackColor = true;
            this.btnLoadRaster.Click += new System.EventHandler(this.btnLoadRaster_Click);
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.Checked = true;
            this.rbtnAll.Location = new System.Drawing.Point(12, 236);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(38, 17);
            this.rbtnAll.TabIndex = 9;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "All";
            this.rbtnAll.UseVisualStyleBackColor = true;
            // 
            // rbtnSelected
            // 
            this.rbtnSelected.AutoSize = true;
            this.rbtnSelected.Location = new System.Drawing.Point(70, 236);
            this.rbtnSelected.Name = "rbtnSelected";
            this.rbtnSelected.Size = new System.Drawing.Size(68, 17);
            this.rbtnSelected.TabIndex = 10;
            this.rbtnSelected.TabStop = true;
            this.rbtnSelected.Text = "Selected";
            this.rbtnSelected.UseVisualStyleBackColor = true;
            // 
            // lblTileIndex
            // 
            this.lblTileIndex.AutoSize = true;
            this.lblTileIndex.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTileIndex.Location = new System.Drawing.Point(12, 46);
            this.lblTileIndex.Name = "lblTileIndex";
            this.lblTileIndex.Size = new System.Drawing.Size(56, 13);
            this.lblTileIndex.TabIndex = 11;
            this.lblTileIndex.Text = "Tile Index";
            // 
            // lblTileNameField
            // 
            this.lblTileNameField.AutoSize = true;
            this.lblTileNameField.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTileNameField.Location = new System.Drawing.Point(202, 46);
            this.lblTileNameField.Name = "lblTileNameField";
            this.lblTileNameField.Size = new System.Drawing.Size(86, 13);
            this.lblTileNameField.TabIndex = 12;
            this.lblTileNameField.Text = "Tile Name Field";
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtension.Location = new System.Drawing.Point(221, 103);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(56, 13);
            this.lblExtension.TabIndex = 13;
            this.lblExtension.Text = "Extension";
            // 
            // lblSuffix
            // 
            this.lblSuffix.AutoSize = true;
            this.lblSuffix.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuffix.Location = new System.Drawing.Point(115, 103);
            this.lblSuffix.Name = "lblSuffix";
            this.lblSuffix.Size = new System.Drawing.Size(36, 13);
            this.lblSuffix.TabIndex = 14;
            this.lblSuffix.Text = "Suffix";
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrefix.Location = new System.Drawing.Point(9, 103);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(36, 13);
            this.lblPrefix.TabIndex = 15;
            this.lblPrefix.Text = "Prefix";
            // 
            // lblRasterWorkspace
            // 
            this.lblRasterWorkspace.AutoSize = true;
            this.lblRasterWorkspace.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRasterWorkspace.Location = new System.Drawing.Point(12, 162);
            this.lblRasterWorkspace.Name = "lblRasterWorkspace";
            this.lblRasterWorkspace.Size = new System.Drawing.Size(99, 13);
            this.lblRasterWorkspace.TabIndex = 16;
            this.lblRasterWorkspace.Text = "Raster Workspace";
            // 
            // lblLoadingRasterMethod
            // 
            this.lblLoadingRasterMethod.AutoSize = true;
            this.lblLoadingRasterMethod.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadingRasterMethod.Location = new System.Drawing.Point(12, 220);
            this.lblLoadingRasterMethod.Name = "lblLoadingRasterMethod";
            this.lblLoadingRasterMethod.Size = new System.Drawing.Size(126, 13);
            this.lblLoadingRasterMethod.TabIndex = 17;
            this.lblLoadingRasterMethod.Text = "Raster Loading Method";
            // 
            // btnSelectWorkspace
            // 
            this.btnSelectWorkspace.BackgroundImage = global::RasterTileLoader.Properties.Resources.searches_folder;
            this.btnSelectWorkspace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSelectWorkspace.Location = new System.Drawing.Point(300, 181);
            this.btnSelectWorkspace.Name = "btnSelectWorkspace";
            this.btnSelectWorkspace.Size = new System.Drawing.Size(24, 24);
            this.btnSelectWorkspace.TabIndex = 18;
            this.btnSelectWorkspace.UseVisualStyleBackColor = true;
            this.btnSelectWorkspace.Click += new System.EventHandler(this.btnSelectWorkspace_Click);
            // 
            // cboExtension
            // 
            this.cboExtension.FormattingEnabled = true;
            this.cboExtension.Location = new System.Drawing.Point(225, 128);
            this.cboExtension.Name = "cboExtension";
            this.cboExtension.Size = new System.Drawing.Size(99, 21);
            this.cboExtension.TabIndex = 19;
            this.cboExtension.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboExtension_KeyPress);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(249, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // RasterTileLoader_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(335, 265);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cboExtension);
            this.Controls.Add(this.btnSelectWorkspace);
            this.Controls.Add(this.lblLoadingRasterMethod);
            this.Controls.Add(this.lblRasterWorkspace);
            this.Controls.Add(this.lblPrefix);
            this.Controls.Add(this.lblSuffix);
            this.Controls.Add(this.lblExtension);
            this.Controls.Add(this.lblTileNameField);
            this.Controls.Add(this.lblTileIndex);
            this.Controls.Add(this.rbtnSelected);
            this.Controls.Add(this.rbtnAll);
            this.Controls.Add(this.btnLoadRaster);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.txbRasterWorkspace);
            this.Controls.Add(this.txbSuffix);
            this.Controls.Add(this.txbPrefix);
            this.Controls.Add(this.cboFieldName);
            this.Controls.Add(this.cboTileIndex);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RasterTileLoader_Form";
            this.Text = "Raster Tile Loader";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboTileIndex;
        private System.Windows.Forms.ComboBox cboFieldName;
        private System.Windows.Forms.TextBox txbPrefix;
        private System.Windows.Forms.TextBox txbSuffix;
        private System.Windows.Forms.TextBox txbRasterWorkspace;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Button btnLoadRaster;
        private System.Windows.Forms.RadioButton rbtnAll;
        private System.Windows.Forms.RadioButton rbtnSelected;
        private System.Windows.Forms.Label lblTileIndex;
        private System.Windows.Forms.Label lblTileNameField;
        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.Label lblSuffix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.Label lblRasterWorkspace;
        private System.Windows.Forms.Label lblLoadingRasterMethod;
        private System.Windows.Forms.Button btnSelectWorkspace;
        private System.Windows.Forms.ComboBox cboExtension;
        private System.Windows.Forms.Button btnClose;
    }
}