# region Document Header
//Created By       : SUdheer 
//Created Date     : 
//Description      : 
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

namespace CSS2.Areas.TM.Controllers
{
    #region Usings
    using Aspose.Words;
    using Aspose.Words.Drawing;
    using Aspose.Words.Reporting;
    using Aspose.Words.Tables;
    using CSS2.Areas.TM.Models;
    using CSS2.Areas.WO.Models;
    using CSS2.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using System.Linq;
    #endregion

    public class ASPOSEController : Controller
    {

        #region Old Logic
        //
        // GET: /TM/ASPOSE/
        public ActionResult ASPOSE()
        {         

            if (UserLogin.ValidateUserRequest())
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase DocPath)
        {

            //if (DocPath != null && DocPath.ContentLength > 0)
            //{
            //    var filename = Path.GetFileName(DocPath.FileName);

            //    if (Path.GetExtension(Path.GetFullPath(Server.MapPath(filename))) == ".docx")
            //    {
            //        string OutPutFileName = "Result_" + System.DateTime.Now.Ticks + ".docx";

            //        string OutputFile = Server.MapPath("~/DocTemplates/" + OutPutFileName);

            //        var path = Path.Combine(Server.MapPath("~/DocTemplates/Process/"), filename);
            //        DocPath.SaveAs(path);

            //        // Load in the document
            //        Document Inputdoc = new Document(path.ToString());
            //        // Set up the document which pages will be copied to. Remove the empty section.   

            //        Document dstDoc = new Document();
            //        dstDoc.RemoveAllChildren();


            //       // PageNumberFinder finder = new PageNumberFinder(Inputdoc);

            //        // Split nodes which are found across pages.
            //        finder.SplitNodesAcrossPages(true);

            //        // Copy all content including headers and footers from the specified pages into the destination document.
            //        ArrayList pageSections = finder.RetrieveAllNodesOnPages(1, Inputdoc.Sections.Count, NodeType.Section);

            //        var listValues = KyesSet.GetKeyValues("Keys");

            //        int sectionCount = 0; int pageNumber = 1;
            //        foreach (Section section in pageSections)
            //        {

            //            bool isSectionHaveKeys = false;
            //            bool isKeyReplaced = false;
            //            bool isRepetedSection = false;
            //            bool isClearSection = false;



            //            Section tempSection = ParaRepeat(Inputdoc, section);


            //            foreach (var k in listValues)
            //            {

            //                isSectionHaveKeys = true;
            //                if (!string.IsNullOrEmpty(Convert.ToString(k.Value.ToString())))
            //                {
            //                    if (section.Range.Text.Contains("[Name of all existing directors]"))// ***Multiple Keys*** && section.Range.Text.Contains("[EXISTING NAME]"))
            //                    {
            //                        if (!isRepetedSection)
            //                            isRepetedSection = true;

            //                        if (k.Key != "[Name of all existing directors]") // ***Multiple Keys***// && k.Key != "[EXISTING NAME]")                              
            //                            isKeyReplaced = Replace(isKeyReplaced, tempSection, k);
            //                    }
            //                    else
            //                        isKeyReplaced = Replace(isKeyReplaced, tempSection, k);
            //                }
            //                else
            //                {
            //                    tempSection.ClearContent();
            //                    dstDoc.AppendChild(dstDoc.ImportNode(tempSection, true));
            //                    isClearSection = true;
            //                    break;
            //                }

            //            }

            //            if (!isClearSection)
            //            {
            //                if (!isSectionHaveKeys)
            //                {
            //                    dstDoc.AppendChild(dstDoc.ImportNode(tempSection, true));
            //                }
            //                else if (isKeyReplaced)
            //                {
            //                    if (!isRepetedSection)
            //                        dstDoc.AppendChild(dstDoc.ImportNode(tempSection, true));
            //                }

            //                if (isRepetedSection)
            //                {
            //                    var RepeatedValues = KyesSet.GetKeyValues("RepeatedSecKey");

            //                    foreach (var data in RepeatedValues)
            //                    {
            //                        Section tempSection1 = tempSection.Clone();

            //                        if (data.Key == "[Name of all existing directors]")
            //                            tempSection1.Range.Replace(new Regex("\\[Name of all existing directors\\]"), data.Value.ToString());


            //                        //tempSection1.PageSetup.SectionStart = SectionStart.NewPage;
            //                        dstDoc.AppendChild(dstDoc.ImportNode(tempSection1, true));
            //                    }
            //                }
            //            }




            //            sectionCount++;
            //        }

            //        //****To Add Water Mark to Out Put Docuemnt*******
            //        // InsertWatermarkText(dstDoc, "Confidential");

            //        //CreateTable(dstDoc);
            //        //DocumentBuilder builder = new DocumentBuilder(dstDoc);

            //        //// Insert a table of contents at the beginning of the document.
            //        //builder.InsertTableOfContents("\\o \"1-3\" \\h \\z \\u");

            //        //// The newly inserted table of contents will be initially empty.
            //        //// It needs to be populated by updating the fields in the document.
            //        //dstDoc.UpdateFields();
            //        string[] asb;
            //        asb = new string[] { "(Rajaadfsaf a fdsfsa fsa)1", "(NagaSudheer Dunaboyana)2", "(Rajender asdfsafsfsafsfsafdsa Erra)3", "(NagaSudheer Nad asdfsafsf adgaSudheer Nag)4", "(Kevin asdfsafsafsa fsf safsafsafaf PeterSon)5", "(Sachin Tendulker)6", "(Rajender ReddyErra)7" };
            //        // asb = new string[] { "(Rajaadfsaf a fdsfsa fsa)1", "(NagaSudheer Dunaboyana)2" };//, "(Rajender asdfsafsfsafsfsafdsa Erra)3", "(NagaSudheer Nad asdfsafsf adgaSudheer Nag)4", "(Kevin asdfsafsafsa fsf safsafsafaf PeterSon)5", "(Sachin Tendulker)6", "(Rajender ReddyErra)7" };


            //        //dstDoc.Range.Replace(new Regex(@"\[CHAIRMAN\]", RegexOptions.IgnoreCase), new ReplaceEvaluatorSignature(asb), false);

            //        dstDoc.Save(OutputFile);

            //        GetFile(OutPutFileName);

            //        if (System.IO.File.Exists(OutputFile))
            //            System.IO.File.Delete(OutputFile);
            //    }

            //}
            return View();
        }

        private static void CreateTable(Document dstDoc)
        {
            // We start by creating the table object. Note how we must pass the document object
            // to the constructor of each node. This is because every node we create must belong
            // to some document.
            Table table = new Table(dstDoc);
            // Add the table to the document.
            dstDoc.FirstSection.Body.AppendChild(table);




            // Here we could call EnsureMinimum to create the rows and cells for us. This method is used
            // to ensure that the specified node is valid, in this case a valid table should have at least one
            // row and one cell, therefore this method creates them for us.

            // Instead we will handle creating the row and table ourselves. This would be the best way to do this
            // if we were creating a table inside an algorthim for example.
            Row row = new Row(dstDoc);
            row.RowFormat.AllowBreakAcrossPages = true;
            table.AppendChild(row);

            // We can now apply any auto fit settings.
            table.AutoFit(AutoFitBehavior.FixedColumnWidths);

            // Create a cell and add it to the row
            Cell cell = new Cell(dstDoc);
            cell.CellFormat.Shading.BackgroundPatternColor = Color.LightBlue;
            cell.CellFormat.Width = 80;

            // Add a paragraph to the cell as well as a new run with some text.
            cell.AppendChild(new Paragraph(dstDoc));
            cell.FirstParagraph.AppendChild(new Run(dstDoc, "Row 1, Cell 1 Text"));

            // Add the cell to the row.
            row.AppendChild(cell);

            // We would then repeat the process for the other cells and rows in the table.
            // We can also speed things up by cloning existing cells and rows.
            row.AppendChild(cell.Clone(false));
            row.LastCell.AppendChild(new Paragraph(dstDoc));
            row.LastCell.FirstParagraph.AppendChild(new Run(dstDoc, "Row 1, Cell 2 Text"));
        }

        /// <summary>
        /// Description  : To Replace keys with values for Section
        /// Created By   : Sudheer
        /// Created Date : 20 March 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        private static bool Replace(bool isKeyReplaced, Section tempSection, KyesSet k)
        {
            tempSection.Range.Replace(new Regex("\\[" + k.Key.ToString().TrimStart('[').TrimEnd(']') + "\\]"), k.Value.ToString());
            isKeyReplaced = true;
            return isKeyReplaced;
        }

        /// <summary>
        /// Description  : To Repeat the ParaGraph in a section Based on Given Key
        /// Created By   : Sudheer
        /// Created Date : 20 March 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        private Section ParaRepeat(Document Inputdoc, Section section)
        {
            NodeCollection paragraphs = section.GetChildNodes(NodeType.Paragraph, true);
            if (paragraphs.Count > 0)
            {
                var paraKeys = KyesSet.GetKeyValues("RepeatedParaKey");

                for (int i = 0; i < paragraphs.Count; i++)
                {
                    string paraText = paragraphs[i].Range.Text;

                    if (paraText.Contains("[Article No.1]") && paraText.Contains("[Name of retiring Director]"))
                    {
                        var ArticalData = KyesSet.GetArticalInfo("[Article No.1]", "[Name of retiring Director]");
                        VerticalParaReplacing(ArticalData, paragraphs[i]);
                    }


                    if (paraText.Contains("[Name of chairman]"))
                    {


                        string Chairman = "";

                        Paragraph para = (Paragraph)paragraphs[i].Clone(true);


                        int result = para.Range.Replace(new Regex("\\[" + "[Name of chairman]".TrimStart('[').TrimEnd(']') + "\\]"), Chairman);//, false, true);

                        ((Paragraph)paragraphs[i]).ParentNode.InsertAfter(para, paragraphs[i]);

                        ((Paragraph)paragraphs[i]).Remove();
                    }

                    if (paraText.Contains("[Name of allottee]"))
                    {
                        var data = Allotment.AllotmentList();

                        foreach (var a in data)
                        {
                            Paragraph para = (Paragraph)paragraphs[i].Clone(true);
                            int result = para.Range.Replace(new Regex("\\[" + "[Name of allottee]".TrimStart('[').TrimEnd(']') + "\\]"), a.NameOfAllottee.ToString());//, false, true);

                            ((Paragraph)paragraphs[i]).ParentNode.InsertAfter(para, paragraphs[i]);


                        }
                        ((Paragraph)paragraphs[i]).Remove();
                    }




                    if (paraText.Contains("[No. of shares]"))//&& paraText.Contains("[Total no. of shares]"))
                    {
                        var data = Allotment.AllotmentList();

                        foreach (var a in data)
                        {
                            Paragraph para = (Paragraph)paragraphs[i].Clone(true);
                            int result = para.Range.Replace(new Regex("\\[" + "[No. of shares]".TrimStart('[').TrimEnd(']') + "\\]"), a.NoOfshares.ToString());//, false, true);

                            ((Paragraph)paragraphs[i]).ParentNode.InsertAfter(para, paragraphs[i]);

                        }
                        ((Paragraph)paragraphs[i]).Remove();
                    }

                    int TotalNoOfShares = 0;
                    if (paraText.Contains("[Total no. of shares]"))
                    {

                        var data = Allotment.AllotmentList();
                        foreach (var a in data)
                            TotalNoOfShares += (a.NoOfshares == string.Empty) ? 0 : Convert.ToInt32(a.NoOfshares);


                        Paragraph para = (Paragraph)paragraphs[i].Clone(true);
                        int result = para.Range.Replace(new Regex("\\[" + "[Total no. of shares]".TrimStart('[').TrimEnd(']') + "\\]"), TotalNoOfShares.ToString());//, false, true);

                        ((Paragraph)paragraphs[i]).ParentNode.InsertAfter(para, paragraphs[i]);

                        ((Paragraph)paragraphs[i]).Remove();
                    }

                    //if (paraText.ToUpper().Contains("[CHAIRMAN]"))
                    //{
                    //    var data = Allotment.AllotmentList();
                    //    foreach (var a in data)
                    //        TotalNoOfShares += (a.NoOfshares == string.Empty) ? 0 : Convert.ToInt32(a.NoOfshares);


                    //    Paragraph para = (Paragraph)paragraphs[i].Clone(true);

                    //    string[] asb = new string[] { "asd", "asdfsaf" };

                    //    para.Range.Replace(new Regex(@"\[CHAIRMAN\]", RegexOptions.IgnoreCase), new ReplaceEvaluatorSignature(asb), false);

                    //   // int result = para.Range.Replace(new Regex("\\[" + "[CHAIRMAN]".TrimStart('[').TrimEnd(']') + "\\]"), TotalNoOfShares.ToString());//, false, true);

                    //    ((Paragraph)paragraphs[i]).ParentNode.InsertAfter(para, paragraphs[i]);

                    //    ((Paragraph)paragraphs[i]).Remove();
                    //}
                    //if (paraText.Contains("[Article No. 2]") && paraText.Contains("[Name of retiring Director]"))
                    //{
                    //    var ArticalData = KyesSet.GetArticalInfo("[Article No.1]", "[Name of retiring Director]");
                    //    VerticalParaReplacing(ArticalData, paragraphs[i]);    
                    //}
                    //else
                    //{

                    //    if (paragraphs[i].Range.Text.Contains("[Article No.1]"))
                    //        ParaReplacing(paraKeys, paragraphs, i, "[Article No.1]");

                    //    if (paragraphs[i].Range.Text.Contains("[Name of retiring Director]"))
                    //        ParaReplacing(paraKeys, paragraphs, i, "[Name of retiring Director]");
                    //}

                }
            }

            Section sec = section.Clone();
            return sec;
        }

        /// <summary>
        /// Description  : To Repeat the Paragraph Based on Given Key
        /// Created By   : Sudheer
        /// Created Date : 20 March 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        private void VerticalParaReplacing(List<KyesSet> ArticalData, Node node)
        {
            for (int j = ArticalData.Count - 1; j > 0; j--)
            {
                Paragraph para = (Paragraph)node.Clone(true);

                if (para.Range.Text.Contains(ArticalData[0].Key.ToString())) //("[Article No.1]"))
                    para.Range.Replace(new Regex("\\[" + ArticalData[0].Key.ToString().TrimStart('[').TrimEnd(']') + "\\]"), ArticalData[0].Value.ToString());//, false, true);               

                int result = para.Range.Replace(new Regex("\\[" + ArticalData[j].Key.ToString().TrimStart('[').TrimEnd(']') + "\\]"), ArticalData[j].Value.ToString());//, false, true);

                ((Paragraph)node).ParentNode.InsertAfter(para, node);
            }
            ((Paragraph)node).Remove();
        }

        /// <summary>
        /// Description  : To Repeat the Paragraph Based on Given Key
        /// Created By   : Sudheer
        /// Created Date : 20 March 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        private static void ParaReplacing(List<KyesSet> paraKeys, NodeCollection paragraphs, int i, string repeatedKey)
        {
            for (int j = paraKeys.Count - 1; j >= 0; j--)
            {
                if (paraKeys[j].Key == repeatedKey && !string.IsNullOrEmpty(paraKeys[j].Value))
                {
                    Paragraph para = (Paragraph)paragraphs[i].Clone(true);

                    int result = para.Range.Replace(new Regex("\\[" + paraKeys[j].Key.ToString().TrimStart('[').TrimEnd(']') + "\\]"), paraKeys[j].Value.ToString());//, false, true);

                    ((Paragraph)paragraphs[i]).ParentNode.InsertAfter(para, paragraphs[i]);
                }
            }
            ((Paragraph)paragraphs[i]).Remove();
        }


        #region Download File
        /// <summary>
        /// Description  : To Download Template
        /// Created By   : Anji
        /// Created Date : 18 March 2014
        /// Modified By  :
        /// Modified Date:
        /// </summary>
        public void GetFile(string filename)
        {
            try
            {

                var context = ControllerContext.HttpContext;
                var filePath = context.Server.MapPath("~/DocTemplates/" + filename);
                var file = new System.IO.FileInfo(filePath);
                switch (file.Extension)
                {
                    case ".pdf":
                        context.Response.ContentType = "application/pdf";
                        break;
                    case ".doc":
                        context.Response.ContentType = "application/msword";
                        break;
                    case ".docx":
                        context.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        break;
                    case ".txt":
                        context.Response.ContentType = "text/plain";
                        break;
                    case ".xlsx":
                    case ".xls":
                        context.Response.Buffer = true;
                        context.Response.ContentType = "application/x-msdownload";
                        context.Response.StatusCode = 200;
                        break;
                    case ".jpg":
                    case ".jpeg":
                        context.Response.ContentType = "image/jpg";
                        break;
                    case ".csv":
                        context.Response.ContentType = "application/csv";
                        break;
                    case ".ics":
                        context.Response.ContentType = "text/calendar";
                        break;

                }

                Response.AppendHeader("content-disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath));
                context.Response.TransmitFile(filePath);
                ControllerContext.HttpContext.ApplicationInstance.CompleteRequest();
                context.Response.End();

            }
            catch
            {
            }
        }

        #endregion

        #region SampleData for Template
        public class KyesSet
        {
            public string Key { get; set; }
            public string Value { get; set; }

            public static List<KyesSet> GetKeyValues(string k)
            {
                List<KyesSet> objKeyValue = new List<KyesSet>();
                if (k == "Keys")
                {
                    DataSet ds = new DataSet();
                    ds = GetDataTable();
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            KyesSet userinfo = new KyesSet();
                            userinfo.Key = dt.Rows[i]["Key"].ToString();
                            userinfo.Value = dt.Rows[i]["Value"].ToString();
                            objKeyValue.Add(userinfo);
                        }
                    }
                }
                else if (k == "RepeatedSecKey")
                {
                    DataSet ds = new DataSet();
                    ds = GetDataTable();
                    DataTable dt = ds.Tables[1];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            KyesSet userinfo = new KyesSet();
                            userinfo.Key = dt.Rows[i]["Key"].ToString();
                            userinfo.Value = dt.Rows[i]["Value"].ToString();
                            objKeyValue.Add(userinfo);
                        }
                    }
                }
                else if (k == "RepeatedParaKey")
                {
                    DataSet ds = new DataSet();
                    ds = GetDataTable();
                    DataTable dt = ds.Tables[2];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            KyesSet userinfo = new KyesSet();
                            userinfo.Key = dt.Rows[i]["Key"].ToString();
                            userinfo.Value = dt.Rows[i]["Value"].ToString();
                            objKeyValue.Add(userinfo);
                        }
                    }

                }
                return objKeyValue;
            }

            /// <summary>
            /// Description  : Sample Data fro Keys and Values
            /// Created By   : Sudheer
            /// Created Date : 8th Aprial
            /// Modified By  :
            /// Modified Date:
            /// </summary>
            /// <param name="data">pass the word data</param>    
            public static System.Data.DataSet GetDataTable()
            {
                DataSet ds = new DataSet();

                string[] MergFields = new string[] { "[Registration No.]"
                                                 ,"[REFER TO STD]"
                                                 ,"[REFER TO STD CONTENT]"
                                                 ,"[_Name of allottee]"
                                                 ,"[_No. of shares]"
                                                 ,"[Amount of Consideration]"        
                                                 ,"[_Total no. of shares]"
                                                 ,"[EXISTING NAME]"
                                                 ,"[Reg No.]"
                                                 ,"[Country]"
                                                 ,"[REFER TO HEADER]"
                                                 ,"[No. of Shares to be allotted]"
                                                 ,"[Amount of allotment]"                                                 
                                                 ,"[Date of DRIW]"                                                 
                                                 ,"[_Total consideration]"
                                                 ,"[FYE]"
                                                 ,"[STD]"
                                                 ,"[COMPANY NAME]"
                                                 };

                // Add new Tables to the data set.
                DataRow row;
                ds.Tables.Add();

                ds.Tables[0].TableName = "Result";
                ds.Tables[0].Columns.Add("Key");
                ds.Tables[0].Columns.Add("Value");

                // Inserting values to the tables.
                foreach (string result in MergFields)
                {
                    row = ds.Tables["Result"].NewRow();

                    switch (result)
                    {
                        case "[Registration No.]":
                            row["key"] = "[Registration No.]";
                            row["Value"] = "Reg123456";
                            break;

                        case "[REFER TO STD]":
                            row["key"] = "[REFER TO STD]";
                            row["Value"] = "REFER 2015";
                            break;

                        case "[REFER TO STD CONTENT]":
                            row["key"] = "[REFER TO STD CONTENT]";
                            row["Value"] = "STD 2014";
                            break;

                        case "[_Name of allottee]":
                            row["key"] = "[_Name of allottee]";
                            row["Value"] = "Allottee WS";
                            break;

                        case "[_No. of shares]":
                            row["key"] = "[_No. of shares]";
                            row["Value"] = "shares 500";
                            break;

                        case "[Amount of Consideration]":
                            row["key"] = "[Amount of Consideration]";
                            row["Value"] = "Amount 6000";
                            break;

                        case "[_Total no. of shares]":
                            row["key"] = "[_Total no. of shares]";
                            row["Value"] = "Value 7";
                            break;

                        case "[EXISTING NAME]":
                            row["key"] = "[EXISTING NAME]";
                            row["Value"] = "Value 8";
                            break;

                        case "[Reg No.]":
                            row["key"] = "[Reg No.]";
                            row["Value"] = "Reg 92345";
                            break;

                        case "[Country]":
                            row["key"] = "[Country]";
                            row["Value"] = "India";
                            break;

                        case "[REFER TO HEADER]":
                            row["key"] = "[REFER TO HEADER]";
                            row["Value"] = "Value 11";
                            break;

                        case "[No. of Shares to be allotted]":
                            row["key"] = "[No. of Shares to be allotted]";
                            row["Value"] = "Value 12";
                            break;

                        case "[Amount of allotment]":
                            row["key"] = "[Amount of allotment]";
                            row["Value"] = "Value 13";
                            break;

                        case "[Date of DRIW]":
                            row["key"] = "[Date of DRIW]";
                            row["Value"] = "Value 14";
                            break;

                        case "[Name of all existing directors]":
                            row["key"] = "[Name of all existing directors]";
                            row["Value"] = "Value 15";
                            break;

                        case "[_Total consideration]":
                            row["key"] = "[_Total consideration]";
                            row["Value"] = "Value 16";
                            break;

                        case "[FYE]":
                            row["key"] = "[FYE]";
                            row["Value"] = "2014";
                            break;

                        case "[STD]":
                            row["key"] = "[STD]";
                            row["Value"] = "2020";// "*************VAlue for STD**************";
                            break;
                        case "[COMPANY NAME]":
                            row["key"] = "[COMPANY NAME]";
                            row["Value"] = "Web Synergies";// "*************VAlue for STD**************";
                            break;

                    }
                    ds.Tables["Result"].Rows.Add(row);

                }

                ds.Tables.Add();

                ds.Tables[1].TableName = "RepetedList";
                ds.Tables[1].Columns.Add("Key");
                ds.Tables[1].Columns.Add("Value");

                for (int i = 0; i < 4; i++)
                {
                    row = ds.Tables["RepetedList"].NewRow();
                    if (i == 0)
                    {
                        row["Key"] = "[Name of all existing directors]";
                        row["Value"] = "Sudheer";
                    }
                    else if (i == 1)
                    {
                        row["Key"] = "[Name of all existing directors]";
                        row["Value"] = "Rajender";
                    }
                    else if (i == 2)
                    {
                        row["Key"] = "[Name of all existing directors]";
                        row["Value"] = "Raja";
                    }
                    else if (i == 3)
                    {
                        row["Key"] = "[Name of all existing directors]";
                        row["Value"] = "Anji";
                    }

                    ds.Tables["RepetedList"].Rows.Add(row);
                }

                ds.Tables.Add();

                ds.Tables[2].TableName = "RepetedParaList";
                ds.Tables[2].Columns.Add("Key");
                ds.Tables[2].Columns.Add("Value");

                for (int i = 0; i < 7; i++)
                {
                    List<string> paraKeys = new List<string>(new string[] { "", "" });
                    row = ds.Tables["RepetedParaList"].NewRow();
                    if (i == 0)
                    {
                        //row["Key"] = "(##Director##)";
                        //row["Value"] = "Director One";
                        row["Key"] = "[Article No.1]";
                        row["Value"] = "Article Replaced";
                    }
                    else if (i == 1)
                    {
                        row["Key"] = "[Name of retiring Director]";
                        row["Value"] = "Director One";
                    }
                    else if (i == 2)
                    {
                        row["Key"] = "[Name of retiring Director]";
                        row["Value"] = "Director Two";
                    }
                    else if (i == 3)
                    {
                        row["Key"] = "[Name of retiring Director]";
                        row["Value"] = "Director Three";
                    }
                    else if (i == 4)
                    {
                        row["Key"] = "[Name of retiring Director]";
                        row["Value"] = "Director Four";
                    }
                    else if (i == 5)
                    {
                        row["Key"] = "[Name of retiring Director]";
                        row["Value"] = "Director Five";
                    }
                    else if (i == 6)
                    {
                        row["Key"] = "[Name of retiring Director]";
                        row["Value"] = "Director Six";
                    }

                    ds.Tables["RepetedParaList"].Rows.Add(row);
                }

                return ds; //.Tables["Result"];

            }


            internal static List<KyesSet> GetArticalInfo(string Artical, string Key)
            {

                List<KyesSet> objKeyValue = new List<KyesSet>();
                DataSet ds = new DataSet();
                ds = GetDataTable();
                DataTable dt = ds.Tables[2];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        KyesSet userinfo = new KyesSet();
                        userinfo.Key = dt.Rows[i]["Key"].ToString();
                        userinfo.Value = dt.Rows[i]["Value"].ToString();
                        objKeyValue.Add(userinfo);
                    }
                }

                return objKeyValue;
            }
        }
        public class Allotment
        {
            public string NameOfAllottee { get; set; }
            public string NoOfshares { get; set; }
            public string AmountOfConsideration { get; set; }

            public static List<Allotment> AllotmentList()
            {
                List<Allotment> alt = new List<Allotment>();
                DataTable dt = GetDataTable();

                foreach (DataRow dr in dt.Rows)
                {
                    Allotment lst = new Allotment();
                    lst.NameOfAllottee = dr["NameOfAllottee"].ToString();
                    lst.NoOfshares = dr["NoOfshares"].ToString();
                    lst.AmountOfConsideration = dr["AmountOfConsideration"].ToString();
                    alt.Add(lst);
                }

                return alt;
            }

            public static System.Data.DataTable GetDataTable()
            {
                DataTable dt = new DataTable();

                DataColumn dc = new DataColumn();
                dc.ColumnName = "NameOfAllottee";
                dt.Columns.Add(dc);

                DataColumn dc2 = new DataColumn();
                dc2.ColumnName = "NoOfshares";
                dt.Columns.Add(dc2);

                DataColumn dc3 = new DataColumn();
                dc3.ColumnName = "AmountOfConsideration";
                dt.Columns.Add(dc3);

                dt.Rows.Add(new object[] { "Sample Allottee One", "1", "6" });
                dt.Rows.Add(new object[] { "Sample Allottee two", "2", "7" });
                dt.Rows.Add(new object[] { "Sample Allottee three", "3", "8" });
                dt.Rows.Add(new object[] { "Sample Allottee four", "4", "9" });
                dt.Rows.Add(new object[] { "Sample Allottee five", "5", "10" });

                return dt;
            }
        }

        class SampleA
        {
            public void Show()
            {
                Console.WriteLine("Sample A Test Method");
            }
        }
        class SampleB : SampleA
        {
            public void Show()
            {
                Console.WriteLine("Sample B Test Method");
            }
        }

        #endregion

        #region Adding WaterMart Text

        /// <summary>
        /// Inserts a watermark into a document.
        /// </summary>
        /// <param name="doc">The input document.</param>
        /// <param name="watermarkText">Text of the watermark.</param>
        private static void InsertWatermarkText(Document doc, string watermarkText)
        {
            // Create a watermark shape. This will be a WordArt shape. 
            // You are free to try other shape types as watermarks.
            Shape watermark = new Shape(doc, ShapeType.TextPlainText);

            // Set up the text of the watermark.
            watermark.TextPath.Text = watermarkText;
            watermark.TextPath.FontFamily = "Arial";
            watermark.Width = 150;
            watermark.Height = 100;
            // Text will be directed from the bottom-left to the top-right corner.
            watermark.Rotation = -40;
            // Remove the following two lines if you need a solid black text.
            watermark.Fill.Color = Color.Gray; // Try LightGray to get more Word-style watermark
            watermark.StrokeColor = Color.Gray; // Try LightGray to get more Word-style watermark

            // Place the watermark in the page center.
            watermark.RelativeHorizontalPosition = RelativeHorizontalPosition.Page;
            watermark.RelativeVerticalPosition = RelativeVerticalPosition.Page;
            watermark.WrapType = WrapType.None;
            watermark.VerticalAlignment = VerticalAlignment.Center;
            watermark.HorizontalAlignment = HorizontalAlignment.Center;

            // Create a new paragraph and append the watermark to this paragraph.
            Paragraph watermarkPara = new Paragraph(doc);
            watermarkPara.AppendChild(watermark);

            // Insert the watermark into all headers of each document section.
            foreach (Section sect in doc.Sections)
            {
                // There could be up to three different headers in each section, since we want
                // the watermark to appear on all pages, insert into all headers.
                InsertWatermarkIntoHeader(watermarkPara, sect, HeaderFooterType.HeaderPrimary);
                InsertWatermarkIntoHeader(watermarkPara, sect, HeaderFooterType.HeaderFirst);
                InsertWatermarkIntoHeader(watermarkPara, sect, HeaderFooterType.HeaderEven);
            }
        }

        private static void InsertWatermarkIntoHeader(Paragraph watermarkPara, Section sect, HeaderFooterType headerType)
        {
            HeaderFooter header = sect.HeadersFooters[headerType];

            if (header == null)
            {
                // There is no header of the specified type in the current section, create it.
                header = new HeaderFooter(sect.Document, headerType);
                sect.HeadersFooters.Add(header);
            }

            // Insert a clone of the watermark into the header.
            header.AppendChild(watermarkPara.Clone(true));
        }

        #endregion
        #endregion

        #region New Merge


        public ActionResult Documents()
        {
            string files = Request.QueryString["files"];
            ViewBag.Files = files;
            if (UserLogin.ValidateUserRequest())
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        public string Documents(string DocFiles)
        {

            var json_serializer = new JavaScriptSerializer();
            Dictionary<string, object> routes_list = (Dictionary<string, object>)json_serializer.DeserializeObject(DocFiles);

            string strFileIDs = string.Empty;

            object files = routes_list["TemplateFields"];
            object woid = routes_list["WOID"];
            object woCode = routes_list["WOTYPE"];

            object[] objFiles = (object[])files;

            ArrayList arrFilesList = new ArrayList();

            foreach (Dictionary<string, object> a in objFiles)
            {
                //arrFilesList.Add(Convert.ToString(a["FileFullName"]));
                strFileIDs += Convert.ToString(a["FileID"]) + ",";
            }

            if (strFileIDs.Length > 0)
                strFileIDs = strFileIDs.TrimEnd(',');

            DocumentGenerate objDocGenerate = new DocumentGenerate();

            objDocGenerate.GenrateDocuments(woCode.ToString(), Convert.ToInt32(woid), strFileIDs);

            return string.Empty;
        }    

        public ActionResult DocGenrate()
        {
            if (UserLogin.ValidateUserRequest())
            {
                return RedirectToAction("Login", "Home", new { area = "" });
            }

            return View();
        }

        [HttpPost]
        public ActionResult DocGenrate1(string DocPath)
        {
            DocumentGenerate objDocGenerate = new DocumentGenerate();
            objDocGenerate.GenrateDocuments("AGM", 13578, string.Empty);
            
            return View();
        }      

        [HttpPost]
        public ActionResult DocGenrate(string DocPath)
        {
            //var filename = Path.GetFileName(DocPath.FileName);
            var filename1 = "TestDoc4level.docx";

            if (Path.GetExtension(Path.GetFullPath(Server.MapPath(filename1))) == ".docx" || Path.GetExtension(Path.GetFullPath(Server.MapPath(filename1))) == ".doc")
            {

                string exeDir = Path.GetDirectoryName(Path.GetFullPath(Server.MapPath(filename1))) + Path.DirectorySeparatorChar;
                string dataDir = Server.MapPath("~/DocTemplates/Type B/AGM/");
                
                Document doc = new Document(dataDir + filename1);

                DataSet ds = new DataSet();             

                DataTable Company = new DataTable("Company");
                Company.Columns.Add("ID");
                Company.Columns.Add("CompanyName");
                Company.Columns.Add("City");
                Company.Columns.Add("Reg.No");
                Company.Rows.Add(new object[] { 1, "RSM", "Singapore","Red123" });
                //Company.Rows.Add(new object[] { 2, "TCS", "India", "Red456" });
                //Company.Rows.Add(new object[] { 3, "Infosys", "India", "Red789" });
                ds.Tables.Add(Company);


                DataTable Cash = new DataTable("Cash");
                Cash.Columns.Add("ID");
                Cash.Columns.Add("Text");
                Cash.Rows.Add(new object[] { 1, "Yes" });
                ds.Tables.Add(Cash);

                DataTable director = new DataTable("Director");
                director.Columns.Add("ID");
                director.Columns.Add("PID");
                director.Columns.Add("Name");
                director.Columns.Add("Age");
                director.Columns.Add("City");
                director.Rows.Add(new object[] { 1, 1, "Pranay", 35, "CITY 1" });
                director.Rows.Add(new object[] { 2, 1, "Anji", 27, "CITY 2" });

                director.Rows.Add(new object[] { 3, 1, "Raj", 34, "CITY 3" });
                director.Rows.Add(new object[] { 4, 1, "Rajender", 28, "CITY4" });
                                                    
                director.Rows.Add(new object[] { 5, 1, "Siva", 23, "CITY 5" });
                director.Rows.Add(new object[] { 6, 1, "Pavan", 20, "CITY 6" });


                //director.Rows.Add(new object[] { 3, 2, "Raj", 34, "CITY 3" });
                //director.Rows.Add(new object[] { 4, 2, "Rajender", 28, "CITY4" });

                //director.Rows.Add(new object[] { 5, 3, "Siva", 23, "CITY 5" });
                //director.Rows.Add(new object[] { 6, 3, "Pavan", 20, "CITY 6" });

                ds.Tables.Add(director);

                ds.Relations.Add("CDR", ds.Tables["Company"].Columns["ID"],
                ds.Tables["Director"].Columns["PID"]);

                DataTable address = new DataTable("Address");
                address.Columns.Add("ID");
                address.Columns.Add("PID");
                address.Columns.Add("City");
                address.Columns.Add("Zip");
                address.Rows.Add(new object[] { 1, 1, "Hyd1", 123 });
                address.Rows.Add(new object[] { 2, 1, "Hyd2", 121 });


                address.Rows.Add(new object[] { 3, 2, "Guntur", 522436 });
                address.Rows.Add(new object[] { 4, 2, "Hyderabad", 123 });

                //address.Rows.Add(new object[] { 5, 3, "HYD", 123 });
                //address.Rows.Add(new object[] { 6, 3, "VZG", 456 });


                ds.Tables.Add(address);
                ds.Relations.Add("DAR", ds.Tables["Director"].Columns["ID"],
                ds.Tables["Address"].Columns["PID"]);


                DataTable phone = new DataTable("Phone");
                phone.Columns.Add("ID");
                phone.Columns.Add("PID");
                phone.Columns.Add("Number");

                phone.Rows.Add(new object[] { 1, 1, 1111 });
                phone.Rows.Add(new object[] { 2, 1, 2222 });
                phone.Rows.Add(new object[] { 3, 2, 3333 });
                phone.Rows.Add(new object[] { 4, 2, 4444 });


                phone.Rows.Add(new object[] { 5, 3, 5555 });
                phone.Rows.Add(new object[] { 6, 3, 6666 });

                phone.Rows.Add(new object[] { 7, 4, 7777 });

                ds.Tables.Add(phone);
                ds.Relations.Add("PAR", ds.Tables["Address"].Columns["ID"],
                ds.Tables["Phone"].Columns["PID"]);


                //this.GenerateSingleDocument(ds, doc, dataDir, filename1);

                //this.GenerateMultipuleDocsParentLevel(ds, "Company", doc, dataDir);
                this.GenerateMultipuleDocsFirstLevel(ds, "Company", "Director", doc, dataDir);
                //this.GenerateMultipuleDocsChildLevel(ds, "Company", "Director", "Address", doc, dataDir);


            }
            return View();
        }

        public void GenerateSingleDocument(DataSet ds, Document doc, string dataDir, string fileName)
        {
            doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;
            doc.MailMerge.ExecuteWithRegions(ds);

            this.MailMergeRepeatH(doc);

            ArrayList arrFiles = new ArrayList();
            string savedPath = fileName + "_" + 1 + ".docx";
            doc.Save(string.Format(dataDir + savedPath, 1));
            arrFiles.Add(dataDir + savedPath);


            HelperClasses.ArchiveFiles(arrFiles, "AGM");
        }


        public void GenerateMultipuleDocsParentLevel(DataSet ds, string LoopOnEntity, Document doc, string dataDir)
        {

            int counter = 1;
            ArrayList arrFiles = new ArrayList();
            ////// Loop though all records in the data source.           

            foreach (DataRow row in ds.Tables[LoopOnEntity].Rows)
            {

                //Clone the data
                DataSet dsCopy = ds.Clone();
               
                //Filter entity's data
                ds.Tables[LoopOnEntity].Select("ID='" + row["ID"] + "'").CopyToDataTable(dsCopy.Tables[LoopOnEntity], LoadOption.OverwriteChanges);

                //Loop through relations to filter child & grand-child data
                foreach (DataRelation drel in ds.Relations)
                {
                    if (drel.ParentTable.TableName == LoopOnEntity)
                    {
                        //Add the first child level data
                        ds.Tables[drel.ChildTable.TableName].Select("PID='" + row["ID"] + "'").CopyToDataTable(dsCopy.Tables[drel.ChildTable.TableName], LoadOption.OverwriteChanges);

                        //Loop to find grand-child data
                        foreach (DataRelation drel2 in ds.Relations)
                        {
                            if (drel2.ParentTable.TableName == drel.ChildTable.TableName)
                            {
                                //Since this is grand-child, loop to add data
                                foreach (DataRow rowAdd in dsCopy.Tables[drel.ChildTable.TableName].Rows)
                                {
                                    //filter phones
                                    if (Convert.ToInt32(rowAdd["PID"]) == Convert.ToInt32(row["ID"]))
                                        ds.Tables[drel2.ChildTable.TableName].Select("PID='" + rowAdd["ID"] + "'").CopyToDataTable(dsCopy.Tables[drel2.ChildTable.TableName], LoadOption.OverwriteChanges);

                                    foreach (DataRelation drel3 in ds.Relations)
                                    {
                                        if (drel3.ParentTable.TableName == drel2.ChildTable.TableName)
                                        {
                                            //Since this is grand-child, loop to add data
                                            foreach (DataRow rowAdd1 in dsCopy.Tables[drel2.ChildTable.TableName].Rows)
                                            {
                                                //filter phones
                                                if (Convert.ToInt32(rowAdd1["PID"]) == Convert.ToInt32(rowAdd["ID"]))
                                                ds.Tables[drel3.ChildTable.TableName].Select("PID='" + rowAdd1["ID"] + "'").CopyToDataTable(dsCopy.Tables[drel3.ChildTable.TableName], LoadOption.OverwriteChanges);
                                                
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Clone the template instead of loading it from disk (for speed).
                Document dstDoc = (Document)doc.Clone(true);


                //Execute mail merge with regions
                dstDoc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;
                dstDoc.MailMerge.ExecuteWithRegions(dsCopy);
                this.MailMergeRepeatH(dstDoc);

                // Save the document.
                string savedPath = "Result_" + counter + ".docx";
                dstDoc.Save(string.Format(dataDir + savedPath, counter++));
                arrFiles.Add(dataDir + savedPath);
               
                // GetFileLatest(dataDir + savedPath);
            }
            HelperClasses.ArchiveFiles(arrFiles, "AGM");
           
            
        }

        public void GenerateMultipuleDocsFirstLevel(DataSet ds,string ParentEntity, string LoopOnEntity, Document doc, string dataDir)
        {

            int counter = 1;
            ArrayList arrFiles = new ArrayList();
            ////// Loop though all records in the data source.           


            foreach (DataRow row in ds.Tables[LoopOnEntity].Rows)
            {

                //Clone the data
                DataSet dsCopy = ds.Clone();

                //Filter parent's data
                ds.Tables[ParentEntity].Select("ID IS NOT NULL").CopyToDataTable(dsCopy.Tables[ParentEntity], LoadOption.OverwriteChanges);

                //Filter entity's data
                ds.Tables[LoopOnEntity].Select("ID='" + row["ID"] + "'").CopyToDataTable(dsCopy.Tables[LoopOnEntity], LoadOption.OverwriteChanges);

                //Loop through relations to filter child & grand-child data
                foreach (DataRelation drel in ds.Relations)
                {
                    if (drel.ParentTable.TableName == LoopOnEntity)
                    {
                        //Add the first child level data
                        ds.Tables[drel.ChildTable.TableName].Select("PID='" + row["ID"] + "'").CopyToDataTable(dsCopy.Tables[drel.ChildTable.TableName], LoadOption.OverwriteChanges);

                        //Loop to find grand-child data
                        foreach (DataRelation drel2 in ds.Relations)
                        {
                            if (drel2.ParentTable.TableName == drel.ChildTable.TableName)
                            {
                                //Since this is grand-child, loop to add data
                                foreach (DataRow rowAdd in dsCopy.Tables[drel.ChildTable.TableName].Rows)
                                {
                                    if (Convert.ToInt32(rowAdd["PID"]) == Convert.ToInt32(row["ID"]))
                                        ds.Tables[drel2.ChildTable.TableName].Select("PID='" + rowAdd["ID"] + "'").CopyToDataTable(dsCopy.Tables[drel2.ChildTable.TableName], LoadOption.OverwriteChanges);
                                }
                            }
                        }
                    }
                }

                // Clone the template instead of loading it from disk (for speed).
                Document dstDoc = (Document)doc.Clone(true);


                //Execute mail merge with regions
                dstDoc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;
                dstDoc.MailMerge.ExecuteWithRegions(dsCopy);
                this.MailMergeRepeatH(dstDoc);

                // Save the document.
                string savedPath = "Result_" + counter + ".docx";
                dstDoc.Save(string.Format(dataDir + savedPath, counter++));
                arrFiles.Add(dataDir + savedPath);
                
            }
            HelperClasses.ArchiveFiles(arrFiles, "AGM");
           
        }

        public void GenerateMultipuleDocsChildLevel(DataSet ds, string GrandParentEntity, string ParentEntity, string LoopOnEntity, Document doc, string dataDir)
        {

            int counter = 1;
            ArrayList arrFiles = new ArrayList();
            ////// Loop though all records in the data source.


            foreach (DataRow row in ds.Tables[LoopOnEntity].Rows)
            {

                //Clone the data
                DataSet dsCopy = ds.Clone();

                //Filter grand parent's data
                ds.Tables[GrandParentEntity].Select("ID IS NOT NULL").CopyToDataTable(dsCopy.Tables[GrandParentEntity], LoadOption.OverwriteChanges);

                //Filter parent's data
                ds.Tables[ParentEntity].Select("ID IS NOT NULL").CopyToDataTable(dsCopy.Tables[ParentEntity], LoadOption.OverwriteChanges);

                //Filter entity's data
                ds.Tables[LoopOnEntity].Select("ID='" + row["ID"] + "'").CopyToDataTable(dsCopy.Tables[LoopOnEntity], LoadOption.OverwriteChanges);

                //Loop through relations to filter child & grand-child data
                foreach (DataRelation drel in ds.Relations)
                {
                    if (drel.ParentTable.TableName == LoopOnEntity)
                    {
                        //Add the first child level data
                        ds.Tables[drel.ChildTable.TableName].Select("PID='" + row["ID"] + "'").CopyToDataTable(dsCopy.Tables[drel.ChildTable.TableName], LoadOption.OverwriteChanges);

                        //Loop to find grand-child data
                        foreach (DataRelation drel2 in ds.Relations)
                        {
                            if (drel2.ParentTable.TableName == drel.ChildTable.TableName)
                            {
                                //Since this is grand-child, loop to add data
                                foreach (DataRow rowAdd in dsCopy.Tables[drel.ChildTable.TableName].Rows)
                                {
                                    if (Convert.ToInt32(rowAdd["PID"]) == Convert.ToInt32(row["ID"]))
                                        ds.Tables[drel2.ChildTable.TableName].Select("PID='" + rowAdd["ID"] + "'").CopyToDataTable(dsCopy.Tables[drel2.ChildTable.TableName], LoadOption.OverwriteChanges);
                                }
                            }
                        }
                    }
                }

                // Clone the template instead of loading it from disk (for speed).
                Document dstDoc = (Document)doc.Clone(true);


                //Execute mail merge with regions
                dstDoc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;
                dstDoc.MailMerge.ExecuteWithRegions(dsCopy);
                this.MailMergeRepeatH(dstDoc);

                // Save the document.
                //string savedPath = "Result_" + counter + "_" + System.DateTime.Now.Ticks + ".docx";
                string savedPath = "Result_" + counter +".docx";
                dstDoc.Save(string.Format(dataDir + savedPath, counter++));
                arrFiles.Add(dataDir + savedPath);
                // GetFileLatest(dataDir + savedPath);

            }
            HelperClasses.ArchiveFiles(arrFiles,"AGM");
           
        }
             

        #region CustomLogicOnEmptyRegions
        /// <summary>
        /// Applies logic defined in the passed handler class to all unused regions in the document. This allows to manually control
        /// how unused regions are handled in the document.
        /// </summary>
        /// <param name="doc">The document containing unused regions</param>
        /// <param name="handler">The handler which implements the IFieldMergingCallback interface and defines the logic to be applied to each unmerged region.</param>
        public static void ExecuteCustomLogicOnEmptyRegions(Document doc, IFieldMergingCallback handler)
        {
            ExecuteCustomLogicOnEmptyRegions(doc, handler, null); // Pass null to handle all regions found in the document.
        }

        /// <summary>
        /// Applies logic defined in the passed handler class to specific unused regions in the document as defined in regionsList. This allows to manually control
        /// how unused regions are handled in the document.
        /// </summary>
        /// <param name="doc">The document containing unused regions</param>
        /// <param name="handler">The handler which implements the IFieldMergingCallback interface and defines the logic to be applied to each unmerged region.</param>
        /// <param name="regionsList">A list of strings corresponding to the region names that are to be handled by the supplied handler class. Other regions encountered will not be handled and are removed automatically.</param>
        public static void ExecuteCustomLogicOnEmptyRegions(Document doc, IFieldMergingCallback handler, ArrayList regionsList)
        {
            // Certain regions can be skipped from applying logic to by not adding the table name inside the CreateEmptyDataSource method.
            // Enable this cleanup option so any regions which are not handled by the user's logic are removed automatically.
            doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;

            // Set the user's handler which is called for each unmerged region.
            doc.MailMerge.FieldMergingCallback = handler;

            // Execute mail merge using the dummy dataset. The dummy data source contains the table names of 
            // each unmerged region in the document (excluding ones that the user may have specified to be skipped). This will allow the handler 
            // to be called for each field in the unmerged regions.
            doc.MailMerge.ExecuteWithRegions(CreateDataSourceFromDocumentRegions(doc, regionsList));
        }

        /// <summary>
        /// Returns a DataSet object containing a DataTable for the unmerged regions in the specified document.
        /// If regionsList is null all regions found within the document are included. If an ArrayList instance is present
        /// the only the regions specified in the list that are found in the document are added.
        /// </summary>
        private static DataSet CreateDataSourceFromDocumentRegions(Document doc, ArrayList regionsList)
        {
            const string tableStartMarker = "TableStart:";
            DataSet dataSet = new DataSet();
            string tableName = null;

            foreach (string fieldName in doc.MailMerge.GetFieldNames())
            {
                if (fieldName.Contains(tableStartMarker))
                {
                    tableName = fieldName.Substring(tableStartMarker.Length);
                }
                else if (tableName != null)
                {
                    // Only add the table name as a new DataTable if it doesn't already exists in the DataSet.
                    if (dataSet.Tables[tableName] == null)
                    {
                        DataTable table = new DataTable(tableName);
                        table.Columns.Add(fieldName);

                        // We only need to add the first field for the handler to be called for the fields in the region.
                        if (regionsList == null || regionsList.Contains(tableName))
                        {
                            table.Rows.Add("FirstField");
                        }

                        dataSet.Tables.Add(table);
                    }
                    tableName = null;
                }
            }

            return dataSet;
        }
        #endregion

        #region Get File
        public void GetFileLatest(string filePath)
        {
            try
            {

                var context = ControllerContext.HttpContext;
                //var filePath = context.Server.MapPath("~/DocTemplates/" + filename);
                var file = new System.IO.FileInfo(filePath);
                switch (file.Extension)
                {
                    case ".pdf":
                        context.Response.ContentType = "application/pdf";
                        break;
                    case ".doc":
                        context.Response.ContentType = "application/msword";
                        break;
                    case ".docx":
                        context.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        break;
                    case ".txt":
                        context.Response.ContentType = "text/plain";
                        break;
                    case ".xlsx":
                    case ".xls":
                        context.Response.Buffer = true;
                        context.Response.ContentType = "application/x-msdownload";
                        context.Response.StatusCode = 200;
                        break;
                    case ".jpg":
                    case ".jpeg":
                        context.Response.ContentType = "image/jpg";
                        break;
                    case ".csv":
                        context.Response.ContentType = "application/csv";
                        break;
                    case ".ics":
                        context.Response.ContentType = "text/calendar";
                        break;

                }

                Response.AppendHeader("content-disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath));
                context.Response.TransmitFile(filePath);
                ControllerContext.HttpContext.ApplicationInstance.CompleteRequest();
                context.Response.End();

            }
            catch
            {
            }
        }

        public DataTable ConvertToDatatable<T>(List<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
        #endregion

        #region Repeat H
        public void MailMergeRepeatH(Document doc)
        {
            DocumentBuilder mBuilder = new DocumentBuilder(doc);
            NodeCollection arTables = mBuilder.Document.GetChildNodes(NodeType.Table, true);

            foreach (Table tbl in arTables)
            {
                //get @Horizontal tables
                if (tbl.Rows.Count > 1 && tbl.Rows[0].Cells[0].Range.Text.Contains("@Horizontal"))
                {

                    //get table index
                    int iTblIndex = arTables.IndexOf(tbl);

                    //get row count
                    int iRows = tbl.Rows.Count;

                    //populate arrays
                    ArrayList arEven = GetEven(iRows);
                    ArrayList arOdd = GetOdd(iRows);

                    //copy cell data
                    foreach (int iIndex in arOdd)
                    {
                        //original code to copy text - but does not copy the format
                        //mBuilder.MoveToCell(iTblIndex, iIndex - 1, 1, 0);
                        //mBuilder.Write(tbl.Rows[iIndex].Cells[0].Range.Text);

                        //new code to copy the cell node. This copies the formatting too
                        tbl.Rows[iIndex - 1].Cells.RemoveAt(1);
                        tbl.Rows[iIndex - 1].AppendChild(tbl.Rows[iIndex].Cells[0]);
                    }

                    //delete odd rows
                    int iIncrement = 0;
                    foreach (int iIndex in arOdd)
                    {
                        tbl.Rows.RemoveAt(iIndex - iIncrement++);
                    }
                }
            }

            mBuilder.Document.Range.Replace("@Horizontal", "", false, false);

        }

        public ArrayList GetEven(int iCount)
        {
            ArrayList arEven = new ArrayList();

            for (int i = 0; i < iCount; i++)
            {

                if (i % 2 == 0) { arEven.Add(i); }
            }
            return arEven;
        }

        public ArrayList GetOdd(int iCount)
        {
            ArrayList arOdd = new ArrayList();

            for (int i = 0; i < iCount; i++)
            {

                if (i % 2 != 0) { arOdd.Add(i); }
            }
            return arOdd;
        }
        #endregion

        #endregion
    }  
}