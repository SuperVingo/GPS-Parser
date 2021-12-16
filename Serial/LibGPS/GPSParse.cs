using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGPS.GPSProtocol;
using LibGPS.GPSDefinition;

namespace LibGPS
{
    namespace GPSParse
    {
        static class GPSParse
        {
            // $GPRMC,071854.00,A,3727.99858,N,12651.22218,E,0.205,,131020,,,A*77
            public static GPSParseResult ParseGPRMC(String raw, out GPRMC pGPRMC)
            {
                String[] Attributes = raw.Split(new char[] { ',', '*' });
                pGPRMC = null;

                // Check Header
                if (!Attributes[Definition.GPRMC_Header].Substring(1).Equals("GPRMC"))
                {
                    pGPRMC = null;
                    return GPSParseResult.GPSPS_HeaderNotMatch;
                }

                // Check Attributes Count
                if (Attributes.Length > 14)
                {
                    pGPRMC = null;
                    return GPSParseResult.GPSPS_AttributesTooMany;
                }
                else if (Attributes.Length < 14)
                {
                    pGPRMC = null;
                    return GPSParseResult.GPSPS_AttributesNotEnough;
                }
                
                try
                {
                    // Create Attributes
                    Time TimeToken = null;
                    char? Status = null, Latitude_Direction = null, Longitude_Direction = null, Magnetic_Variation_Direction = null;
                    double? Latitude = null, Longitude = null, SpeedInKnot = null, AngleInDegree = null, Magnetic_Variation = null;

                    if (Attributes[Definition.GPRMC_TimeToken].Length >= 6)
                    {
                        String Hour = Attributes[Definition.GPRMC_TimeToken].Substring(0, 2);
                        String Min = Attributes[Definition.GPRMC_TimeToken].Substring(2, 2);
                        String Sec = Attributes[Definition.GPRMC_TimeToken].Substring(4, 2);
                        TimeToken = new Time(Convert.ToByte(Hour), Convert.ToByte(Min), Convert.ToByte(Sec));
                    }

                    if (Attributes[Definition.GPRMC_Status].Length > 0)
                        Status = Convert.ToChar(Attributes[Definition.GPRMC_Status]);

                    if (!Attributes[Definition.GPRMC_Latitude].Equals(""))
                        Latitude = Convert.ToDouble(Attributes[Definition.GPRMC_Latitude]);

                    if (!Attributes[Definition.GPRMC_Longitude].Equals(""))
                        Longitude = Convert.ToDouble(Attributes[Definition.GPRMC_Longitude]);

                    if (!Attributes[Definition.GPRMC_SpeedInKnot].Equals(""))
                        SpeedInKnot = Convert.ToDouble(Attributes[Definition.GPRMC_SpeedInKnot]);

                    if (!Attributes[Definition.GPRMC_AngleInDegree].Equals(""))
                        AngleInDegree = Convert.ToDouble(Attributes[Definition.GPRMC_AngleInDegree]);

                    if (!Attributes[Definition.GPRMC_Magnetic_Variation].Equals(""))
                        Magnetic_Variation = Convert.ToDouble(Attributes[Definition.GPRMC_Magnetic_Variation]);

                    if (Attributes[Definition.GPRMC_Latitude_Direction].Length > 0)
                        Latitude_Direction = Convert.ToChar(Attributes[Definition.GPRMC_Latitude_Direction]);

                    if (Attributes[Definition.GPRMC_Longitude_Direction].Length > 0)
                        Longitude_Direction = Convert.ToChar(Attributes[Definition.GPRMC_Longitude_Direction]);

                    if (Attributes[Definition.GPRMC_Magnetic_Variation_Direction].Length > 0)
                        Magnetic_Variation_Direction = Convert.ToChar(Attributes[Definition.GPRMC_Magnetic_Variation_Direction]);

                    Date DateToken = null;
                    if (Attributes[Definition.GPRMC_TimeToken].Length == 6)
                    {
                        String Year = Attributes[Definition.GPRMC_DateToken].Substring(4, 2);
                        String Month = Attributes[Definition.GPRMC_DateToken].Substring(2, 2);
                        String Day = Attributes[Definition.GPRMC_DateToken].Substring(0, 2);
                        DateToken = new Date(Convert.ToByte(Year), Convert.ToByte(Month), Convert.ToByte(Day));
                    }
                    int CheckSum = Convert.ToInt32(Attributes[Definition.GPRMC_CheckSum], 16);

                    pGPRMC = new GPRMC(TimeToken, Status, Latitude, Longitude, Latitude_Direction, Longitude_Direction,
                            SpeedInKnot, AngleInDegree, DateToken, Magnetic_Variation, Magnetic_Variation_Direction, CheckSum);
                }
                catch (Exception e)
                {
                    pGPRMC = null;
                    return GPSParseResult.GPSPS_AttributesConvertError;
                }

                return GPSParseResult.GPSPS_OK;
            }

            // $GPVTG,,T,,M,0.006,N,0.011,K,A*25
            public static GPSParseResult ParseGPVTG(String raw, out GPVTG pGPVTG)
            {
                String[] Attributes = raw.Split(new char[] { ',', '*' });
                pGPVTG = null;

                // Check Header
                if (!Attributes[Definition.GPVTG_Header].Substring(1).Equals("GPVTG"))
                {
                    pGPVTG = null;
                    return GPSParseResult.GPSPS_HeaderNotMatch;
                }

                // Check Attributes Count
                if (Attributes.Length > 11)
                {
                    pGPVTG = null;
                    return GPSParseResult.GPSPS_AttributesTooMany;
                }
                else if (Attributes.Length < 11)
                {
                    pGPVTG = null;
                    return GPSParseResult.GPSPS_AttributesNotEnough;
                }

                try
                {
                    double? TrueTrack = null, MagneticTrack = null, SpeedInKnots = null, SpeedInKmh = null;
                    char? TrueTrack_Direction = null, MagneticTrack_Direction = null, SpeedInKnots_Unit = null, SpeedInKmh_Unit = null;

                    if (!Attributes[Definition.GPVTG_TrueTrack].Equals(""))
                        TrueTrack = Convert.ToDouble(Attributes[Definition.GPVTG_TrueTrack]);

                    if (!Attributes[Definition.GPVTG_MagneticTrack].Equals("")) 
                        MagneticTrack = Convert.ToDouble(Attributes[Definition.GPVTG_MagneticTrack]);

                    if (Attributes[Definition.GPVTG_TrueTrack_Direction].Length > 0)
                        TrueTrack_Direction = Convert.ToChar(Attributes[Definition.GPVTG_TrueTrack_Direction]);

                    if (Attributes[Definition.GPVTG_MagneticTrack_Direction].Length > 0)
                        MagneticTrack_Direction = Convert.ToChar(Attributes[Definition.GPVTG_MagneticTrack_Direction]);

                    if (!Attributes[Definition.GPVTG_SpeedInKnots].Equals(""))
                        SpeedInKnots = Convert.ToDouble(Attributes[Definition.GPVTG_SpeedInKnots]);

                    if (!Attributes[Definition.GPVTG_SpeedInKmh].Equals(""))
                        SpeedInKmh = Convert.ToDouble(Attributes[Definition.GPVTG_SpeedInKmh]);

                    if (!Attributes[Definition.GPVTG_SpeedInKnots_Unit].Equals(""))
                        SpeedInKnots_Unit = Convert.ToChar(Attributes[Definition.GPVTG_SpeedInKnots_Unit]);

                    if (!Attributes[Definition.GPVTG_SpeedInKmh_Unit].Equals(""))
                        SpeedInKmh_Unit = Convert.ToChar(Attributes[Definition.GPVTG_SpeedInKmh_Unit]);

                    int CheckSum = Convert.ToInt32(Attributes[Definition.GPVTG_CheckSum], 16);

                    pGPVTG = new GPVTG(TrueTrack, MagneticTrack, SpeedInKnots, SpeedInKmh, 
                        TrueTrack_Direction, MagneticTrack_Direction, SpeedInKnots_Unit, SpeedInKmh_Unit, CheckSum);
                }
                catch (Exception e)
                {
                    pGPVTG = null;
                    return GPSParseResult.GPSPS_AttributesConvertError;
                }

                return GPSParseResult.GPSPS_OK;
            }

            // $GPGGA,071828.00,3727.99848,N,12651.22465,E,1,07,1.70,73.6,M,18.2,M,,*63
            public static GPSParseResult ParseGPGGA(String raw, out GPGGA pGPGGA)
            {
                String[] Attributes = raw.Split(new char[] { ',', '*' });
                pGPGGA = null;

                // Check Header
                if (!Attributes[Definition.GPGGA_Header].Substring(1).Equals("GPGGA"))
                {
                    pGPGGA = null;
                    return GPSParseResult.GPSPS_HeaderNotMatch;
                }

                // Check Attributes Count
                if (Attributes.Length > 16)
                {
                    pGPGGA = null;
                    return GPSParseResult.GPSPS_AttributesTooMany;
                }
                else if (Attributes.Length < 16)
                {
                    pGPGGA = null;
                    return GPSParseResult.GPSPS_AttributesNotEnough;
                }

                try
                {
                    Time TimeToken = null;
                    double? Latitude = null, Longitude = null, HDOP = null,
                        Horizontal_Sea_Level = null, Horizontal_WGS84 = null, DGPS_Update_Time = null;
                    char? Latitude_Direction = null, Longitude_Direction = null, Horizontal_Sea_Level_Unit = null, Horizontal_WGS84_Unit = null;
                    byte? Quality = null, Satellites = null;
                    int? DGPS_ID = null, CheckSum = null;

                    if (Attributes[Definition.GPGGA_TimeToken].Length >= 6)
                    {
                        String Hour = Attributes[Definition.GPGGA_TimeToken].Substring(0, 2);
                        String Min = Attributes[Definition.GPGGA_TimeToken].Substring(2, 2);
                        String Sec = Attributes[Definition.GPGGA_TimeToken].Substring(4, 2);
                        TimeToken = new Time(Convert.ToByte(Hour), Convert.ToByte(Min), Convert.ToByte(Sec));
                    }

                    if (!Attributes[Definition.GPGGA_Latitude].Equals(""))
                        Latitude = Convert.ToDouble(Attributes[Definition.GPGGA_Latitude]);

                    if (!Attributes[Definition.GPGGA_Longitude].Equals(""))
                        Longitude = Convert.ToDouble(Attributes[Definition.GPGGA_Longitude]);

                    if (!Attributes[Definition.GPGGA_Latitude_Direction].Equals(""))
                        Latitude_Direction = Convert.ToChar(Attributes[Definition.GPGGA_Latitude_Direction]);

                    if (!Attributes[Definition.GPGGA_Longitude_Direction].Equals(""))
                        Longitude_Direction = Convert.ToChar(Attributes[Definition.GPGGA_Longitude_Direction]);

                    if (!Attributes[Definition.GPGGA_Quaility].Equals(""))
                        Quality = Convert.ToByte(Attributes[Definition.GPGGA_Quaility]);

                    if (!Attributes[Definition.GPGGA_Satellites].Equals(""))
                        Satellites = Convert.ToByte(Attributes[Definition.GPGGA_Satellites]);

                    if (!Attributes[Definition.GPGGA_HDOP].Equals(""))
                        HDOP = Convert.ToDouble(Attributes[Definition.GPGGA_HDOP]);

                    if (!Attributes[Definition.GPGGA_Horizontal_Sea_Level].Equals(""))
                        Horizontal_Sea_Level = Convert.ToDouble(Attributes[Definition.GPGGA_Horizontal_Sea_Level]);

                    if (!Attributes[Definition.GPGGA_Horizontal_WGS84].Equals(""))
                        Horizontal_WGS84 = Convert.ToDouble(Attributes[Definition.GPGGA_Horizontal_WGS84]);

                    if (!Attributes[Definition.GPGGA_Horizontal_Sea_Level_Unit].Equals(""))
                        Horizontal_Sea_Level_Unit = Convert.ToChar(Attributes[Definition.GPGGA_Horizontal_Sea_Level_Unit]);

                    if (!Attributes[Definition.GPGGA_Horizontal_WGS84_Unit].Equals(""))
                        Horizontal_WGS84_Unit = Convert.ToChar(Attributes[Definition.GPGGA_Horizontal_WGS84_Unit]);

                    if (!Attributes[Definition.GPGGA_DGPS_Update_Time].Equals(""))
                        DGPS_Update_Time = Convert.ToDouble(Attributes[Definition.GPGGA_DGPS_Update_Time]);

                    if (!Attributes[Definition.GPGGA_DGPS_ID].Equals(""))
                        DGPS_ID = Convert.ToInt32(Attributes[Definition.GPGGA_DGPS_ID]);

                    if (!Attributes[Definition.GPGGA_CheckSum].Equals(""))
                        CheckSum = Convert.ToInt32(Attributes[Definition.GPGGA_CheckSum], 16);

                    pGPGGA = new GPGGA(TimeToken, Latitude, Longitude, Latitude_Direction, Longitude_Direction,
                        Quality, Satellites, HDOP, Horizontal_Sea_Level, Horizontal_WGS84, Horizontal_Sea_Level_Unit, Horizontal_WGS84_Unit,
                        DGPS_Update_Time, DGPS_ID, CheckSum);
                }
                catch (Exception e)
                {
                    pGPGGA = null;
                    return GPSParseResult.GPSPS_AttributesConvertError;
                }

                return GPSParseResult.GPSPS_OK;
            }

            //$GPGSA,A,2,18,20,32,10,23,,,,,,,,2.58,2.38,1.00*0E
            public static GPSParseResult ParseGPGSA(String raw, out GPGSA pGPGSA)
            {
                String[] Attributes = raw.Split(new char[] { ',', '*' });
                pGPGSA = null;

                // Check Header
                if (!Attributes[Definition.GPGSA_Header].Substring(1).Equals("GPGSA"))
                {
                    pGPGSA = null;
                    return GPSParseResult.GPSPS_HeaderNotMatch;
                }

                // Check Attributes Count
                if (Attributes.Length > 19)
                {
                    pGPGSA = null;
                    return GPSParseResult.GPSPS_AttributesTooMany;
                }
                else if (Attributes.Length < 19)
                {
                    pGPGSA = null;
                    return GPSParseResult.GPSPS_AttributesNotEnough;
                }

                try
                {
                    double? PDOP = null, HDOP = null, VDOP = null;
                    char? AutoSelection = null;
                    byte? nDFix = Convert.ToByte(Attributes[Definition.GPGSA_nDFix]);
                    byte?[] usedSatellites = new byte?[12];
                    int? CheckSum = null;

                    if (!Attributes[Definition.GPGSA_AutoSelection].Equals(""))
                        AutoSelection = Convert.ToChar(Attributes[Definition.GPGSA_AutoSelection]);

                    for (int i = Definition.GPGSA_usedSatellites1; i <= Definition.GPGSA_usedSatellites12; i++)
                    {
                        if (!Attributes[i].Equals(""))
                            usedSatellites[i - Definition.GPGSA_usedSatellites1] = Convert.ToByte(Attributes[i]);
                        else
                            usedSatellites[i - Definition.GPGSA_usedSatellites1] = null;
                    }

                    if (!Attributes[Definition.GPGSA_PDOP].Equals(""))
                        PDOP = Convert.ToDouble(Attributes[Definition.GPGSA_PDOP]);

                    if (!Attributes[Definition.GPGSA_HDOP].Equals(""))
                        HDOP = Convert.ToDouble(Attributes[Definition.GPGSA_HDOP]);

                    if (!Attributes[Definition.GPGSA_VDOP].Equals(""))
                        VDOP = Convert.ToDouble(Attributes[Definition.GPGSA_VDOP]);

                    if (!Attributes[Definition.GPGSA_CheckSum].Equals(""))
                        CheckSum = Convert.ToInt32(Attributes[Definition.GPGSA_CheckSum], 16);

                    pGPGSA = new GPGSA(AutoSelection, nDFix, usedSatellites, PDOP, HDOP, VDOP, CheckSum);
                }
                catch (Exception e)
                {
                    pGPGSA = null;
                    return GPSParseResult.GPSPS_AttributesConvertError;
                }

                return GPSParseResult.GPSPS_OK;
            }

            //$GPGSV,3,3,12,25,00,177,,28,03,032,,32,07,271,32,50,43,151,30*73
            public static GPSParseResult ParseGPGSV(String raw, out GPGSV pGPGSV)
            {
                String[] Attributes = raw.Split(new char[] { ',', '*' });
                byte satecount = 0;
                pGPGSV = null;

                // Check Header
                if (!Attributes[Definition.GPGSV_Header].Substring(1).Equals("GPGSV"))
                {
                    pGPGSV = null;
                    return GPSParseResult.GPSPS_HeaderNotMatch;
                }

                // Check Attributes Count
                if(Attributes.Length == 5)
                {
                    satecount = 0;
                }
                else if (Attributes.Length == 9)
                {
                    satecount = 1;
                }
                else if (Attributes.Length == 13)
                {
                    satecount = 2;
                }
                else if (Attributes.Length == 17)
                {
                    satecount = 3;
                }
                else if (Attributes.Length == 21)
                {
                    satecount = 4;
                }
                else
                {
                    pGPGSV = null;
                    return GPSParseResult.GPSPS_AttributesNotEnough;
                }

                try
                {
                    byte? AllSentence = null, SentenceIndex = null, NumOfSatellites = null;
                    int? SatellitesID_1 = null, SatellitesID_2 = null, SatellitesID_3 = null, SatellitesID_4 = null;
                    int? Elevation_1 = null, Elevation_2 = null, Elevation_3 = null, Elevation_4 = null;
                    int? Azimuth_1 = null, Azimuth_2 = null, Azimuth_3 = null, Azimuth_4 = null;
                    int? SNR_1 = null, SNR_2 = null, SNR_3 = null, SNR_4 = null;
                    int? CheckSum = null;

                    if (!Attributes[Definition.GPGSV_AllSentence].Equals(""))
                        AllSentence = Convert.ToByte(Attributes[Definition.GPGSV_AllSentence]);

                    if (!Attributes[Definition.GPGSV_SentenceIndex].Equals(""))
                        SentenceIndex = Convert.ToByte(Attributes[Definition.GPGSV_SentenceIndex]);

                    if (!Attributes[Definition.GPGSV_NumOfSatellites].Equals(""))
                        NumOfSatellites = Convert.ToByte(Attributes[Definition.GPGSV_NumOfSatellites]);

                    if (satecount >= 1)
                    {
                        if (!Attributes[Definition.GPGSV_Satellite1_SatelliteID].Equals(""))
                            SatellitesID_1 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite1_SatelliteID]);

                        if (!Attributes[Definition.GPGSV_Satellite1_Elevation].Equals(""))
                            Elevation_1 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite1_Elevation]);

                        if (!Attributes[Definition.GPGSV_Satellite1_Azimuth].Equals(""))
                            Azimuth_1 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite1_Azimuth]);

                        if (!Attributes[Definition.GPGSV_Satellite1_SNR].Equals(""))
                            SNR_1 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite1_SNR]);
                    }
                    if (satecount >= 2)
                    {
                        if (!Attributes[Definition.GPGSV_Satellite2_SatelliteID].Equals(""))
                            SatellitesID_2 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite2_SatelliteID]);

                        if (!Attributes[Definition.GPGSV_Satellite2_Elevation].Equals(""))
                            Elevation_2 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite2_Elevation]);

                        if (!Attributes[Definition.GPGSV_Satellite2_Azimuth].Equals(""))
                            Azimuth_2 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite2_Azimuth]);

                        if (!Attributes[Definition.GPGSV_Satellite2_SNR].Equals(""))
                            SNR_2 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite2_SNR]);
                    }
                    if (satecount >= 3)
                    {
                        if (!Attributes[Definition.GPGSV_Satellite3_SatelliteID].Equals(""))
                            SatellitesID_3 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite3_SatelliteID]);

                        if (!Attributes[Definition.GPGSV_Satellite3_Elevation].Equals(""))
                            Elevation_3 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite3_Elevation]);

                        if (!Attributes[Definition.GPGSV_Satellite3_Azimuth].Equals(""))
                            Azimuth_3 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite3_Azimuth]);

                        if (!Attributes[Definition.GPGSV_Satellite3_SNR].Equals(""))
                            SNR_3 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite3_SNR]);
                    }
                    if (satecount >= 4)
                    {
                        if (!Attributes[Definition.GPGSV_Satellite4_SatelliteID].Equals(""))
                            SatellitesID_4 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite4_SatelliteID]);

                        if (!Attributes[Definition.GPGSV_Satellite4_Elevation].Equals(""))
                            Elevation_4 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite4_Elevation]);

                        if (!Attributes[Definition.GPGSV_Satellite4_Azimuth].Equals(""))
                            Azimuth_4 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite4_Azimuth]);

                        if (!Attributes[Definition.GPGSV_Satellite4_SNR].Equals(""))
                            SNR_4 = Convert.ToInt32(Attributes[Definition.GPGSV_Satellite4_SNR]);
                    }

                    switch (satecount)
                    {
                        case 0:
                            {
                                if (!Attributes[Definition.GPGSV_S0_CheckSum].Equals(""))
                                    CheckSum = Convert.ToInt32(Attributes[Definition.GPGSV_S0_CheckSum], 16);
                                pGPGSV = new GPGSV(AllSentence, SentenceIndex, NumOfSatellites, CheckSum);
                                break;
                            }
                        case 1:
                            {
                                if (!Attributes[Definition.GPGSV_S1_CheckSum].Equals(""))
                                    CheckSum = Convert.ToInt32(Attributes[Definition.GPGSV_S1_CheckSum], 16);
                                pGPGSV = new GPGSV(AllSentence, SentenceIndex, NumOfSatellites,
                                                SatellitesID_1, Elevation_1, Azimuth_1, SNR_1, CheckSum);
                                break;
                            }
                        case 2:
                            {
                                if (!Attributes[Definition.GPGSV_S2_CheckSum].Equals(""))
                                    CheckSum = Convert.ToInt32(Attributes[Definition.GPGSV_S2_CheckSum], 16);
                                pGPGSV = new GPGSV(AllSentence, SentenceIndex, NumOfSatellites,
                                                SatellitesID_1, Elevation_1, Azimuth_1, SNR_1,
                                                SatellitesID_2, Elevation_2, Azimuth_2, SNR_2, CheckSum);
                                break;
                            }
                        case 3:
                            {
                                if (!Attributes[Definition.GPGSV_S3_CheckSum].Equals(""))
                                    CheckSum = Convert.ToInt32(Attributes[Definition.GPGSV_S3_CheckSum], 16);
                                pGPGSV = new GPGSV(AllSentence, SentenceIndex, NumOfSatellites,
                                                SatellitesID_1, Elevation_1, Azimuth_1, SNR_1,
                                                SatellitesID_2, Elevation_2, Azimuth_2, SNR_2,
                                                SatellitesID_3, Elevation_3, Azimuth_3, SNR_3, CheckSum);
                                break;
                            }
                        case 4:
                            {
                                if (!Attributes[Definition.GPGSV_S4_CheckSum].Equals(""))
                                    CheckSum = Convert.ToInt32(Attributes[Definition.GPGSV_S4_CheckSum], 16);
                                pGPGSV = new GPGSV(AllSentence, SentenceIndex, NumOfSatellites,
                                                SatellitesID_1, Elevation_1, Azimuth_1, SNR_1,
                                                SatellitesID_2, Elevation_2, Azimuth_2, SNR_2,
                                                SatellitesID_3, Elevation_3, Azimuth_3, SNR_3,
                                                SatellitesID_4, Elevation_4, Azimuth_4, SNR_4, CheckSum);
                                break;
                            }
                    }
                }
                catch (Exception e)
                {
                    pGPGSV = null;
                    return GPSParseResult.GPSPS_AttributesConvertError;
                }

                return GPSParseResult.GPSPS_OK;
            }

            //$GPGLL,3727.99979,N,12651.17557,E,070808.00,A,A*68
            public static GPSParseResult ParseGPGLL(String raw, out GPGLL pGPGLL)
            {
                String[] Attributes = raw.Split(new char[] { ',', '*' });
                pGPGLL = null;

                // Check Header
                if (!Attributes[Definition.GPVTG_Header].Substring(1).Equals("GPGLL"))
                {
                    pGPGLL = null;
                    return GPSParseResult.GPSPS_HeaderNotMatch;
                }

                // Check Attributes Count
                if (Attributes.Length > 9)
                {
                    pGPGLL = null;
                    return GPSParseResult.GPSPS_AttributesTooMany;
                }
                else if (Attributes.Length < 9)
                {
                    pGPGLL = null;
                    return GPSParseResult.GPSPS_AttributesNotEnough;
                }

                try
                {
                    double? Latitude = null, Longitude = null;
                    char? Latitude_Direction = null, Longitude_Direction = null, Status = null, Border = null;
                    Time TimeToken = null;
                    int? CheckSum = null;

                    if (!Attributes[Definition.GPGLL_Latitude].Equals(""))
                        Latitude = Convert.ToDouble(Attributes[Definition.GPGLL_Latitude]);

                    if (!Attributes[Definition.GPGLL_Longitude].Equals(""))
                        Longitude = Convert.ToDouble(Attributes[Definition.GPGLL_Longitude]);

                    if (!Attributes[Definition.GPGLL_Latitude_Direction].Equals(""))
                        Latitude_Direction = Convert.ToChar(Attributes[Definition.GPGLL_Latitude_Direction]);

                    if (!Attributes[Definition.GPGLL_Longitude_Direction].Equals(""))
                        Longitude_Direction = Convert.ToChar(Attributes[Definition.GPGLL_Longitude_Direction]);

                    if (Attributes[Definition.GPGLL_TimeToken].Length >= 6)
                    {
                        String Hour = Attributes[Definition.GPGLL_TimeToken].Substring(0, 2);
                        String Min = Attributes[Definition.GPGLL_TimeToken].Substring(2, 2);
                        String Sec = Attributes[Definition.GPGLL_TimeToken].Substring(4, 2);
                        TimeToken = new Time(Convert.ToByte(Hour), Convert.ToByte(Min), Convert.ToByte(Sec));
                    }

                    if (!Attributes[Definition.GPGLL_Status].Equals(""))
                        Status = Convert.ToChar(Attributes[Definition.GPGLL_Status]);

                    if (!Attributes[Definition.GPGLL_Border].Equals(""))
                        Border = Convert.ToChar(Attributes[Definition.GPGLL_Border]);

                    if (!Attributes[Definition.GPGLL_CheckSum].Equals(""))
                        CheckSum = Convert.ToInt32(Attributes[Definition.GPGLL_CheckSum], 16);

                    pGPGLL = new GPGLL(Latitude, Longitude, Latitude_Direction, Longitude_Direction, TimeToken, Status, CheckSum);
                }
                catch (Exception e)
                {
                    pGPGLL = null;
                    return GPSParseResult.GPSPS_AttributesConvertError;
                }

                return GPSParseResult.GPSPS_OK;
            }
        }
    }
}
 