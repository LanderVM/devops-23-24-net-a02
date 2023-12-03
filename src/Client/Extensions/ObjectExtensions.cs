using System;
using System.Collections;
using System.Web;

namespace devops_23_24_net_a02.Client.Extensions;

public static class ObjectExtensions
{
  public static string AsQueryString(this object obj)
  {
    var properties = from p in obj.GetType().GetProperties()
                     where p.GetValue(obj, null) != null
                     select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

    return string.Join("&", properties.ToArray());
  }
  private static string GetQueryStringFromProperty(System.Reflection.PropertyInfo property)
  {
    object value = property.GetValue(property.DeclaringType, null);

    if (value is IEnumerable enumerable && !(value is string))
    {
      var values = from object item in enumerable
                   select $"{property.Name}={HttpUtility.UrlEncode(item.ToString())}";

      return string.Join("&", values.ToArray());
    }
    else
    {
      return $"{property.Name}={HttpUtility.UrlEncode(value.ToString())}";
    }
  }
}
