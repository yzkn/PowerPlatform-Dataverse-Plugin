using System;
using System.ServiceModel;
using System.Xml.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json.Linq;

namespace CreatedByPlugin
{
    /// <summary>
    /// The plug-in creates a task activity after a new account is created. The activity reminds the user to
    /// follow-up with the new account customer one week after the account was created.
    /// </summary>
    /// <remarks>Register this plug-in on the Create message, account entity, and asynchronous mode.
    /// </remarks>

    public sealed class CreatedByPlugin : IPlugin
    {
        /// <summary>
        /// Execute method that is required by the IPlugin interface.
        /// </summary>
        /// <param name="serviceProvider">The service provider from which you can obtain the
        /// tracing service, plug-in execution context, organization service, and more.</param>
        public void Execute(IServiceProvider serviceProvider)
        {
            //Extract the tracing service for use in debugging sandboxed plug-ins.
            ITracingService tracingService =
                (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.
            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            // The InputParameters collection contains all the data passed in the message request.
            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the input parameters.
                Entity entity = (Entity)context.InputParameters["Target"];

                // Verify that the target entity represents an account.
                // If not, this plug-in was not registered correctly.
                if (entity.LogicalName != "ya_kuriya")
                    return;

                try
                {
                    // 作成者を、UPN列で指定されたUPNを持つユーザーに変更する
                    IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                    IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                    EntityReference beforeER = (EntityReference)entity["createdby"];
                    Guid beforeGuid = beforeER.Id;
                    entity["ya_beforeguid"] = beforeGuid.ToString();

                    var newGUID = "";
                    QueryExpression query = new QueryExpression("systemuser");
                    query.ColumnSet = new ColumnSet("systemuserid");
                    query.Criteria.AddCondition("internalemailaddress", ConditionOperator.Equal, entity["ya_upn"]);
                    EntityCollection collection = service.RetrieveMultiple(query);
                    if (collection.Entities.Count > 0)
                    {
                        var item = collection.Entities[0];
                        newGUID = item.Id.ToString();
                    }
                    entity["ya_afterguid"] = newGUID;

                    entity["createdby"] = new EntityReference("systemuser", Guid.Parse(newGUID));
                }
                catch (FaultException<OrganizationServiceFault> ex)
                {
                    throw new InvalidPluginExecutionException("An error occurred in the FollowupPlugin plug-in.", ex);
                }

                catch (Exception ex)
                {
                    tracingService.Trace("FollowupPlugin: {0}", ex.ToString());
                    throw;
                }
            }
        }
    }
}
