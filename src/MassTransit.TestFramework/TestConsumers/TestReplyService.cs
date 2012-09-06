// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.TestFramework.TestConsumers
{
    using MassTransit.Context;
    using Magnum.Reflection;

    public class TestReplyService<TMessage, TKey, TReplyMessage> :
		TestConsumerBase<TMessage>,
		Consumes<TMessage>.All
		where TMessage : class, CorrelatedBy<TKey>
		where TReplyMessage : class, CorrelatedBy<TKey>
	{
		public override void Consume(TMessage message)
		{
			base.Consume(message);

			var reply = FastActivator<TReplyMessage>.Create(message.CorrelationId);
			ContextStorage.Context().Respond(reply);
		}
	}
}