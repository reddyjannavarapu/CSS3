# region Document Header
//Created By       : Anji 
//Created Date     : 
//Description      : 
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion

namespace CSS2.Areas.WO.Models
{
    #region Usings
    using CSS2.Models;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    #endregion

    public class WOInformation
    {
        public WOInformation()
        {
            QItems = new List<WOInformationForm>();
        }

        public int Id { get; set; }
        public string InfoCode { get; set; }
        public string WOCode { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public List<WOInformationForm> QItems { get; set; }


        private void FetchData(WOInformation woForm, SafeDataReader reader)
        {
            woForm.Id = reader.GetInt32("Id");
            woForm.Name = reader.GetString("Name");
            woForm.InfoCode = reader.GetString("InfoCode");
            woForm.WOCode = reader.GetString("WOCode");
        }

        private void FetchItems(WOInformationForm item, SafeDataReader reader)
        {
            item.ID = reader.GetInt32("Id");
            item.FieldName = reader.GetString("FieldName");
            item.FieldDisplayName = reader.GetString("FieldDisplayName");
            item.InfoCode = reader.GetString("InfoCode");
            item.QuestionOptions = reader.GetString("QuestionOptions");
            item.FieldType = reader.GetInt32("FieldType");
            item.RatingFrom = reader.GetInt32("RatingFrom");
            item.RatingTo = reader.GetInt32("RatingTo");
            item.IsMandatory = reader.GetInt32("IsMandatory");
            item.AnswerType = reader.GetInt32("AnswerType");
            item.IncludeOthers = reader.GetInt32("IncludeOthers");
            item.IsMultiSelection = reader.GetInt32("IsMultiSelection");
            item.DefaultValue = reader.GetString("DefaultValue");
        }


        public static WOInformation GetWOInformationForm(string InfoCode)
        {
            var question = new WOInformation();
            using (var con = new SqlConnection(ConnectionUtility.GetConnectionString()))
            {
                con.Open();
                var cmd = new SqlCommand("SpGetWOInfoFormByGridCode", con) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@InfoCode", InfoCode);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    question.FetchData(question, new SafeDataReader(reader));
                reader.NextResult();
                while (reader.Read())
                {
                    var item = new WOInformationForm();
                    question.FetchItems(item, new SafeDataReader(reader));
                    question.QItems.Add(item);
                }
            }
            return question;
        }

        public static int InsertWoInformation(int WOID, string InfoCode, int FieldID, string FieldName, string FieldValue, int PersonID, string PersonSource, int SavedBy, string FieldDisplayName)
        {
            int result = 0;
            using (var con = new SqlConnection(ConnectionUtility.GetConnectionString()))
            {
                try
                {
                    con.Open();
                    var cmd = new SqlCommand("SpInsertWOInformationDetails", con) { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.AddWithValue("@WOID", WOID);
                    cmd.Parameters.AddWithValue("@InfoCode", InfoCode);
                    cmd.Parameters.AddWithValue("@PersonID", PersonID);
                    cmd.Parameters.AddWithValue("@PersonSource", PersonSource);
                    cmd.Parameters.AddWithValue("@FieldID", FieldID);
                    cmd.Parameters.AddWithValue("@FieldName", FieldName);
                    cmd.Parameters.AddWithValue("@FieldValue", FieldValue);
                    cmd.Parameters.AddWithValue("@SavedBy", SavedBy);
                    cmd.Parameters.AddWithValue("@FieldDisplayName", FieldDisplayName);

                    result = cmd.ExecuteNonQuery();
                }
                catch
                {
                }
                return result;
            }
        }


    }


    public class WOInformationForm
    {
        #region Propperties

        public int ID { get; set; }
        public string InfoCode { get; set; }
        public int FieldType { get; set; }
        public string FieldName { get; set; }
        public string FieldDisplayName { get; set; }
        public int IsMandatory { get; set; }
        public int AnswerType { get; set; }
        public int RatingFrom { get; set; }
        public int RatingTo { get; set; }
        public string QuestionOptions { get; set; }
        public int IncludeOthers { get; set; }
        public int IsMultiSelection { get; set; }
        public string DefaultValue { get; set; }
        public string GivenAnswer { get; set; }

        #endregion
    }
}