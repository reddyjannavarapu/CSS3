
# region Document Header
//Created By       : Anji 
//Created Date     : 12 August 2014
//Description      : Document Generate Logic
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

namespace CSS2.Areas.TM.Models
{
    #region Usings
    using Aspose.Words;
    using Aspose.Words.Reporting;
    using Aspose.Words.Tables;
    using CSS2.Areas.WO.Models;
    using CSS2.Models;
    using log4net;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Web;
    #endregion

    public class DocumentGenerate
    {
        private static ILog log = LogManager.GetLogger(typeof(DocumentGenerate));

        public void GenrateDocuments(string woCode, int woID, string strFileIDs)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                //Aspose.Words.License license = new Aspose.Words.License();
                //license.SetLicense("Aspose.Words.lic");

                var filesList = WOTemplateFileDetails.GetDocumentFiles(strFileIDs);
                string templateDir = filesList[0].FilePath + "/";

                DataSet dsData = this.GetDocumentDataDetails(woID, woCode);

                if (woCode == Enum.GetName(typeof(WOType), (int)WOType.AGM))
                {
                    dsData = this.PrepareDatasetForAGM(dsData);
                }
                else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.INCO))
                {
                    dsData = this.PrepareDatasetForIncorp(dsData);
                }
                else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.ALLT))
                {
                    dsData = this.PrepareDatasetForAllotment(dsData);
                }

                else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.APPA))
                {
                    dsData = this.PrepareDatasetForAppointmentCessationofAuditors(dsData);
                }
                else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.APPO))
                {
                    dsData = this.PrepareDatasetForAppointmentofOfficer(dsData);
                }

                else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.CESO))
                {
                    dsData = this.PrepareDatasetForCessationofOfficer(dsData);
                }
                else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.INTD))
                {
                    dsData = this.PrepareDatasetForInterimDividend(dsData);
                }

                else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.TRAN))
                {
                    dsData = this.PrepareDatasetForTransfer(dsData);
                }
                else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.DUPL))
                {
                    dsData = this.PrepareDatasetForDuplicate(dsData);
                }
                else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.ECEN))
                {
                    dsData = this.PrepareDatasetForExistingClientEngaging(dsData);
                }
                else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.TAKE))
                {
                    dsData = this.PrepareDatasetForTakeover(dsData);
                }
                else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.EGMA))
                {
                    dsData = this.PrepareDatasetForEGMAcquisitionDisposalofProperty(dsData);
                }
                else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.EGMC))
                {
                    dsData = this.PrepareDatasetForEGMChangeofName(dsData);
                }
                else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.BOIS))
                {
                    dsData = this.PrepareDatasetForBonusIssue(dsData);
                }
                else
                {
                    dsData = this.PrepareDatasetForTypeA(dsData);
                }

                // Genrating Files
                dsData = this.DeleteEmptyTables(dsData);
                this.GetGenratedZipFiles(woID, dsData, filesList, templateDir, woCode);

            }
            catch (Exception ex)
            {

                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);

        }

        #region Common Code

        #region Files

        public void GetGenratedZipFiles(int WOID, DataSet dsData, List<WOTemplateFileDetails> arrFiles, string TemplateDirectory, string zipFloderName)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);

            ArrayList arrGenratedFiles = new ArrayList();
            List<Document> lstGenratedFiles = new List<Document>();

            var context = HttpContext.Current;
            string TemplateDir = context.Server.MapPath(TemplateDirectory);
            string GenratedDocsDir = context.Server.MapPath("~/DocTemplates/GenratedDocs/");
            string ReadMeFile = context.Server.MapPath("~/DocTemplates/ReadMe.txt");

            foreach (var file in arrFiles)
            {
                try
                {
                    string filename = file.FileName;


                    if (Path.GetExtension(Path.GetFullPath(context.Server.MapPath(filename))) == ".docx" || Path.GetExtension(Path.GetFullPath(context.Server.MapPath(filename))) == ".doc")
                    {
                        string exeDir = Path.GetDirectoryName(Path.GetFullPath(context.Server.MapPath(filename))) + Path.DirectorySeparatorChar;

                        Document doc = new Document(TemplateDir + filename);

                        //code with doc returned
                        //lstGenratedFiles.Add(this.GenerateSingleDocument(dsData, doc));                    

                        //code with filename returned 
                        if (!file.IsMultiple)
                        {
                            arrGenratedFiles.Add(this.GenerateSingleDocumentFile(dsData, doc, GenratedDocsDir, filename, WOID));
                        }
                        else if (file.IsMultiple)
                        {
                            ArrayList filenames = this.GenerateMultipuleDocsFirstLevel(dsData, "Company", file.MultipleEntity, doc, GenratedDocsDir, filename, WOID);
                            foreach (var genfilepath in filenames)
                                arrGenratedFiles.Add(genfilepath);
                        }

                        //arrGenratedFiles.Add(this.GenerateMultipuleDocsParentLevel(dsData, "Company ", doc, dataDir, filename));

                    }
                }
                catch (Exception ex)
                {
                    log.Error("Error: " + ex);
                }
            }

            try
            {
                //Parallel.ForEach(arrFiles.ToArray(), file =>
                //{
                //    string filename = file.ToString();


                //    if (Path.GetExtension(Path.GetFullPath(context.Server.MapPath(filename))) == ".docx" || Path.GetExtension(Path.GetFullPath(context.Server.MapPath(filename))) == ".doc")
                //    {
                //        string exeDir = Path.GetDirectoryName(Path.GetFullPath(context.Server.MapPath(filename))) + Path.DirectorySeparatorChar;


                //        Document doc = new Document(dataDir + filename);

                //        //code with doc returned
                //        lstGenratedFiles.Add(this.GenerateSingleDocument(dsData, doc, dataDir, filename));                   

                //        //code with filename returned
                //        arrGenratedFiles.Add(this.GenerateSingleDocumentFile(dsData, doc, dataDir, filename));

                //    }
                //});
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }


            //new code with doc list
            //if (lstGenratedFiles.Count > 0)
            //    HelperClasses.ArchiveFiles(lstGenratedFiles);

            //old code with filenames
            if (arrGenratedFiles.Count > 0)
            {
                ArrayList arrFinalGenratedFiles = new ArrayList();
                foreach (var genfilepath in arrGenratedFiles)
                {
                    if (!string.IsNullOrEmpty(genfilepath.ToString()))
                        arrFinalGenratedFiles.Add(genfilepath);
                }
                if (arrFinalGenratedFiles.Count == 0)
                {
                    arrFinalGenratedFiles.Add(ReadMeFile);
                }
                HelperClasses.ArchiveFiles(arrFinalGenratedFiles, zipFloderName);
            }
        }

        public DataSet DeleteEmptyTables(DataSet dsData)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                //foreach (DataTable dt in dsData.Tables)
                //{
                //    if (dt.Rows.Count <= 0)
                //    {
                //        string tablename = dt.TableName;
                //        if (dsData.Tables[tablename] != null)
                //        {
                //            for (int f = dsData.Tables[tablename].ChildRelations.Count - 1; f >= 0; f--)
                //            {
                //                dsData.Tables[tablename].ChildRelations[f].ChildTable.Constraints.Remove(dsData.Tables[tablename].ChildRelations[f].RelationName);
                //                dsData.Tables[tablename].ChildRelations.RemoveAt(f);
                //            }
                //            dsData.Tables[tablename].ChildRelations.Clear();
                //            dsData.Tables[tablename].ParentRelations.Clear();
                //            dsData.Tables[tablename].Constraints.Clear();
                //            dsData.Tables.Remove(tablename);
                //        }
                //    }
                //}

                int currentTablesCount = dsData.Tables.Count;
                for (int currentTableIndex = 0; currentTableIndex < currentTablesCount; currentTableIndex++)
                {
                    if (dsData.Tables[currentTableIndex].Rows.Count <= 0)
                    {
                        for (int f = dsData.Tables[currentTableIndex].ChildRelations.Count - 1; f >= 0; f--)
                        {
                            dsData.Tables[currentTableIndex].ChildRelations[f].ChildTable.Constraints.Remove(dsData.Tables[currentTableIndex].ChildRelations[f].RelationName);
                            dsData.Tables[currentTableIndex].ChildRelations.RemoveAt(f);
                        }
                        dsData.Tables[currentTableIndex].ChildRelations.Clear();
                        dsData.Tables[currentTableIndex].ParentRelations.Clear();
                        dsData.Tables[currentTableIndex].Constraints.Clear();
                        dsData.Tables.Remove(dsData.Tables[currentTableIndex].TableName);
                        currentTablesCount--;
                        currentTableIndex--;
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            return dsData;
        }
        #endregion

        #region Genrate Single Documents

        private string GenerateSingleDocumentFile(DataSet ds, Document doc, string dataDir, string fileName, int woid)
        {
            string filePath = string.Empty;

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedFields;
                doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveEmptyParagraphs;
                doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;

                doc.MailMerge.ExecuteWithRegions(ds);

                this.MailMergeRepeatH(doc);
                this.MailMergeRepeatRemove(doc);

                string savedPath = fileName + "_" + woid.ToString() + ".docx";
                doc.Save(string.Format(dataDir + savedPath));
                filePath = dataDir + savedPath;

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);

            return filePath;
        }

        private Document GenerateSingleDocument(DataSet ds, Document doc)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedFields;
                doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveEmptyParagraphs;
                doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;

                doc.MailMerge.ExecuteWithRegions(ds);

                this.MailMergeRepeatH(doc);
                this.MailMergeRepeatRemove(doc);

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);


            return doc;
        }

        #endregion

        #region Genrate Multipule Documents
        private ArrayList GenerateMultipuleDocsParentLevel(DataSet ds, string LoopOnEntity, Document doc, string dataDir, string fileName, int woid)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);

            int counter = 1;
            ArrayList arrFiles = new ArrayList();
            string filePath = string.Empty;
            ////// Loop though all records in the data source.           
            try
            {
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

                    doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedFields;
                    doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveEmptyParagraphs;
                    doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;

                    dstDoc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;
                    dstDoc.MailMerge.ExecuteWithRegions(dsCopy);
                    this.MailMergeRepeatH(dstDoc);
                    this.MailMergeRepeatRemove(dstDoc);

                    // Save the document.
                    string savedPath = fileName + "_" + counter + "_" + woid.ToString() + ".docx";
                    dstDoc.Save(string.Format(dataDir + savedPath, counter++));
                    arrFiles.Add(dataDir + savedPath);

                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            return arrFiles;
        }

        private ArrayList GenerateMultipuleDocsFirstLevel(DataSet ds, string ParentEntity, string LoopOnEntity, Document doc, string dataDir, string fileName, int woid)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);

            string filePath = string.Empty;
            int counter = 1;
            ArrayList arrFiles = new ArrayList();
            ////// Loop though all records in the data source.           
            try
            {

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

                    doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedFields;
                    doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveEmptyParagraphs;
                    doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;

                    dstDoc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;
                    dstDoc.MailMerge.ExecuteWithRegions(dsCopy);
                    this.MailMergeRepeatH(dstDoc);
                    this.MailMergeRepeatRemove(dstDoc);

                    // Save the document.
                    string savedPath = fileName + "_" + counter + "_" + woid.ToString() + ".docx";
                    dstDoc.Save(string.Format(dataDir + savedPath, counter++));
                    arrFiles.Add(dataDir + savedPath);
                    filePath = dataDir + savedPath;

                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            return arrFiles;
        }

        private ArrayList GenerateMultipuleDocsChildLevel(DataSet ds, string GrandParentEntity, string ParentEntity, string LoopOnEntity, Document doc, string dataDir, string fileName, int woid)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);

            int counter = 1;
            string filePath = string.Empty;
            ArrayList arrFiles = new ArrayList();

            try
            {
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

                    doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedFields;
                    doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveEmptyParagraphs;
                    doc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;

                    dstDoc.MailMerge.CleanupOptions = MailMergeCleanupOptions.RemoveUnusedRegions;
                    dstDoc.MailMerge.ExecuteWithRegions(dsCopy);
                    this.MailMergeRepeatH(dstDoc);
                    this.MailMergeRepeatRemove(doc);

                    // Save the document.
                    //string savedPath = "Result_" + counter + "_" + System.DateTime.Now.Ticks + ".docx";
                    string savedPath = fileName + "_" + counter + "_" + woid.ToString() + ".docx";
                    dstDoc.Save(string.Format(dataDir + savedPath, counter++));
                    arrFiles.Add(dataDir + savedPath);
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            log.Debug("End: " + methodBase.Name);

            return arrFiles;
        }

        #endregion


        #region Repeat H
        private void MailMergeRepeatH(Document doc)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                DocumentBuilder mBuilder = new DocumentBuilder(doc);
                NodeCollection arTables = mBuilder.Document.GetChildNodes(NodeType.Table, true);

                foreach (Table tbl in arTables)
                {
                    //get @Horizontal tables
                    if (tbl.Rows.Count > 1 && tbl.Rows[0].Cells[0].Range.Text.Contains("@Horizontal"))
                    {

                        //get table index
                        //int iTblIndex = arTables.IndexOf(tbl);

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
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);

        }

        private ArrayList GetEven(int iCount)
        {
            ArrayList arEven = new ArrayList();

            for (int i = 0; i < iCount; i++)
            {

                if (i % 2 == 0) { arEven.Add(i); }
            }
            return arEven;
        }

        private ArrayList GetOdd(int iCount)
        {
            ArrayList arOdd = new ArrayList();

            for (int i = 0; i < iCount; i++)
            {

                if (i % 2 != 0) { arOdd.Add(i); }
            }
            return arOdd;
        }

        private ArrayList GetALL(int iCount)
        {
            ArrayList arALL = new ArrayList();

            for (int i = 0; i < iCount; i++)
            {
                arALL.Add(i);
            }
            return arALL;
        }

        private void MailMergeRepeatRemove(Document doc)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                DocumentBuilder mBuilder = new DocumentBuilder(doc);
                NodeCollection arTables = mBuilder.Document.GetChildNodes(NodeType.Table, true);

                foreach (Table tbl in arTables)
                {
                    //get @RepeatRemove tables
                    if (tbl.Rows.Count > 1 && tbl.Rows[0].Cells[0].Range.Text.Contains("RepeatRemove") || tbl.Rows.Count > 1 && tbl.Rows[0].Cells[0].Range.Text.Contains("@RepeatRemove"))
                    {

                        //get row count
                        int iRows = tbl.Rows.Count;

                        //populate arrays                       
                        ArrayList arALL = GetALL(iRows);

                        //copy cell data

                        //for (int i = 1; i < arALL.Count; i++)
                        //{
                        //    foreach (int iIndex in arALL)
                        //    {
                        //        if (tbl.Rows[iIndex].Cells[0].Range.Text == tbl.Rows[iIndex - i].Cells[0].Range.Text)
                        //        {
                        //            tbl.Rows[iIndex].Cells[0].Range.Replace(new Regex(@"[\w ]*"), "");
                        //        }
                        //    }
                        //}

                        for (int i = 0; i < arALL.Count; i++)
                        {
                            for (int j = 0; j < arALL.Count; j++)
                            {
                                if (tbl.Rows[i].Cells[0].Range.Text == tbl.Rows[j].Cells[0].Range.Text && i != j)
                                {
                                    tbl.Rows[j].Cells[0].Range.Replace(new Regex(@"[\w ]*"), "");
                                }
                            }
                        }

                    }
                }

                foreach (Table tbl in arTables)
                {
                    //get @DuplicateRow tables
                    if (tbl.Rows.Count > 1 && tbl.Rows[1].Cells[0].Range.Text.Contains("DuplicateRow") || tbl.Rows.Count > 1 && tbl.Rows[1].Cells[0].Range.Text.Contains("@DuplicateRow"))
                    {

                        //get row count
                        int iRows = tbl.Rows.Count;

                        //populate arrays                       
                        ArrayList arALL = GetALL(iRows);

                        //copy cell data
                        for (int i = 1; i < arALL.Count; i++)
                        {
                            foreach (int iIndex in arALL)
                            {
                                if (tbl.Rows[iIndex].Cells[0].Range.Text == tbl.Rows[iIndex - i].Cells[0].Range.Text)
                                {
                                    tbl.Rows[iIndex].Cells[0].Range.Replace(new Regex(@"[\w ]*"), "");
                                }
                            }
                        }

                    }
                }

                mBuilder.Document.Range.Replace("@DuplicateRow", "", false, false);
                mBuilder.Document.Range.Replace("DuplicateRow", "", false, false);

                mBuilder.Document.Range.Replace("@RepeatRemove", "", false, false);
                mBuilder.Document.Range.Replace("RepeatRemove", "", false, false);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);

        }



        #endregion

        #region List To Table

        private DataTable ConvertToDatatable<T>(List<T> data)
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

        #endregion

        #region Documents
        private DataSet PrepareDatasetForAGM(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {

                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "ShareholdersDividend";
                            dsData.Tables[2].TableName = "Auditors";
                            dsData.Tables[3].TableName = "ListOfActiveChargeNo";
                            dsData.Tables[4].TableName = "Director";
                            dsData.Tables[5].TableName = "AlternateDirector";

                            dsData.Tables[6].TableName = "DividendTrue";
                            dsData.Tables[7].TableName = "DividendFalse";

                            dsData.Tables[8].TableName = "DirectorsFeeAmountTrue";
                            dsData.Tables[9].TableName = "DirectorsFeeAmountFalse";

                            dsData.Tables[10].TableName = "RemunerationAmountTrue";
                            dsData.Tables[11].TableName = "RemunerationAmountFalse";

                            dsData.Tables[12].TableName = "S161toissuesharesTrue";
                            dsData.Tables[13].TableName = "S161toissuesharesFalse";

                            dsData.Tables[14].TableName = "AuditorsTrue";
                            dsData.Tables[15].TableName = "AuditorsFalse";

                            dsData.Tables[16].TableName = "AuditorsReappointmentTrue";
                            dsData.Tables[17].TableName = "AuditorsReappointmentFalse";

                            dsData.Tables[18].TableName = "AuditorsRetirementTrue";
                            dsData.Tables[19].TableName = "AuditorsRetirementFalse";

                            dsData.Tables[20].TableName = "DirectorsDueForReelectionTrue";
                            dsData.Tables[21].TableName = "DirectorsDueForReelectionFalse";


                            dsData.Tables[22].TableName = "DirectorsDueForReelectionunsort";

                            dsData.Tables[23].TableName = "ApprovalFSTrue";
                            dsData.Tables[24].TableName = "ApprovalFSFalse";

                            dsData.Tables[25].TableName = "Shareholders";

                            dsData.Tables[26].TableName = "Members";


                            if (dsData.Tables[22].Rows.Count > 0)
                            {
                                DataView dv = dsData.Tables["DirectorsDueForReelectionunsort"].DefaultView;
                                dv.Sort = "ArticleNumber asc";
                                DataTable sortedDT = dv.ToTable();
                                sortedDT.TableName = "DirectorsDueForReelection";
                                dsData.Tables.Add(sortedDT);

                            }


                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CDR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Director"].Columns["PID"]);

                            dsData.Relations.Add("DADR", dsData.Tables["Director"].Columns["ID"],
                            dsData.Tables["AlternateDirector"].Columns["PID"]);

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {

                            if ((dsData.Tables["DividendTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["DividendTrue"].Rows[0]["IsDividend"]) == 0))
                                dsData.Tables.Remove("DividendTrue");
                            else
                                dsData.Tables.Remove("DividendFalse");


                            if (dsData.Tables["DirectorsFeeAmountTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["DirectorsFeeAmountTrue"].Rows[0]["isDirectorsFeeamount"]) == 0)
                            {
                                dsData.Tables.Remove("DirectorsFeeAmountTrue");
                            }
                            else
                                dsData.Tables.Remove("DirectorsFeeAmountFalse");

                            if (dsData.Tables["RemunerationAmountTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["RemunerationAmountTrue"].Rows[0]["isremunerationamount"]) == 0)
                            {
                                dsData.Tables.Remove("RemunerationAmountTrue");
                            }
                            else
                                dsData.Tables.Remove("RemunerationAmountFalse");

                            if (dsData.Tables["S161toissuesharesTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["S161toissuesharesTrue"].Rows[0]["iss161toissueshares"]) == 0)
                            {
                                dsData.Tables.Remove("S161toissuesharesTrue");
                            }
                            else
                                dsData.Tables.Remove("S161toissuesharesFalse");

                            if (dsData.Tables["AuditorsTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["AuditorsTrue"].Rows[0]["isAuditor"]) == 0)
                            {
                                dsData.Tables.Remove("AuditorsReappointmentTrue");
                                dsData.Tables.Remove("AuditorsRetirementTrue");
                            }
                            else
                            {
                                if (Convert.ToInt32(dsData.Tables["AuditorsTrue"].Rows[0]["Auditors"]) == 1)
                                {
                                    dsData.Tables.Remove("AuditorsRetirementTrue");
                                }
                                else if (Convert.ToInt32(dsData.Tables["AuditorsTrue"].Rows[0]["Auditors"]) == 2)
                                {

                                    dsData.Tables.Remove("AuditorsReappointmentTrue");
                                }
                            }


                            if (dsData.Tables["DirectorsDueForReelectionTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["DirectorsDueForReelectionTrue"].Rows[0]["isDirectorsDueForRetirement"]) == 0)
                            {
                                dsData.Tables.Remove("DirectorsDueForReelectionTrue");
                            }
                            else
                                dsData.Tables.Remove("DirectorsDueForReelectionFalse");

                            if ((dsData.Tables["ApprovalFSTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["ApprovalFSTrue"].Rows[0]["IsApprovalFS"]) == 0))
                                dsData.Tables.Remove("ApprovalFSTrue");
                            else
                                dsData.Tables.Remove("ApprovalFSFalse");

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForIncorp(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {

                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "FMGasRegisteredAddressTrue";
                            dsData.Tables[2].TableName = "FMGasRegisteredAddressFalse";
                            dsData.Tables[3].TableName = "FirstDirectorsDetails";
                            dsData.Tables[4].TableName = "NomineeDirectorsDetails";

                            string[] ColumnNamesINCNDD = { "Fee due to Nominee", "No.ofSharesheldintrust" };
                            DataTable dtINCNDD = HelperClasses.GetCamaSeparetedValue(dsData.Tables[4], ColumnNamesINCNDD);
                            dsData.Merge(dtINCNDD);

                            dsData.Tables[5].TableName = "CompanySecretaryDetails";
                            dsData.Tables[6].TableName = "NomineeSecretaryDetails";

                            string[] ColumnNamesINCNSD = { "FeeduetoNominee" };
                            DataTable dtINCNSD = HelperClasses.GetCamaSeparetedValue(dsData.Tables[6], ColumnNamesINCNSD);
                            dsData.Merge(dtINCNSD);

                            dsData.Tables[7].TableName = "FirstSubscribers";
                            dsData.Tables[8].TableName = "FirstSubscribersI";
                            dsData.Tables[9].TableName = "FirstSubscribersC";
                            dsData.Tables[10].TableName = "AuthorizedPersonCorporateSubscriberDetails";
                            dsData.Tables[11].TableName = "PrincipalDetails";
                            dsData.Tables[12].TableName = "AuthorizedPersonPrincipalDetails";
                            dsData.Tables[13].TableName = "MemberDetailsforCompanylimitedbyGuarantee";

                            string[] ColumnNamesMDFCLBG = { "FeeduetoNominee" };
                            DataTable dtMDFCLBG = HelperClasses.GetCamaSeparetedValue(dsData.Tables[13], ColumnNamesMDFCLBG);
                            dsData.Merge(dtMDFCLBG);


                            dsData.Tables[14].TableName = "SigningDirectorsNomineeDirectorSvcAgt";
                            dsData.Tables[15].TableName = "SigningDirectorsNomineeSecSvcAgt";

                            DataTable SigningDirectors = dsData.Tables["FirstDirectorsDetails"].Clone();
                            SigningDirectors.TableName = "SigningDirectors";
                            dsData.Tables.Add(SigningDirectors);

                            dsData.Tables["FirstDirectorsDetails"].Select("IsSigningDirector=1").CopyToDataTable(dsData.Tables["SigningDirectors"], LoadOption.OverwriteChanges);

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            //Modified Relation based on 13th Nov 2014 Documents

                            dsData.Relations.Add("CDR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["FirstSubscribersC"].Columns["PID"]);

                            dsData.Relations.Add("PDR", dsData.Tables["FirstSubscribersC"].Columns["ID"],
                            dsData.Tables["AuthorizedPersonCorporateSubscriberDetails"].Columns["PID"]);

                            dsData.Relations.Add("CPR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["PrincipalDetails"].Columns["PID"]);

                            dsData.Relations.Add("APDR", dsData.Tables["PrincipalDetails"].Columns["ID"],
                            dsData.Tables["AuthorizedPersonPrincipalDetails"].Columns["PID"]);

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {
                            if ((dsData.Tables["FMGasRegisteredAddressTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["FMGasRegisteredAddressTrue"].Rows[0]["FMGasRegisteredAddress"]) == 0))
                                dsData.Tables.Remove("FMGasRegisteredAddressTrue");
                            else
                                dsData.Tables.Remove("FMGasRegisteredAddressFalse");
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForAllotment(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {
                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "RoPlaceOfMeetingTrue";
                            dsData.Tables[2].TableName = "RoPlaceOfMeetingFalse";
                            dsData.Tables[3].TableName = "AllotteeDetails";

                            string[] ColumnNames = { "Consideration", "Noofsharesheld" };
                            DataTable dt = HelperClasses.GetCamaSeparetedValue(dsData.Tables[3], ColumnNames);
                            dsData.Merge(dt);

                            dsData.Tables[4].TableName = "Director";
                            dsData.Tables[5].TableName = "AlternateDirector";
                            dsData.Tables[6].TableName = "Shareholders";


                            DataTable AllotteeDetailsC = dsData.Tables["AllotteeDetails"].Clone();
                            AllotteeDetailsC.TableName = "AllotteeDetailsC";
                            dsData.Tables.Add(AllotteeDetailsC);

                            DataTable AllotteeDetailsI = dsData.Tables["AllotteeDetails"].Clone();
                            AllotteeDetailsI.TableName = "AllotteeDetailsI";
                            dsData.Tables.Add(AllotteeDetailsI);

                            dsData.Tables["AllotteeDetails"].Select("PersonSource='CSS1C' OR PersonSource='CSS1NC' OR PersonSource='CSS2C'").CopyToDataTable(dsData.Tables["AllotteeDetailsC"], LoadOption.OverwriteChanges);
                            dsData.Tables["AllotteeDetails"].Select("PersonSource='CSS1I' OR PersonSource='CSS2I'").CopyToDataTable(dsData.Tables["AllotteeDetailsI"], LoadOption.OverwriteChanges);

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CDR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Director"].Columns["PID"]);

                            dsData.Relations.Add("DADR", dsData.Tables["Director"].Columns["ID"],
                            dsData.Tables["AlternateDirector"].Columns["PID"]);

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {
                            if ((dsData.Tables["RoPlaceOfMeetingTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["RoPlaceOfMeetingTrue"].Rows[0]["isRoPlaceOfMeeting"]) == 0))
                                dsData.Tables.Remove("RoPlaceOfMeetingTrue");
                            else
                                dsData.Tables.Remove("RoPlaceOfMeetingFalse");
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForTransfer(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {

                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "isPreEmptionRightsTrue";
                            dsData.Tables[2].TableName = "isPreEmptionRightsFalse";
                            dsData.Tables[3].TableName = "TransferDetails";
                            dsData.Tables[4].TableName = "Director";
                            dsData.Tables[5].TableName = "AlternateDirector";
                            dsData.Tables[6].TableName = "Shareholders";

                            dsData.Tables[7].TableName = "WaiverShareholders";
                            dsData.Tables[8].TableName = "TransferWaiverDetails";
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CAOR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Director"].Columns["PID"]);

                            dsData.Relations.Add("DADR", dsData.Tables["Director"].Columns["ID"],
                            dsData.Tables["AlternateDirector"].Columns["PID"]);


                            dsData.Relations.Add("WACD", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["WaiverShareholders"].Columns["PID"]);

                            dsData.Relations.Add("TWDR", dsData.Tables["WaiverShareholders"].Columns["ID"],
                            dsData.Tables["TransferWaiverDetails"].Columns["PID"]);


                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {
                            if ((dsData.Tables["isPreEmptionRightsTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["isPreEmptionRightsTrue"].Rows[0]["IsPreEmptionRights"]) == 0))
                                dsData.Tables.Remove("isPreEmptionRightsTrue");
                            else
                                dsData.Tables.Remove("isPreEmptionRightsFalse");
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForAppointmentofOfficer(DataSet dsData)
        {
            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {

                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "AppointmentofOfficer";
                            dsData.Tables[2].TableName = "AppointmentOfficerAltOrMainDirectorDetails";
                            dsData.Tables[3].TableName = "Director";
                            dsData.Tables[4].TableName = "AlternateDirector";
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CAOR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Director"].Columns["PID"]);

                            dsData.Relations.Add("DADR", dsData.Tables["Director"].Columns["ID"],
                            dsData.Tables["AlternateDirector"].Columns["PID"]);

                            dsData.Relations.Add("APOR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["AppointmentofOfficer"].Columns["PID"]);

                            dsData.Relations.Add("APDR", dsData.Tables["AppointmentofOfficer"].Columns["ID"],
                            dsData.Tables["AppointmentOfficerAltOrMainDirectorDetails"].Columns["PID"]);

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {



                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForCessationofOfficer(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {

                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "CessationOfficerDetails";
                            dsData.Tables[2].TableName = "CessationOfficerAltOrMainDirectorDetails";
                            dsData.Tables[3].TableName = "Director";
                            dsData.Tables[4].TableName = "AlternateDirector";
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CCOR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Director"].Columns["PID"]);


                            dsData.Relations.Add("DADR", dsData.Tables["Director"].Columns["ID"],
                            dsData.Tables["AlternateDirector"].Columns["PID"]);

                            dsData.Relations.Add("CCOD", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["CessationOfficerDetails"].Columns["PID"]);


                            dsData.Relations.Add("DADA", dsData.Tables["CessationOfficerDetails"].Columns["ID"],
                            dsData.Tables["CessationOfficerAltOrMainDirectorDetails"].Columns["PID"]);

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {



                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForInterimDividend(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {

                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "InterimDividendDetails";
                            dsData.Tables[2].TableName = "Director";
                            dsData.Tables[3].TableName = "AlternateDirector";

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CCOR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Director"].Columns["PID"]);

                            dsData.Relations.Add("DADR", dsData.Tables["Director"].Columns["ID"],
                            dsData.Tables["AlternateDirector"].Columns["PID"]);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {



                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForTakeover(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {
                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "Director";
                            dsData.Tables[2].TableName = "Shareholders";
                            dsData.Tables[3].TableName = "NomineeDirectorsDetails";

                            string[] ColumnNamesNDD = { "FeeduetoNominee", "NoofSharesheldintrust" };
                            DataTable dtNDD = HelperClasses.GetCamaSeparetedValue(dsData.Tables[3], ColumnNamesNDD);
                            dsData.Merge(dtNDD);

                            dsData.Tables[4].TableName = "SecretaryDetailsIN";

                            string[] ColumnNamesSDIN = { "FeeduetoNominee" };
                            DataTable dtSDIN = HelperClasses.GetCamaSeparetedValue(dsData.Tables[4], ColumnNamesSDIN);
                            dsData.Merge(dtSDIN);

                            dsData.Tables[5].TableName = "SecretaryDetailsOUT";

                            string[] ColumnNamesSDOUT = { "FeeduetoNominee" };
                            DataTable dtSDOUT = HelperClasses.GetCamaSeparetedValue(dsData.Tables[5], ColumnNamesSDOUT);
                            dsData.Merge(dtSDOUT);

                            dsData.Tables[6].TableName = "PrincipalDetails";
                            dsData.Tables[7].TableName = "AuthorizedPersonPrincipalDetails";
                            dsData.Tables[8].TableName = "SigningDirectorsNomineeDirectorSvcAgt";
                            dsData.Tables[9].TableName = "SigningDirectorsNomineeSecSvcAgt";
                            dsData.Tables[10].TableName = "FMGasRegisteredAddressTrue";
                            dsData.Tables[11].TableName = "FMGasRegisteredAddressFalse";

                            DataTable dtSecretaryIN = dsData.Tables[4];
                            DataTable dtSecretaryOUT = dsData.Tables[5];

                            dtSecretaryOUT.Columns.Add("FeeduetoNominee");

                            DataTable mergeNomineeSecretaryDetails = new DataTable();
                            mergeNomineeSecretaryDetails.Merge(dtSecretaryIN);
                            mergeNomineeSecretaryDetails.Merge(dtSecretaryOUT);
                            dsData.Tables.Add(mergeNomineeSecretaryDetails);
                            mergeNomineeSecretaryDetails.Columns["CompanySecretaryName"].ColumnName = "NomineeSecretaryName";

                            dsData.Tables[12].TableName = "NomineeSecretaryDetails";

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CPR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["PrincipalDetails"].Columns["PID"]);

                            dsData.Relations.Add("APDR", dsData.Tables["PrincipalDetails"].Columns["ID"],
                            dsData.Tables["AuthorizedPersonPrincipalDetails"].Columns["PID"]);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {
                            if ((dsData.Tables["FMGasRegisteredAddressTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["FMGasRegisteredAddressTrue"].Rows[0]["IsFMRegisteredAddress"]) == 0))
                                dsData.Tables.Remove("FMGasRegisteredAddressTrue");
                            else
                                dsData.Tables.Remove("FMGasRegisteredAddressFalse");
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForAppointmentCessationofAuditors(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {

                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "IsROPlaceofMeetingTrue";
                            dsData.Tables[2].TableName = "IsROPlaceofMeetingFalse";
                            dsData.Tables[3].TableName = "ShareHolders";
                            dsData.Tables[4].TableName = "Director";
                            dsData.Tables[5].TableName = "AlternateDirector";

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CDR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Director"].Columns["PID"]);

                            dsData.Relations.Add("DADR", dsData.Tables["Director"].Columns["ID"],
                            dsData.Tables["AlternateDirector"].Columns["PID"]);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {



                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForDuplicate(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {
                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {
                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "Shareholders";
                            dsData.Tables[2].TableName = "ShareCertificate";
                            dsData.Tables[3].TableName = "Director";
                            dsData.Tables[4].TableName = "AlternateDirector";
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CDR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Director"].Columns["PID"]);

                            dsData.Relations.Add("DADR", dsData.Tables["Director"].Columns["ID"],
                            dsData.Tables["AlternateDirector"].Columns["PID"]);

                            dsData.Relations.Add("CSDR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Shareholders"].Columns["PID"]);

                            dsData.Relations.Add("SHDS", dsData.Tables["Shareholders"].Columns["ID"],
                            dsData.Tables["ShareCertificate"].Columns["PID"]);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForEGMChangeofName(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {

                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "IsROPlaceofMeetingTrue";
                            dsData.Tables[2].TableName = "IsROPlaceofMeetingFalse";
                            dsData.Tables[3].TableName = "Director";
                            dsData.Tables[4].TableName = "AlternateDirector";
                            dsData.Tables[5].TableName = "Shareholders";
                            dsData.Tables[6].TableName = "Members";
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CDR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Director"].Columns["PID"]);

                            dsData.Relations.Add("DADR", dsData.Tables["Director"].Columns["ID"],
                            dsData.Tables["AlternateDirector"].Columns["PID"]);

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {
                            if ((dsData.Tables["IsROPlaceofMeetingTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["IsROPlaceofMeetingTrue"].Rows[0]["IsROPlaceOfMeeting"]) == 0))
                                dsData.Tables.Remove("IsROPlaceofMeetingTrue");
                            else
                                dsData.Tables.Remove("IsROPlaceofMeetingFalse");

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForEGMAcquisitionDisposalofProperty(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {

                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "IsROPlaceofMeetingTrue";
                            dsData.Tables[2].TableName = "IsROPlaceofMeetingFalse";
                            dsData.Tables[3].TableName = "ShareHolders";
                            dsData.Tables[4].TableName = "Director";
                            dsData.Tables[5].TableName = "AlternateDirector";
                            dsData.Tables[6].TableName = "Members";
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CDR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Director"].Columns["PID"]);

                            dsData.Relations.Add("DADR", dsData.Tables["Director"].Columns["ID"],
                            dsData.Tables["AlternateDirector"].Columns["PID"]);

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {

                            if ((dsData.Tables["IsROPlaceofMeetingTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["IsROPlaceofMeetingTrue"].Rows[0]["ISROPlaceOfMeeting"]) == 0))
                                dsData.Tables.Remove("IsROPlaceofMeetingTrue");
                            else
                                dsData.Tables.Remove("IsROPlaceofMeetingFalse");

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForExistingClientEngaging(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {
                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "NomineeDirectorsDetails";

                            string[] ColumnNamesNDD = { "FeeduetoNominee", "NoofSharesheldintrust" };
                            DataTable dtNDD = HelperClasses.GetCamaSeparetedValue(dsData.Tables[1], ColumnNamesNDD);
                            dsData.Merge(dtNDD);

                            dsData.Tables[2].TableName = "NomineeSecretaryDetails";

                            string[] ColumnNamesNSD = { "FeeduetoNominee" };
                            DataTable dtNSD = HelperClasses.GetCamaSeparetedValue(dsData.Tables[2], ColumnNamesNSD);
                            dsData.Merge(dtNSD);

                            dsData.Tables[3].TableName = "PrincipalDetails";
                            dsData.Tables[4].TableName = "AuthorizedPersonPrincipalDetails";
                            dsData.Tables[5].TableName = "Director";
                            dsData.Tables[6].TableName = "AlternateDirector";
                            dsData.Tables[7].TableName = "Shareholders";
                            dsData.Tables[8].TableName = "SigningDirectorsNomineeDirectorSvcAgt";
                            dsData.Tables[9].TableName = "SigningDirectorsNomineeSecSvcAgt";

                            DataTable PrincipalDetailsC = dsData.Tables["PrincipalDetails"].Clone();
                            PrincipalDetailsC.TableName = "PrincipalDetailsC";
                            dsData.Tables.Add(PrincipalDetailsC);

                            DataTable PrincipalDetailsI = dsData.Tables["PrincipalDetails"].Clone();
                            PrincipalDetailsI.TableName = "PrincipalDetailsI";
                            dsData.Tables.Add(PrincipalDetailsI);

                            dsData.Tables["PrincipalDetails"].Select("PersonSource='CSS1C' OR PersonSource='CSS1NC' OR PersonSource='CSS2C'").CopyToDataTable(dsData.Tables["PrincipalDetailsC"], LoadOption.OverwriteChanges);
                            dsData.Tables["PrincipalDetails"].Select("PersonSource='CSS1I' OR PersonSource='CSS2I'").CopyToDataTable(dsData.Tables["PrincipalDetailsI"], LoadOption.OverwriteChanges);

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CDR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Director"].Columns["PID"]);

                            dsData.Relations.Add("DADR", dsData.Tables["Director"].Columns["ID"],
                            dsData.Tables["AlternateDirector"].Columns["PID"]);

                            dsData.Relations.Add("CPR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["PrincipalDetails"].Columns["PID"]);

                            dsData.Relations.Add("APDR", dsData.Tables["PrincipalDetails"].Columns["ID"],
                            dsData.Tables["AuthorizedPersonPrincipalDetails"].Columns["PID"]);

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {
                            //if ((dsData.Tables["FMGasRegisteredAddressTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["FMGasRegisteredAddressTrue"].Rows[0]["IsFMRegisteredAddress"]) == 0))
                            //    dsData.Tables.Remove("FMGasRegisteredAddressTrue");
                            //else
                            //    dsData.Tables.Remove("FMGasRegisteredAddressFalse");

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForTypeA(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {
                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "Director";
                            dsData.Tables[2].TableName = "AlternateDirector";
                            dsData.Tables[3].TableName = "Auditors";
                            dsData.Tables[4].TableName = "ShareCapitalOfCompany";
                            dsData.Tables[5].TableName = "ShareHolders";
                            dsData.Tables[6].TableName = "Members";

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CDR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Director"].Columns["PID"]);

                            dsData.Relations.Add("DADR", dsData.Tables["Director"].Columns["ID"],
                            dsData.Tables["AlternateDirector"].Columns["PID"]);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {



                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }
        private DataSet PrepareDatasetForBonusIssue(DataSet dsData)
        {

            System.Diagnostics.StackFrame stackFrame = new System.Diagnostics.StackFrame();
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            log.Debug("Start: " + methodBase.Name);
            try
            {

                if (dsData.Tables.Count > 0)
                {
                    try
                    {

                        #region Table Names
                        try
                        {
                            dsData.Tables[0].TableName = "Company";
                            dsData.Tables[1].TableName = "IsRegisteredAddressasPlaceOfMeetingTrue";
                            dsData.Tables[2].TableName = "IsRegisteredAddressasPlaceOfMeetingFalse";
                            dsData.Tables[3].TableName = "ShareholdersBonus";
                            dsData.Tables[4].TableName = "Director";
                            dsData.Tables[5].TableName = "AlternateDirector";
                            dsData.Tables[6].TableName = "Shareholders";
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion

                        #region Relations
                        try
                        {
                            dsData.Relations.Add("CDR", dsData.Tables["Company"].Columns["ID"],
                            dsData.Tables["Director"].Columns["PID"]);

                            dsData.Relations.Add("DADR", dsData.Tables["Director"].Columns["ID"],
                            dsData.Tables["AlternateDirector"].Columns["PID"]);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }

                        #endregion

                        #region Remove Tables
                        try
                        {

                            if ((dsData.Tables["IsRegisteredAddressasPlaceOfMeetingTrue"].Rows.Count == 0 || Convert.ToInt32(dsData.Tables["IsRegisteredAddressasPlaceOfMeetingTrue"].Rows[0]["IsRegisteredAddressasPlaceOfMeeting"]) == 0))
                                dsData.Tables.Remove("IsRegisteredAddressasPlaceOfMeetingTrue");
                            else
                                dsData.Tables.Remove("IsRegisteredAddressasPlaceOfMeetingFalse");

                        }
                        catch (Exception ex)
                        {
                            log.Error("Error: " + ex);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Error: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            log.Debug("End: " + methodBase.Name);
            return dsData;
        }

        #endregion

        #region GetData
        /// <summary>
        /// Created By   : Anji
        /// Created Date : 10 August 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Get Document Details For AGM to genarte Documents 
        /// </summary>
        /// <param name="WOID"></param>
        /// <returns></returns>
        public DataSet GetDocumentDataDetails(int WOID, string woCode)
        {

            string spName = string.Empty;

            if (woCode == Enum.GetName(typeof(WOType), (int)WOType.AGM))
            {
                spName = "SpGetDocumentDetailsForAGM";
            }
            else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.INCO))
            {
                spName = "SpGetDocumentDetailsForINCORP";
            }
            else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.ALLT))
            {
                spName = "SpGetDocumentDetailsForAllotment";
            }
            else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.APPA))
            {
                spName = "SpGetDocumentDetailsForAPPTCessationofAuditors";
            }
            else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.APPO))
            {
                spName = "SpGetDocumentDetailsForAPPTOfficer";
            }

            else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.CESO))
            {
                spName = "SpGetDocumentDetailsForCessationOfficer";
            }
            else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.INTD))
            {
                spName = "SpGetDocumentDetailsForInterimDividend";
            }

            else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.TRAN))
            {
                spName = "SpGetDocumentDetailsForTransfer";
            }
            else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.DUPL))
            {
                spName = "SpGetDocumentDetailsForDuplicate";
            }
            else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.ECEN))
            {
                spName = "SpGetDocumentDetailsForExistingClientEngaging";
            }
            else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.TAKE))
            {
                spName = "SpGetDocumentDetailsForTakeOver";
            }
            else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.EGMA))
            {
                spName = "SpGetDocumentDetailsForEGMAcquisitiondisposal";
            }
            else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.EGMC))
            {
                spName = "SpGetDocumentDetailsForEGMChangeOfName";
            }
            else if (woCode == Enum.GetName(typeof(WOType), (int)WOType.BOIS))
            {
                spName = "SpGetDocumentDetailsForBonusIssue";
            }
            else
            {
                spName = "SpGetDocumentDetailsForTypeA";
            }

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@WOID", WOID);
            var data = SqlHelper.ExecuteDataset(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, spName, sqlParams);

            return data;

        }
        #endregion

        #region Update WOStatus as Doccument in WO
        /// <summary>
        /// Created By   : Shiva
        /// Created Date : 7 Sep 2014
        /// Modified By  :
        /// Modified Date:
        /// Description  : Update WOStatus as Doccument in WO.
        /// </summary>
        /// <param name="WOID"></param>
        /// <param name="UserID"></param>
        /// <returns>Updated StatusCode status</returns>
        internal int UpdateWOStatusInDoccuments(int WOID, int UserID)
        {
            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@StatusCode", "DOC");
            sqlParams[1] = new SqlParameter("@WOID", WOID);
            sqlParams[2] = new SqlParameter("@Comment", "Document generated");
            sqlParams[3] = new SqlParameter("@SavedBy", UserID);
            UserID = SqlHelper.ExecuteNonQuery(ConnectionUtility.GetConnectionString(), CommandType.StoredProcedure, "SpSaveWOStatusForWoTypesAndDoccument", sqlParams);
            return UserID;
        }
        #endregion
    }
}