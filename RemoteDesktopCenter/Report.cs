using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteDesktopCenter
{
    public partial class Report : Form
    {
        #region GlobalVariable
        private DataTable dtStatic = new DataTable();
        #endregion
        public Report()
        {
            InitializeComponent();
        }
        private void Report_Load(object sender, EventArgs e)
        {
            setDefault();
        }
        private void setDefault()
        {
            #region Variable
            var clsSQL = new clsSQL();
            #endregion
            #region Procedure
            dtFrom.Value = new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, DateTime.Now.AddDays(-1).Day, 0, 0, 0);
            dtTo.Checked = false;
            lblFooterDetail.Text = string.Format("Server : {0}", clsSQL.getAppSettingServerName(clsGlobal.cs));
            #endregion
        }
        private void btSearch_Click(object sender, EventArgs e)
        {
            #region Variable
            var clsSQL = new clsSQL(clsGlobal.dbType, clsGlobal.cs);
            var strSQL = new StringBuilder();
            var dt = new DataTable();
            #endregion
            #region Procedure
            #region SQLQuery
            strSQL.Append("");
            strSQL.Append("SELECT ");
            strSQL.Append("ServerName, Domain, Username, DATE_FORMAT(ConnectWhen,'%d/%m/%Y %T') ConnectWhen,TIMESTAMPDIFF(SECOND, ConnectWhen, DisconnectWhen) UsageSecond, TIMESTAMPDIFF(MINUTE, ConnectWhen, DisconnectWhen) UsageMinute ");
            strSQL.Append("FROM ");
            strSQL.Append("sessionlog ");
            strSQL.Append("WHERE ");
            if (dtFrom.Checked)
            {
                strSQL.Append("ConnectWhen>='"+dtFrom.Value.ToString("yyyy-MM-dd HH:mm")+ "' AND ");
            }
            if (dtTo.Checked)
            {
                strSQL.Append("ConnectWhen<='" + dtTo.Value.ToString("yyyy-MM-dd HH:mm") + "' AND ");
            }
            strSQL.Append("StatusFlag = 'A' AND NOT DisconnectWhen IS NULL;");
            #endregion
            dt = clsSQL.Bind(strSQL.ToString());
            if(dt!=null && dt.Rows.Count > 0)
            {
                gvDefault.Visible = true;
                gvDefault.DataSource = dt;
                dtStatic = dt;
                btExport.Enabled = true;
            }
            else
            {
                dtStatic = null; btExport.Enabled = false;
                gvDefault.Visible = false;
                MessageBox.Show("ไม่พบข้อมูลที่ต้องการ", "Not found.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion
        }
        private void btMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btExport_Click(object sender, EventArgs e)
        {
            #region Variable
            var fileName = clsGlobal.ApplicationName+"-Export-"+DateTime.Now.ToString("yyyyMMddHHmm")+".xlsx";
            #endregion
            #region Procedure
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Export");
                if (dtStatic != null && dtStatic.Rows.Count > 0)
                {
                    #region HeaderBuilder
                    for (int c = 0; c < dtStatic.Columns.Count; c++)
                    {
                        worksheet.Cells[1, c + 1].Value = dtStatic.Columns[c].ColumnName;
                        #region Style
                        worksheet.Cells[1, c + 1].Style.Font.Bold = true;
                        worksheet.Cells[1, c + 1].Style.Font.Size = 12;
                        worksheet.Cells[1, c + 1].Style.Font.Name = "Tahoma";
                        worksheet.Cells[1, c + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[1, c + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#13A38D"));
                        worksheet.Cells[1, c + 1].Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFF"));
                        worksheet.Cells[1, c + 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        worksheet.Cells[1, c + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[1, c + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, c + 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, c + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, c + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[1, c + 1].Style.Border.Left.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
                        worksheet.Cells[1, c + 1].Style.Border.Top.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
                        worksheet.Cells[1, c + 1].Style.Border.Right.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
                        worksheet.Cells[1, c + 1].Style.Border.Bottom.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
                        #endregion
                    }
                    #endregion
                    #region ContentBuilder
                    for (int r = 0; r < dtStatic.Rows.Count; r++)
                    {
                        for (int c = 0; c < dtStatic.Columns.Count; c++)
                        {
                            worksheet.Cells[r + 2, c + 1].Value = dtStatic.Rows[r][c];
                            #region Style
                            worksheet.Cells[r + 2, c + 1].Style.Font.Bold = false;
                            worksheet.Cells[r + 2, c + 1].Style.Font.Size = 12;
                            worksheet.Cells[r + 2, c + 1].Style.Font.Name = "Tahoma";
                            worksheet.Cells[r + 2, c + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[r + 2, c + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFF"));
                            worksheet.Cells[r + 2, c + 1].Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#444"));
                            worksheet.Cells[r + 2, c + 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            worksheet.Cells[r + 2, c + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                            worksheet.Cells[r + 2, c + 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[r + 2, c + 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[r + 2, c + 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[r + 2, c + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[r + 2, c + 1].Style.Border.Left.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
                            worksheet.Cells[r + 2, c + 1].Style.Border.Top.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
                            worksheet.Cells[r + 2, c + 1].Style.Border.Right.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
                            worksheet.Cells[r + 2, c + 1].Style.Border.Bottom.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#DDD"));
                            #endregion
                        }
                    }
                    #endregion
                    #region ColumnResize
                    for (int c = 0; c < dtStatic.Columns.Count; c++)
                    {
                        worksheet.Column(c + 1).AutoFit();
                    }
                    #endregion
                    package.Workbook.Properties.Title = clsGlobal.ApplicationName;
                    try
                    {
                        FileInfo fi = new FileInfo(@"Export\" + fileName);
                        if (!Directory.Exists(@"Export\"))
                        {
                            Directory.CreateDirectory(@"Export\");
                        }
                        package.SaveAs(fi);
                        Process.Start(fi.FullName);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("เกิดข้อผิดพลาดขณะสร้างไฟล์" + Environment.NewLine + ex.Message, "Fail on create file.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            #endregion
        }
    }
}
