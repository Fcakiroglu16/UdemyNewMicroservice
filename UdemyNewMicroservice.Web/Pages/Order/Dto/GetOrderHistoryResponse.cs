#region

using UdemyNewMicroservice.Web.Pages.Order.ViewModel;

#endregion

namespace UdemyNewMicroservice.Web.Pages.Order.Dto;

public record GetOrderHistoryResponse(DateTime Created, decimal TotalPrice, List<OrderItemViewModel> Items);