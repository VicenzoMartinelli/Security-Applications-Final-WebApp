using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityWebApp.Models.Config
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using System.Reflection;

  public static class EnumHelper
  {
    private static string LookupResource(Type resourceManagerProvider, string resourceKey)
    {
      foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
      {
        if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
        {
          System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
          return resourceManager.GetString(resourceKey);
        }
      }

      return resourceKey;
    }

    public static string GetDisplayValue(Enum value)
    {
      var fieldInfo = value.GetType().GetField(value.ToString());

      var descriptionAttributes = fieldInfo.GetCustomAttributes(
          typeof(DisplayAttribute), false) as DisplayAttribute[];

      if (descriptionAttributes[0].ResourceType != null)
        return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

      if (descriptionAttributes == null) return string.Empty;
      return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
    }
  }
}
