using com.mobiquity.exception;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace com.mobiquity.service.Test
{
    public class MobiquityServiceTest
    {
        const string WEIGHT_SCENARIO = @"TestData\WeightCost.txt";
        const string ITEMS_SCENARIO = @"TestData\ITemsScenario.txt";
        const string MAXW_SCENARIO = @"TestData\MaxWeight.txt";

        [Fact]
        public void ValidateItemsScenario()
        {
            Assert.Throws<APIException>(() => MobiquityService.pack(ITEMS_SCENARIO));
        }


        [Fact]
        public void ValidateWeightScenario()
        {
            Assert.Throws<APIException>(() => MobiquityService.pack(MAXW_SCENARIO));
        }

        [Fact]
        public void ValidateWeightCostScenario()
        {
            Assert.Throws<APIException>(() => MobiquityService.pack(WEIGHT_SCENARIO));
        }

    }
}
