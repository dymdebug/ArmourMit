 /*  作者：       tianzh
 *  创建时间：   2012/7/27 10:04:26
 *
 */


using RMTFRK.Core.Helper.Tool;
using RMTFRK.Web.Helper.Json;


namespace RMTFRK.Web.Helper.Filter
{
  public class FilterHelper
    {
      /// <summary>
      /// 解析where并返回翻译对象(请确保UI层查询条件与数据库的模型一致,否则请自行进行对where的条件进行参数转化解析)
      /// </summary>
      /// <param name="where">查询条件where(与数据库模型一致的查询对象的where)</param>
      /// <returns></returns>
      public static string GetFilterTanslate(string where)
      {
          string commandText = "";
          if (!where.IsNullOrEmpty())
          {
              FilterTranslator translate = new FilterTranslator();
              translate.Group = NJsonHelper.FromJson<FilterGroup>(where);
              translate.Translate();
              commandText = FilterParam.AddParameters(translate.CommandText, translate.Parms);
          }
              return commandText;
      }
      //public static string ConvertToFilterRulesNameParamsToSqlParams(string Where,
    }
}
