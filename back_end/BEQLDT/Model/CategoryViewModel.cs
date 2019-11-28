using BEQLDT.Model.Abstracts;
namespace BEQLDT.Model
{
    public class CategoryViewModel : AuditableViewModel
    {
        public int Id { get; set; }
        public string NameCategory { get; set; }
    }
}
