﻿using Green.App.Service.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Green.App.Service.Dao
{
    public class SchemaVersionMap : ClassMapping<SchemaVersion>
    {
        public SchemaVersionMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.Version);
            Property(x => x.Script);
        }
    }
}