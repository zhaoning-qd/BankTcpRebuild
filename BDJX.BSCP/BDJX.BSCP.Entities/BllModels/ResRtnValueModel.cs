using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 响应报文中的返回值实体
    /// </summary>
    public class ResRtnValueModel
    {
        /// <summary>
        /// 返回值对应的序号数组
        /// </summary>
        public int[] RtnCodeArray { get; set; }

        /// <summary>
        /// 返回值信息
        /// </summary>
        public string RtnInfo { get; set; }

        /// <summary>
        /// 获取实时类响应报文的返回值
        /// </summary>
        /// <returns>返回值</returns>
        public string GetRtnValueOnline()
        {
            string result = string.Empty;
            result = BasicOperation.GetOnlineTranscationReturnValue(1);
            Random r = new Random();
            int num = r.Next(50);
            for (int i=0; i< RtnCodeArray.Length; i++)
            {
                if (num == RtnCodeArray[i])
                {
                    result = BasicOperation.GetOnlineTranscationReturnValue(num);
                    break;
                }
            }
            this.RtnInfo = BasicOperation.GetFhxxByFhz(result);

            return result;
        }

        /// <summary>
        /// 获取批量类明细文件的返回值
        /// </summary>
        /// <returns>返回值</returns>
        public string GetRtnValueBatch()
        {
            string result = string.Empty;
            result = BasicOperation.GetOnlineTranscationReturnValue(1);
            Random r = new Random();
            int num = r.Next(30);
            for (int i = 0; i < RtnCodeArray.Length; i++)
            {
                if (num == RtnCodeArray[i])
                {
                    result = BasicOperation.GetBacthTranscationReturnValue(num);
                    break;
                }
            }

            return result;
        }
    }
}
