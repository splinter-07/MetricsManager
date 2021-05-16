using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.DAL
{
    public class DateTimeOffsetHalder: SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override DateTimeOffset Parse(object value) => DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(value));
        
        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value) => parameter.Value = value;
    }
}
