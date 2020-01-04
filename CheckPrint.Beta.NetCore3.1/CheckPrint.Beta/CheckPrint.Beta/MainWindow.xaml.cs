using PdfSharp.Pdf;
using PdfSharp.Pdf.AcroForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckPrint.Beta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            check objCheck = new check();
            objCheck.payee = txtPayee.Text;
            objCheck.amount = txtAmount.Text;
            objCheck.amountinword = txtAmountWord.Text;
            objCheck.date = txtDate.Text;
            //
            ////List<ReportParameter> listParam = new List<ReportParameter>();
            //ReportViewer _reportviewer = new ReportViewer();
            //_reportViewer.LocalReport.ReportEmbeddedResource = "<VSProjectName>.rptCheck.rdlc";
            ////_reportViewer.RefreshReport();
            PdfSharp.Pdf.PdfDocument docPDF = PdfSharp.Pdf.IO.PdfReader.Open(@"CheckGeneral7.pdf", PdfSharp.Pdf.IO.PdfDocumentOpenMode.Modify);
            if (docPDF.AcroForm != null)
            {
                if (docPDF.AcroForm.Elements.ContainsKey("/NeedAppearances"))
                {
                    docPDF.AcroForm.Elements["/NeedAppearances"] = new PdfBoolean(true);
                }
                else
                {
                    docPDF.AcroForm.Elements.Add("/NeedAppearances", new PdfBoolean(true));
                }

            }
            /*
             docPDF.AcroForm.Fields[0].Name DATE
    docPDF.AcroForm.Fields[1].Name untitled1
    docPDF.AcroForm.Fields[2].Name amountword1
    docPDF.AcroForm.Fields[3].Name amountword2
    docPDF.AcroForm.Fields[4].Name amount
             */
            //docPDF.AcroForm.Fields[0].ReadOnly = false;
            //docPDF.AcroForm.Fields.Item("FirstName").Value = new PdfSharp.Pdf.PdfString("***" + objCheck.payee + "***");
            docPDF.AcroForm.Fields["payee"].Value = new PdfSharp.Pdf.PdfString("***" + objCheck.payee + "***");
            //((PdfTextField)docPDF.AcroForm.Fields[0]).Text = "***" + objCheck.payee + "***";

            //docPDF.AcroForm.Fields[0].ReadOnly = true;
            //
            //docPDF.AcroForm.Fields[4].ReadOnly = false;
            docPDF.AcroForm.Fields[4].Value = new PdfSharp.Pdf.PdfString("***" + objCheck.amount + "***");
            //docPDF.AcroForm.Fields[4].ReadOnly = true;

            //65
            string strAmountWord1 = objCheck.amountinword;
            string strAmountWord2 = "";
            if (strAmountWord1.Length > 59)
            {
                strAmountWord1 = "***" + objCheck.amountinword.Substring(0, 58);
                strAmountWord2 = objCheck.amountinword.Substring(58, objCheck.amountinword.Length - 58) + "***";
            }
            else
            {
                strAmountWord1 = "***" + strAmountWord1 + "***";
            }
            //docPDF.AcroForm.Fields[1].ReadOnly = false;
            docPDF.AcroForm.Fields[1].Value = new PdfSharp.Pdf.PdfString(strAmountWord1);
            //docPDF.AcroForm.Fields[1].ReadOnly = true;
            //
            //docPDF.AcroForm.Fields[2].ReadOnly = false;
            docPDF.AcroForm.Fields[2].Value = new PdfSharp.Pdf.PdfString(strAmountWord2);
            //docPDF.AcroForm.Fields[2].ReadOnly = true;

            //
            //docPDF.AcroForm.Fields[3].ReadOnly = false;
            docPDF.AcroForm.Fields[3].Value = new PdfSharp.Pdf.PdfString(objCheck.date.Replace("-", "").Replace("/", ""));
            //docPDF.AcroForm.Fields[3].ReadOnly = true;
            //
            docPDF.Flatten();
            //
            string filename = string.Empty;
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = objCheck.date + "." + objCheck.payee.Replace(" ", "");
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF documents (.pdf)|*.pdf";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Save document 
                filename = dlg.FileName;
            }
            docPDF.Save(filename);
        }

    }


    class check
    {
        public string payee;
        public string date;
        public string amount;
        public string amountinword;
    }
}
