using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models.User;

namespace UI.Helpers
{
    public interface IStateHelper
    {
        void SetState(UserVM authenticatedUser, bool RememberMe);
        void SetState(Dictionary<string, string> stateDataFromCookie);
        void ClearState();
    }
}
