using Krypton.Toolkit;
using it13Project.UI;
namespace it13Project.Forms;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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


    private void InitializeComponent()
    {
        lblUsername = new Label();
        lblPassword = new Label();
        txtUsername = new TextBox();
        txtPassword = new TextBox();
        btnLogin = new Button();
        btnClear = new Button();
        lblTitle = new Label();
        lblFooter = new Label();
        pictureLogo = new PictureBox();
        SuspendLayout();

        // === Form ===
        AutoScaleDimensions = new SizeF(7F, 16F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(400, 450);
        BackColor = ThemeColors.MenuBackground;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Video Game CRM - Login";

        // Horizontal centering helper
        int CenterX(int controlWidth) => (ClientSize.Width - controlWidth) / 2;

        // === Logo ===
        pictureLogo.Image = Properties.Resources.LogoPlaceholder;
        pictureLogo.SizeMode = PictureBoxSizeMode.Zoom;
        pictureLogo.Size = new Size(100, 100);
        pictureLogo.Location = new Point(CenterX(pictureLogo.Width), 20);
        pictureLogo.BackColor = Color.Transparent;

        // === Title ===
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Gadugi", 18F, FontStyle.Bold);
        lblTitle.ForeColor = ThemeColors.AccentPrimary;
        lblTitle.Text = "🎮 Video Game CRM";
        lblTitle.Location = new Point(CenterX(lblTitle.PreferredWidth), pictureLogo.Bottom + 10);

        // === Username Label ===
        lblUsername.AutoSize = true;
        lblUsername.Font = new Font("Gadugi", 12F);
        lblUsername.ForeColor = ThemeColors.TextColor;
        lblUsername.Text = "Username:";
        lblUsername.Location = new Point(50, lblTitle.Bottom + 30);

        // === Username Input ===
        txtUsername.Font = new Font("Gadugi", 12F);
        txtUsername.Size = new Size(300, 29);
        txtUsername.BackColor = ThemeColors.MenuHover;
        txtUsername.ForeColor = ThemeColors.TextColor;
        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Location = new Point(CenterX(txtUsername.Width), lblUsername.Bottom + 5);

        // === Password Label ===
        lblPassword.AutoSize = true;
        lblPassword.Font = new Font("Gadugi", 12F);
        lblPassword.ForeColor = ThemeColors.TextColor;
        lblPassword.Text = "Password:";
        lblPassword.Location = new Point(50, txtUsername.Bottom + 15);

        // === Password Input ===
        txtPassword.Font = new Font("Gadugi", 12F);
        txtPassword.Size = new Size(300, 29);
        txtPassword.BackColor = ThemeColors.MenuHover;
        txtPassword.ForeColor = ThemeColors.TextColor;
        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.PasswordChar = '*';
        txtPassword.Location = new Point(CenterX(txtPassword.Width), lblPassword.Bottom + 5);

        // === Login Button ===
        btnLogin.BackColor = ThemeColors.AccentPrimary;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.Font = new Font("Gadugi", 12F, FontStyle.Bold);
        btnLogin.ForeColor = ThemeColors.TextColor;
        btnLogin.Size = new Size(120, 40);
        btnLogin.Text = "Login";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Cursor = Cursors.Hand;
        btnLogin.Location = new Point(CenterX(btnLogin.Width * 2 + 20), txtPassword.Bottom + 25);
        btnLogin.Click += btnLogin_Click;

        // === Clear Button ===
        btnClear.BackColor = ThemeColors.AccentSecondary;
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.FlatAppearance.BorderSize = 0;
        btnClear.Font = new Font("Gadugi", 12F, FontStyle.Bold);
        btnClear.ForeColor = ThemeColors.TextColor;
        btnClear.Size = new Size(120, 40);
        btnClear.Text = "Clear";
        btnClear.UseVisualStyleBackColor = false;
        btnClear.Cursor = Cursors.Hand;
        btnClear.Location = new Point(btnLogin.Right + 20, btnLogin.Top);
        btnClear.Click += btnClear_Click;

        // === Footer ===
        lblFooter.AutoSize = true;
        lblFooter.Font = new Font("Gadugi", 8F, FontStyle.Italic);
        lblFooter.ForeColor = Color.Gray;
        lblFooter.Text = "© 2025 VideoGameCRM - Powered by Sentiment AI";
        lblFooter.Location = new Point(CenterX(lblFooter.PreferredWidth), btnClear.Bottom + 30);

        // === Add Controls ===
        Controls.Add(pictureLogo);
        Controls.Add(lblTitle);
        Controls.Add(lblFooter);
        Controls.Add(btnClear);
        Controls.Add(btnLogin);
        Controls.Add(txtPassword);
        Controls.Add(txtUsername);
        Controls.Add(lblPassword);
        Controls.Add(lblUsername);

        // === Tab Order ===
        txtUsername.TabIndex = 0;
        txtPassword.TabIndex = 1;
        btnLogin.TabIndex = 2;
        btnClear.TabIndex = 3;

        ResumeLayout(false);
        PerformLayout();
    }



    #endregion

    // Component declarations
    private Label lblUsername;
    private Label lblPassword;
    private TextBox txtUsername;
    private TextBox txtPassword;
    private Button btnLogin;
    private Button btnClear;
    private Label lblTitle;
    private Label lblFooter;
    private PictureBox pictureLogo;


}
