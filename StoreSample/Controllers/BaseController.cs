using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;
using DesignPatterns.ApplyWorkflow.Builder;
using StoreSample.Models;

namespace StoreSample.Controllers
{
    /// <summary>
    /// 项目应用的Control基类
    /// </summary>
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        //初始化用户信息
        protected override void Initialize(RequestContext requestContext)
        {
            //根据当前的状态选择跳转的url
            #region Url过滤
            var sitePatch = requestContext.HttpContext.Request.PhysicalApplicationPath;
            if (requestContext.HttpContext.Request.Url != null)
            {
                var url = requestContext.HttpContext.Request.Url.ToString();
                if (!url.Contains("Account") || !url.Contains("Apply/ApplyMain"))
                {

                    //如果请求的不是登陆页,则需要根据申请流定位显示页面

                    var response = requestContext.HttpContext.Response;
                    switch (States)
                    {
                        //申请状态
                        case ApplyStates.Apply:
                            response.Redirect(sitePatch + "Apply/ApplyMain");
                            response.End();
                            return;

                        case ApplyStates.Approving:
                            response.Redirect(sitePatch + "Apply/ApplyApproving");
                            response.End();
                            return;

                        case ApplyStates.ApproveFail:
                            response.Redirect(sitePatch + "Apply/ApplyFail");
                            response.End();
                            return;

                        case ApplyStates.CompleteData:
                            response.Redirect(sitePatch + "StoreManage/CompleteInformation");
                            response.End();
                            return;
                        default:
                            if (url.Contains("Apply/ApplyApproving") || url.Contains("Apply/ApplyFail") || url.Contains("StoreManage/CompleteInformation"))
                            {
                                response.Redirect(sitePatch + "Shared/Error");
                                response.End();
                                return;
                            } break;
                    }
                }
            }
            #endregion

            base.Initialize(requestContext);
        }


        /// <summary>
        /// 获取站点地图节点
        /// </summary>
        /// <returns></returns>
        protected List<ManageNode> ManageNodes()
        {
            var doc = XDocument.Load(string.Format(@HttpContext.Request.PhysicalApplicationPath + @"/ManageNodes.xml"));
            var xElement = doc.Element("node");
            return FindChildeNode(xElement);
        }

        /// <summary>
        ///递归获取站点地图子节点
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private List<ManageNode> FindChildeNode(XElement element)
        {
            if (element == null || !element.HasElements) return null;
            var nodes = from item in element.Elements()
                        select new ManageNode()
                        {
                            Name = item.Attribute("title") == null ? string.Empty : item.Attribute("title").Value,
                            Url = item.Attribute("url") == null ? string.Empty : item.Attribute("url").Value,
                            ChildNodes = FindChildeNode(item)
                        };
            return nodes.ToList();
        }


        public ApplyStates States { get; set; }
    }
}
