//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrackerLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class Timesheets
    {
        public int TimesheetId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int Time { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
    
        public virtual Projects Projects { get; set; }
        public virtual UserProfiles UserProfiles { get; set; }
        public virtual Tasks Tasks { get; set; }
    }
}
