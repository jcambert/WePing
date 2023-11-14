using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Volo.Abp.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Notifications;
using Volo.Abp.DependencyInjection;
using We.Ping.Queries;
using We.Ping.Smart.Localization;

namespace We.Ping.Smart.Blazor;

public class SmartPingComponentBase<T>: AbpComponentBase
    where T:class,ISmartQuery
{
    private bool _isLoading = false;
    private readonly string _queryName ;
    protected string ExternalApiUrl { get; set; } = string.Empty;

    [Inject]IAbpLazyServiceProvider ServiceProvider { get; set; }
    [Inject] protected T Query { get; set; }
    protected IJSRuntime jsRuntime  => ServiceProvider.LazyGetRequiredService<IJSRuntime>();

    protected ISmartAppService AppService => ServiceProvider.LazyGetRequiredService<ISmartAppService>();

    protected IUiNotificationService? NotificationService=>ServiceProvider.LazyGetService<IUiNotificationService>();
    protected IValidator<T>? Validator => ServiceProvider.LazyGetService<IValidator<T>>();
    public SmartPingComponentBase()
    {
        LocalizationResource = typeof(SmartResource);
        _queryName=typeof(T).Name;
    }

    protected string QueryName => _queryName;
    public bool IsLoading => _isLoading;

    protected void Loading(bool v)=>_isLoading = v;

    protected virtual bool ValidateQuery() => Validator?.Validate(Query).IsValid ?? true;
    protected virtual void Clear() {
        ExternalApiUrl = string.Empty;
    }
    public virtual Task SendRequest()
    {
        Clear();
        return Task.CompletedTask;
      
    }
    public async Task NavigateToSmartPingApi(MouseEventArgs e)
    {
        try
        {

            await jsRuntime.InvokeAsync<object>("open", ExternalApiUrl, "_blank");
        }
        catch (Exception ex)
        {
            await Notify.Error(ex.Message);
        }
    }
}
