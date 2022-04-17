namespace Movies.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this object @enum) => @enum.GetType().GetMember(Convert.ToString(@enum)!)[0].GetCustomAttribute<DescriptionAttribute>()!.Description;
}