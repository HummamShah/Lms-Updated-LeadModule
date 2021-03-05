using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Department
{
    public class EditDepartmentRequest
    {
        private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public object RunRequest(EditDepartmentRequest request)
        {
            var response = new EditDepartmentResponse();
            var Company = _dbContext.Department.FirstOrDefault(x => x.Id == request.Id);
            Company.UpdatedAt = DateTime.Today;
            Company.UpdatedBy = request.UpdatedBy;
            Company.Description = request.Description;
            Company.Name = request.Name;
            _dbContext.SaveChanges();
            return response;
        }
    }
    public class EditDepartmentResponse
    {
        public bool IsVendorAdded { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; }
    }
}