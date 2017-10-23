using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Entities
{
    public enum IssueType
    {
        Question = 1,
        Task,
        Bug
    }
    public class IssueTypeList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}