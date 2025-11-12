using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using it13Project.UI;

namespace it13Project.Pages
{
    public partial class AlertsNotificationsPage : UserControl
    {
        public AlertsNotificationsPage()
        {
            InitializeComponent();
            SetupAlertColors();
        }

        private void SetupAlertColors()
        {
            dgvAlerts.CellFormatting += (s, e) =>
            {
                if (dgvAlerts.Columns[e.ColumnIndex].Name == "Severity")
                {
                    if (e.Value != null)
                    {
                        string severity = e.Value.ToString();
                        switch (severity)
                        {
                            case "Critical":
                                dgvAlerts.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                                dgvAlerts.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                                break;
                            case "Warning":
                                dgvAlerts.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Khaki;
                                dgvAlerts.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                                break;
                            case "Info":
                                dgvAlerts.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                                dgvAlerts.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                                break;
                            default:
                                dgvAlerts.Rows[e.RowIndex].DefaultCellStyle.BackColor = ThemeColors.ContentBackground;
                                dgvAlerts.Rows[e.RowIndex].DefaultCellStyle.ForeColor = ThemeColors.TextColor;
                                break;
                        }
                    }
                }
            };
        }
    }
}
