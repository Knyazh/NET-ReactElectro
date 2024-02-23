using ElectroEcommerce.DataBase.DTOs.Order;

namespace ElectroEcommerce.Services.Abstracts;

public interface IOrderService
{
	Task<string> PrepareAndSendOrderInvoiceAsync(OrderDetailsDTO DTO);
}
