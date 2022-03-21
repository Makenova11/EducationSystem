using System.Collections.Generic;

namespace EducationSystem.ViewModels
{
    public class EventVM
    {
        public EventVM()
        {
            Task = new HashSet<TaskVM>();
        }

        public int EventCode { get; set; }
        public string EventName { get; set; }

        public virtual ICollection<TaskVM> Task { get; set; }
    }
}