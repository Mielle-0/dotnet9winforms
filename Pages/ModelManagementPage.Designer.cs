namespace it13Project.Pages
{
    partial class ModelManagementPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "ModelManagementPage";
            this.Padding = new Padding(20);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);

            var lblTitle = new Label { Text = "Model Management", Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold), Dock = DockStyle.Top, Height = 40 };

            var gbxCurrentModel = new GroupBox { Text = "Current Model", Dock = DockStyle.Top, Height = 150, Padding = new Padding(10) };
            var lblModelInfo = new Label { Text = "Version: 2.1.3\nDeployed: 2025-08-15\nAccuracy: 92.5%", Dock = DockStyle.Fill };
            gbxCurrentModel.Controls.Add(lblModelInfo);

            var gbxActions = new GroupBox { Text = "Actions", Dock = DockStyle.Top, Height = 100, Padding = new Padding(10) };
            var flowActions = new FlowLayoutPanel { Dock = DockStyle.Fill };
            flowActions.Controls.Add(new Button { Text = "Upload New Model Version", AutoSize = true });
            flowActions.Controls.Add(new Button { Text = "Test Model Accuracy", AutoSize = true });
            gbxActions.Controls.Add(flowActions);

            this.Controls.Add(gbxActions);
            this.Controls.Add(gbxCurrentModel);
            this.Controls.Add(lblTitle);
            this.ResumeLayout(false);
        }
        #endregion
    }
}
