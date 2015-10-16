using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.ApplyWorkflow.Builder
{
    public enum ApplyStates
    {
        //申请
        Apply,
        //申请等待
        Approving,
        //申请失败
        ApproveFail,
        //完善资料
        CompleteData,
        //提交保证金
        SubmitDeposit
    }
}
