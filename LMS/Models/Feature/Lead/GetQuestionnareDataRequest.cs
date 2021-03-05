using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{
    public class GetQuestionnareDataRequest
    {
		private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
		public int LeadId { get; set; }
		public int Type { get; set; }
		public object RunRequest(GetQuestionnareDataRequest req)
		{
			var response = new GetQuestionnareDataResponse();
			response.Data = new List<QuestionnareData>();
			var Lead = _dbContext.Lead.Where(x => x.Id == req.LeadId).FirstOrDefault();
			var QuestionnareDetails = Lead.QuestionnareDetails;
			if( QuestionnareDetails.Count > 0)
            {
				response.IsQuestionnareFilled = true;
				foreach (var Questionnare in QuestionnareDetails)
				{
					var Temp = new QuestionnareData();
					Temp.Id = Questionnare.Id;
					Temp.LeadId = Questionnare.LeadId;
					Temp.Requirements = Questionnare.Requirements;
					Temp.HeadOffice = Questionnare.HeadOffice;
					Temp.BranchOffice = Questionnare.BranchOffice;
					Temp.Details = Questionnare.Details;
					Temp.Type = Questionnare.Type;
					response.Data.Add(Temp);
				}
			}
			return response;
		}
	}
	public class GetQuestionnareDataResponse
	{
		public List<QuestionnareData> Data { get; set; }
		public bool IsQuestionnareFilled { get; set; }

	}

	public class QuestionnareData
    {
		public int Id { get; set; }
		public int LeadId { get; set; }
		public string Requirements { get; set; }
		public string HeadOffice { get; set; }
		public string BranchOffice { get; set; }
		public string Details { get; set; }
		public int? Type { get; set; }

	}
}
