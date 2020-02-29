using Soccer.Common.Models;
using System.Collections.Generic;

namespace Soccer.Common.Helpers
{
    public interface ITransformHelper
    {
        List<Group> ToGroups(List<GroupResponse> groupResponses);
    }
}
