using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKKInfo
{
    public class ParcelData
    {
        public string CadastralNumber;
        public string Address;
        public string UtilityByDict;
        public string UtilityByDoc;
        public double Area;
        public string AreaUnit;
        public double CadastralCost;
        public string CadastralCostUnit;
        public string CadastralEngineer;
        public string Status;
        public string CadastralRegDate;
        public double CenterX;
        public double CenterY;

        public ParcelData(PKKObjectParcel rawData)
        {
            // Кадастровый номер
            CadastralNumber = rawData.feature.attrs.cn;

            // Адрес
            Address = rawData.feature.attrs.address == null ? null : rawData.feature.attrs.address.ToString();

            // Разрешенное использование по справочнику
            string utilityRawStr = rawData.feature.attrs.util_code;
            if (RosreestrDictionaries.UtilityDictionary.ContainsKey(utilityRawStr))
                UtilityByDict = RosreestrDictionaries.UtilityDictionary[utilityRawStr];
            else
                UtilityByDict = $"Неизвестно (код {utilityRawStr})";

            // Разрешенное использование по документу
            UtilityByDoc = rawData.feature.attrs.util_by_doc;
            UtilityByDoc = UtilityByDoc.Substring(0, 1).ToUpper() + UtilityByDoc.Substring(1);

            // Площадь
            Area = rawData.feature.attrs.area_value;

            // Единицы измерения площади
            string areaUnitRawStr = rawData.feature.attrs.area_unit;
            if (RosreestrDictionaries.Units.ContainsKey(areaUnitRawStr))
                AreaUnit = RosreestrDictionaries.Units[areaUnitRawStr];
            else
                AreaUnit = "кв. м";

            // Кадастровая стоимость
            CadastralCost = rawData.feature.attrs.cad_cost;

            // Единицы измерения кадастровой стоимости
            string cadCostRawStr = rawData.feature.attrs.cad_unit;
            if (RosreestrDictionaries.Units.ContainsKey(cadCostRawStr))
                CadastralCostUnit = RosreestrDictionaries.Units[cadCostRawStr];
            else
                CadastralCostUnit = "руб.";

            // Сведения о кадастровом инженере
            var cadEng = rawData.feature.attrs.cad_eng_data;
            if (cadEng != null)
            {
                if (cadEng.co_name != null)
                {
                    CadastralEngineer = cadEng.co_name;
                }
                else if (cadEng.ci_first != null)
                {
                    CadastralEngineer = String.Format("{0} {1} {2}, аттестат № {3}", cadEng.ci_surname, cadEng.ci_first, cadEng.ci_patronymic, cadEng.ci_n_certificate);
                }
                else
                    CadastralEngineer = null;
            }
            else
                CadastralEngineer = null;

            // Статус
            string rawStateStr = rawData.feature.attrs.statecd;
            if (RosreestrDictionaries.State.ContainsKey(rawStateStr))
                Status = RosreestrDictionaries.State[rawStateStr];
            else
                Status = $"Неизвестно (код {rawStateStr})";

            // Дата постановки на кадастровый учет
            string rawCadReg = rawData.feature.attrs.date_create;
            DateTime cadRegDate;
            bool dateParsed = DateTime.TryParse(rawCadReg, out cadRegDate);
            if (dateParsed)
                CadastralRegDate = cadRegDate.ToLongDateString();
            else
                CadastralRegDate = $"Неизвестно ({rawCadReg})";

            // Центральная позиция X и Y
            CenterX = rawData.feature.center.x;
            CenterY = rawData.feature.center.y;
        }
    }
}
