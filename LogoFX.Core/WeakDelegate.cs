// Partial Copyright (c) LogoUI Software Solutions LTD
// Autor: Vladislav Spivak
// This source file is the part of LogoFX Framework http://logofx.codeplex.com
// See accompanying licences and credits.

//based on Paolo Zemek's Pfz project

using System.Reflection;
using System.Collections.Specialized;
namespace System.ComponentModel
{
    // ReSharper disable RedundantDelegateCreation

    /// <summary>
    /// Static class used to convert real (strong) delegates into weak
    /// delegates.
    /// </summary>
    public static class WeakDelegate
    {
        #region IsWeakDelegate
        /// <summary>
        /// Verifies if a handler is already a weak delegate.
        /// </summary>
        /// <param name="handler">The handler to verify.</param>
        /// <returns><see langword="true"/> if the handler is already a weak delegate, <see langword="false"/> otherwise.</returns>
        public static bool IsWeakDelegate(Delegate handler)
        {
            return handler.Target != null && handler.Target is WeakDelegateBase;
        }
        #endregion

        #region From(Action)
        private sealed class WeakActionWrapper :
            WeakDelegateBase
        {
            internal WeakActionWrapper(Action handler) :
                base(handler)
            {
            }
            internal void Execute()
            {
                Invoke(null);
            }
        }

        /// <summary>
        /// Creates a weak delegate from an Action delegate.
        /// </summary>
        public static Action From(Action strongHandler)
        {
            if (IsWeakDelegate(strongHandler))
                throw new ArgumentException("Your delegate is already a weak-delegate.");

            var wrapper = new WeakActionWrapper(strongHandler);
            return new Action(wrapper.Execute);
        }
        #endregion
        #region From(Action<T>)
        private sealed class WeakActionWrapper<T> :
            WeakDelegateBase
        {
            internal WeakActionWrapper(Action<T> handler) :
                base(handler)
            {
            }
            internal void Execute(T parameter)
            {
                Invoke(new object[] { parameter });
            }
        }

        /// <summary>
        /// Creates a weak delegate from an Action delegate.
        /// </summary>
        public static Action<T> From<T>(Action<T> strongHandler)
        {
            if (IsWeakDelegate(strongHandler))
                throw new ArgumentException("Your delegate is already a weak-delegate.");

            var wrapper = new WeakActionWrapper<T>(strongHandler);
            return new Action<T>(wrapper.Execute);
        }
        #endregion
        #region From(Action<T1, T2>)
        private sealed class WeakActionWrapper<T1, T2> :
            WeakDelegateBase
        {
            internal WeakActionWrapper(Action<T1, T2> handler) :
                base(handler)
            {
            }
            internal void Execute(T1 parameter1, T2 parameter2)
            {
                Invoke(new object[] { parameter1, parameter2 });
            }
        }

        /// <summary>
        /// Creates a weak delegate from an Action  delegate.
        /// </summary>
        public static Action<T1, T2> From<T1, T2>(Action<T1, T2> strongHandler)
        {
            if (IsWeakDelegate(strongHandler))
                throw new ArgumentException("Your delegate is already a weak-delegate.");

            var wrapper = new WeakActionWrapper<T1, T2>(strongHandler);
            return new Action<T1, T2>(wrapper.Execute);
        }
        #endregion
        #region From(Action<T1, T2, T3>)
        private sealed class WeakActionWrapper<T1, T2, T3> :
            WeakDelegateBase
        {
            internal WeakActionWrapper(Action<T1, T2, T3> handler) :
                base(handler)
            {
            }
            internal void Execute(T1 parameter1, T2 parameter2, T3 parameter3)
            {
                Invoke(new object[] { parameter1, parameter2, parameter3 });
            }
        }

        /// <summary>
        /// Creates a weak delegate from an Action delegate.
        /// </summary>
        public static Action<T1, T2, T3> From<T1, T2, T3>(Action<T1, T2, T3> strongHandler)
        {
            if (IsWeakDelegate(strongHandler))
                throw new ArgumentException("Your delegate is already a weak-delegate.");

            var wrapper = new WeakActionWrapper<T1, T2, T3>(strongHandler);
            return new Action<T1, T2, T3>(wrapper.Execute);
        }
        #endregion
        #region From(Action<T1, T2, T3, T4>)
        private sealed class WeakActionWrapper<T1, T2, T3, T4> :
            WeakDelegateBase
        {
            internal WeakActionWrapper(Action<T1, T2, T3, T4> handler) :
                base(handler)
            {
            }
            internal void Execute(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4)
            {
                Invoke(new object[] { parameter1, parameter2, parameter3, parameter4 });
            }
        }

        /// <summary>
        /// Creates a weak delegate from an Action delegate.
        /// </summary>
        public static Action<T1, T2, T3, T4> From<T1, T2, T3, T4>(Action<T1, T2, T3, T4> strongHandler)
        {
            if (IsWeakDelegate(strongHandler))
                throw new ArgumentException("Your delegate is already a weak-delegate.");

            var wrapper = new WeakActionWrapper<T1, T2, T3, T4>(strongHandler);
            return new Action<T1, T2, T3, T4>(wrapper.Execute);
        }
        #endregion
        #region From(EventHandler)
        private sealed class WeakEventHandlerWrapper :
            WeakDelegateBase
        {
            internal WeakEventHandlerWrapper(EventHandler handler) :
                base(handler)
            {
            }
            internal void Execute(object sender, EventArgs e)
            {
                Invoke(new[] { sender, e });
            }
        }

        /// <summary>
        /// Creates a weak delegate from an <see cref="EventHandler"/> delegate.
        /// </summary>
        public static EventHandler From(EventHandler strongHandler)
        {
            if (IsWeakDelegate(strongHandler))
                throw new ArgumentException("Your delegate is already a weak-delegate.");

            var wrapper = new WeakEventHandlerWrapper(strongHandler);
            return new EventHandler(wrapper.Execute);
        }
        #endregion
        #region From(EventHandler<TEventArgs>)
        private sealed class WeakEventHandlerWrapper<TEventArgs> :
            WeakDelegateBase
        where
            TEventArgs : EventArgs
        {
            internal WeakEventHandlerWrapper(EventHandler<TEventArgs> handler) :
                base(handler)
            {
            }
            internal void Execute(object sender, TEventArgs e)
            {
                Invoke(new[] { sender, e });
            }
        }

        /// <summary>
        /// Creates a weak delegate from an Action delegate.
        /// </summary>
        public static EventHandler<TEventArgs> From<TEventArgs>(EventHandler<TEventArgs> strongHandler)
        where
            TEventArgs : EventArgs
        {
            if (IsWeakDelegate(strongHandler))
                throw new ArgumentException("Your delegate is already a weak-delegate.");

            var wrapper = new WeakEventHandlerWrapper<TEventArgs>(strongHandler);
            return new EventHandler<TEventArgs>(wrapper.Execute);
        }
        #endregion
        #region From(NotifyCollectionChangedEventHandler)
        private sealed class WeakNotifyCollectionChangedEventHandler :
            WeakDelegateBase
        {
            internal WeakNotifyCollectionChangedEventHandler(NotifyCollectionChangedEventHandler handler) :
                base(handler)
            {
            }
            internal void Execute(object sender, NotifyCollectionChangedEventArgs e)
            {
                Invoke(new[] { sender, e });
            }
        }

        /// <summary>
        /// Creates a weak delegate from an Action delegate.
        /// </summary>
        public static NotifyCollectionChangedEventHandler From(NotifyCollectionChangedEventHandler strongHandler)
        {
            if (IsWeakDelegate(strongHandler))
                throw new ArgumentException("Your delegate is already a weak-delegate.");

            var wrapper = new WeakNotifyCollectionChangedEventHandler(strongHandler);
            return new NotifyCollectionChangedEventHandler(wrapper.Execute);
        }
        #endregion
        #region From(NotifyPropertyChangedEvenHandler)
        private sealed class WeakPropertyChangedEventHandler :
            WeakDelegateBase
        {
            internal WeakPropertyChangedEventHandler(PropertyChangedEventHandler handler) :
                base(handler)
            {
            }
            internal void Execute(object sender, PropertyChangedEventArgs e)
            {
                Invoke(new[] { sender, e });
            }
        }

        /// <summary>
        /// Creates a weak delegate from an Action delegate.
        /// </summary>
        public static PropertyChangedEventHandler From(PropertyChangedEventHandler strongHandler)
        {
            if (IsWeakDelegate(strongHandler))
                throw new ArgumentException("Your delegate is already a weak-delegate.");

            var wrapper = new WeakPropertyChangedEventHandler(strongHandler);
            return new PropertyChangedEventHandler(wrapper.Execute);
        }
        #endregion
    }

    /// Provides a weak reference to a null target object, which, unlike
    /// other weak references, is always considered to be alive. This
    /// facilitates handling null dictionary values, which are perfectly
    /// legal.
    public class WeakNullReference<T> : WeakReference<T> where T : class
    {
        public static readonly WeakNullReference<T> Singleton = new WeakNullReference<T>();


        private WeakNullReference() : base(null) { }


        public override bool IsAlive
        {

            get { return true; }
        }
    }


    /// Adds strong typing to WeakReference.Target using generics. Also,
    /// the Create factory method is used in place of a constructor
    /// to handle the case where target is null, but we want the
    /// reference to still appear to be alive.
    public class WeakReference<T> where T : class
    {
        private WeakReference _inner;
        public static WeakReference<T> Create(T target)
        {
            if (target == null)
                return WeakNullReference<T>.Singleton;

            return new WeakReference<T>(target);
        }

        protected WeakReference(T target)
        {
            if (target == null) throw new ArgumentNullException("target");
            this._inner = new WeakReference((object)target);

        }

        public T Target
        {

            get { return (T)_inner.Target; }
        }
        public virtual bool IsAlive
        {
            get
            {
                return this._inner.IsAlive;
            }
        }
    }

    /// <summary>
    /// A class used as the base class to implement weak delegates.
    /// See <see cref="WeakDelegate"/>.From method implementations to see how it works.
    /// </summary>
    public class WeakDelegateBase :
        WeakReference<object>
    {
        #region CTOR
        /// <summary>
        /// Creates this weak-delegate class based as a copy of the given 
        /// delegate handler.
        /// </summary>
        /// <param name="handler">The handler to copy information from.</param>
        public WeakDelegateBase(Delegate handler) :
            base(handler.Target)
        {
#if WinRT
            Method = (MethodInfo)handler.GetType().GetRuntimeProperty("Method").GetValue(handler);
#else
            Method = handler.Method;
#endif
        }
        #endregion

        #region Method
        /// <summary>
        /// Gets the method used by this delegate.
        /// </summary>
        public MethodInfo Method { get; private set; }
        #endregion

        #region Invoke
        /// <summary>
        /// Invokes this delegate with the given parameters.
        /// </summary>
        /// <param name="parameters">The parameters to be used by the delegate.</param>
        public void Invoke(object[] parameters)
        {
            object target = Target;
            if (target != null || Method.IsStatic)
                Method.Invoke(target, parameters);
        }
        #endregion
    }
    // ReSharper restore RedundantDelegateCreation
}
