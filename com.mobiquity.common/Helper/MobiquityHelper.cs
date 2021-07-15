using com.mobiquity.common.Models;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.common.Helper
{
    public static class MobiquityHelper
    {
        /// <summary>
        /// Below two method is Rescursive funtion to identify below :
        /// - package so that the total weight is less than or equal to the package limit and the total cost is as large as possible
        /// - to send a package which weighs less in case there is more than one package with the same price.
        /// </summary>
        /// <param name="packList"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static string ProcessPacketList(List<PackDto> packList, double weight)
        {
            string res = string.Empty;
            double tempWeight = packList[0].Weight, remainWeight = 0.0;
            int tempCost = packList[0].Price;
            int id = packList[0].Id;
            foreach (var list in packList)
            {
                if (list.Price > tempCost)
                {
                    tempCost = list.Price;
                    tempWeight = list.Weight;
                    id = list.Id;
                }
                else if (list.Price == tempCost && list.Weight < tempWeight)
                {
                    tempWeight = list.Weight;
                    id = list.Id;
                }
            }
            res += id;
            remainWeight = weight - tempWeight;
            if (remainWeight > 0)
            {
                string temp = OtherPackets(remainWeight, packList.Where(x => x.Id != id).ToList());
                if (!string.IsNullOrEmpty(temp))
                {
                    res += "," + temp;
                }
            }

            return res;
        }

        private static string OtherPackets(double remainWeight, List<PackDto> packList)
        {
            string res = string.Empty;
            List<PackDto> packDtos = packList.Where(x => x.Weight <= remainWeight).ToList();
            if (packDtos.Count > 0)
            {
                res = ProcessPacketList(packDtos, remainWeight);
            }
            return res;
        }
    }
}
