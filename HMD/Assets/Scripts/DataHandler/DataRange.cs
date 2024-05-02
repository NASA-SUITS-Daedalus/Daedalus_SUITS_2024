using UnityEngine;

public class DataRanges : MonoBehaviour
{
    [System.Serializable]
    public class DataRange
    {
        public double Min;
        public double Nominal;
        public double Max;

        public DataRange(double min, double nominal, double max)
        {
            Min = min;
            Nominal = nominal;
            Max = max;
        }
    }

    // -1 means no nominal value is provided
    public DataRange batt_time_left = new DataRange(3600, -1, 10800);
    public DataRange oxy_pri_storage = new DataRange(20, -1, 100);
    public DataRange oxy_sec_storage = new DataRange(20, -1, 100);
    public DataRange oxy_pri_pressure = new DataRange(600, -1, 3000);
    public DataRange oxy_sec_pressure = new DataRange(600, -1, 3000);
    public DataRange oxy_time_left = new DataRange(3600, -1, 21600);
    public DataRange coolant_storage = new DataRange(80, 100, 100);
    public DataRange heart_rate = new DataRange(50, 90, 160);
    public DataRange oxy_consumption = new DataRange(0.05, 0.1, 0.15);
    public DataRange co2_production = new DataRange(0.05, 0.1, 0.15);
    public DataRange suit_pressure_oxy = new DataRange(3.5, 4.0, 4.1);
    public DataRange suit_pressure_co2 = new DataRange(0, 0, 0.1);
    public DataRange suit_pressure_other = new DataRange(0, 0, 0.5);
    public DataRange suit_pressure_total = new DataRange(3.5, 4.0, 4.5);
    public DataRange helmet_pressure_co2 = new DataRange(0, 0.1, 0.15);
    public DataRange fan_pri_rpm = new DataRange(20000, 30000, 30000);
    public DataRange fan_sec_rpm = new DataRange(20000, 30000, 30000);
    public DataRange scrubber_a_co2_storage = new DataRange(0, -1, 60);
    public DataRange scrubber_b_co2_storage = new DataRange(0, -1, 60);
    public DataRange temperature = new DataRange(50, 70, 90);
    public DataRange coolant_liquid_pressure = new DataRange(100, 500, 700);
    public DataRange coolant_gas_pressure = new DataRange(0, 0, 700);
}
