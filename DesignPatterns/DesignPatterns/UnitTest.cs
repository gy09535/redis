using System.Runtime.Remoting.Contexts;
using DesignPatterns.ApplyWorkflow.Builder;
using DesignPatterns.ApplyWorkflow.Handelers;
using DesignPatterns.OrderWorkflow.States;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPatterns
{

    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void OrderWorkflow()
        {
            //实例化订货工作流
            //构造指导者
            var director = new PurchaseDirector();
            //构建制造者
            var handelBuilder = new ApplyBuilder();
            //指导者指导构造者构造责任链
            director.Construction(handelBuilder);
            //获取构造责任链
            var handel = handelBuilder.GetHanders();
            var context = new ApplyContext() { States = ApplyStates.Apply };
            //责任链开始执行订购工作流 
            for (var i = 0; i < 4; i++)
            {

                handel.ProcessHandel(context);
                switch (context.States)
                {
                    case ApplyStates.Apply:
                    //  context.States = ApplyStates.Approve; break;
                    //case ApplyStates.Approve:
                    //  context.States = ApplyStates.CompleteData; break;
                    case ApplyStates.CompleteData:
                        context.States = ApplyStates.SubmitDeposit; break;
                }
            }

            Assert.AreEqual(context.States, ApplyStates.SubmitDeposit);
        }
    }
}
