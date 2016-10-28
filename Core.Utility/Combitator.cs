using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utility
{
    public static class Combitator
    {
        //不动点组合子 Y组合子
        //delegate TResult SelfApplicable<TResult>(SelfApplicable<TResult> self);
        //public static Func<TInput, TResult> Fix<TInput, TResult>(Func<Func<TInput, TResult>, Func<TInput, TResult>> g)
        //{
        //    SelfApplicable<Func<TInput, TResult>> h = x => n => g(x(x))(n);
        //    return h(h);
        //}

        public static Func<T, TResult> Fix<T, TResult>(Func<Func<T, TResult>, Func<T, TResult>> f)
        {
            return x => f(Fix(f))(x);
        }

        public static Func<T1, T2, TResult> Fix<T1, T2, TResult>(Func<Func<T1, T2, TResult>, Func<T1, T2, TResult>> f)
        {
            return (x, y) => f(Fix(f))(x, y);
        }
    }
}
