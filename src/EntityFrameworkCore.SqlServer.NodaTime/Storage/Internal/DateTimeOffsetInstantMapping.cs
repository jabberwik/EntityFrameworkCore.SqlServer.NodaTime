﻿using System.Data;
using System.Data.Common;
using EntityFrameworkCore.SqlServer.NodaTime.Storage.ValueConversion;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;

namespace EntityFrameworkCore.SqlServer.NodaTime.Storage.Internal
{
    public class DateTimeOffsetInstantMapping : RelationalTypeMapping
    {
        public DateTimeOffsetInstantMapping()
            : base(SqlServerTypeNames.DateTimeOffset, typeof(Instant))
        {
        }

        protected DateTimeOffsetInstantMapping(RelationalTypeMappingParameters parameters)
            : base(parameters)
        {
        }

        public override ValueConverter Converter => InstantToDateTimeOffsetConverter.Instance;

        protected override string SqlLiteralFormatString => FormatStrings.GetDateTimeOffsetUtcFormat(this.Size);

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters) => new DateTimeOffsetInstantMapping(parameters);

        protected override void ConfigureParameter(DbParameter parameter)
        {
            base.ConfigureParameter(parameter);

            if (parameter is SqlParameter sqlParameter)
            {
                sqlParameter.SqlDbType = SqlDbType.DateTimeOffset;
            }

            if (Size.HasValue && Size.Value != -1)
            {
                parameter.Size = Size.Value;
            }
        }
    }
}
