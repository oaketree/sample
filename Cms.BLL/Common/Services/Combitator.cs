using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.BLL.Common.Services
{
    public static class Combitator
    {
        //Y组合子
        //delegate TResult SelfApplicable<TResult>(SelfApplicable<TResult> self);
        //public static Func<TInput, TResult> Fix<TInput, TResult>(Func<Func<TInput, TResult>, Func<TInput, TResult>> g)
        //{
        //    SelfApplicable<Func<TInput, TResult>> h = x => n => g(x(x))(n);
        //    return h(h);
        //}

        public static Func<T, TResult> Fix<T, TResult>(Func<Func<T, TResult>, Func<T, TResult>> f)
        {
            return n => f(Fix(f))(n);
        }
    }
}
