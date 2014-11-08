using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Reflection;
using System.Collections;

namespace RMTFRK.Core.Data
{
    [Serializable]
    public class ExtensionObject : DynamicObject, IDynamicMetaObjectProvider,IDictionary<string,object>
    {
        object _instance;

        System.Type _instanceType;
        PropertyInfo[] _cacheInstancePropertyInfos;
        IEnumerable<PropertyInfo> _instancePropertyInfos
        {
            get
            {
                if (_cacheInstancePropertyInfos == null && _instance != null)
                {
                    var properties = new List<PropertyInfo>(8);
                    var instanceProperties = _instance.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
                    //移除索引参数类型的属性
                    properties.AddRange(instanceProperties
                        .Where(instanceProperty => instanceProperty.GetIndexParameters().Length == 0));
                    _cacheInstancePropertyInfos = properties.ToArray();
                }

                return _cacheInstancePropertyInfos;
            }
        }

        /// <remarks>Using PropertyBag to support XML Serialization of the dictionary</remarks>
        public PropertyBag Properties = new PropertyBag();

        public ExtensionObject()
        {
            Initialize(this);
        }

        /// <remarks>
        /// You can pass in null here if you don't want to 
        /// check native properties and only check the Dictionary!
        /// </remarks>
        /// <param name="instance"></param>
        public ExtensionObject(object instance)
        {
            Initialize(instance);
        }


        protected virtual void Initialize(object instance)
        {
            _instance = instance;
            if (instance != null)
                _instanceType = instance.GetType();
        }


        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return GetDynamicMemberNames(true);
        }

        public IEnumerable<string> GetDynamicMemberNames(bool includeInstanceMemberName)
        {
            return this.GetProperties(includeInstanceMemberName).Select(prop => prop.Key);
        }

        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;

            // first check the Properties collection for member
            if (Properties.Keys.Contains(binder.Name))
            {
                result = Properties[binder.Name];
                return true;
            }


            // Next check for Public properties via Reflection
            if (_instance != null)
            {
                try
                {
                    return GetProperty(_instance, binder.Name, out result);
                }
                catch (Exception)
                { }
            }

            // failed to retrieve a property
            return false;
        }

        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {

            // first check to see if there's a native property to set
            if (_instance != null)
            {
                try
                {
                    bool result = SetProperty(_instance, binder.Name, value);
                    if (result)
                        return true;
                }
                catch { }
            }

            // no match - set or add to dictionary
            Properties[binder.Name] = value;
            return true;
        }

        /// <param name="binder"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (_instance != null)
            {
                try
                {
                    // check instance passed in for methods to invoke
                    if (InvokeMethod(_instance, binder.Name, args, out result))
                        return true;
                }
                catch (Exception)
                { }
            }

            result = null;
            return false;
        }

        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected bool GetProperty(object instance, string name, out object result)
        {
            if (instance == null)
                instance = this;

            var miArray = _instanceType.GetMember(name, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance);
            if (miArray != null && miArray.Length > 0)
            {
                var mi = miArray[0];
                if (mi.MemberType == MemberTypes.Property)
                {
                    result = ((PropertyInfo)mi).GetValue(instance, null);
                    return true;
                }
            }

            result = null;
            return false;
        }

        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool SetProperty(object instance, string name, object value)
        {
            if (instance == null)
                instance = _instance;

            var miArray = _instanceType.GetMember(name, BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance);
            if (miArray != null && miArray.Length > 0)
            {
                var pi = miArray[0] as PropertyInfo;
                if (pi != null)
                {
                    if (value == null || string.IsNullOrEmpty(value.ToString()))
                    {
                        pi.SetValue(instance, null, null);
                    }
                    else if (pi.PropertyType.IsGenericType)
                    {
                        var ptype = pi.PropertyType.GetGenericArguments()[0];
                        if (typeof(IConvertible).IsAssignableFrom(ptype))
                        {
                            pi.SetValue(instance, Convert.ChangeType(value, ptype), null);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        pi.SetValue(instance, Convert.ChangeType(value, pi.PropertyType), null);
                    }
                    return true;
                }
            }
            return false;
        }

        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected bool InvokeMethod(object instance, string name, object[] args, out object result)
        {
            if (instance == null)
                instance = this;

            // Look at the instanceType
            var miArray = _instanceType.GetMember(name,
                                    BindingFlags.InvokeMethod |
                                    BindingFlags.Public | BindingFlags.Instance);

            if (miArray != null && miArray.Length > 0)
            {
                var mi = miArray[0] as MethodInfo;
                result = mi.Invoke(_instance, args);
                return true;
            }

            result = null;
            return false;
        }
       
        public object this[string key]
        {
            get
            {
                try
                {
                    // try to get from properties collection first
                    return Properties[key];
                }
                catch (KeyNotFoundException ex)
                {
                    // try reflection on instanceType
                    object result = null;
                    if (GetProperty(_instance, key, out result))
                        return result;

                    // nope doesn't exist
                    throw;
                }
            }
            set
            {
                if (Properties.ContainsKey(key))
                {
                    Properties[key] = value;
                    return;
                }

                // check instance for existance of type first
                var miArray = _instanceType.GetMember(key, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance);
                if (miArray.Length>0)
                    SetProperty(_instance, key, value);
                else
                    Properties[key] = value;
            }
        }

        /// <param name="includeInstanceProperties">是否包含对象实例属性</param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, object>> GetProperties(bool includeInstanceProperties = false)
        {
            if (includeInstanceProperties && _instance != null)
            {
                foreach (var prop in this._instancePropertyInfos)
                    yield return new KeyValuePair<string, object>(prop.Name, prop.GetValue(_instance, null));

            }

            foreach (var key in this.Properties.Keys)
                yield return new KeyValuePair<string, object>(key, this.Properties[key]);

        }
        /// <param name="includeInstanceProperties">是否包含对象实例属性</param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, object>> GetInstanceProperties()
        {
            if (_instance != null)
            {
                foreach (var prop in this._instancePropertyInfos)
                    yield return new KeyValuePair<string, object>(prop.Name, prop.GetValue(_instance, null));
            }
        }
        /// <param name="item"></param>
        /// <param name="includeInstanceProperties">是否包含对象实例属性</param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<string, object> item, bool includeInstanceProperties = false)
        {
            bool res = Properties.ContainsKey(item.Key);
            if (res)
                return true;

            if (includeInstanceProperties && _instance != null)
            {
                foreach (var prop in this._instancePropertyInfos)
                {
                    if (prop.Name == item.Key)
                        return true;
                }
            }

            return false;
        }
        /// <param name="key"></param>
        /// <param name="includeInstanceProperties">是否包含对象实例属性</param>
        /// <returns></returns>
        public bool Contains(string key, bool includeInstanceProperties = false)
        {
            bool res = Properties.ContainsKey(key);
            if (res)
                return true;

            if (includeInstanceProperties && _instance != null)
            {
                foreach (var prop in this._instancePropertyInfos)
                {
                    if (prop.Name == key)
                        return true;
                }
            }

            return false;
        }

        void IDictionary<string, object>.Add(string key, object value)
        {
            Properties.Add(key,value);
        }

        bool IDictionary<string, object>.ContainsKey(string key)
        {
            return this.Contains(key,true);
        }

        ICollection<string> IDictionary<string, object>.Keys
        {
            get { return (ICollection<string>) GetDynamicMemberNames(); }
        }

        bool IDictionary<string, object>.Remove(string key)
        {
            return Properties.Remove(key);
        }

        bool IDictionary<string, object>.TryGetValue(string key, out object value)
        {
            value = null;
            var list = GetProperties(true);
            foreach (var valuePair in list)
            {
                if (valuePair.Key == key)
                {
                    value = valuePair.Value;
                    return true;
                }
            }
            return false;
        }

        ICollection<object> IDictionary<string, object>.Values
        {
            get { return (ICollection<object>)GetProperties(true).Select(c => c.Value); }
        }

        object IDictionary<string, object>.this[string key]
        {
            get { return this[key]; }
            set { this[key] = value; }
        }

        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            ((ICollection<KeyValuePair<string, object>>)Properties).Add(item);
        }

        void ICollection<KeyValuePair<string, object>>.Clear()
        {
            ((ICollection<KeyValuePair<string, object>>)Properties).Clear();
        }

        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)GetProperties(true)).Contains(item);
        }

        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, object>>)Properties).CopyTo(array,arrayIndex);
        }

        int ICollection<KeyValuePair<string, object>>.Count
        {
            get { return ((ICollection<KeyValuePair<string, object>>)GetProperties(true)).Count; }
        }

        bool ICollection<KeyValuePair<string, object>>.IsReadOnly
        {
            get { return ((ICollection<KeyValuePair<string, object>>)GetProperties(true)).IsReadOnly; }
        }

        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            return ((ICollection<KeyValuePair<string, object>>)Properties).Remove(item);
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return GetProperties(true).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)GetProperties(true)).GetEnumerator();
        }
    }
}
