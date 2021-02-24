using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace PowerPlantChallenge
{
    [ApiController]
    [Route("[controller]")]
    public class ProductionPlanController : ControllerBase
    {
        [HttpPost]
        public List<ProductionPlan> PostProductionPlan([FromBody] LoadFile newLoad)
        {
            // list to return
            List<ProductionPlan> productionPlans = new List<ProductionPlan>();

            // Applying a sort of Merit Order by caculating the Marginal Cost on each plant in short terme
            newLoad.Powerplants.ForEach(plant => {
                switch (plant.Type)
                {
                    case "gasfired":
                        plant.MarginalCost = plant.Pmin * plant.Efficiency * newLoad.Fuels.GasEuroMWh;
                        break;
                    case "turbojet":
                        plant.MarginalCost = plant.Efficiency * newLoad.Fuels.KerosineEuroMWh;
                        break;
                    case "windturbine":
                        plant.MarginalCost = 0;
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("Error: Powerplant type not found.");
                        break;
                }
            });

            // Sort by maximun power available, then by marginal cost to reduce production cost
            List<Powerplant> orderedPowerPlants = newLoad.Powerplants.OrderBy(p => p.Pmax).ThenBy(p => p.MarginalCost).ToList();

            // apply distribution : gas, turbo, wind
            bool ending = false;
            for (int i = orderedPowerPlants.Count - 1; i >= 0; i--)
            {
                if (newLoad.Load >= orderedPowerPlants[i].Pmin && !ending)
                {
                    int nb = newLoad.Load / orderedPowerPlants[i].Pmax;
                    newLoad.Load = newLoad.Load % orderedPowerPlants[i].Pmax;

                    // check if load amount reached
                    if (nb == 0)
                    {
                        productionPlans.Add(new ProductionPlan(orderedPowerPlants[i].Name, newLoad.Load));
                        ending = true;
                    }
                    else
                    {
                        productionPlans.Add(new ProductionPlan(orderedPowerPlants[i].Name, orderedPowerPlants[i].Pmax * nb));
                    }
                }
                else
                {
                    productionPlans.Add(new ProductionPlan(orderedPowerPlants[i].Name, 0));
                }
            };

            return productionPlans;
        }
    }
}
