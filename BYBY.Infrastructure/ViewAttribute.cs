using System;

namespace BYBY.Infrastructure
{
    public class ViewAttribute : Attribute
    {
        private string _displayName;
        private string _enDisplayName;
        private int _sortIndex;

        public ViewAttribute(string displayname, int sortIndex = 0)
        {
            _displayName = displayname;
            _sortIndex = sortIndex;
        }
        public ViewAttribute(string displayname, string enDisplayName, int sortIndex = 0)
        {
            _displayName = displayname;
            _enDisplayName = enDisplayName;
            _sortIndex = sortIndex;
        }

        public string DisplayName
        {
            get
            {
                return _displayName;
            }

            set
            {
                _displayName = value;
            }
        }

        public string EnDisplayName
        {
            get
            {
                return _enDisplayName == null ? "" : _enDisplayName;
            }

            set
            {
                _enDisplayName = value;
            }
        }

        public int SortIndex
        {
            get
            {
                return _sortIndex;
            }

            set
            {
                _sortIndex = value;
            }
        }
    }
}
