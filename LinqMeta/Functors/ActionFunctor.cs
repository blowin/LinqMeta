using System;
using LinqMetaCore;
using LinqMetaCore.Intefaces;
using LinqMetaCore.Utils;

namespace LinqMeta.Functors
{
    public struct ActionFunctor<T> : IFunctor<T, MetaVoid>
    {
        private Action<T> _action;

        public ActionFunctor(Action<T> action)
        {
            ErrorUtil.NullCheck(action, "action");
            _action = action;
        }
        
        public MetaVoid Invoke(T param)
        {
            _action.Invoke(param);
            return MetaVoid.Def;
        }
    }
}