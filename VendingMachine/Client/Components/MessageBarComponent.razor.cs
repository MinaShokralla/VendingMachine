using Microsoft.AspNetCore.Components;

namespace VendingMachine.Client.Components;

public partial class MessageBarComponent : ComponentBase
{
    private string? _InputMessage;

    [Parameter]
    public string? InputMessage
    {
        get { return _InputMessage; }
        set
        {
            _InputMessage = value;
            if (string.IsNullOrWhiteSpace(_InputMessage)) return;
            if (_InputMessage.ToUpper().StartsWith("ERROR") || _InputMessage.ToUpper().StartsWith("FAIL"))
                Style = "alert-danger";
            else if (_InputMessage.ToUpper().StartsWith("SUCCESS"))
                Style = "alert-success";
            else
                Style = "alert-primary";
        }
    }

    public string Style { get; set; } = string.Empty;

}
