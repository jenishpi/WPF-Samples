using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace RDLC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public ReportViewer _reportviewer;
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

            /*
             docPDF.AcroForm.Fields[0].Name DATE
docPDF.AcroForm.Fields[1].Name untitled1
docPDF.AcroForm.Fields[2].Name amountword1
docPDF.AcroForm.Fields[3].Name amountword2
docPDF.AcroForm.Fields[4].Name amount
             */
            //docPDF.AcroForm.Fields[0].ReadOnly = false;
            docPDF.AcroForm.Fields[0].Value = new PdfSharp.Pdf.PdfString("***" + objCheck.payee + "***");
            
            //docPDF.AcroForm.Fields[0].ReadOnly = true;
            //
            //docPDF.AcroForm.Fields[4].ReadOnly = false;
            docPDF.AcroForm.Fields[4].Value = new PdfSharp.Pdf.PdfString("***" + objCheck.amount + "***");
            //docPDF.AcroForm.Fields[4].ReadOnly = true;

            //65
            string strAmountWord1 = objCheck.amountinword;
            string strAmountWord2 = "";
            if(strAmountWord1.Length > 59)
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
            docPDF.AcroForm.Fields[2].Value = new PdfSharp.Pdf.PdfString(strAmountWord2 );
            //docPDF.AcroForm.Fields[2].ReadOnly = true;
            
            //
            //docPDF.AcroForm.Fields[3].ReadOnly = false;
            docPDF.AcroForm.Fields[3].Value = new PdfSharp.Pdf.PdfString(objCheck.date.Replace("-","").Replace("/", ""));
            //docPDF.AcroForm.Fields[3].ReadOnly = true;
            //
            docPDF.Flatten();
            //
            string filename = string.Empty;
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = objCheck.date + "." + objCheck.payee.Replace(" ","");
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

    //public class PersonViewModel
    //{

    //    private MainWindow _window;
    //    private LocalReport _Report;
    //    private ReportViewer _reportviewer;
    //    public ViewModel(MainWindow window)
    //    {
    //        _window = window;
    //        this._reportviewer = new ReportViewer();
    //        Initialize();

    //    }

    //    private IEnumerable<Person> people = new List<Person>() { new Person { Name = "Gloria", id = 46, Age =12} ,
    //    new Person {Name = "John", id = 1, Age =23},
    //     new Person {Name = "Francis My Staff", id = 2, Age =12},
    //    new Person {Name = "Ndu", id = 3, Age =32},
    //    new Person {Name = "Murphy", id = 4, Age =22},
    //    new Person {Name = "Mr Charles our boss", id = 5, Age =52}};
    //    private void Initialize()
    //    {
    //        _reportviewer.LocalReport.DataSources.Clear();
    //        var rpds_model = new ReportDataSource() { Name = "Person_DS", Value = people };
    //        _reportviewer.LocalReport.DataSources.Add(rpds_model);
    //        _reportviewer.LocalReport.EnableExternalImages = true;
    //        private static string _path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())));
    //        public static string ContentStart = _path + @"\ReportProject\MainPage.rdlc";

    //    _reportviewer.LocalReport.ReportPath = ContentStart;  
    //        _reportviewer.SetDisplayMode(DisplayMode.PrintLayout);  
    //        _reportviewer.Refresh();  
    //        _reportviewer.RefreshReport();  
    //    }
}  

