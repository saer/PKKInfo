using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Automatically generated from JSON on http://jsonutils.com/

namespace PKKInfo
{
    public class CadEngData
    {
        [JsonProperty("actual_date")]
        public string actual_date { get; set; }

        [JsonProperty("co_name")]
        public string co_name { get; set; }

        [JsonProperty("ci_first")]
        public string ci_first { get; set; }

        [JsonProperty("ci_n_certificate")]
        public string ci_n_certificate { get; set; }

        [JsonProperty("ci_patronymic")]
        public string ci_patronymic { get; set; }

        [JsonProperty("ci_surname")]
        public string ci_surname { get; set; }

        [JsonProperty("lastmodified")]
        public string lastmodified { get; set; }

        [JsonProperty("rc_type")]
        public int rc_type { get; set; }
    }

    public class Attrs
    {
        [JsonProperty("adate")]
        public string adate { get; set; }

        [JsonProperty("address")]
        public object address { get; set; }

        [JsonProperty("anno_text")]
        public string anno_text { get; set; }

        [JsonProperty("area_type")]
        public string area_type { get; set; }

        [JsonProperty("area_unit")]
        public string area_unit { get; set; }

        [JsonProperty("area_value")]
        public double area_value { get; set; }

        [JsonProperty("cad_cost")]
        public double cad_cost { get; set; }

        [JsonProperty("cad_eng_data")]
        public CadEngData cad_eng_data { get; set; }

        [JsonProperty("cad_record_date")]
        public string cad_record_date { get; set; }

        [JsonProperty("cad_unit")]
        public string cad_unit { get; set; }

        [JsonProperty("category_type")]
        public string category_type { get; set; }

        [JsonProperty("cn")]
        public string cn { get; set; }

        [JsonProperty("date_cost")]
        public string date_cost { get; set; }

        [JsonProperty("date_create")]
        public string date_create { get; set; }

        [JsonProperty("fp")]
        public string fp { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("kvartal")]
        public string kvartal { get; set; }

        [JsonProperty("kvartal_cn")]
        public string kvartal_cn { get; set; }

        [JsonProperty("okrug")]
        public string okrug { get; set; }

        [JsonProperty("okrug_cn")]
        public string okrug_cn { get; set; }

        [JsonProperty("pubdate")]
        public string pubdate { get; set; }

        [JsonProperty("rayon")]
        public string rayon { get; set; }

        [JsonProperty("rayon_cn")]
        public string rayon_cn { get; set; }

        [JsonProperty("reg")]
        public int reg { get; set; }

        [JsonProperty("rifr")]
        public object rifr { get; set; }

        [JsonProperty("rights_reg")]
        public int rights_reg { get; set; }

        [JsonProperty("sale")]
        public object sale { get; set; }

        [JsonProperty("statecd")]
        public string statecd { get; set; }

        [JsonProperty("util_by_doc")]
        public string util_by_doc { get; set; }

        [JsonProperty("util_code")]
        public string util_code { get; set; }
    }

    public class Center
    {
        [JsonProperty("x")]
        public double x { get; set; }

        [JsonProperty("y")]
        public double y { get; set; }
    }

    public class Extent
    {
        [JsonProperty("xmax")]
        public double xmax { get; set; }

        [JsonProperty("xmin")]
        public double xmin { get; set; }

        [JsonProperty("ymax")]
        public double ymax { get; set; }

        [JsonProperty("ymin")]
        public double ymin { get; set; }
    }

    public class Feature
    {

        [JsonProperty("attrs")]
        public Attrs attrs { get; set; }

        [JsonProperty("center")]
        public Center center { get; set; }

        [JsonProperty("extent")]
        public Extent extent { get; set; }

        [JsonProperty("stat")]
        public object stat { get; set; }

        [JsonProperty("type")]
        public int type { get; set; }
    }

    public class PKKObjectParcel
    {

        [JsonProperty("feature")]
        public Feature feature { get; set; }

        [JsonProperty("note")]
        public string note { get; set; }

        [JsonProperty("status")]
        public int status { get; set; }
    }

}

