using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.User
{
    //public class GetUserRequest
    //{
    //}
	public class GetUserResponse
	{
		public int Id { get; set; }
		public int AgentId { get; set; }
		public string UpdatedBy { get; set; }
		public int? DepartmentId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Contact1 { get; set; }
		public string Contact2 { get; set; }
		public string Email { get; set; }
		public bool? HasSupervisor { get; set; }
		public int? SupervisorId { get; set; }
	}

	public class GetUserRequest
	{
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public int Id { get; set; }
		public object RunRequest(GetUserRequest req)
		{
			var response = new GetUserResponse();
			
			//TODO set data  according to type = pmd,presale,lead
			var Data = _dbContext.Agent.Where(x => x.Id == req.Id).FirstOrDefault();
			response.Id = Data.Id;
			response.FirstName = Data.FisrtName;
			response.Address = Data.Address;
			response.AgentId = Data.Id;
			response.Contact1 = Data.Contact1;
			response.Contact2 = Data.Contact2;
			response.DepartmentId = Data.DepartmentId;
			response.Email = Data.Email;
			response.HasSupervisor = Data.HasSupervisor;
			response.LastName = Data.LastName;
			response.SupervisorId = Data.SuperVisorId;
			return response;
		}
	}
}