namespace BEQLDT.Service.Model
{
    public class Topics
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string NameUser { get; set; }
        public string FullName { get; set; }
        public string NameTopic { get; set; }
        public int? PeriodId { get; set; }
        public string NamePeriod { get; set; }
        public int? SchoolId { get; set; }
        public string NameSchool { get; set; }
        public int? CategoryId { get; set; }
        public string NameCategory { get; set; }
        public int? FileId { get; set; }
        public string NameFile { get; set; }
        public string Base64File { get; set; }
        public int? DecisionId { get; set; }
        public string NameDecision { get; set; }
        public string Description { get; set; }
    }
}
