using System;
using System.ComponentModel;
using System.Reflection;

namespace Admin.Enums;

/*
    Enums cho trạng thái đơn hàng
*/

public enum OrderStatus
{
    [Description("Đã thanh toán")]
    PAID,

    [Description("Đơn hàng đang được xử lý")]
    PROCESSING,

    [Description("Đã giao hàng thành công")]
    DELIVERED,

    [Description("Đơn hàng bị từ chối")]
    DENIED
}

public static class EnumHelper
{
    public static string GetDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();

        return attribute?.Description ?? value.ToString();
    }
}