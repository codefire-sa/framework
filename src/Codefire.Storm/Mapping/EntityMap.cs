using System;
using System.Collections.Generic;

namespace Codefire.Storm.Mapping
{
    public class EntityMap
    {
        private Type _entityType;
        private string _tableName;
        private string _viewName;
        private string _insertProcedure;
        private string _updateProcedure;
        private string _deleteProcedure;
        private string _reinstateProcedure;
        private string _currentUserParameter;
        private bool _readOnly;
        private IdMap _id;
        private List<PropertyMap> _properties;
        private List<JoinMap> _joins;

        public EntityMap()
        {
            _properties = new List<PropertyMap>();
            _joins = new List<JoinMap>();
        }

        public Type EntityType
        {
            get { return _entityType; }
            set { _entityType = value; }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public string ViewName
        {
            get { return _viewName; }
            set { _viewName = value; }
        }

        public string InsertProcedure
        {
            get { return _insertProcedure; }
            set { _insertProcedure = value; }
        }

        public string UpdateProcedure
        {
            get { return _updateProcedure; }
            set { _updateProcedure = value; }
        }

        public string DeleteProcedure
        {
            get { return _deleteProcedure; }
            set { _deleteProcedure = value; }
        }

        public string ReinstateProcedure
        {
            get { return _reinstateProcedure; }
            set { _reinstateProcedure = value; }
        }

        public string CurrentUserParameter
        {
            get { return _currentUserParameter; }
            set { _currentUserParameter = value; }
        }

        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; }
        }

        public IdMap Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public List<PropertyMap> Properties
        {
            get { return _properties; }
        }

        public List<JoinMap> Joins
        {
            get { return _joins; }
        }
    }
}