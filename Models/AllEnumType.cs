
# region Document Header
//Created By       : Anji 
//Created Date     : 22 May 2014
//Description      : To Mantian all ENUM types
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
    using System.Linq;
    using System.Web;
    #endregion
  
    public enum ActionType
    {
        Create = 0,
        Update = 1,
        Delete = 2,
        Search = 3
    }

    public enum RecordStatus
    {
        All = -1,
        InActive = 0,
        Active = 1

    }

    public enum PagingValues
    {
        PageIndex = 1,
        PageSize = 10
    }

    public enum WOCategory
    {

        TypeA = 1,
        TypeB = 2,
        TypeC = 3,
    }

    public enum WOType
    {

        INCO = 1,  // Incorp
        AGM  = 2,  // AGM
        ALLT = 3,  // Allotment
        TRAN = 4,  // Transfer
        APPO = 5,  // Appointment of Officer
        CESO = 6,  // Cessation of Officer
        INTD = 7,  // Interim Dividend
        TAKE = 8,  // Takeover
        APPA = 9,  // Appointment Cessation of Auditors      
        DUPL = 11, // Duplicate
        EGMC = 12, // EGM Change of Name
        EGMA = 13, // EGM Acquisition Disposal of Property
        ECEN = 14, // Existing Client Engaging
        BOIS = 16, // Bonus Issue      


    }

    public enum UserType
    {
        SuperAdmin = 1,
        Admin = 2,
        Manager = 3,
        User = 4
    }
}
















