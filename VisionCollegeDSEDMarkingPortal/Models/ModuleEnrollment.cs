//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VisionCollegeDSEDMarkingPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ModuleEnrollment
    {
        public int ModulesAssignedID { get; set; }
        public Nullable<int> StudentFk { get; set; }
        public Nullable<int> ModuleFk { get; set; }
        public Nullable<int> ReslutsFK { get; set; }
        public Nullable<int> AssignmentSubmissionFK { get; set; }
    
        public virtual AssignmentSubmission AssignmentSubmission { get; set; }
        public virtual ModuleDetail ModuleDetail { get; set; }
        public virtual Result Result { get; set; }
        public virtual StudentDetail StudentDetail { get; set; }
    }
}
