using Microsoft.AspNetCore.Components;
using VendingMachine.Client.Services.Buyer;
using VendingMachine.Shared.Dto;

namespace VendingMachine.Client.Pages.Buyer
{
    public partial class OrdersHistory
    {
        [Inject] public IBuyerService _buyerService { get; set; }

        public string message { get; set; } = string.Empty;


        public List<CustomerProductDto>? LstOfOrders { get; set; }


        protected override async Task OnInitializedAsync()
        {
            LstOfOrders = await _buyerService.GetProductOrderedHistory();
        }

    }
}
