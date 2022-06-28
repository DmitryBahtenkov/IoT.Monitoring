using System.Runtime.CompilerServices;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Iot.Main.Infrastructure.Builders;

public class FluxQueryBuilder
{
    private StringBuilder _stringBuilder;

    public FluxQueryBuilder()
    {
        _stringBuilder = new();
    }

    public FluxQueryBuilder FromBucket(string bucketName)
    {
        _stringBuilder.Append($"from(bucket: \"{bucketName}\")");
        return this;
    }

    public FluxQueryBuilder WithNullRange()
    {
        _stringBuilder.Append("|> range(start: 0)");
        return this;
    }

    public FluxQueryBuilder WithFields(params string[] fields)
    {
        var filterString = "r[\"_field\"] == \"{0}\"";

        var or = string.Join(" or ", fields.Select(x => string.Format(filterString, x)));

        _stringBuilder.Append(string.Format("|> filter(fn: (r) => {0})", or));
        
        return this;
    }

    public FluxQueryBuilder WithEqual(string column, string value)
    {
        _stringBuilder.Append($"|> filter(fn: (r) => r[\"{column}\"] == \"{value}\")");
        return this;
    }

    public FluxQueryBuilder WithPivot(string rowKey, string columnKey, string valueColumn)
    {
        _stringBuilder.Append($"|> pivot(rowKey: [\"{rowKey}\"], columnKey: [\"{columnKey}\"], valueColumn: \"{valueColumn}\")");
        return this;
    }

    public string BuildYield(string name)
    {
        _stringBuilder.Append($"|> yield(name: \"{name}\")");
        return _stringBuilder.ToString();
    }
}