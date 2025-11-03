using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Expert.Common.Library;
using System.Configuration;
using Expert.Common.Library.DTOs;

namespace Vilani.MatrixVision.DataBase
{
    internal class DataBaseAccess
    {
        private OleDbConnection _connection = null;

        private OleDbCommand _command = null;

        private OleDbDataReader _dataReader = null;

        private int errorCode = 0;

        private static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public DataBaseAccess()
        {
            //this.con = new OleDbConnection(string.Format(" Data Source={0};Integrated Security=SSPI;Password=password;Provider=Microsoft.SQLSERVER.CE.OLEDB.4.0;", Path.Combine(Application.StartupPath, "VisionDB.sdf")));
            this._connection = new OleDbConnection(string.Format("Data Source={0};Provider=Microsoft.SQLSERVER.CE.OLEDB.4.0;", Path.Combine(Application.StartupPath, "VisionDB.sdf")));
            this._command = new OleDbCommand();
            this._command.Connection = this._connection;
        }

        private bool OpenDb()
        {
            bool result = false;
            try
            {
                if (this._connection.State != ConnectionState.Open)
                    this._connection.Open();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                result = false;
            }
            if (this._connection.State == ConnectionState.Open)
            {
                result = true;
            }
            return result;
        }

        private bool CloseDb()
        {
            bool result = false;
            try
            {
                if (this._dataReader != null && !this._dataReader.IsClosed)
                {
                    this._dataReader.Close();
                }
                this._connection.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                result = false;
            }
            if (this._connection.State == ConnectionState.Closed)
            {
                result = true;
            }
            return result;
        }

        internal int GetSerialPortData(bool bIsInputPort, out string sPortNumber, out string sBaudRate, out string sParity, out string sStopBits, out string sDataBits, out string sok, out string snotok)
        {
            this.errorCode = 0;
            sPortNumber = "";
            sBaudRate = "";
            sParity = "";
            sStopBits = "";
            sDataBits = "";
            sok = "";
            snotok = "";
            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("SELECT * from tblSerialPortConfigurations WHERE IsInputConfig={0}", Convert.ToInt32(bIsInputPort));
                this._dataReader = this._command.ExecuteReader();
                while (this._dataReader.Read())
                {
                    sPortNumber = this._dataReader["PortName"].ToString();
                    sBaudRate = this._dataReader["BaudRate"].ToString();
                    sParity = this._dataReader["Parity"].ToString();
                    sStopBits = this._dataReader["StopBits"].ToString();
                    sDataBits = this._dataReader["DataBits"].ToString();
                    sok = this._dataReader["Ok"].ToString();
                    snotok = this._dataReader["Notok"].ToString();
                }
            }
            catch (Exception var_0_13A)
            {
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return this.errorCode;
        }

        internal SerialPortDTO GetSerialPortData(bool isInputPort)
        {
            this.errorCode = 0;
            SerialPortDTO serialPort = new SerialPortDTO();
            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("SELECT * from tblSerialPortConfigurations WHERE IsInputConfig={0}", Convert.ToInt32(isInputPort));
                this._dataReader = this._command.ExecuteReader();
                while (this._dataReader.Read())
                {
                    serialPort.PortNumber = string.Format("{0}{1}", "COM", this._dataReader["PortName"]);
                    serialPort.BaudRate = Convert.ToInt32(this._dataReader["BaudRate"]);
                    serialPort.Parity = (System.IO.Ports.Parity)(Convert.ToInt32(this._dataReader["Parity"]));
                    serialPort.StopBits = (System.IO.Ports.StopBits)(Convert.ToInt32(this._dataReader["StopBits"]));
                    serialPort.DataBits = Convert.ToInt32(this._dataReader["DataBits"]);
                    serialPort.OK = this._dataReader["Ok"].ToString();
                    serialPort.NotOK = this._dataReader["Notok"].ToString();
                    serialPort.IsInputPort = isInputPort;
                }
            }
            catch (Exception var_0_13A)
            {
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return serialPort;
        }

        internal int UpdateSerialPortConfiguration(bool bIsInputPort, string sPortNumber, string sBaudRate, string sParity, string sStopBits, string sDataBits, string sok, string snotok)
        {
            this.errorCode = 0;
            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("UPDATE tblSerialPortConfigurations SET PortName='{0}', BaudRate='{1}', Parity='{2}', StopBits='{3}', DataBits='{4}', Ok='{5}', Notok='{6}' WHERE IsInputConfig={7}", new object[]
				{
					sPortNumber,
					sBaudRate,
					sParity,
					sStopBits,
					sDataBits,
					sok,
					snotok,
					Convert.ToByte(bIsInputPort)
				});
                this._command.ExecuteNonQuery();
            }
            catch
            {
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return this.errorCode;
        }

        internal string[] GetModelsList()
        {
            this.errorCode = 0;
            List<string> list = new List<string>();
            try
            {
                this.OpenDb();
                this._command.CommandText = "SELECT ModelName from tblModels";
                this._dataReader = this._command.ExecuteReader();
                while (this._dataReader.Read())
                {
                    list.Add(this._dataReader[0].ToString());
                }
            }
            catch
            {
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return list.ToArray();
        }

        internal bool IsModelExists(string sModelName)
        {
            bool result = false;
            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("SELECT * FROM tblModels WHERE ModelName='{0}'", sModelName.ToUpper());
                this._dataReader = this._command.ExecuteReader();
                result = this._dataReader.HasRows;
            }
            catch
            {
            }
            finally
            {
                this.CloseDb();
            }
            return result;
        }


        internal int UpdateModelDetails(Model model)
        {
            this.errorCode = 0;
            string commandText = "";
            try
            {
                commandText = string.Format("UPDATE tblModels SET ModelName ='{1}', Score='{2}', PLCInputValue='{3}', PLCOutputValue='{4}',InvertResult='{5}' WHERE ModelID='{0}'", new object[]
					{
						model.ModelID,
						model.ModelName,
						model.Score,
						model.PLCInputValue,
						model.PLCOutoutValue,
                        model.InvertResult ? 1:0,
					});

                this.OpenDb();
                this._command.CommandText = commandText;
                this._command.ExecuteNonQuery();

            }

            catch (Exception exception)
            {
                logger.Error("UpdateModelDetails-Db", exception);
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return this.errorCode;

        }


        internal int AddNewModelDetails(Model model)
        {
            int modelID = 0;
            string commandText = "";
            try
            {
                commandText = string.Format("INSERT INTO tblModels (ModelName, Score,PLCInputValue,PLCOutputValue, InvertResult) VALUES ('{0}','{1}','{2}','{3}','{4}')", new object[]
					{
						model.ModelName,
						model.Score,
						model.PLCInputValue,
						model.PLCOutoutValue,						
                        model.InvertResult? 1:0
					});
                this.OpenDb();
                this._command.CommandText = commandText;
                this._command.ExecuteNonQuery();
                this.CloseDb();


                this._command.CommandText = string.Format("SELECT ModelID FROM tblModels WHERE ModelName='{0}'", model.ModelName.ToUpper());
                this.OpenDb();
                this._dataReader = this._command.ExecuteReader();
                while (this._dataReader.Read())
                {
                    modelID = Convert.ToInt32(this._dataReader["ModelID"]);
                }

            }

            catch (Exception exception)
            {
                logger.Error("AddModelDetails-Db", exception);
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return modelID;

        }

        internal Model GetModelDetails(int modelID)
        {
            Model model = new Model();
            model.ReferenceImages = new List<ReferenceImageDB>();

            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("SELECT * FROM tblModels WHERE ModelID='{0}'", modelID);
                this._dataReader = this._command.ExecuteReader();
                while (this._dataReader.Read())
                {
                    model.ModelID = Convert.ToInt32(this._dataReader["ModelID"]);
                    model.ModelName = this._dataReader["ModelName"].ToString();
                    model.PLCInputValue = this._dataReader["PLCInputValue"].ToString();
                    model.Score = this._dataReader["Score"].ToString();
                    model.PLCOutoutValue = this._dataReader["PLCOutputValue"].ToString();

                    try
                    {
                        model.InvertResult = Convert.ToBoolean(this._dataReader["InvertResult"]);
                    }
                    catch (Exception)
                    {
                        model.InvertResult = false;
                    }

                  
                }

            }
            catch
            {
                model.ErrorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return model;
        }


        internal List<ReferenceImageDB> GetModelReferenceImages(int modelID)
        {
            List<ReferenceImageDB> referenceImages = new List<ReferenceImageDB>();
            try
            {
                this.OpenDb();

                this._command.CommandText = string.Format("SELECT * FROM tblReferenceImages WHERE ModelID='{0}'", modelID);
                this._dataReader = this._command.ExecuteReader();
                while (this._dataReader.Read())
                {
                    ReferenceImageDB image = new ReferenceImageDB();
                    image.ImageName = this._dataReader["ImageName"].ToString();
                    image.ImagePath = this._dataReader["ImagePath"].ToString();
                    image.Score = Convert.ToInt32(this._dataReader["Score"]);
                    image.NumberOfOccurances = Convert.ToInt32(this._dataReader["NumberOfOccurances"]);
                    image.ReferenceImageID = Convert.ToInt32(this._dataReader["ReferenceImageID"]);
                    image.ModelID = Convert.ToInt32(this._dataReader["ModelID"]);
                    image.ImageSize = Convert.ToString(this._dataReader["ImageSize"]);
                    image.AOISize = Convert.ToString(this._dataReader["AOISize"]);
                    referenceImages.Add(image);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseDb();
            }
            return referenceImages;
        }

        internal int DeleteModel(int modelID)
        {
            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("DELETE FROM tblModels WHERE ModelID='{0}'", modelID);
                this._command.ExecuteNonQuery();
                this.CloseDb();

                this.OpenDb();
                this._command.CommandText = string.Format("DELETE FROM tblReferenceImages WHERE ModelID='{0}'", modelID);
                this._command.ExecuteNonQuery();
                this.CloseDb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseDb();
            }
            return modelID;
        }



        internal List<Model> GetAllModelsDataWithReferneceImages()
        {
            List<Model> modelList = null;
            try
            {
                modelList = GetModelsListDataSource();

                foreach (var model in modelList)
                {
                    model.ReferenceImages = GetModelReferenceImages(model.ModelID);
                    model.ReferenceImageOccurancePairList = new List<Item>();

                    var noOfImages = Convert.ToInt32(ConfigurationManager.AppSettings["SupportedReferenceImages"]);
                    int counter = 1;
                    foreach (var item in model.ReferenceImages)
                    {
                        if (noOfImages <= 16)
                            model.ReferenceImageOccurancePairList.Add(new Item() { ReferenceImageID = item.ReferenceImageID, SequenceNumber = counter, Name = item.ImageName, Score = item.Score, Value = item.NumberOfOccurances });
                        counter++;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                this.CloseDb();
            }
            return modelList;
        }



        internal bool ValidatePassword(string sPwd)
        {
            bool result = false;
            string arg = "admin";
            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("SELECT * FROM tblUsers WHERE UserName='{0}' AND Password='{1}'", arg, sPwd);
                this._dataReader = this._command.ExecuteReader();
                if (this._dataReader.HasRows)
                {
                    result = true;
                }
            }
            catch (Exception var_2_53)
            {
            }
            finally
            {
                this.CloseDb();
            }
            return result;
        }

        internal int UpdatePassword(string sNewPwd)
        {
            this.errorCode = 0;
            string arg = "admin";
            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("UPDATE tblUsers SET Password='{0}' WHERE uname='{1}'", sNewPwd, arg);
                this._command.ExecuteNonQuery();
            }
            catch
            {
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return this.errorCode;
        }

        internal int UpdateSerialPortConfiguration(SerialPortDTO serialPort)
        {

            this.errorCode = 0;
            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("UPDATE tblSerialPortConfigurations SET PortName='{0}', BaudRate='{1}', Parity='{2}', StopBits='{3}', DataBits='{4}', Ok='{5}', Notok='{6}' WHERE IsInputConfig={7}", new object[]
				{
					serialPort.PortNumber,
					serialPort.BaudRate,
					(int)serialPort.Parity,
					(int)serialPort.StopBits,
					serialPort.DataBits,
					serialPort.OK,
					serialPort.NotOK
,
					Convert.ToByte(serialPort.IsInputPort)
				});
                this._command.ExecuteNonQuery();
            }
            catch
            {
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return this.errorCode;
        }

        internal List<Model> GetModelsListDataSource()
        {
            this.errorCode = 0;
            List<Model> list = new List<Model>();
            try
            {
                this.OpenDb();
                this._command.CommandText = "SELECT * from tblModels";
                this._dataReader = this._command.ExecuteReader();

                while (this._dataReader.Read())
                {
                    Model item = new Model();
                    item.ModelID = Convert.ToInt32(this._dataReader["ModelID"]);
                    item.ModelName = this._dataReader["ModelName"].ToString();
                    item.PLCInputValue = this._dataReader["PLCInputValue"].ToString();
                    try
                    {
                        item.InvertResult = Convert.ToBoolean(this._dataReader["InvertResult"]);
                    }
                    catch (Exception)
                    {
                        item.InvertResult = false;
                    }

                    list.Add(item);
                }

            }
            catch
            {
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return list;
        }


        internal int AddModelReferenceImage(ReferenceImageDB image)
        {
            this.errorCode = 0;
            string commandText = "";
            try
            {
                commandText = string.Format("INSERT INTO tblReferenceImages (ImageName, ImagePath,NumberOfOccurances,Score, ModelID, ImageSize, AOISize) VALUES ('{0}','{1}','{2}','{3}',{4},'{5}','{6}')", new object[]
					{
						image.ImageName,
						image.ImagePath,
						image.NumberOfOccurances,
						image.Score,						
                        image.ModelID,
                        image.ImageSize,
                        image.AOISize
					});
                this.OpenDb();
                this._command.CommandText = commandText;
                this._command.ExecuteNonQuery();
            }

            catch (Exception exception)
            {
                logger.Error("AddModelReferenceImage-Db", exception);
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return this.errorCode;
        }


        internal int UpdateModelReferenceImageScore(int id, int score)
        {
            this.errorCode = 0;
            string commandText = "";
            try
            {
                commandText = string.Format("UPDATE tblReferenceImages SET Score='{0}' WHERE ReferenceImageID ={1}", new object[]
					{
						score, id
					});
                this.OpenDb();
                this._command.CommandText = commandText;
                this._command.ExecuteNonQuery();

            }

            catch (Exception exception)
            {
                logger.Error("AddModelReferenceImage-Db", exception);
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return this.errorCode;
        }

        internal int DeleteReferenceImage(int referenceImageID)
        {
            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("DELETE FROM tblReferenceImages WHERE ReferenceImageID='{0}'", referenceImageID);
                this._command.ExecuteNonQuery();
                this.CloseDb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseDb();
            }
            return referenceImageID;
        }




        internal List<Settings> GetSettings()
        {
            this.errorCode = 0;

            List<Settings> list = new List<Settings>();

            string commandText = "";
            try
            {

                this.OpenDb();


                this._command.CommandText = string.Format("SELECT SettingID, SettingName, SettingValue from tblSettings");
                this._dataReader = this._command.ExecuteReader();


                while (this._dataReader.Read())
                {
                    Settings item = new Settings();
                    item.SettingID = Convert.ToInt32(this._dataReader["SettingID"]);
                    item.SettingName = this._dataReader["SettingName"].ToString();
                    item.SettingValue = this._dataReader["SettingValue"].ToString();
                    list.Add(item);
                }

            }

            catch (Exception exception)
            {
                logger.Error("GetSettings-Db", exception);
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return list;
        }

        internal int UpdateSettings(List<Settings> settings)
        {
            this.errorCode = 0;
            string commandText = "";
            try
            {

                this.OpenDb();

                foreach (var item in settings)
                {
                    this._command.CommandText = string.Format("SELECT * from tblSettings Where SettingName = '{0}'", item.SettingName);
                    this._dataReader = this._command.ExecuteReader();

                    if (this._dataReader.HasRows)
                    {
                        int settingID = 0;
                        while (this._dataReader.Read())
                        {
                            settingID = Convert.ToInt32(this._dataReader["SettingID"]);
                        }

                        commandText = string.Format("UPDATE tblSettings SET SettingValue='{0}' WHERE SettingID ={1}", new object[]
					        {
						        item.SettingValue,
						        settingID						
					        });
                        this.CloseDb();
                        this.OpenDb();
                        this._command.CommandText = commandText;
                        this._command.ExecuteNonQuery();

                    }
                    else
                    {
                        commandText = string.Format("INSERT INTO tblSettings (SettingName, SettingValue) VALUES ('{0}','{1}')", new object[]
					        {
						        item.SettingName,
						        item.SettingValue						
					        });

                        this.CloseDb();
                        this.OpenDb();
                        this._command.CommandText = commandText;
                        this._command.ExecuteNonQuery();

                    }


                }

            }

            catch (Exception exception)
            {
                logger.Error("UpdateSettings-Db", exception);
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return this.errorCode;
        }



        internal bool IsReferenceImageAlreadyExists(int modelID, string imageName)
        {
            this.errorCode = 0;

            bool imageExists = false;

            string commandText = "";
            try
            {

                this.OpenDb();


                this._command.CommandText = string.Format("SELECT * from tblReferenceImages WHERE ModelID='{0}' and ImageName='{1}'", new object[]
					{
						modelID,
						imageName						
					});

                this._dataReader = this._command.ExecuteReader();


                while (this._dataReader.Read())
                {
                    imageExists = true;
                }

            }

            catch (Exception exception)
            {
                logger.Error("IsReferenceImageAlreadyExists-Db", exception);
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return imageExists;
        }

        internal bool IsModelPLCInputValueExists(string plcInputValue)
        {
            bool result = false;
            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("SELECT * FROM tblModels WHERE PLCInputValue='{0}'", plcInputValue.ToUpper());
                this._dataReader = this._command.ExecuteReader();
                result = this._dataReader.HasRows;
            }
            catch
            {
            }
            finally
            {
                this.CloseDb();
            }
            return result;
        }

        internal bool IsReferenceImageAlreadyExists(string p)
        {
            throw new NotImplementedException();
        }

        internal void DeleteSetting(int settingID)
        {
            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("DELETE FROM tblSettings  WHERE SettingID ={0}", settingID);
                this._command.ExecuteNonQuery();
                this.CloseDb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.CloseDb();
            }

        }

        internal TcpIPDTO GetTcpIPPortData(int operationType)
        {
            this.errorCode = 0;
            TcpIPDTO tcpIPDTO = null;
            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("SELECT * from tblTcpIPConfigurations WHERE ConnectionType={0}", operationType);
                this._dataReader = this._command.ExecuteReader();
                while (this._dataReader.Read())
                {
                    tcpIPDTO = new TcpIPDTO();
                    tcpIPDTO.TcpIPID = Convert.ToInt32(this._dataReader["TcpIPID"]);
                    tcpIPDTO.ConnectionName = this._dataReader["ConnectionName"].ToString();
                    tcpIPDTO.IPAddress = this._dataReader["IPAddress"].ToString();
                    tcpIPDTO.PortNumber = Convert.ToInt32(this._dataReader["PortNumber"]);
                    tcpIPDTO.ConnectionType = Convert.ToInt32(this._dataReader["ConnectionType"]);
                    tcpIPDTO.CreatedDate = Convert.ToDateTime(this._dataReader["CreatedDate"]);
                    tcpIPDTO.OK = this._dataReader["OK"].ToString();
                    tcpIPDTO.NotOK = this._dataReader["NotOK"].ToString();
                }
            }
            catch (Exception var_0_13A)
            {
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return tcpIPDTO;
        }

        internal int UpdateTcpIPPortData(TcpIPDTO tcpIPPort)
        {

            this.errorCode = 0;
            try
            {
                this.OpenDb();
                this._command.CommandText = string.Format("SELECT * from tblTcpIPConfigurations WHERE ConnectionType = " + tcpIPPort.ConnectionType);
                this._dataReader = this._command.ExecuteReader();
                bool tableHasRows = this._dataReader.HasRows;
                this.CloseDb();

                if (tableHasRows)
                {

                    this.OpenDb();
                    this._command.CommandText = string.Format("UPDATE tblTcpIPConfigurations SET ConnectionName='{0}', ConnectionType='{1}', IPAddress='{2}', PortNumber='{3}', CreatedDate='{4}', Ok='{5}', Notok='{6}' WHERE ConnectionType ={7}", new object[]
				            {
					            tcpIPPort.ConnectionName,
                                tcpIPPort.ConnectionType,
                                tcpIPPort.IPAddress,
                                tcpIPPort.PortNumber,
                                tcpIPPort.CreatedDate,
                                tcpIPPort.OK,
                                tcpIPPort.NotOK ,
                    tcpIPPort.ConnectionType
				            });
                    this.errorCode = this._command.ExecuteNonQuery();
                    this.CloseDb();

                }
                else
                {
                    this.OpenDb();
                    this._command.CommandText = string.Format("INSERT INTO tblTcpIPConfigurations (ConnectionName, ConnectionType , IPAddress , PortNumber , CreatedDate , Ok , Notok  ) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", new object[]
					        {
						        tcpIPPort.ConnectionName,
                            tcpIPPort.ConnectionType,
                            tcpIPPort.IPAddress,
                            tcpIPPort.PortNumber,
                            tcpIPPort.CreatedDate,
                            tcpIPPort.OK,
                            tcpIPPort.NotOK ,						
					        });

                    this.errorCode = this._command.ExecuteNonQuery();
                    this.CloseDb();
                }
            }

            catch (Exception ex)
            {
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return this.errorCode;
        }

        internal int UpdateTeachImageConfiguration(List<TeachImageConfigDTO> techImageConfigDTOList)
        {

            this.errorCode = 0;
            try
            {

                this.OpenDb();
                foreach (var techImageConfig in techImageConfigDTOList)
                {
                    this._command.CommandText =
                        //"INSERT INTO tblteachImageConfigurations ( ImageFactor, AreaGreaterThan , CropArea , CropAreaPercent, FillHoles , ValidHoles,MinRange, MaxRange ) VALUES( {0} , {1} ,'{2}',{3},{4},'{5}',{6},{7}) "

                        string.Format("INSERT INTO tblteachImageConfigurations (ImageFactor, AreaGreaterThan, CropArea, CropAreaPercent, FillHoles, ValidHoles, MinRange, MaxRange) VALUES     ({0}, {1}, '{2}', {3}, {4}, '{5}', {6}, {7})",
                        new object[]
					        {
				                techImageConfig.ImageFactor,
					            techImageConfig.AreaGreaterThan,
					            techImageConfig.CropArea,
					            techImageConfig.CropAreaPercent,
					            techImageConfig.FillHoles?1:0,
					            techImageConfig.validHoles,
					            techImageConfig.MinRange,
					            techImageConfig.MaxRange,
					        });
                    this.errorCode = this._command.ExecuteNonQuery();
                }
                this.CloseDb();


            }

            catch (Exception ex)
            {
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return this.errorCode;
        }

        internal List<TeachImageConfigDTO> GetTeachImageConfiguration()
        {
            this.errorCode = 0;
            List<TeachImageConfigDTO> teachImageConfigList = new List<TeachImageConfigDTO>();
            try
            {
                this.OpenDb();
                this._command.CommandText = "SELECT * from tblteachImageConfigurations";
                this._dataReader = this._command.ExecuteReader();
                while (this._dataReader.Read())
                {
                    var teachImageConfigDTO = new TeachImageConfigDTO();
                    teachImageConfigDTO.TeachImageConfigID = Convert.ToInt32(this._dataReader["TeachImageConfigID"]);
                    teachImageConfigDTO.ImageFactor = Convert.ToInt32(this._dataReader["ImageFactor"].ToString());
                    teachImageConfigDTO.AreaGreaterThan = Convert.ToInt32(this._dataReader["AreaGreaterThan"].ToString());
                    teachImageConfigDTO.CropArea = Convert.ToString(this._dataReader["CropArea"]);
                    teachImageConfigDTO.CropAreaPercent = Convert.ToInt32(this._dataReader["CropAreaPercent"]);
                    teachImageConfigDTO.FillHoles = Convert.ToBoolean(this._dataReader["FillHoles"]);
                    teachImageConfigDTO.validHoles = Convert.ToString(this._dataReader["ValidHoles"]);
                    teachImageConfigDTO.MinRange = Convert.ToInt32(this._dataReader["MinRange"].ToString());
                    teachImageConfigDTO.MaxRange = Convert.ToInt32(this._dataReader["MaxRange"].ToString());
                    teachImageConfigList.Add(teachImageConfigDTO);
                }
            }
            catch (Exception var_0_13A)
            {
                this.errorCode = -1;
            }
            finally
            {
                this.CloseDb();
            }
            return teachImageConfigList;
        }

    }
}
