using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.GrpcService.Answers.Base
{
    public interface IAnswer;

    public interface IAnswer<TResult> : IAnswer;
}
