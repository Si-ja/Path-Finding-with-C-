namespace PathFinder
{
    partial class PathFinder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathFinder));
            this.btn_Generate = new System.Windows.Forms.Button();
            this.cBox_Objects = new System.Windows.Forms.ComboBox();
            this.btn_Find = new System.Windows.Forms.Button();
            this.lbl_ChooseMembers = new System.Windows.Forms.Label();
            this.lbl_DescriptionInfo = new System.Windows.Forms.Label();
            this.lbl_DescriptionIntro = new System.Windows.Forms.Label();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.btn_ClearPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_Generate
            // 
            this.btn_Generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Generate.Location = new System.Drawing.Point(426, 246);
            this.btn_Generate.Name = "btn_Generate";
            this.btn_Generate.Size = new System.Drawing.Size(95, 23);
            this.btn_Generate.TabIndex = 0;
            this.btn_Generate.Text = "Generate Map";
            this.btn_Generate.UseVisualStyleBackColor = true;
            this.btn_Generate.Click += new System.EventHandler(this.btn_Generate_Click);
            // 
            // cBox_Objects
            // 
            this.cBox_Objects.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBox_Objects.FormattingEnabled = true;
            this.cBox_Objects.Items.AddRange(new object[] {
            "Start",
            "Finish"});
            this.cBox_Objects.Location = new System.Drawing.Point(426, 302);
            this.cBox_Objects.Name = "cBox_Objects";
            this.cBox_Objects.Size = new System.Drawing.Size(95, 23);
            this.cBox_Objects.TabIndex = 1;
            // 
            // btn_Find
            // 
            this.btn_Find.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Find.Location = new System.Drawing.Point(426, 342);
            this.btn_Find.Name = "btn_Find";
            this.btn_Find.Size = new System.Drawing.Size(95, 23);
            this.btn_Find.TabIndex = 2;
            this.btn_Find.Text = "Find Path";
            this.btn_Find.UseVisualStyleBackColor = true;
            this.btn_Find.Click += new System.EventHandler(this.btn_Find_Click);
            // 
            // lbl_ChooseMembers
            // 
            this.lbl_ChooseMembers.AutoSize = true;
            this.lbl_ChooseMembers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ChooseMembers.Location = new System.Drawing.Point(424, 286);
            this.lbl_ChooseMembers.Name = "lbl_ChooseMembers";
            this.lbl_ChooseMembers.Size = new System.Drawing.Size(94, 13);
            this.lbl_ChooseMembers.TabIndex = 3;
            this.lbl_ChooseMembers.Text = "Chose Path Points";
            // 
            // lbl_DescriptionInfo
            // 
            this.lbl_DescriptionInfo.AutoSize = true;
            this.lbl_DescriptionInfo.BackColor = System.Drawing.Color.OldLace;
            this.lbl_DescriptionInfo.Location = new System.Drawing.Point(424, 46);
            this.lbl_DescriptionInfo.Name = "lbl_DescriptionInfo";
            this.lbl_DescriptionInfo.Size = new System.Drawing.Size(120, 143);
            this.lbl_DescriptionInfo.TabIndex = 5;
            this.lbl_DescriptionInfo.Text = resources.GetString("lbl_DescriptionInfo.Text");
            // 
            // lbl_DescriptionIntro
            // 
            this.lbl_DescriptionIntro.AutoSize = true;
            this.lbl_DescriptionIntro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DescriptionIntro.Location = new System.Drawing.Point(423, 40);
            this.lbl_DescriptionIntro.Name = "lbl_DescriptionIntro";
            this.lbl_DescriptionIntro.Size = new System.Drawing.Size(87, 16);
            this.lbl_DescriptionIntro.TabIndex = 6;
            this.lbl_DescriptionIntro.Text = "Description";
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Status.Location = new System.Drawing.Point(428, 374);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(90, 15);
            this.lbl_Status.TabIndex = 7;
            this.lbl_Status.Text = "Status: Nothing";
            // 
            // btn_ClearPath
            // 
            this.btn_ClearPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btn_ClearPath.Location = new System.Drawing.Point(426, 402);
            this.btn_ClearPath.Name = "btn_ClearPath";
            this.btn_ClearPath.Size = new System.Drawing.Size(95, 23);
            this.btn_ClearPath.TabIndex = 8;
            this.btn_ClearPath.Text = "Clear Path";
            this.btn_ClearPath.UseVisualStyleBackColor = true;
            this.btn_ClearPath.Click += new System.EventHandler(this.btn_ClearPath_Click);
            // 
            // PathFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(544, 437);
            this.Controls.Add(this.btn_ClearPath);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.lbl_DescriptionIntro);
            this.Controls.Add(this.lbl_DescriptionInfo);
            this.Controls.Add(this.lbl_ChooseMembers);
            this.Controls.Add(this.btn_Find);
            this.Controls.Add(this.cBox_Objects);
            this.Controls.Add(this.btn_Generate);
            this.Name = "PathFinder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Path Finder";
            this.Load += new System.EventHandler(this.PathFinder_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PathFinder_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Generate;
        private System.Windows.Forms.ComboBox cBox_Objects;
        private System.Windows.Forms.Button btn_Find;
        private System.Windows.Forms.Label lbl_ChooseMembers;
        private System.Windows.Forms.Label lbl_DescriptionInfo;
        private System.Windows.Forms.Label lbl_DescriptionIntro;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.Button btn_ClearPath;
    }
}

