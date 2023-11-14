using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Modularity;

namespace We.Ping.PointCalculator;

/// <summary>
/// 
/// </summary>
public class PointCalculatorService:AbpModule
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
    }
}

public abstract class PointCalculatorAppService : ApplicationService
{
    protected PointCalculatorAppService()
    {
       // LocalizationResource = typeof(PointCalculatorResource);
        ObjectMapperContext = typeof(PointCalculatorService);
    }
}


public sealed record PointGagnePerdu(double PointsJ1,double PointsJ2);
public sealed record PointsJoueur(string Licence,double Points);
/// <summary>
/// 
/// </summary>
public interface ICalculateurPoints
{
    PointGagnePerdu Calculate(PointsJoueur Joueur1,PointsJoueur Joueur2,bool victoireJ1);
}


/// <summary>
/// 
/// </summary>
[Dependency(ServiceLifetime.Scoped), ExposeServices(typeof(ICalculateurPoints))]

public class CalculateurPointsAppService : PointCalculatorAppService, ICalculateurPoints
{
    public PointGagnePerdu Calculate(PointsJoueur Joueur1, PointsJoueur Joueur2, bool victoireJ1)
    {
        throw new NotImplementedException();
    }
}

