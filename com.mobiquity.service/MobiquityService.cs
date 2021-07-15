using com.mobiquity.common.Constant;
using com.mobiquity.common.Helper;
using com.mobiquity.common.Models;
using com.mobiquity.exception;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace com.mobiquity.service
{
    public sealed class MobiquityService
    {
        public static string pack(string filePath)
        {
            string res = string.Empty;
            List<string> lines = File.ReadAllLines(filePath).ToList();
            MobiquityService packer = new MobiquityService();
            foreach (var line in lines)
            {
                res += packer.ProcessPackages(line) + Environment.NewLine;
            }

            return res;
        }

        /// <summary>
        /// Process Individual line, here item is a single line from the given file
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string ProcessPackages(string item)
        {
            var packageWeightList = item.Split(':').ToList();
            string weight = packageWeightList[0];
            string res;
            if (int.Parse(weight) <= MobiquityConstant.MAX_PACKAGE_WEIGHT)
            {
                res = ProcessItem(packageWeightList[1], double.Parse(weight));

            }
            else
            {
                throw new APIException("Package Weight is hiher than expected");
            }
            return res;
        }

        /// <summary>
        /// Process single package from list of Packages for the given weight
        /// </summary>
        /// <param name="item"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        private string ProcessItem(string item, double weight)
        {
            // To store list of package which is within given weight
            List<PackDto> packList = new List<PackDto>();
            string res = string.Empty;
            var temp = item.Trim().Replace(" ", "").Split(')').ToList();
            if (temp.Count <= MobiquityConstant.MAX_ITEMS_COUNT)
            {
                foreach (var pack in temp)
                {
                    if (!string.IsNullOrEmpty(pack))
                    {
                        var packets = pack.Trim().Replace("(", "").Split(',').ToList();
                        if (double.Parse(packets[1]) <= MobiquityConstant.MAX_ITEM_WEIGHT && int.Parse(packets[2].Substring(1)) <= MobiquityConstant.MAX_ITEM_COST)
                        {
                            if (double.Parse(packets[1]) <= weight)
                            {
                                PackDto pack1 = new PackDto
                                {
                                    Id = int.Parse(packets[0]),
                                    Weight = double.Parse(packets[1]),
                                    Price = int.Parse(packets[2].Substring(1))
                                };
                                packList.Add(pack1);
                            }

                        }
                        else
                        {
                            throw new APIException("Max weight and cost of an item Exceeds");
                        }
                    }


                }
            }
            else
            {
                throw new APIException("Exceed Packed Count");
            }
            if (packList.Count > 0)
            {
                //To Identify Package ID which is weight is less than or equal to the package limit and the total cost is as large as possible
                res = MobiquityHelper.ProcessPacketList(packList, weight);
            }
            else
            {
                res = "-";
            }

            return res;
        }
    }
}
