//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace MadPiranha.Plugster.Base.Param
{
    public class KeyValueParam : CompositeParam
    {
        public KeyValueParam()
        {
            keyValuePairs = new ArrayList();
        }

        private IList keyValuePairs;
        public IList KeyValuePairs
        {
            get { return keyValuePairs; }
//            set { keyValuePairs = value; }
        }

        private string keyString;
        public string KeyString
        {
            get { return keyString; }
//            set { keyString = value; }
        }

        private string valueString;
        public string ValueString
        {
            get { return valueString; }
//            set { valueString = value; }
        }

        private string selectedText;
        public string SelectedText
        {
            get { return selectedText; }
            set { selectedText = value; }
        }

        public void Add(string key, string val)
        {
            KeyValuePairs.Add(new KeyVal(key, val));
        }

        public override IParam[] Values
        {
            get { throw new NotImplementedException(); }
        }
    }

    [Serializable]
    class KeyVal : MarshalByRefObject
    {
        private string key;
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        private string value;
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public KeyVal(string key, string val)
        {
            Key = key;
            Value = val;
        }
    }

}
