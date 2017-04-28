namespace PathFindingTest1
{
    partial class frmPathfinding
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmPathfinding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Name = "frmPathfinding";
            this.Text = "PathFinding";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmPathfinding_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmPathfinding_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmPathfinding_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmPathfinding_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmPathfinding_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmPathfinding_MouseUp);
            this.Resize += new System.EventHandler(this.frmPathfinding_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

