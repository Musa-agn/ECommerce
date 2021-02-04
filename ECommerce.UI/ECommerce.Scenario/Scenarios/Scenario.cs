using ECommerce.Scenario.Model;
using ECommerce.Scenario.Operations;
using System;
using System.IO;

namespace ECommerce.Scenario.Scenarios
{
    public class Scenario
    {
        ProductOperation productOperation;
        CampaignOperation campaignOperation;
        OrderOperation orderOperation;
        IncreaseTimeOperation increaseTimeOperation;
        ScenarioOperation scenarioOperation;

        public Scenario()
        {
            productOperation = new ProductOperation();
            campaignOperation = new CampaignOperation();
            orderOperation = new OrderOperation();
            increaseTimeOperation = new IncreaseTimeOperation();
            scenarioOperation = new ScenarioOperation();
        }
        public void ScenariosRun()
        {
            string[] scenariosList = GetScenarioFilesPath();
            int scenarioCount = 0;
            foreach (var path in scenariosList)
            {
                scenarioCount++;
                Console.WriteLine("Scneario" + scenarioCount);

                StreamReader streamReader = new StreamReader(path);
                var scenario = streamReader.ReadToEnd().Split('\n');
                foreach (var item in scenario)
                {
                    if (string.IsNullOrWhiteSpace(item))
                        continue;

                    var operationParamaters = item.Split(' ');
                    if (Enum.IsDefined(typeof(OperationNameEnum), operationParamaters[0]))
                    {
                        var operationName = Enum.Parse(typeof(OperationNameEnum), operationParamaters[0]);
                        switch (operationName)
                        {
                            case OperationNameEnum.create_product:
                                productOperation.CreateProduct(operationParamaters);
                                break;
                            case OperationNameEnum.create_campaign:
                                campaignOperation.CreateCampaign(operationParamaters);
                                break;
                            case OperationNameEnum.create_order:
                                orderOperation.CreateOrder(operationParamaters);
                                break;
                            case OperationNameEnum.get_product_info:
                                productOperation.GetProductInfo(operationParamaters);
                                break;
                            case OperationNameEnum.increase_time:
                                increaseTimeOperation.IncreaseTime(operationParamaters);
                                break;
                            case OperationNameEnum.get_campaign_info:
                                campaignOperation.GetCampaignInfo(operationParamaters);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                        Console.WriteLine("\n****Operation name not found");
                }
                scenarioOperation.ResetData();
                Console.WriteLine("\n------------------------\n");
            }
        }

        private string[] GetScenarioFilesPath()
        {
            var pathScenario = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))) + "\\ScenarioFiles\\";
            string[] scenariosList = Directory.GetFiles(pathScenario);
            return scenariosList;
        }
    }
}
