using Soccer.Common.Models;
using System.Collections.Generic;

namespace Soccer.Common.Helpers
{
    public class TransformHelper : ITransformHelper
    {
        public List<Group> ToGroups(List<GroupResponse> groupResponses)
        {
            List<Group> list = new List<Group>();

            foreach (GroupResponse groupResponse in groupResponses)
            {
                Group group = new Group();
                foreach (GroupDetailResponse groupDetail in groupResponse.GroupDetails)
                {
                    group.Add(groupDetail);
                }

                group.Name = groupResponse.Name;
                list.Add(group);
            }

            return list;
        }
    }
}
