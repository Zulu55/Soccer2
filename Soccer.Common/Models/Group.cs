using System.Collections.Generic;

namespace Soccer.Common.Models
{
    public class Group : List<GroupDetailResponse>
    {
        public string Name { get; set; }

        public List<GroupDetailResponse> Teams => this;
    }
}
