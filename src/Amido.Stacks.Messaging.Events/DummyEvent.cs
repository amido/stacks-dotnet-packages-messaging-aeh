using System;
using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Core.Operations;
using Newtonsoft.Json;

namespace Amido.Stacks.Messaging.Events
{
    public class DummyEvent : IApplicationEvent
    {
		[JsonConstructor]
		public DummyEvent(int operationCode, Guid correlationId)
		{
			OperationCode = operationCode;
			CorrelationId = correlationId;
		}

		public DummyEvent(IOperationContext context)
		{
			OperationCode = context.OperationCode;
			CorrelationId = context.CorrelationId;
		}

		public int EventCode => 9871;

		public int OperationCode { get; }

		public Guid CorrelationId { get; }
	}
}
