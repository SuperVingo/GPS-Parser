using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGPS.GPSProtocol;
using LibGPS.GPSParse;
using LibGPS.GPSDefinition;

namespace LibGPS
{
    namespace GPSData
    {
        class GPSData
        {
            private String Raw;
            private List<String> RawList;

            private GPRMC GPRMC_Data = null;
            private GPVTG GPVTG_Data = null;
            private GPGGA GPGGA_Data = null;
            private GPGSA GPGSA_Data = null;
            private List<GPGSV> GPGSV_Data = new List<GPGSV>();
            private GPGSV GPGSV_Temp = null;
            private GPGLL GPGLL_Data = null;

            private GPSDataState State = GPSDataState.GPSDS_OK;

            private String ErrorMessage = "";

            public GPSData(List<String> raw)
            {
                this.RawList = raw;
                for (int i = 0; i < raw.Count; i++)
                    this.Raw += (raw[i] + "\r\n");
                ParseRawData();

            }

            public String GetRawData() { return Raw; }
            public String GetErorrMessage() { return ErrorMessage; }
            public GPRMC GetGPRMC() { return GPRMC_Data; }
            public GPVTG GetGPVTG() { return GPVTG_Data; }
            public GPGGA GetGPGGA() { return GPGGA_Data; }
            public GPGSA GetGPGSA() { return GPGSA_Data; }
            public List<GPGSV> GetGPGSV() { return GPGSV_Data; }
            public GPGLL GetGPGLL() { return GPGLL_Data; }

            public void ParseRawData()
            {
                for(int i = 0; i < RawList.Count; i++)
                {
                    String rawData = RawList[i];
                    GPSParseResult result;
                    if(rawData.StartsWith("$GPRMC"))
                    {
                        result = GPSParse.GPSParse.ParseGPRMC(rawData, out GPRMC_Data);
                        if (result != GPSParseResult.GPSPS_OK)
                        {
                            State = GPSDataState.GPSDS_ERROR;
                            ErrorMessage += "Error In GPRMC Protocol. Check The Raw Data.\r\n" + GetErrorDetail(result);
                        }
                    }
                    else if(rawData.StartsWith("$GPVTG"))
                    {
                        result = GPSParse.GPSParse.ParseGPVTG(rawData, out GPVTG_Data);
                        if (result != GPSParseResult.GPSPS_OK)
                        {
                            State = GPSDataState.GPSDS_ERROR;
                            ErrorMessage += "Error In GPVTG Protocol. Check The Raw Data.\r\n" + GetErrorDetail(result);
                        }
                    }
                    else if(rawData.StartsWith("$GPGGA"))
                    {
                        result = GPSParse.GPSParse.ParseGPGGA(rawData, out GPGGA_Data);
                        if (result != GPSParseResult.GPSPS_OK)
                        {
                            State = GPSDataState.GPSDS_ERROR;
                            ErrorMessage += "Error In GPGGA Protocol. Check The Raw Data.\r\n" + GetErrorDetail(result);
                        }
                    }
                    else if(rawData.StartsWith("$GPGSA"))
                    {
                        result = GPSParse.GPSParse.ParseGPGSA(rawData, out GPGSA_Data);
                        if (result != GPSParseResult.GPSPS_OK)
                        {
                            State = GPSDataState.GPSDS_ERROR;
                            ErrorMessage += "Error In GPGSA Protocol. Check The Raw Data.\r\n" + GetErrorDetail(result);
                        }
                    }
                    else if(rawData.StartsWith("$GPGSV"))
                    {
                        result = GPSParse.GPSParse.ParseGPGSV(rawData, out GPGSV_Temp);
                        if (result != GPSParseResult.GPSPS_OK)
                        {
                            State = GPSDataState.GPSDS_ERROR;
                            ErrorMessage += "Error In GPGSV Protocol. Check The Raw Data.\r\n" + GetErrorDetail(result);
                        }
                        else
                        {
                            GPGSV_Data.Add(GPGSV_Temp);
                        }
                    }
                    else if(rawData.StartsWith("$GPGLL"))
                    {
                        result = GPSParse.GPSParse.ParseGPGLL(rawData, out GPGLL_Data);
                        if (result != GPSParseResult.GPSPS_OK)
                        {
                            State = GPSDataState.GPSDS_ERROR;
                            ErrorMessage += "Error In GPGLL Protocol. Check The Raw Data.\r\n" + GetErrorDetail(result);
                        }
                    }
                }
            }

            private String GetErrorDetail(GPSParseResult result)
            {
                switch(result)
                {
                    case GPSParseResult.GPSPS_AttributesConvertError:
                        return "Attributes Cant Convert\r\n";
                    case GPSParseResult.GPSPS_AttributesNotEnough:
                        return "Attriubutes Not Enough\r\n";
                    case GPSParseResult.GPSPS_AttributesTooMany:
                        return "Attributes Too Many\r\n";
                    case GPSParseResult.GPSPS_HeaderNotMatch:
                        return "Header Not Match\r\n";
                }
                return "";
            }
        }
    }
}