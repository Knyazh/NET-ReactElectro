using ElectroEcommerce.DataBase.DTOs.Order;
using System.Text;

namespace ElectroEcommerce.Contracts;

public class InVoice
{
	public static string GenerateInvoiceHtml(OrderDetailsDTO DTO)
	{
		StringBuilder htmlBuilder = new StringBuilder();

		htmlBuilder.Append($@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Document</title>
</head>
<body style=""margin: 0px; padding: 0px; box-sizing: border-box;"">
    <header style=""width: 100%;"">
        <div style=""width: 96%; margin: 0px auto;"" class=""container"">
            <div style=""width: 100%; padding: 30px 0px;"" class=""row"">
                 <div style=""width: 100%; display: flex; align-items: center; justify-content: space-between; padding: 25px 0px; border-bottom: 1px solid black;"" class=""provider"">
                       <div style=""width: 250px; height: 100px;"" class=""image"">
                       </div>
                       <div style=""display: flex; align-items: center; justify-content: center; color: #c5df59; font-size: 20px;"" class=""Provider-name""><h1>.NET ELECTRO</h1></div>
                       <div style=""display: flex; align-items: center; justify-content: flex-end; font-size: 24px; font-weight: bold; letter-spacing: 2px;""><p>{DTO.OrderTrackingCode}</p></div>
                 </div>

                 <div style=""width: 100%; padding: 15px 0px; display: flex; align-items: center; justify-content: center;"">
                     <div style=""display: flex; margin: 0px 30px; font-size: 12px; font-weight: bold;""><p style=""margin-right: 15px;"">Invoice ID: </p><p>{DTO.OrderID}</p></div>
                     <div style=""display: flex; margin: 0px 30px; font-size: 12px; font-weight: bold;""><p style=""margin-right: 15px;"">Invoice Date: </p><p>{DTO.OrderCreatedAt}</p></div>
                     <div style=""display: flex; margin: 0px 30px; font-size: 12px; font-weight: bold;""><p style=""margin-right: 15px;"">Order status: </p><p>{DTO.CurrentOrderStatus}</p></div>
                 </div>

                 <div style=""width: 100%; padding: 15px 0px; display: flex; align-items: center; justify-content: center;"">
                    <div style=""display: flex; margin: 0px 30px; font-size: 12px; font-weight: bold;""><p style=""margin-right: 15px;"">Customer fullname: </p><p>{DTO.CurrentUserName + " " + DTO.CurrentUserSurname}</p></div>
                    <div style=""display: flex; margin: 0px 30px; font-size: 12px; font-weight: bold;""><p style=""margin-right: 15px;"">Customer email address: </p><p>{DTO.CurrentUserEmail}</p></div>
                    <div style=""display: flex; margin: 0px 30px; font-size: 12px; font-weight: bold;""><p style=""margin-right: 15px;"">Customer phone number: </p><p>{DTO.CurrentUserPhoneNumber}</p></div>
                </div>

                <div style=""width: 100%; padding: 15px 0px; display: flex; align-items: center; justify-content: center;"">
                    <div style=""display: flex; margin: 0px 30px; font-size: 12px; font-weight: bold;""><p style=""margin-right: 15px;"">Address: </p><p>Azerbaijan Baku</p></div>
                    <div style=""display: flex; margin: 0px 30px; font-size: 12px; font-weight: bold;""><p style=""margin-right: 15px;"">Contact: </p><p>+994121234567</p></div>
                </div>
            </div>
        </div>
    </header>
    <main style=""font-family: Arial, sans-serif; margin: 0; padding: 0;"">
        
            <div style=""width: 96%; margin: auto; padding: 20px; box-shadow: 0px 0px 10px 15px rgba(0,0,0,0.1); border-radius: 10px; overflow: hidden;"">
                <h1 style=""text-align: center; color: #333;"">Order Details</h1>
                <table style=""width: 100%; border-collapse: collapse; margin-top: 20px;"">
                    <tr>
                        <th style=""border: 1px solid #9c9c9c; background-color: rgb(197, 239, 89); color: white; text-align: left; padding: 10px;"">#</th>
                        <th style=""border: 1px solid #9c9c9c; background-color: rgb(197, 239, 89); color: white; text-align: left; padding: 10px;"">Product Code</th>
                        <th style=""border: 1px solid #9c9c9c; background-color: rgb(197, 239, 89); color: white; text-align: left; padding: 10px;"">Category</th>
                        <th style=""border: 1px solid #9c9c9c; background-color: rgb(197, 239, 89); color: white; text-align: left; padding: 10px;"">Brand</th>
                        <th style=""border: 1px solid #dddddd; background-color: rgb(197, 239, 89); color: white; text-align: left; padding: 10px;"">Product Name</th>
                        <th style=""border: 1px solid #dddddd; background-color: rgb(197, 239, 89); color: white; text-align: left; padding: 10px;"">Quantity</th>
                        <th style=""border: 1px solid #dddddd; background-color: rgb(197, 239, 89); color: white; text-align: left; padding: 10px;"">Single Price</th>
                        <th style=""border: 1px solid #dddddd; background-color: rgb(197, 239, 89); color: white; text-align: left; padding: 10px;"">Total Price</th>
                    </tr>");

		int count = 0;
		foreach (var Order_Item_Details_DTO in DTO.Order_Item_Details_DTOs)
		{
			count++;
			htmlBuilder.Append($@"<tr>
                        <td>{count}</td>
                        <td>{Order_Item_Details_DTO.ProductCode}</td>
                        <td>{Order_Item_Details_DTO.CategoryName}</td>
                        <td>{Order_Item_Details_DTO.BrandName}</td>
                        <td>{Order_Item_Details_DTO.ProductName}</td>
                        <td>{Order_Item_Details_DTO.Quantity}</td>
                        <td>{Order_Item_Details_DTO.OrderItemSinglePrice}<span>$</span></td>
                        <td>{Order_Item_Details_DTO.OrderItemTotalPrice}<span>$</span></td>
                       
                    </tr>");
		}

		htmlBuilder.Append($@"</table>
            </div>
        
    </main>
    <footer style=""width: 100%;"">
        
        <div style=""width: 96%; margin: 0px auto;"" class=""container"">
            <div style=""width: 100%; padding: 30px 0px; display: flex; align-items: center; justify-content: flex-end;"" class=""row"">
                 <div style=""display: flex; align-items: center; width: 25%; font-size: 36px; border-radius: 8px; justify-content: center; background-color: #c5df59; color: white; padding: 0px 25px;""><p>Summary Total: </p><p style=""margin-left: 10px;"">{DTO.SummaryTotal}</p><span>$</span></div>
            </div>
        </div>

    </footer>
</body>
</html>");

		return htmlBuilder.ToString();
	}
}
