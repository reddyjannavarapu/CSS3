
# region Document Header
//Created By       : Anji 
//Created Date     : 10 May 2014
//Description      : To Mantian all models metadata
//------------------------------------------------------------------------------------------------------------------------------------------------
//Modified By       Modified Date       Description(Reference of Bug ID if any)         Reviewed By     Reviewed Date 
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
//-------------------------------------------------------------------------------------------------------------------------------------------------
//
# endregion


namespace CSS2.Models
{
    #region Usings
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    #endregion

    public class UserDetailsMetadata
    {
        #region Properties
        [Display(Name = "User Id")]
        public int Id;

        [Display(Name = "User Type")]
        
        public int UserType;

        [Display(Name = "User Name")]
        [StringLength(100)]
       
        public string UserName;

        [Display(Name = "Email")]
        [StringLength(100)]
       
        public string Email { get; set; }
        [Display(Name = "First Name")]
        [StringLength(100)]
      
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(100)]       
        public string LastName { get; set; }

        [Display(Name = "Password")]        
        public string Password; 

        #endregion
    }

    public class ServiceMetadata
    {
        #region Properties
       
        [Display(Name = "Code Info")]
       
        [MaxLength (10,ErrorMessage="Code is max legth 10")]
        public int Code;

        [Display(Name = "Description")]
        [StringLength(100)]       
        public string Description;

       
        #endregion
    }

    public class GLCodeMetadata
    {
        #region Properties
       
        [Display(Name = "Code")]
        [StringLength(10)] 
        public int Code;

        [Display(Name = "Description")]
        [StringLength(100)]       
        public string Description;       

        #endregion
    }

    public class GlobalSettingsMetadata
    {
        #region Properties
       
        [Display(Name = "Code")]
        [StringLength(10)] 
        public int Code;

        [Display(Name = "Description")]
        [StringLength(100)]       
        public string Description;

        [Display(Name = "Value")]
        public string Value { get; set; }

        [Display(Name = "RangeMin")]
        [StringLength(10)] 
        public string RangeMin { get; set; }

        [Display(Name = "RangeMax")]
        [StringLength(10)] 
        public string RangeMax { get; set; }

        #endregion
    }

    public class FeeMetadata
    {
        #region Properties

        [Display(Name = "Code Info")]

        [MaxLength(10, ErrorMessage = "Code is max legth 10")]
        public int Code;

        [Display(Name = "Description")]
        [StringLength(100)]
        public string Description;


        #endregion
    }

    public class VendorMetadata
    {
        #region Properties

        [Display(Name = "Report #")]
        public int VRID { get; set; }

        [Display(Name = "Records")]
        public int RecordCount { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "UploadedOn")]
        public string UploadedOn { get; set; }

        [Display(Name = "UploadedBy")]
        public int UploadedBy { get; set; }

        public DataTable dtvendor { set; get; }

        #endregion
    }
}