using ECommerce.Scenario.BaseClient;

namespace ECommerce.Scenario.Operations
{
    public class ScenarioOperation
    {
        private readonly RestClientHelper _restClientHelper;
        public ScenarioOperation()
        {
            _restClientHelper = new RestClientHelper();
        }
        public void ResetData()
        {
            _restClientHelper.ResetData();
        }

    }
}
