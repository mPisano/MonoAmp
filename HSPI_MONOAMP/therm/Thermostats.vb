Imports HomeSeerAPI
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Scheduler.Classes
Imports System
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices

Namespace HSPI_INSTEON_THERMOSTAT
	Public Class Thermostats
		Public Sub New()
			MyBase.New()
		End Sub

		Private Function AddHvacDevices() As Boolean
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim flag As Boolean = True
			Try
				enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gHVACs.Values.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = enumerator.Current
					Dim str As String = Conversions.ToString(current("Name"))
					Dim str1 As String = Conversions.ToString(current("Location"))
					Dim deviceClass As Scheduler.Classes.DeviceClass = Nothing
					Dim str2 As String = String.Concat(str, " Mode")
					deviceClass = Me.LocateHSDeviceByPEDName(str2)
					If (deviceClass IsNot Nothing) Then
						If (Not current.Contains("HSREF_MODE")) Then
							current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_MODE", Nothing, Nothing)
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing HVAC MODE device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						Dim num As Integer = Me.CreateHvacModeDevice(str2, str1, str)
						If (num <= 0) Then
							flag = False
						Else
							current.Add(num, "HSREF_MODE", Nothing, Nothing)
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new HVAC MODE device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						End If
					End If
					Dim str3 As String = String.Concat(str, " Fan")
					deviceClass = Me.LocateHSDeviceByPEDName(str3)
					If (deviceClass IsNot Nothing) Then
						If (Not current.Contains("HSREF_FAN")) Then
							current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_FAN", Nothing, Nothing)
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing HVAC FAN device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						Dim num1 As Integer = Me.CreateHvacFanDevice(str3, str1, str, Conversions.ToInteger(current("HSREF_MODE")))
						If (num1 <= 0) Then
							flag = False
						Else
							current.Add(num1, "HSREF_FAN", Nothing, Nothing)
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new HVAC FAN device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						End If
					End If
					Dim str4 As String = String.Concat(str, " Cool")
					deviceClass = Me.LocateHSDeviceByPEDName(str4)
					If (deviceClass IsNot Nothing) Then
						If (Not current.Contains("HSREF_COOL")) Then
							current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_COOL", Nothing, Nothing)
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing HVAC COOL device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						Dim num2 As Integer = Me.CreateHvacCoolDevice(str4, str1, str, Conversions.ToInteger(current("HSREF_MODE")))
						If (num2 <= 0) Then
							flag = False
						Else
							current.Add(num2, "HSREF_COOL", Nothing, Nothing)
							Me.AdjustHvacCounterByPedRef(num2, "Cool", 0, True)
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new HVAC COOL device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						End If
					End If
					Dim str5 As String = String.Concat(str, " Heat")
					deviceClass = Me.LocateHSDeviceByPEDName(str5)
					If (deviceClass IsNot Nothing) Then
						If (Not current.Contains("HSREF_HEAT")) Then
							current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_HEAT", Nothing, Nothing)
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing HVAC Heat device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						Dim num3 As Integer = Me.CreateHvacHeatDevice(str5, str1, str, Conversions.ToInteger(current("HSREF_MODE")))
						If (num3 <= 0) Then
							flag = False
						Else
							current.Add(num3, "HSREF_HEAT", Nothing, Nothing)
							Me.AdjustHvacCounterByPedRef(num3, "Heat", 0, True)
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new HVAC Heat device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						End If
					End If
					Dim str6 As String = String.Concat(str, " Maintenance")
					deviceClass = Me.LocateHSDeviceByPEDName(str6)
					If (deviceClass IsNot Nothing) Then
						If (Not current.Contains("HSREF_MAINT")) Then
							current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_MAINT", Nothing, Nothing)
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing HVAC Maintenance device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						Dim num4 As Integer = Me.CreateHvacMaintenanceDevice(str6, str1, str, Conversions.ToInteger(current("HSREF_MODE")))
						If (num4 <= 0) Then
							flag = False
						Else
							current.Add(num4, "HSREF_MAINT", Nothing, Nothing)
							Me.AdjustHvacCounterByPedRef(num4, "MaintenanceInterval", 0, True)
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new HVAC Maintenance device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						End If
					End If
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			Return flag
		End Function

		Private Function AddThermostatDevices() As Boolean
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim flag As Boolean = True
			Try
				enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats.Values.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = enumerator.Current
					Dim str As String = Conversions.ToString(current("Name"))
					Dim str1 As String = Conversions.ToString(current("Location"))
					Dim flag1 As Boolean = Conversions.ToBoolean(current("Humidistat"))
					Dim flag2 As Boolean = Conversions.ToBoolean(current("ExtSensor"))
					Dim deviceClass As Scheduler.Classes.DeviceClass = Nothing
					Dim str2 As String = String.Concat(str, " Program")
					deviceClass = Me.LocateHSDeviceByPEDName(str2)
					If (deviceClass IsNot Nothing) Then
						If (Not current.Contains("HSREF_PROGRAM")) Then
							current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_PROGRAM", Nothing, Nothing)
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing TSTAT PROGRAM device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						Dim num As Integer = Me.CreateTstatProgramDevice(str2, str1, str)
						If (num <= 0) Then
							flag = False
						Else
							current.Add(num, "HSREF_PROGRAM", Nothing, Nothing)
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new TSTAT PROGRAM device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						End If
					End If
					Dim str3 As String = String.Concat(str, " Mode")
					deviceClass = Me.LocateHSDeviceByPEDName(str3)
					If (deviceClass IsNot Nothing) Then
						If (Not current.Contains("HSREF_MODE")) Then
							current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_MODE", Nothing, Nothing)
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing TSTAT MODE device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						Dim num1 As Integer = Me.CreateTstatModeDevice(str3, str1, str, Conversions.ToInteger(current("HSREF_PROGRAM")))
						If (num1 <= 0) Then
							flag = False
						Else
							current.Add(num1, "HSREF_MODE", Nothing, Nothing)
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new TSTAT MODE device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						End If
					End If
					Dim str4 As String = String.Concat(str, " Fan")
					deviceClass = Me.LocateHSDeviceByPEDName(str4)
					If (deviceClass IsNot Nothing) Then
						If (Not current.Contains("HSREF_FAN")) Then
							current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_FAN", Nothing, Nothing)
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing TSTAT FAN device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						Dim num2 As Integer = Me.CreateTstatFanDevice(str4, str1, str, Conversions.ToInteger(current("HSREF_PROGRAM")))
						If (num2 <= 0) Then
							flag = False
						Else
							current.Add(num2, "HSREF_FAN", Nothing, Nothing)
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new TSTAT FAN device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						End If
					End If
					Dim str5 As String = String.Concat(str, " Hold")
					deviceClass = Me.LocateHSDeviceByPEDName(str5)
					If (deviceClass IsNot Nothing) Then
						If (Not current.Contains("HSREF_HOLD")) Then
							current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_HOLD", Nothing, Nothing)
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing TSTAT HOLD device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						Dim num3 As Integer = Me.CreateTstatHoldDevice(str5, str1, str, Conversions.ToInteger(current("HSREF_PROGRAM")))
						If (num3 <= 0) Then
							flag = False
						Else
							current.Add(num3, "HSREF_HOLD", Nothing, Nothing)
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new TSTAT HOLD device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						End If
					End If
					Dim str6 As String = String.Concat(str, " Heat")
					deviceClass = Me.LocateHSDeviceByPEDName(str6)
					If (deviceClass IsNot Nothing) Then
						If (Not current.Contains("HSREF_HEAT")) Then
							current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_HEAT", Nothing, Nothing)
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing TSTAT HEAT device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						Dim num4 As Integer = Me.CreateTstatHeatDevice(str6, str1, str, Conversions.ToInteger(current("HSREF_PROGRAM")))
						If (num4 <= 0) Then
							flag = False
						Else
							current.Add(num4, "HSREF_HEAT", Nothing, Nothing)
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new TSTAT HEAT device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						End If
					End If
					Dim str7 As String = String.Concat(str, " Cool")
					deviceClass = Me.LocateHSDeviceByPEDName(str7)
					If (deviceClass IsNot Nothing) Then
						If (Not current.Contains("HSREF_COOL")) Then
							current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_COOL", Nothing, Nothing)
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing TSTAT COOL device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						Dim num5 As Integer = Me.CreateTstatCoolDevice(str7, str1, str, Conversions.ToInteger(current("HSREF_PROGRAM")))
						If (num5 <= 0) Then
							flag = False
						Else
							current.Add(num5, "HSREF_COOL", Nothing, Nothing)
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new TSTAT COOL device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						End If
					End If
					Dim str8 As String = String.Concat(str, " Temp")
					deviceClass = Me.LocateHSDeviceByPEDName(str8)
					If (deviceClass IsNot Nothing) Then
						If (Not current.Contains("HSREF_TEMP")) Then
							current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_TEMP", Nothing, Nothing)
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing TSTAT TEMP device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						Dim num6 As Integer = Me.CreateTstatTempDevice(str8, str1, str, Conversions.ToInteger(current("HSREF_PROGRAM")))
						If (num6 <= 0) Then
							flag = False
						Else
							current.Add(num6, "HSREF_TEMP", Nothing, Nothing)
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new TSTAT TEMP device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						End If
					End If
					If (flag2) Then
						Dim str9 As String = String.Concat(str, " Ext. Temp")
						deviceClass = Me.LocateHSDeviceByPEDName(str9)
						If (deviceClass IsNot Nothing) Then
							If (Not current.Contains("HSREF_EXTTEMP")) Then
								current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_EXTTEMP", Nothing, Nothing)
							End If
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing TSTAT EXT TEMP device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
						Else
							Dim num7 As Integer = Me.CreateTstatExtTempDevice(str9, str1, str, Conversions.ToInteger(current("HSREF_PROGRAM")))
							If (num7 <= 0) Then
								flag = False
							Else
								current.Add(num7, "HSREF_EXTTEMP", Nothing, Nothing)
								HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new TSTAT EXT TEMP device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
							End If
						End If
					End If
					If (Not flag1) Then
						Continue While
					End If
					Dim str10 As String = String.Concat(str, " Humidity")
					deviceClass = Me.LocateHSDeviceByPEDName(str10)
					If (deviceClass IsNot Nothing) Then
						If (Not current.Contains("HSREF_HUMIDITY")) Then
							current.Add(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs), "HSREF_HUMIDITY", Nothing, Nothing)
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Found existing TSTAT HUMIDITY device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						Dim num8 As Integer = Me.CreateTstatHumidityDevice(str10, str1, str, Conversions.ToInteger(current("HSREF_PROGRAM")))
						If (num8 <= 0) Then
							flag = False
						Else
							current.Add(num8, "HSREF_HUMIDITY", Nothing, Nothing)
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Created new TSTAT HUMIDITY device for: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						End If
					End If
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			Return flag
		End Function

		Friend Sub AdjustHvacCounterByHvac(ByVal hvac As Collection, ByVal pedRefName As String, ByVal pedCommand As String, ByVal value As Integer, Optional ByVal ResetFlag As Boolean = False)
			Dim str As String = "?"
			Dim [integer] As Integer = -1
			Try
				str = Conversions.ToString(hvac("Name"))
				[integer] = Conversions.ToInteger(hvac(pedRefName))
				Me.AdjustHvacCounterByPedRef([integer], pedCommand, value, ResetFlag)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "Problem adjusting HVAC ", str, " counter ", pedCommand, " device ref ", Conversions.ToString([integer]), " by value", Conversions.ToString(value), " ResetFlag? ", Conversions.ToString(ResetFlag), " : ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Friend Sub AdjustHvacCounterByName(ByVal hName As String, ByVal pedRefName As String, ByVal pedCommand As String, ByVal value As Integer, Optional ByVal ResetFlag As Boolean = False)
			Try
				Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gHVACs(hName)
				Me.AdjustHvacCounterByHvac(item, pedRefName, pedCommand, value, ResetFlag)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "Problem adjusting HVAC ", hName, " counter ", pedCommand, " by value", Conversions.ToString(value), " ResetFlag? ", Conversions.ToString(ResetFlag), " : ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Friend Sub AdjustHvacCounterByPedRef(ByVal pedRef As Integer, ByVal pedCommand As String, ByVal value As Integer, Optional ByVal ResetFlag As Boolean = False)
			Dim [integer] As Integer = -1
			Dim str As String = "-1"
			Dim str1 As String = "?"
			Try
				Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(pedRef), DeviceClass)
				Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				str1 = Conversions.ToString(plugExtraDataGet.GetNamed("ParentName"))
				[integer] = Conversions.ToInteger(plugExtraDataGet.GetNamed(pedCommand))
				If (Not ResetFlag) Then
					[integer] = [integer] + value
				Else
					Dim str2 As String = pedCommand
					If (Operators.CompareString(str2, "Cool", False) = 0) Then
						[integer] = 0
						plugExtraDataGet.RemoveNamed("CoolResetTime")
						plugExtraDataGet.AddNamed("CoolResetTime", DateAndTime.Now)
					ElseIf (Operators.CompareString(str2, "Heat", False) = 0) Then
						[integer] = 0
						plugExtraDataGet.RemoveNamed("HeatResetTime")
						plugExtraDataGet.AddNamed("HeatResetTime", DateAndTime.Now)
					ElseIf (Operators.CompareString(str2, "MaintenanceInterval", False) = 0) Then
						Dim num As Integer = 0
						Try
							Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gHVACs(Conversions.ToString(plugExtraDataGet.GetNamed("ParentName")))
							num = Conversions.ToInteger(item("MaintenanceInterval"))
						Catch exception As System.Exception
							ProjectData.SetProjectError(exception)
							num = 0
							ProjectData.ClearProjectError()
						End Try
						[integer] = num * 60
						plugExtraDataGet.RemoveNamed("MaintenanceResetTime")
						plugExtraDataGet.AddNamed("MaintenanceResetTime", DateAndTime.Now)
					End If
				End If
				plugExtraDataGet.RemoveNamed(pedCommand)
				plugExtraDataGet.AddNamed(pedCommand, [integer])
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, plugExtraDataGet)
				str = TimeSpan.FromMinutes(CDbl([integer])).ToString()
				str = str.Substring(0, str.Length - 3)
				Dim strArrays() As String = { str }
				deviceByRef.set_AdditionalDisplayData(HSPI_INSTEON_THERMOSTAT.utils.hs, strArrays)
				(DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.CAPIGetStatus(pedRef), CAPI.CAPIStatus)).Status = str
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(pedRef, DateAndTime.Now)
			Catch exception2 As System.Exception
				ProjectData.SetProjectError(exception2)
				Dim exception1 As System.Exception = exception2
				Dim strArrays1() As String = { "Problem adjusting HVAC ", str1, " counter ", pedCommand, " device ref ", Conversions.ToString(pedRef), " to value", Conversions.ToString([integer]), " represented by string ", str, " : ", exception1.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays1), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Friend Sub CallForHVAC(ByRef curStat As Collection, ByRef curHvac As Collection, ByVal mode As Integer, ByVal turnOn As Boolean)
			Dim str As String()
			Try
				If (Not turnOn) Then
					HSPI_INSTEON_THERMOSTAT.utils.myConfig.RemoveFromTPH(curStat, "HVACCall")
				Else
					HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatShort(curStat, "HVACCall", CShort(mode))
				End If
				Dim str1 As String = Conversions.ToString(curHvac("Name"))
				Dim [integer] As Integer = Conversions.ToInteger(curHvac("HSREF_MODE"))
				Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef([integer]), DeviceClass)
				Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				Dim num As Integer = Conversions.ToInteger(plugExtraDataGet.GetNamed("Mode"))
				If (mode = num) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str1, " already set to ", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(mode), " No change required."), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Test)
				Else
					Dim str2 As String = Conversions.ToString(curStat("Name"))
					Dim num1 As Integer = Me.OnCount(str1)
					If (turnOn) Then
						If (num1 <> 1) Then
							str = New String() { str1, " - ", Conversions.ToString(num1), " thermostats report this HVAC is on.  HVAC reports mode ", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(num), " Discarding this change request from ", str2, " for mode ", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(mode) }
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
						Else
							str = New String() { str1, " switching from ", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(num), " to ", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(mode) }
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
							plugExtraDataGet.RemoveNamed("Mode")
							plugExtraDataGet.AddNamed("Mode", mode)
							plugExtraDataGet.RemoveNamed("HVACCallStartTime")
							plugExtraDataGet.AddNamed("HVACCallStartTime", DateAndTime.Now)
							deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, plugExtraDataGet)
							HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef([integer], CDbl(mode), True)
						End If
					ElseIf (num1 <> 0) Then
						str = New String() { str1, " - ", Conversions.ToString(num1), " thermostats report this HVAC is still on.  Discarding this change request from ", str2, " for mode ", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(mode) }
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					Else
						str = New String() { str1, " switching from ", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(num), " to ", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(mode) }
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						Dim [date] As DateTime = Conversions.ToDate(plugExtraDataGet.GetNamed("HVACCallStartTime"))
						Dim num2 As Integer = CInt(DateAndTime.DateDiff("n", [date], DateAndTime.Now, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1))
						plugExtraDataGet.RemoveNamed("Mode")
						plugExtraDataGet.AddNamed("Mode", mode)
						plugExtraDataGet.RemoveNamed("HVACCallStartTime")
						deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, plugExtraDataGet)
						HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef([integer], CDbl(mode), True)
						Me.AdjustHvacCounterByHvac(curHvac, "HSREF_MAINT", "MaintenanceInterval", 0 - num2, False)
						Select Case num
							Case 1
								Me.AdjustHvacCounterByHvac(curHvac, "HSREF_HEAT", "Heat", num2, False)
								Exit Select
							Case 2
								Me.AdjustHvacCounterByHvac(curHvac, "HSREF_COOL", "Cool", num2, False)
								Exit Select
							Case Else
								str = New String() { str1, " Unexpected HVAC mode ", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(num), " when switching HVAC to ", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(mode), "  Can't adjust HEAT/COOL timers if mode is not HEAT/COOL." }
								HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
								Exit Select
						End Select
					End If
				End If
				HSPI_INSTEON_THERMOSTAT.utils.hs.SaveEventsDevices()
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Function CheckForMasterProgramDevice() As Boolean
			Dim flag As Boolean = False
			Try
				If (HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef > CLng(0) AndAlso DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef)), DeviceClass) IsNot Nothing) Then
					flag = True
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Private Function CreateHvacCoolDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String, ByVal rootDeviceRef As Integer) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(name)
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), Scheduler.Classes.DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.MISC_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.dvMISC.SHOW_VALUES)
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, False)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "HVAC Cool")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Plug_In,
					.Device_Type = 7,
					.Device_SubType = 2
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
				deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootDeviceRef)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootDeviceRef), Scheduler.Classes.DeviceClass)
				deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, num1)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("Cool", 0)
				clsPlugExtraDatum.AddNamed("CoolResetTime", DateAndTime.Now)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 0,
					.Graphic = "/images/INSTEON_THERMOSTAT/time.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 0,
					.HasAdditionalData = True,
					.Status = "@%0@"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Control) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 1,
					.Status = "Reset",
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				Dim strArrays() As String = { "00:00" }
				deviceByRef.set_AdditionalDisplayData(HSPI_INSTEON_THERMOSTAT.utils.hs, strArrays)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 0, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating HVAC COOL device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Private Function CreateHvacFanDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String, ByVal rootDeviceRef As Integer) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(name)
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), Scheduler.Classes.DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, False)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "HVAC Fan")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Plug_In,
					.Device_Type = 5
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
				deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootDeviceRef)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootDeviceRef), Scheduler.Classes.DeviceClass)
				deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, num1)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("Fan", 0)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 0,
					.Graphic = "/images/INSTEON_THERMOSTAT/fan-off.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 1,
					.Graphic = "/images/INSTEON_THERMOSTAT/fan-on.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 0,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(0)
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 1,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(1)
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 0, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating HVAC FAN device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Private Function CreateHvacHeatDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String, ByVal rootDeviceRef As Integer) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(name)
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), Scheduler.Classes.DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.MISC_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.dvMISC.SHOW_VALUES)
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, False)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "HVAC Heat")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Plug_In,
					.Device_Type = 7,
					.Device_SubType = 1
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
				deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootDeviceRef)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootDeviceRef), Scheduler.Classes.DeviceClass)
				deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, num1)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("Heat", 0)
				clsPlugExtraDatum.AddNamed("HeatResetTime", DateAndTime.Now)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 0,
					.Graphic = "/images/INSTEON_THERMOSTAT/time.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 0,
					.HasAdditionalData = True,
					.Status = "@%0@"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Control) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 1,
					.Status = "Reset",
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				Dim strArrays() As String = { "00:00" }
				deviceByRef.set_AdditionalDisplayData(HSPI_INSTEON_THERMOSTAT.utils.hs, strArrays)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 0, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating HVAC Heat device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Private Function CreateHvacMaintenanceDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String, ByVal rootDeviceRef As Integer) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(name)
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), Scheduler.Classes.DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.MISC_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.dvMISC.SHOW_VALUES)
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, False)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "HVAC Maintenance")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Plug_In,
					.Device_Type = 12
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
				deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootDeviceRef)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootDeviceRef), Scheduler.Classes.DeviceClass)
				deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, num1)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("MaintenanceInterval", 0)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 0,
					.Graphic = "/images/INSTEON_THERMOSTAT/time.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 0,
					.HasAdditionalData = True,
					.Status = "@%0@"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Control) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 1,
					.Status = "Reset",
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				Dim strArrays() As String = { "00:00" }
				deviceByRef.set_AdditionalDisplayData(HSPI_INSTEON_THERMOSTAT.utils.hs, strArrays)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 0, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating HVAC Maintenance device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Private Function CreateHvacModeDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(name)
				Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, False)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "HVAC Mode")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Plug_In,
					.Device_Type = 3
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Parent_Root)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("Mode", 0)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 0,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-off.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 1,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-heat.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 2,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-cool.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 3,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-auto.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 5,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-prog.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 6,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-prog-heat.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 7,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-prog-cool.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 0,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(0)
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 1,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(1)
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 2,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(2)
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 3,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(3)
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 5,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(5)
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 6,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(6)
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 7,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(7)
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 0, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating HVAC MODE device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Private Function CreateMasterProgramDevice() As Boolean
			Dim flag As Boolean = False
			Try
				HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef = CLng(HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef("Master Program"))
				Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef)), DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.MISC_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.dvMISC.SHOW_VALUES)
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, False)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "Master Program")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Plug_In,
					.Device_Type = 999
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Standalone)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Name", "Master Program")
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("Program", "None")
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim num As Integer = 0
				Dim uniqueProgramNames As String() = HSPI_INSTEON_THERMOSTAT.utils.myConfig.GetUniqueProgramNames()
				Dim num1 As Integer = 0
				Do
					Dim str As String = uniqueProgramNames(num1)
					Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
					{
						.PairType = VSVGPairs.VSVGPairType.SingleValue,
						.Value = CDbl(num),
						.Status = str,
						.Render = Enums.CAPIControlType.Values
					}
					HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef), vSPair)
					num = num + 1
					num1 = num1 + 1
				Loop While num1 < CInt(uniqueProgramNames.Length)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef), DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef), 0, False)
				flag = True
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				flag = False
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating Master Program device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Private Function CreateTstatCoolDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String, ByVal rootDeviceRef As Integer) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(name)
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), Scheduler.Classes.DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.MISC_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.dvMISC.SHOW_VALUES)
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, True)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "TSTAT Cool")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Thermostat,
					.Device_Type = 6,
					.Device_SubType = 2
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
				deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootDeviceRef)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootDeviceRef), Scheduler.Classes.DeviceClass)
				deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, num1)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("Cool", 78)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.Range,
					.RangeStart = 0,
					.RangeEnd = 125,
					.Graphic = "/images/INSTEON_THERMOSTAT/temp-cool.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.Range,
					.RangeStart = 0,
					.RangeEnd = 125
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Control) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 251,
					.Status = "+",
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Control) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 252,
					.Status = "-",
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 78, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating TSTAT COOL device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Private Function CreateTstatExtTempDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String, ByVal rootDeviceRef As Integer) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(name)
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), Scheduler.Classes.DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, True)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "TSTAT Ext. Temp")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Thermostat,
					.Device_Type = 10
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
				deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootDeviceRef)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootDeviceRef), Scheduler.Classes.DeviceClass)
				deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, num1)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("Temp", 50)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.Range,
					.RangeStart = 0,
					.RangeEnd = 125,
					.Graphic = "/images/INSTEON_THERMOSTAT/temp.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.Range,
					.RangeStart = 0,
					.RangeEnd = 125
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 50, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating TSTAT EXT TEMP device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Private Function CreateTstatFanDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String, ByVal rootDeviceRef As Integer) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(name)
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), Scheduler.Classes.DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.MISC_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.dvMISC.SHOW_VALUES)
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, True)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "TSTAT Fan")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Thermostat,
					.Device_Type = 4
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
				deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootDeviceRef)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootDeviceRef), Scheduler.Classes.DeviceClass)
				deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, num1)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("Fan", 0)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 0,
					.Graphic = "/images/INSTEON_THERMOSTAT/fan-off.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 1,
					.Graphic = "/images/INSTEON_THERMOSTAT/fan-on.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 0,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(0),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 1,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(1),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Control) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 2,
					.Status = "Toggle",
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 0, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating TSTAT FAN device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Private Function CreateTstatHeatDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String, ByVal rootDeviceRef As Integer) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(name)
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), Scheduler.Classes.DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.MISC_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.dvMISC.SHOW_VALUES)
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, True)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "TSTAT Heat")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Thermostat,
					.Device_Type = 6,
					.Device_SubType = 1
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
				deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootDeviceRef)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootDeviceRef), Scheduler.Classes.DeviceClass)
				deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, num1)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("Heat", 68)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.Range,
					.RangeStart = 0,
					.RangeEnd = 125,
					.Graphic = "/images/INSTEON_THERMOSTAT/temp-heat.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.Range,
					.RangeStart = 0,
					.RangeEnd = 125
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Control) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 251,
					.Status = "+",
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Control) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 252,
					.Status = "-",
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 68, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating TSTAT Heat device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Private Function CreateTstatHoldDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String, ByVal rootDeviceRef As Integer) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(name)
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), Scheduler.Classes.DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.MISC_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.dvMISC.SHOW_VALUES)
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, False)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "TSTAT Hold")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Thermostat,
					.Device_Type = 8
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
				deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootDeviceRef)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootDeviceRef), Scheduler.Classes.DeviceClass)
				deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, num1)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("Hold", 0)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 0,
					.Graphic = "/images/INSTEON_THERMOSTAT/run.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 1,
					.Graphic = "/images/INSTEON_THERMOSTAT/hold.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 0,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gHoldOpts(0),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 1,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gHoldOpts(1),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Control) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 2,
					.Status = "Toggle",
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 0, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating TSTAT HOLD device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Private Function CreateTstatHumidityDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String, ByVal rootDeviceRef As Integer) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(name)
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), Scheduler.Classes.DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, True)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "TSTAT Humidity")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Plug_In,
					.Device_Type = 1
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
				deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootDeviceRef)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootDeviceRef), Scheduler.Classes.DeviceClass)
				deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, num1)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("Humidity", 50)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.Range,
					.RangeStart = 0,
					.RangeEnd = 100,
					.Graphic = "/images/INSTEON_THERMOSTAT/humid.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.Range,
					.RangeStart = 0,
					.RangeEnd = 100
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 50, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating TSTAT HUMIDITY device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Private Function CreateTstatModeDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String, ByVal rootDeviceRef As Integer) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(name)
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), Scheduler.Classes.DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.MISC_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.dvMISC.SHOW_VALUES)
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, True)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "TSTAT Mode")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Thermostat,
					.Device_Type = 3
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
				deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootDeviceRef)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootDeviceRef), Scheduler.Classes.DeviceClass)
				deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, num1)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("Mode", 0)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 0,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-off.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 1,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-heat.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 2,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-cool.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 3,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-auto.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 5,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-prog.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 6,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-prog-heat.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 7,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-prog-cool.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 0,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(0),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 1,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(1),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 2,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(2),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 3,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(3),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 5,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(5),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 6,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(6),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 7,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(7),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 0, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating TSTAT MODE device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Private Function CreateTstatProgramDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(parentName)
				Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.MISC_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.dvMISC.SHOW_VALUES)
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, False)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "TSTAT Program")
				deviceByRef.set_Image(HSPI_INSTEON_THERMOSTAT.utils.hs, "/images/INSTEON_THERMOSTAT/hspi_insteon_thermostat.gif")
				deviceByRef.set_ImageLarge(HSPI_INSTEON_THERMOSTAT.utils.hs, "/images/INSTEON_THERMOSTAT/hspi_insteon_thermostat_big.jpg")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Thermostat,
					.Device_Type = 99
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Parent_Root)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("Program", "None")
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim num2 As Integer = 0
				Dim uniqueProgramNames As String() = HSPI_INSTEON_THERMOSTAT.utils.myConfig.GetUniqueProgramNames(parentName)
				Dim num3 As Integer = 0
				Do
					Dim str As String = uniqueProgramNames(num3)
					Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
					{
						.PairType = VSVGPairs.VSVGPairType.SingleValue,
						.Value = CDbl(num2),
						.Status = str,
						.Render = Enums.CAPIControlType.Values
					}
					HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
					num2 = num2 + 1
					num3 = num3 + 1
				Loop While num3 < CInt(uniqueProgramNames.Length)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 0, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating TSTAT PROGRAM device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Private Function CreateTstatTempDevice(ByVal name As String, ByVal loc As String, ByVal parentName As String, ByVal rootDeviceRef As Integer) As Integer
			Dim num As Integer = -1
			Try
				Dim num1 As Integer = HSPI_INSTEON_THERMOSTAT.utils.hs.NewDeviceRef(name)
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num1), Scheduler.Classes.DeviceClass)
				deviceByRef.set_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon Thermostat")
				deviceByRef.set_Address(HSPI_INSTEON_THERMOSTAT.utils.hs, "THERMOSTATS")
				deviceByRef.set_Code(HSPI_INSTEON_THERMOSTAT.utils.hs, HSPI_INSTEON_THERMOSTAT.utils.hs.GetNextVirtualCode())
				deviceByRef.set_Location(HSPI_INSTEON_THERMOSTAT.utils.hs, "Insteon")
				deviceByRef.set_Location2(HSPI_INSTEON_THERMOSTAT.utils.hs, "Thermostats")
				deviceByRef.set_Status_Support(HSPI_INSTEON_THERMOSTAT.utils.hs, True)
				deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "TSTAT Temp")
				Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
				{
					.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Thermostat,
					.Device_Type = 2
				}
				deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
				deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
				deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootDeviceRef)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootDeviceRef), Scheduler.Classes.DeviceClass)
				deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, num1)
				Dim clsPlugExtraDatum As PlugExtraData.clsPlugExtraData = New PlugExtraData.clsPlugExtraData()
				clsPlugExtraDatum.AddNamed("Version", "3.0.0.9")
				clsPlugExtraDatum.AddNamed("HSREF", num1)
				clsPlugExtraDatum.AddNamed("Name", name)
				clsPlugExtraDatum.AddNamed("ParentName", parentName)
				clsPlugExtraDatum.AddNamed("Location", loc)
				clsPlugExtraDatum.AddNamed("Temp", 50)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, clsPlugExtraDatum)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.Range,
					.RangeStart = 0,
					.RangeEnd = 125,
					.Graphic = "/images/INSTEON_THERMOSTAT/temp.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair(num1, vGPair)
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Status) With
				{
					.PairType = VSVGPairs.VSVGPairType.Range,
					.RangeStart = 0,
					.RangeEnd = 125
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(num1, vSPair)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(num1, DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(num1, 50, False)
				num = num1
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				num = -1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem creating TSTAT TEMP device! ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return num
		End Function

		Friend Function GetHvacPedByName(ByVal hName As String, ByVal pedRefName As String, ByVal pedCommand As String) As String
			Dim tstatHvacPedByTstatHvac As String
			Try
				Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gHVACs(hName)
				tstatHvacPedByTstatHvac = Me.GetTstatHvacPedByTstatHvac(item, pedRefName, pedCommand)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "Problem attempting to get HVAC ", hName, " device ", pedCommand, " : ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				tstatHvacPedByTstatHvac = "0"
				ProjectData.ClearProjectError()
			End Try
			Return tstatHvacPedByTstatHvac
		End Function

		Friend Function GetTstatHvacPedByPedRef(ByVal pedRef As Integer, ByVal pedCommand As String) As String
			Dim str As String
			Try
				Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(pedRef), DeviceClass)
				Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				str = Conversions.ToString(plugExtraDataGet.GetNamed(pedCommand))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "Problem attempting to get device name ", pedCommand, " device ref ", Conversions.ToString(pedRef), " : ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				str = "0"
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Friend Function GetTstatHvacPedByTstatHvac(ByVal tStatOrHvac As Collection, ByVal pedRefName As String, ByVal pedCommand As String) As String
			Dim tstatHvacPedByPedRef As String
			Dim str As String = ""
			Try
				str = Conversions.ToString(tStatOrHvac("Name"))
				Dim [integer] As Integer = Conversions.ToInteger(tStatOrHvac(pedRefName))
				tstatHvacPedByPedRef = Me.GetTstatHvacPedByPedRef([integer], pedCommand)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "Problem attempting to get ", str, " device ", pedCommand, " : ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				tstatHvacPedByPedRef = "0"
				ProjectData.ClearProjectError()
			End Try
			Return tstatHvacPedByPedRef
		End Function

		Friend Function GetTstatPedByName(ByVal tName As String, ByVal pedRefName As String, ByVal pedCommand As String) As String
			Dim tstatHvacPedByTstatHvac As String
			Try
				Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
				tstatHvacPedByTstatHvac = Me.GetTstatHvacPedByTstatHvac(item, pedRefName, pedCommand)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "Problem attempting to get thermostat ", tName, " device ", pedCommand, " : ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				tstatHvacPedByTstatHvac = "0"
				ProjectData.ClearProjectError()
			End Try
			Return tstatHvacPedByTstatHvac
		End Function

		Friend Function InitDevices() As Boolean
			Dim flag As Boolean = False
			Try
				Try
					If (Not Me.LocateMasterProgramDevice()) Then
						If (Not Me.CreateMasterProgramDevice()) Then
							Throw New System.Exception("Unable to locate or create Master Program device!")
						End If
						HSPI_INSTEON_THERMOSTAT.utils.Log("Created new Master Program device", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
					End If
					HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef), DateAndTime.Now)
					If (Not Me.AddHvacDevices()) Then
						Throw New System.Exception("Unable to add HVAC device(s)")
					End If
					If (Not Me.AddThermostatDevices()) Then
						Throw New System.Exception("Unable to add Thermostat device(s)")
					End If
					Me.UpdateDevicePrograms()
					Me.VerifyHSDeviceVersions()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					Throw exception
				End Try
			Finally
				HSPI_INSTEON_THERMOSTAT.utils.hs.SaveEventsDevices()
			End Try
			Return flag
		End Function

		Friend Function LocateHSDeviceByPEDName(ByVal findName As String) As Scheduler.Classes.DeviceClass
			Dim deviceClass As Scheduler.Classes.DeviceClass = Nothing
			Dim [next] As Scheduler.Classes.DeviceClass = Nothing
			Try
				Dim deviceEnumerator As clsDeviceEnumeration = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceEnumerator(), clsDeviceEnumeration)
				If (deviceEnumerator IsNot Nothing) Then
					Do
						[next] = deviceEnumerator.GetNext()
						If ([next] IsNot Nothing) Then
							If (Operators.CompareString([next].get_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs), "Insteon Thermostat", False) <> 0) Then
								Continue Do
							End If
							Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = [next].get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
							If (plugExtraDataGet Is Nothing OrElse Not Operators.ConditionalCompareObjectEqual(plugExtraDataGet.GetNamed("Name"), findName, False)) Then
								Continue Do
							End If
							deviceClass = [next]
							Exit Do
						End If
					Loop While Not deviceEnumerator.get_Finished()
				Else
					HSPI_INSTEON_THERMOSTAT.utils.Log("Can't get list of devices from HomeSeer Enumerator!", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem looking for HS device with PED name ", findName, " : ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return deviceClass
		End Function

		Friend Function LocateMasterProgramDevice() As Boolean
			Dim flag As Boolean = False
			Try
				Dim deviceEnumerator As clsDeviceEnumeration = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceEnumerator(), clsDeviceEnumeration)
				If (deviceEnumerator IsNot Nothing) Then
					Do
						Dim [next] As DeviceClass = deviceEnumerator.GetNext()
						If ([next] IsNot Nothing) Then
							If (Operators.CompareString([next].get_Interface(HSPI_INSTEON_THERMOSTAT.utils.hs), "Insteon Thermostat", False) <> 0) Then
								Continue Do
							End If
							Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = [next].get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
							If (plugExtraDataGet Is Nothing) Then
								Continue Do
							End If
							Dim objectValue As Object = RuntimeHelpers.GetObjectValue(plugExtraDataGet.GetNamed("Version"))
							If (Not Conversions.ToBoolean(If(Conversions.ToBoolean(Operators.CompareObjectEqual(objectValue, "3.0.0.0", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(objectValue, "3.0.0.1", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(objectValue, "3.0.0.2", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(objectValue, "3.0.0.3", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(objectValue, "3.0.0.4", False)), True, False))) Then
								If (Not Operators.ConditionalCompareObjectEqual(plugExtraDataGet.GetNamed("Name"), "Master Program", False)) Then
									Continue Do
								End If
								HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef = CLng([next].get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs))
								flag = True
								Exit Do
							Else
								If (Not Operators.ConditionalCompareObjectEqual(plugExtraDataGet.GetNamed("Name"), "Insteon Thermostat", False)) Then
									Continue Do
								End If
								HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef = CLng([next].get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs))
								flag = True
								Exit Do
							End If
						End If
					Loop While Not deviceEnumerator.get_Finished()
				Else
					HSPI_INSTEON_THERMOSTAT.utils.Log("Can't get list of devices from HomeSeer Enumerator!", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Exception locating Master Program device!  Ex=", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Private Function OnCount(ByVal hName As String) As Integer
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim num As Integer = 0
			Try
				enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats.Values.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = enumerator.Current
					If (Not current.Contains("HVACUnit") OrElse Not Operators.ConditionalCompareObjectEqual(current("HVACUnit"), hName, False) OrElse Not current.Contains("HVACCall")) Then
						Continue While
					End If
					num = num + 1
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			Return num
		End Function

		Friend Sub SetHvacPedByName(ByVal hName As String, ByVal pedRefName As String, ByVal pedCommand As String, ByVal iVal As Integer, ByVal iStr As String)
			Try
				Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gHVACs(hName)
				Me.SetTstatHvacPedByTstatHvac(item, pedRefName, pedCommand, iVal, iStr)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "Problem attempting to set HVAC device ", pedCommand, " to ", Conversions.ToString(iVal), " ", iStr, " : ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Friend Sub SetTstatHvacPedByPedRef(ByVal pedRef As Integer, ByVal pedCommand As String, ByVal iVal As Integer, ByVal iStr As String)
			Try
				Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(pedRef), DeviceClass)
				Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				Dim cAPIStatu As CAPI.CAPIStatus = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.CAPIGetStatus(pedRef), CAPI.CAPIStatus)
				If (Not String.IsNullOrEmpty(iStr)) Then
					plugExtraDataGet.RemoveNamed(pedCommand)
					plugExtraDataGet.AddNamed(pedCommand, iStr)
					deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, plugExtraDataGet)
					Dim strArrays() As String = { iStr }
					deviceByRef.set_AdditionalDisplayData(HSPI_INSTEON_THERMOSTAT.utils.hs, strArrays)
					HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(pedRef, 0, True)
					cAPIStatu.Status = iStr
				Else
					plugExtraDataGet.RemoveNamed(pedCommand)
					plugExtraDataGet.AddNamed(pedCommand, iVal)
					deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, plugExtraDataGet)
					HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(pedRef, CDbl(iVal), True)
				End If
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(pedRef, DateAndTime.Now)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays1() As String = { "Problem attempting to set device name ", pedCommand, " device ref ", Conversions.ToString(pedRef), " to ", Conversions.ToString(iVal), " ", iStr, " : ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays1), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Friend Sub SetTstatHvacPedByTstatHvac(ByVal tStatOrHvac As Collection, ByVal pedRefName As String, ByVal pedCommand As String, ByVal iVal As Integer, ByVal iStr As String)
			Dim flag As Boolean = False
			Dim str As String = ""
			Try
				str = Conversions.ToString(tStatOrHvac("Name"))
				If (Operators.CompareString(pedRefName, "HSREF_EXTTEMP", False) = 0) Then
					Try
						If (Not Conversions.ToBoolean(tStatOrHvac("ExtSensor"))) Then
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Request to set external sensor on ", str, " but config indicates it does not have an external sensor"), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
							flag = True
						End If
					Catch exception1 As System.Exception
						ProjectData.SetProjectError(exception1)
						Dim exception As System.Exception = exception1
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Unable to determine if thermostat has an external sensor during a set request: ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
						ProjectData.ClearProjectError()
					End Try
				End If
				If (Not flag) Then
					If (Operators.CompareString(pedRefName, "HSREF_HUMIDITY", False) = 0) Then
						Try
							If (Not Conversions.ToBoolean(tStatOrHvac("Humidistat"))) Then
								HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Request to set humidity on ", str, " but config indicates it does not have a humidity sensor"), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
								flag = True
							End If
						Catch exception3 As System.Exception
							ProjectData.SetProjectError(exception3)
							Dim exception2 As System.Exception = exception3
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Unable to determine if thermostat has a humidity sensor during a set request: ", exception2.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
							ProjectData.ClearProjectError()
						End Try
					End If
					If (Not flag) Then
						Dim [integer] As Integer = Conversions.ToInteger(tStatOrHvac(pedRefName))
						Me.SetTstatHvacPedByPedRef([integer], pedCommand, iVal, iStr)
					End If
				End If
			Catch exception5 As System.Exception
				ProjectData.SetProjectError(exception5)
				Dim exception4 As System.Exception = exception5
				Dim strArrays() As String = { "Problem attempting to set ", str, " device ", pedCommand, " to ", Conversions.ToString(iVal), " ", iStr, " : ", exception4.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			flag = False
		End Sub

		Friend Sub SetTstatPedByName(ByVal tName As String, ByVal pedRefName As String, ByVal pedCommand As String, ByVal iVal As Integer, ByVal iStr As String)
			Try
				Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
				Me.SetTstatHvacPedByTstatHvac(item, pedRefName, pedCommand, iVal, iStr)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				Dim strArrays() As String = { "Problem attempting to set thermostat device ", pedCommand, " to ", Conversions.ToString(iVal), " ", iStr, " : ", exception.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Friend Sub UpdateDeviceMode(ByVal tName As String)
			Dim flag As Boolean = False
			Dim item As Collection = Nothing
			Dim [integer] As Integer = -1
			Dim deviceByRef As DeviceClass = Nothing
			Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = Nothing
			Dim num As Integer = -1
			Try
				item = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
				[integer] = Conversions.ToInteger(item("HSREF_MODE"))
				deviceByRef = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef([integer]), DeviceClass)
				plugExtraDataGet = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				num = Conversions.ToInteger(plugExtraDataGet.GetNamed("Mode"))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("UpdateDeviceMode: Unable to update Thermostat ", tName, " mode device : ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
				flag = True
			End Try
			If (Not flag) Then
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_ClearAll([integer], True)
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_ClearAll([integer], True)
				Dim vGPair As VSVGPairs.VGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 0,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-off.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair([integer], vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 1,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-heat.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair([integer], vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 2,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-cool.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair([integer], vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 3,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-auto.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair([integer], vGPair)
				vGPair = New VSVGPairs.VGPair() With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Set_Value = 5,
					.Graphic = "/images/INSTEON_THERMOSTAT/mode-prog.png"
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair([integer], vGPair)
				If (HSPI_INSTEON_THERMOSTAT.utils.isVenstarDEVCAT(item)) Then
					vGPair = New VSVGPairs.VGPair() With
					{
						.PairType = VSVGPairs.VSVGPairType.SingleValue,
						.Set_Value = 6,
						.Graphic = "/images/INSTEON_THERMOSTAT/mode-prog-heat.png"
					}
					HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair([integer], vGPair)
					vGPair = New VSVGPairs.VGPair() With
					{
						.PairType = VSVGPairs.VSVGPairType.SingleValue,
						.Set_Value = 7,
						.Graphic = "/images/INSTEON_THERMOSTAT/mode-prog-cool.png"
					}
					HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVGP_AddPair([integer], vGPair)
				ElseIf (num > 5) Then
					num = 5
					plugExtraDataGet.RemoveNamed("Mode")
					plugExtraDataGet.AddNamed("Mode", num)
					deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, plugExtraDataGet)
				End If
				Dim vSPair As VSVGPairs.VSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 0,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(0),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair([integer], vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 1,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(1),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair([integer], vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 2,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(2),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair([integer], vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 3,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(3),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair([integer], vSPair)
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = 5,
					.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(5),
					.Render = Enums.CAPIControlType.Button
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair([integer], vSPair)
				If (HSPI_INSTEON_THERMOSTAT.utils.isVenstarDEVCAT(item)) Then
					vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
					{
						.PairType = VSVGPairs.VSVGPairType.SingleValue,
						.Value = 6,
						.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(6),
						.Render = Enums.CAPIControlType.Button
					}
					HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair([integer], vSPair)
					vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
					{
						.PairType = VSVGPairs.VSVGPairType.SingleValue,
						.Value = 7,
						.Status = HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(7),
						.Render = Enums.CAPIControlType.Button
					}
					HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair([integer], vSPair)
				End If
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange([integer], DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef([integer], CDbl(num), False)
			End If
			flag = False
		End Sub

		Friend Sub UpdateDevicePrograms()
			Dim vSPair As VSVGPairs.VSPair
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef)), DeviceClass)
			Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
			Dim str As String = Conversions.ToString(plugExtraDataGet.GetNamed("Program"))
			Dim num As Integer = -1
			HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_ClearAll(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef), True)
			Dim num1 As Integer = 0
			Dim uniqueProgramNames As String() = HSPI_INSTEON_THERMOSTAT.utils.myConfig.GetUniqueProgramNames()
			Dim num2 As Integer = 0
			Do
				Dim str1 As String = uniqueProgramNames(num2)
				If (Operators.CompareString(str1, str, False) = 0) Then
					num = num1
				End If
				vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
				{
					.PairType = VSVGPairs.VSVGPairType.SingleValue,
					.Value = CDbl(num1),
					.Status = str1,
					.Render = Enums.CAPIControlType.Values
				}
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef), vSPair)
				num1 = num1 + 1
				num2 = num2 + 1
			Loop While num2 < CInt(uniqueProgramNames.Length)
			If (num < 0) Then
				plugExtraDataGet.RemoveNamed("Program")
				plugExtraDataGet.AddNamed("Program", "None")
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, plugExtraDataGet)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef), 0, False)
			Else
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef), CDbl(num), False)
			End If
			HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef), DateAndTime.Now)
			Try
				enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats.Values.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = enumerator.Current
					Dim objectValue As Object = RuntimeHelpers.GetObjectValue(current("HSREF_PROGRAM"))
					deviceByRef = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(Conversions.ToInteger(objectValue)), DeviceClass)
					plugExtraDataGet = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
					Dim str2 As String = Conversions.ToString(plugExtraDataGet.GetNamed("Program"))
					Dim num3 As Integer = -1
					HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_ClearAll(Conversions.ToInteger(objectValue), True)
					num1 = 0
					Dim strArrays As String() = HSPI_INSTEON_THERMOSTAT.utils.myConfig.GetUniqueProgramNames(Conversions.ToString(current("Name")))
					Dim num4 As Integer = 0
					Do
						Dim str3 As String = strArrays(num4)
						If (Operators.CompareString(str3, str2, False) = 0) Then
							num3 = num1
						End If
						vSPair = New VSVGPairs.VSPair(ePairStatusControl.Both) With
						{
							.PairType = VSVGPairs.VSVGPairType.SingleValue,
							.Value = CDbl(num1),
							.Status = str3,
							.Render = Enums.CAPIControlType.Values
						}
						HSPI_INSTEON_THERMOSTAT.utils.hs.DeviceVSP_AddPair(Conversions.ToInteger(objectValue), vSPair)
						num1 = num1 + 1
						num4 = num4 + 1
					Loop While num4 < CInt(strArrays.Length)
					If (num3 < 0) Then
						plugExtraDataGet.RemoveNamed("Program")
						plugExtraDataGet.AddNamed("Program", "None")
						deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, plugExtraDataGet)
						HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(Conversions.ToInteger(objectValue), 0, False)
					Else
						HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(Conversions.ToInteger(objectValue), CDbl(num3), False)
					End If
					HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(Conversions.ToInteger(objectValue), DateAndTime.Now)
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveEventsDevices()
		End Sub

		Private Function UpdateDeviceVersionCheckPED(ByVal dvRef As Integer) As Boolean
			Dim flag As Boolean = True
			Try
				Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(dvRef), DeviceClass)
				Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				If (plugExtraDataGet Is Nothing) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Device Ref=", Conversions.ToString(dvRef), " is missing required PED data!"), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ElseIf (Operators.CompareString(Conversions.ToString(plugExtraDataGet.GetNamed("Version")), "3.0.0.9", False) = 0) Then
					flag = False
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem checking PED version for device ref=", Conversions.ToString(dvRef), " : ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Private Sub UpdateHvacDeviceVersion(ByVal dvType As String, ByVal dvRef As Integer, ByVal rootRef As Integer)
			Dim flag As Boolean = False
			Dim str As String = dvType
			Try
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootRef), Scheduler.Classes.DeviceClass)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(dvRef), Scheduler.Classes.DeviceClass)
				Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceClass.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				str = Conversions.ToString(plugExtraDataGet.GetNamed("Name"))
				Dim objectValue As Object = RuntimeHelpers.GetObjectValue(plugExtraDataGet.GetNamed("Version"))
				If (Operators.ConditionalCompareObjectNotEqual(objectValue, "3.0.0.9", False)) Then
					Dim obj As Object = objectValue
					If (Conversions.ToBoolean(If(Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.0", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.1", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.2", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.3", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.4", False)), True, False))) Then
						deviceClass.AssociatedDevice_ClearAll(HSPI_INSTEON_THERMOSTAT.utils.hs)
						Dim str1 As String = dvType
						If (Operators.CompareString(str1, "HSREF_MODE", False) <> 0) Then
							If (Operators.CompareString(str1, "HSREF_FAN", False) <> 0) Then
								If (Operators.CompareString(str1, "HSREF_COOL", False) <> 0) Then
									If (Operators.CompareString(str1, "HSREF_HEAT", False) <> 0) Then
										flag = Operators.CompareString(str1, "HSREF_MAINT", False) <> 0
									End If
								End If
							End If
							If (Not flag) Then
								Dim deviceTypeGet As DeviceTypeInfo_m.DeviceTypeInfo = deviceClass.get_DeviceType_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
								deviceTypeGet.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Plug_In
								deviceClass.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeGet)
								deviceClass.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
								deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootRef)
								deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, dvRef)
							End If
						Else
							Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
							{
								.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Plug_In,
								.Device_Type = 3
							}
							deviceClass.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
							deviceClass.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Parent_Root)
						End If
					End If
					flag = False
					plugExtraDataGet.RemoveNamed("Version")
					plugExtraDataGet.AddNamed("Version", "3.0.0.9")
					deviceClass.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, plugExtraDataGet)
					HSPI_INSTEON_THERMOSTAT.utils.Log(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(String.Concat(String.Concat("Upgraded ", str), " device from "), objectValue), " to "), "3.0.0.9")), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem attempting to upgrade ", str, " : ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Private Sub UpdateMasterProgramDeviceVersion(ByVal dvRef As Integer)
			Try
				Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(dvRef), DeviceClass)
				Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				Dim objectValue As Object = RuntimeHelpers.GetObjectValue(plugExtraDataGet.GetNamed("Version"))
				If (Operators.ConditionalCompareObjectNotEqual(objectValue, "3.0.0.9", False)) Then
					Dim obj As Object = objectValue
					If (Conversions.ToBoolean(If(Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.0", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.1", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.2", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.3", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.4", False)), True, False))) Then
						plugExtraDataGet.RemoveNamed("Name")
						plugExtraDataGet.AddNamed("Name", "Master Program")
						Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
						{
							.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Plug_In,
							.Device_Type = 999
						}
						deviceByRef.set_Device_Type_String(HSPI_INSTEON_THERMOSTAT.utils.hs, "Master Program")
						deviceByRef.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
						deviceByRef.AssociatedDevice_ClearAll(HSPI_INSTEON_THERMOSTAT.utils.hs)
						deviceByRef.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Standalone)
					End If
					plugExtraDataGet.RemoveNamed("Version")
					plugExtraDataGet.AddNamed("Version", "3.0.0.9")
					deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, plugExtraDataGet)
					HSPI_INSTEON_THERMOSTAT.utils.Log(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Upgraded Master Program device from ", objectValue), " to "), "3.0.0.9")), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Caught exception attempting to upgrade Master Program device ref=", Conversions.ToString(dvRef), ". Ex=", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Private Sub UpdateTstatDeviceVersion(ByVal dvType As String, ByVal dvRef As Integer, ByVal rootRef As Integer)
			Dim flag As Boolean = False
			Dim flag1 As Boolean = False
			Dim str As String = dvType
			Try
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(rootRef), Scheduler.Classes.DeviceClass)
				Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(dvRef), Scheduler.Classes.DeviceClass)
				Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceClass.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				str = Conversions.ToString(plugExtraDataGet.GetNamed("Name"))
				Dim objectValue As Object = RuntimeHelpers.GetObjectValue(plugExtraDataGet.GetNamed("Version"))
				If (Operators.ConditionalCompareObjectNotEqual(objectValue, "3.0.0.9", False)) Then
					Dim obj As Object = objectValue
					If (Conversions.ToBoolean(If(Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.0", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.1", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.2", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.3", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.4", False)), True, False))) Then
						deviceClass.AssociatedDevice_ClearAll(HSPI_INSTEON_THERMOSTAT.utils.hs)
						Dim str1 As String = dvType
						If (Operators.CompareString(str1, "HSREF_PROGRAM", False) = 0) Then
							Dim deviceTypeInfo As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
							{
								.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Thermostat,
								.Device_Type = 99
							}
							deviceClass.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo)
							deviceClass.set_Image(HSPI_INSTEON_THERMOSTAT.utils.hs, "/images/INSTEON_THERMOSTAT/hspi_insteon_thermostat.gif")
							deviceClass.set_ImageLarge(HSPI_INSTEON_THERMOSTAT.utils.hs, "/images/INSTEON_THERMOSTAT/hspi_insteon_thermostat_big.jpg")
							deviceClass.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Parent_Root)
						ElseIf (Operators.CompareString(str1, "HSREF_HUMIDITY", False) <> 0) Then
							If (Operators.CompareString(str1, "HSREF_MODE", False) <> 0) Then
								If (Operators.CompareString(str1, "HSREF_FAN", False) <> 0) Then
									flag = Operators.CompareString(str1, "HSREF_HOLD", False) = 0
									If (Not flag) Then
										If (Operators.CompareString(str1, "HSREF_HEAT", False) <> 0) Then
											If (Operators.CompareString(str1, "HSREF_COOL", False) <> 0) Then
												If (Operators.CompareString(str1, "HSREF_TEMP", False) <> 0) Then
													flag1 = Operators.CompareString(str1, "HSREF_EXTTEMP", False) <> 0
												End If
											End If
										End If
										If (Not flag1) Then
											If (Operators.ConditionalCompareObjectEqual(objectValue, "3.0.0.0", False)) Then
												Throw New System.Exception("Unable to upgrade this device from 3.0.0.0.  Delete this device via the Home screen and stop/start plug-in to recreate")
											End If
											deviceClass.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
											deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootRef)
											deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, dvRef)
											flag1 = True
										End If
									End If
								End If
							End If
							If (flag OrElse Not flag1) Then
								If (flag OrElse Not flag1) Then
									flag = False
									deviceClass.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
									deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootRef)
									deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, dvRef)
								End If
							End If
						Else
							If (Operators.ConditionalCompareObjectEqual(objectValue, "3.0.0.0", False)) Then
								Throw New System.Exception("Unable to upgrade this device from 3.0.0.0.  Delete this device via the Home screen and stop/start plug-in to recreate")
							End If
							Dim deviceTypeInfo1 As DeviceTypeInfo_m.DeviceTypeInfo = New DeviceTypeInfo_m.DeviceTypeInfo() With
							{
								.Device_API = DeviceTypeInfo_m.DeviceTypeInfo.eDeviceAPI.Plug_In,
								.Device_Type = 1
							}
							deviceClass.set_DeviceType_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, deviceTypeInfo1)
							deviceClass.set_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs, Enums.eRelationship.Child)
							deviceClass.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, rootRef)
							deviceByRef.AssociatedDevice_Add(HSPI_INSTEON_THERMOSTAT.utils.hs, dvRef)
						End If
					ElseIf (Conversions.ToBoolean(If(Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.5", False)) OrElse Conversions.ToBoolean(Operators.CompareObjectEqual(obj, "3.0.0.6", False)), True, False)) AndAlso Operators.CompareString(dvType, "HSREF_PROGRAM", False) = 0) Then
						deviceClass.set_Image(HSPI_INSTEON_THERMOSTAT.utils.hs, "/images/INSTEON_THERMOSTAT/hspi_insteon_thermostat.gif")
						deviceClass.set_ImageLarge(HSPI_INSTEON_THERMOSTAT.utils.hs, "/images/INSTEON_THERMOSTAT/hspi_insteon_thermostat_big.jpg")
					End If
					flag1 = False
					plugExtraDataGet.RemoveNamed("Version")
					plugExtraDataGet.AddNamed("Version", "3.0.0.9")
					deviceClass.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, plugExtraDataGet)
					HSPI_INSTEON_THERMOSTAT.utils.Log(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(String.Concat(String.Concat("Upgraded ", str), " device from "), objectValue), " to "), "3.0.0.9")), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem attempting to upgrade ", str, " : ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Private Sub VerifyHSDeviceVersions()
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim enumerator1 As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			If (Me.UpdateDeviceVersionCheckPED(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef))) Then
				Me.UpdateMasterProgramDeviceVersion(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef))
			End If
			Try
				enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gHVACs.Values.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = enumerator.Current
					Dim [integer] As Integer = Conversions.ToInteger(current("HSREF_MODE"))
					Dim num As Integer = Conversions.ToInteger(current("HSREF_FAN"))
					Dim integer1 As Integer = Conversions.ToInteger(current("HSREF_COOL"))
					Dim num1 As Integer = Conversions.ToInteger(current("HSREF_HEAT"))
					Dim integer2 As Integer = Conversions.ToInteger(current("HSREF_MAINT"))
					If (Me.UpdateDeviceVersionCheckPED([integer])) Then
						Me.UpdateHvacDeviceVersion("HSREF_MODE", [integer], [integer])
					End If
					If (Me.UpdateDeviceVersionCheckPED(num)) Then
						Me.UpdateHvacDeviceVersion("HSREF_FAN", num, [integer])
					End If
					If (Me.UpdateDeviceVersionCheckPED(integer1)) Then
						Me.UpdateHvacDeviceVersion("HSREF_COOL", integer1, [integer])
					End If
					If (Me.UpdateDeviceVersionCheckPED(num1)) Then
						Me.UpdateHvacDeviceVersion("HSREF_HEAT", num1, [integer])
					End If
					If (Not Me.UpdateDeviceVersionCheckPED(integer2)) Then
						Continue While
					End If
					Me.UpdateHvacDeviceVersion("HSREF_MAINT", integer2, [integer])
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			Try
				enumerator1 = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats.Values.GetEnumerator()
				While enumerator1.MoveNext()
					Dim collections As Collection = enumerator1.Current
					Dim num2 As Integer = Conversions.ToInteger(collections("HSREF_PROGRAM"))
					Dim integer3 As Integer = Conversions.ToInteger(collections("HSREF_MODE"))
					Dim num3 As Integer = Conversions.ToInteger(collections("HSREF_FAN"))
					Dim integer4 As Integer = Conversions.ToInteger(collections("HSREF_HOLD"))
					Dim num4 As Integer = Conversions.ToInteger(collections("HSREF_HEAT"))
					Dim integer5 As Integer = Conversions.ToInteger(collections("HSREF_COOL"))
					Dim num5 As Integer = Conversions.ToInteger(collections("HSREF_TEMP"))
					If (Me.UpdateDeviceVersionCheckPED(num2)) Then
						Me.UpdateTstatDeviceVersion("HSREF_PROGRAM", num2, num2)
					End If
					If (Me.UpdateDeviceVersionCheckPED(integer3)) Then
						Me.UpdateTstatDeviceVersion("HSREF_MODE", integer3, num2)
					End If
					If (Me.UpdateDeviceVersionCheckPED(num3)) Then
						Me.UpdateTstatDeviceVersion("HSREF_FAN", num3, num2)
					End If
					If (Me.UpdateDeviceVersionCheckPED(integer4)) Then
						Me.UpdateTstatDeviceVersion("HSREF_HOLD", integer4, num2)
					End If
					If (Me.UpdateDeviceVersionCheckPED(num4)) Then
						Me.UpdateTstatDeviceVersion("HSREF_HEAT", num4, num2)
					End If
					If (Me.UpdateDeviceVersionCheckPED(integer5)) Then
						Me.UpdateTstatDeviceVersion("HSREF_COOL", integer5, num2)
					End If
					If (Me.UpdateDeviceVersionCheckPED(num5)) Then
						Me.UpdateTstatDeviceVersion("HSREF_TEMP", num5, num2)
					End If
					Try
						If (Conversions.ToBoolean(collections("ExtSensor"))) Then
							Dim integer6 As Integer = Conversions.ToInteger(collections("HSREF_EXTTEMP"))
							If (Me.UpdateDeviceVersionCheckPED(integer6)) Then
								Me.UpdateTstatDeviceVersion("HSREF_EXTTEMP", integer6, num2)
							End If
						End If
					Catch exception As System.Exception
						ProjectData.SetProjectError(exception)
						ProjectData.ClearProjectError()
					End Try
					Try
						If (Conversions.ToBoolean(collections("Humidistat"))) Then
							Dim num6 As Integer = Conversions.ToInteger(collections("HSREF_HUMIDITY"))
							If (Me.UpdateDeviceVersionCheckPED(num6)) Then
								Me.UpdateTstatDeviceVersion("HSREF_HUMIDITY", num6, num2)
							End If
						End If
					Catch exception1 As System.Exception
						ProjectData.SetProjectError(exception1)
						ProjectData.ClearProjectError()
					End Try
				End While
			Finally
				(DirectCast(enumerator1, IDisposable)).Dispose()
			End Try
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveEventsDevices()
		End Sub
	End Class
End Namespace