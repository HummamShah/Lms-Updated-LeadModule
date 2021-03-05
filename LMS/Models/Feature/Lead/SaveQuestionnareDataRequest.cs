using LMS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models.Feature.Lead
{
	public class SaveQuestionnareDataRequest
	{
	private Sharptel_Lms_DbEntities _dbContext = new Sharptel_Lms_DbEntities();
	public List<QuestionnareData> Questionnares { get; set; }
		public object RunRequest(SaveQuestionnareDataRequest req)
		{
			var response = new SaveQuestionnareDataResponse();
            foreach (var Questionnare in req.Questionnares)
            {
                if (Questionnare.Id == 0)
                {
					//It is newly added
					var Quest = new QuestionnareDetails();
					Quest.LeadId = Questionnare.LeadId;
					Quest.HeadOffice = Questionnare.HeadOffice;
					Quest.BranchOffice = Questionnare.BranchOffice;
					Quest.Details = Questionnare.Details;
					Quest.Requirements = Questionnare.Requirements;
                    if (Questionnare.Type.HasValue)
                    {
						Quest.Type = Questionnare.Type.Value;
					}
					_dbContext.QuestionnareDetails.Add(Quest);
					_dbContext.SaveChanges();

				}
                if (Questionnare.Id != 0)
                {
					//Edit
					var Quest = _dbContext.QuestionnareDetails.Where(x=>x.Id == Questionnare.Id).FirstOrDefault();
					Quest.HeadOffice = Questionnare.HeadOffice;
					Quest.BranchOffice = Questionnare.BranchOffice;
					Quest.Details = Questionnare.Details;
					Quest.Requirements = Questionnare.Requirements;
					if (Questionnare.Type.HasValue)
					{
						Quest.Type = Questionnare.Type.Value;
					}
					_dbContext.SaveChanges();
				}
				
			}
			return response;
		}
	}
	public class SaveQuestionnareDataResponse
	{
		
		public bool IsQuestionnareFilled { get; set; }

	}

	
}
