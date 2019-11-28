namespace BEQLDT.Service.Model
{
    public class TopicCouncils
    {
        public int Id { get; set; }
        public int? CouncilId { get; set; }
        public int? TopicId { get; set; }
        public string NameCouncil { get; set; }
        public string NameTopic { get; set; }
        public string Description { get; set; }
    }
}
