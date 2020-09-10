Imports HomeSeerAPI
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices
Imports System.Text

Namespace HSPI_INSTEON_THERMOSTAT
	Public Class Config
		Public Const INIFILE As String = "hspi_insteon_thermostat.ini"

		Public Const INIFILESECTION_GENERAL As String = "hspi_insteon_thermostat"

		Public Const INIFILESECTION_THERMOSTATS As String = "thermostats"

		Public Const INIFILESECTION_PROGRAMS As String = "programs"

		Public Const INIFILESECTION_HVACS As String = "hvac_units"

		Public logLvl As Integer

		Public logFileDebug As String

		Public Version As String

		Private _LogDebugInfoChecked As Boolean

		Private _LogTesting As Boolean

		Public InsteonFlags As Byte

		Public BoundsTempLow As Integer

		Public BoundsTempHigh As Integer

		Public BoundsHeatSetLow As Integer

		Public BoundsHeatSetHigh As Integer

		Public BoundsCoolSetLow As Integer

		Public BoundsCoolSetHigh As Integer

		Public BoundsHumidityLow As Integer

		Public BoundsHumidityHigh As Integer

		Public ControlPageRefresh As Integer

		Public gPrograms As ArrayList

		Public gTstats As SortedDictionary(Of String, Collection)

		Public gHVACs As SortedDictionary(Of String, Collection)

		Private dTstats As SortedDictionary(Of String, Collection)

		Private dHVACs As SortedDictionary(Of String, Collection)

		Public Property LogDebugInfoChecked As Boolean
			Get
				Return Me._LogDebugInfoChecked
			End Get
			Set(ByVal value As Boolean)
				Me._LogDebugInfoChecked = value
				Me.SetLogLvl()
			End Set
		End Property

		Public Sub New()
			MyBase.New()
			Me.logLvl = 3
			Me._LogTesting = False
			Me.gPrograms = New ArrayList()
			Me.gTstats = New SortedDictionary(Of String, Collection)()
			Me.gHVACs = New SortedDictionary(Of String, Collection)()
			Me.dTstats = New SortedDictionary(Of String, Collection)()
			Me.dHVACs = New SortedDictionary(Of String, Collection)()
		End Sub

		Friend Sub AddHvac(ByVal hvac As Collection)
			Dim str As String = Conversions.ToString(hvac("Name"))
			If (Me.gHVACs.ContainsKey(str)) Then
				Throw New Exception("Duplicate HVAC name!")
			End If
			If (Not Me.dHVACs.ContainsKey(str)) Then
				Me.gHVACs.Add(str, hvac)
			Else
				Me.gHVACs.Add(str, Me.dHVACs(str))
				Me.dHVACs.Remove(str)
			End If
		End Sub

		Friend Sub AddProgram(ByVal program As Collection)
			Me.gPrograms.Add(program)
		End Sub

		Friend Sub AddTstat(ByVal tstat As Collection)
			Dim str As String = Conversions.ToString(tstat("Name"))
			If (Me.gTstats.ContainsKey(str)) Then
				Throw New Exception("Duplicate thermostat name!")
			End If
			If (Not Me.dTstats.ContainsKey(str)) Then
				Me.gTstats.Add(str, tstat)
			Else
				Me.gTstats.Add(str, Me.dTstats(str))
				Me.dTstats.Remove(str)
			End If
		End Sub

		Friend Function BuildCoolSetPointRange() As String()
			Dim boundsCoolSetHigh As Integer = Me.BoundsCoolSetHigh - Me.BoundsCoolSetLow
			Dim str(boundsCoolSetHigh + 1) As String
			Dim num As Integer = boundsCoolSetHigh
			Dim num1 As Integer = 0
			Do
				str(num1) = Conversions.ToString(Me.BoundsCoolSetLow + num1)
				num1 = num1 + 1
			Loop While num1 <= num
			Return str
		End Function

		Friend Function BuildHeatSetPointRange() As String()
			Dim boundsHeatSetHigh As Integer = Me.BoundsHeatSetHigh - Me.BoundsHeatSetLow
			Dim str(boundsHeatSetHigh + 1) As String
			Dim num As Integer = boundsHeatSetHigh
			Dim num1 As Integer = 0
			Do
				str(num1) = Conversions.ToString(Me.BoundsHeatSetLow + num1)
				num1 = num1 + 1
			Loop While num1 <= num
			Return str
		End Function

		Private Function DeleteHSDevice(ByVal hsRef As Integer) As Boolean
			Dim flag As Boolean = False
			Try
				HSPI_INSTEON_THERMOSTAT.utils.hs.DeleteDevice(hsRef)
				flag = True
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				flag = False
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Friend Sub DeletePendingDevices()
			Dim enumerator As SortedDictionary(Of String, Collection).KeyCollection.Enumerator = New SortedDictionary(Of String, Collection).KeyCollection.Enumerator()
			Dim enumerator1 As SortedDictionary(Of String, Collection).KeyCollection.Enumerator = New SortedDictionary(Of String, Collection).KeyCollection.Enumerator()
			Try
				enumerator = Me.dTstats.Keys.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As String = enumerator.Current
					Me.RemoveTstatDevices(current)
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.UnregisterTstat(Me.dTstats(current))
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			Try
				enumerator1 = Me.dHVACs.Keys.GetEnumerator()
				While enumerator1.MoveNext()
					Me.RemoveHvacDevices(enumerator1.Current)
				End While
			Finally
				(DirectCast(enumerator1, IDisposable)).Dispose()
			End Try
			Me.dTstats.Clear()
			Me.dHVACs.Clear()
		End Sub

		Friend Function GetHvac(ByVal tName As String) As Collection
			Dim item As Collection = Nothing
			Dim collections As Collection = Nothing
			Try
				item = Me.gTstats(tName)
				If (item.Contains("HVACUnit") AndAlso Operators.ConditionalCompareObjectNotEqual(RuntimeHelpers.GetObjectValue(item("HVACUnit")), "None", False)) Then
					collections = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gHVACs(Conversions.ToString(item("HVACUnit")))
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				collections = Nothing
				ProjectData.ClearProjectError()
			End Try
			Return collections
		End Function

		Friend Function GetHvacByNum(ByVal hNum As Integer) As Collection
			Return Me.gHVACs(Me.NameByNum(Me.gHVACs, hNum))
		End Function

		Private Function GetPEDString(ByVal tpv As Collection, ByVal command As String) As String
			Dim str As String = Nothing
			Try
				str = Conversions.ToString(tpv(command))
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Friend Function getTstatByAddr(ByVal iAddr As String) As Collection
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim flag As Boolean = False
			Dim collections As Collection = Nothing
			If (Not String.IsNullOrEmpty(iAddr)) Then
				iAddr = iAddr.ToUpper()
			End If
			Try
				enumerator = Me.gTstats.Values.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = enumerator.Current
					If (Operators.CompareString(Conversions.ToString(current("InsteonAddress")), iAddr, False) <> 0) Then
						Continue While
					End If
					collections = current
					flag = True
					If (flag) Then
						Exit While
					End If
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			flag = False
			Return collections
		End Function

		Friend Function getTstatByNum(ByVal tNum As Integer) As Collection
			Dim str As String = Me.NameByNum(Me.gTstats, tNum)
			If (str Is Nothing) Then
				Return Nothing
			End If
			Return Me.gTstats(str)
		End Function

		Friend Function getTstatInsteonLinks(ByRef tstat As Collection) As ArrayList
			Dim arrayLists As ArrayList = Nothing
			arrayLists = If(Not tstat.Contains("InsteonLinks"), New ArrayList(), DirectCast(tstat("InsteonLinks"), ArrayList))
			Return arrayLists
		End Function

		Friend Function GetUniqueOrDefaultProgram(ByVal progName As String, ByVal tstatName As String) As Collection
			Dim collections As Collection
			Dim enumerator As IEnumerator = Nothing
			Dim flag As Boolean = False
			Dim flag1 As Boolean = False
			Dim collections1 As Collection = Nothing
			Try
				enumerator = Me.gPrograms.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = DirectCast(enumerator.Current, Collection)
					Dim objectValue As Object = RuntimeHelpers.GetObjectValue(current("Name"))
					Dim obj As Object = RuntimeHelpers.GetObjectValue(current("Thermostat"))
					If (Not Operators.ConditionalCompareObjectEqual(objectValue, progName, False)) Then
						Continue While
					End If
					If (Not Operators.ConditionalCompareObjectEqual(obj, tstatName, False)) Then
						If (Not Operators.ConditionalCompareObjectEqual(obj, "Default", False)) Then
							Continue While
						End If
						collections1 = current
					Else
						collections = current
						flag = True
					End If
					If (flag) Then
						Exit While
					End If
				End While
				If (Not flag) Then
					flag1 = True
				End If
			Finally
				If (TypeOf enumerator Is IDisposable) Then
					(TryCast(enumerator, IDisposable)).Dispose()
				End If
			End Try
			If (flag OrElse Not flag1) Then
				flag = False
				Return collections
			End If
			flag1 = False
			Return collections1
		End Function

		Friend Function GetUniqueProgramNames() As String()
			Dim enumerator As IEnumerator = Nothing
			Dim arrayLists As ArrayList = New ArrayList()
			Try
				enumerator = Me.gPrograms.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = DirectCast(enumerator.Current, Collection)
					Dim objectValue As Object = RuntimeHelpers.GetObjectValue(current("Name"))
					If (arrayLists.Contains(RuntimeHelpers.GetObjectValue(objectValue))) Then
						Continue While
					End If
					arrayLists.Add(RuntimeHelpers.GetObjectValue(objectValue))
				End While
			Finally
				If (TypeOf enumerator Is IDisposable) Then
					(TryCast(enumerator, IDisposable)).Dispose()
				End If
			End Try
			arrayLists.Sort()
			arrayLists.Insert(0, "None")
			Return DirectCast(arrayLists.ToArray(GetType(String)), String())
		End Function

		Friend Function GetUniqueProgramNames(ByVal forName As String) As String()
			Dim enumerator As IEnumerator = Nothing
			Dim arrayLists As ArrayList = New ArrayList()
			Try
				enumerator = Me.gPrograms.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = DirectCast(enumerator.Current, Collection)
					Dim objectValue As Object = RuntimeHelpers.GetObjectValue(current("Name"))
					Dim obj As Object = RuntimeHelpers.GetObjectValue(current("Thermostat"))
					If (Not Conversions.ToBoolean(Operators.AndObject(Operators.CompareObjectEqual(obj, "Default", False), Not arrayLists.Contains(RuntimeHelpers.GetObjectValue(objectValue))))) Then
						If (Not Conversions.ToBoolean(Operators.AndObject(Operators.AndObject(Not String.IsNullOrEmpty(forName), Operators.CompareObjectEqual(forName, obj, False)), Not arrayLists.Contains(RuntimeHelpers.GetObjectValue(objectValue))))) Then
							Continue While
						End If
						arrayLists.Add(RuntimeHelpers.GetObjectValue(objectValue))
					Else
						arrayLists.Add(RuntimeHelpers.GetObjectValue(objectValue))
					End If
				End While
			Finally
				If (TypeOf enumerator Is IDisposable) Then
					(TryCast(enumerator, IDisposable)).Dispose()
				End If
			End Try
			arrayLists.Sort()
			arrayLists.Insert(0, "None")
			Return DirectCast(arrayLists.ToArray(GetType(String)), String())
		End Function

		Public Sub LogConfig(Optional ByVal Log_Level As Integer = 64)
			HSPI_INSTEON_THERMOSTAT.utils.Log("---------------------------------------------------------------------", DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log("INI values from hspi_insteon_thermostat.ini", DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("INI Version = ", Me.Version), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log("---------------------------------------------------------------------", DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			stringBuilder.Append(" [Always] ")
			If ((Me.logLvl And 1) > 0) Then
				stringBuilder.Append("[Err] ")
			End If
			If ((Me.logLvl And 2) > 0) Then
				stringBuilder.Append("[Info] ")
			End If
			If ((Me.logLvl And 64) > 0) Then
				stringBuilder.Append("[Debug] ")
			End If
			If ((Me.logLvl And 128) > 0) Then
				stringBuilder.Append("[Test] ")
			End If
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("logLvl=", Conversions.ToString(Me.logLvl), stringBuilder.ToString()), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("logFileDebug=", Me.logFileDebug), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("InsteonFlags=", Me.InsteonFlags.ToString("X")), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Bounds Temp Low=", Conversions.ToString(Me.BoundsTempLow)), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Bounds Temp High=", Conversions.ToString(Me.BoundsTempHigh)), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Bounds Heat Set Low=", Conversions.ToString(Me.BoundsHeatSetLow)), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Bounds Heat Set High=", Conversions.ToString(Me.BoundsHeatSetHigh)), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Bounds Cool Set Low=", Conversions.ToString(Me.BoundsCoolSetLow)), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Bounds Cool Set High=", Conversions.ToString(Me.BoundsCoolSetHigh)), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Bounds Humidity Low=", Conversions.ToString(Me.BoundsHumidityLow)), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Bounds Humidity High=", Conversions.ToString(Me.BoundsHumidityHigh)), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Control Page Refresh=", Conversions.ToString(Me.ControlPageRefresh)), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			HSPI_INSTEON_THERMOSTAT.utils.Log("---------------------------------------------------------------------", DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
		End Sub

		Friend Sub LogConfigTPH(Optional ByVal Log_Level As Integer = 64)
			HSPI_INSTEON_THERMOSTAT.utils.Log("---------------------------------------------------------------------", DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			Me.LogThermostats(64)
			HSPI_INSTEON_THERMOSTAT.utils.Log("---------------------------------------------------------------------", DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			Me.LogPrograms(64)
			HSPI_INSTEON_THERMOSTAT.utils.Log("---------------------------------------------------------------------", DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			Me.LogHVACs(64)
			HSPI_INSTEON_THERMOSTAT.utils.Log("---------------------------------------------------------------------", DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
		End Sub

		Friend Sub LogHVACs(Optional ByVal Log_Level As Integer = 64)
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			HSPI_INSTEON_THERMOSTAT.utils.Log("HVACs [Name] [Location] [Maintenance Interval] [ModeRef] [FanRef] [CoolRef] [HeatRef] [MaintRef]", DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			Try
				enumerator = Me.gHVACs.Values.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = enumerator.Current
					Dim pEDString As String = Me.GetPEDString(current, "HSREF_MODE")
					Dim str As String = Me.GetPEDString(current, "HSREF_FAN")
					Dim pEDString1 As String = Me.GetPEDString(current, "HSREF_COOL")
					Dim str1 As String = Me.GetPEDString(current, "HSREF_HEAT")
					Dim pEDString2 As String = Me.GetPEDString(current, "HSREF_MAINT")
					Dim strArrays() As String = { "[", Me.GetPEDString(current, "Name"), "] [", Me.GetPEDString(current, "Location"), "] [", Me.GetPEDString(current, "MaintenanceInterval"), "] [", pEDString, "] [", str, "] [", pEDString1, "] [", str1, "] [", pEDString2, "]" }
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
		End Sub

		Friend Sub LogPrograms(Optional ByVal Log_Level As Integer = 64)
			Dim enumerator As IEnumerator = Nothing
			HSPI_INSTEON_THERMOSTAT.utils.Log("PROGRAMS [Name] [Thermostat] [Heat SP] [Cool SP] [Mode] [Fan]", DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			Try
				enumerator = Me.gPrograms.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = DirectCast(enumerator.Current, Collection)
					Dim pEDString() As String = { "[", Me.GetPEDString(current, "Name"), "] [", Me.GetPEDString(current, "Thermostat"), "] [", Me.GetPEDString(current, "Heat"), "] [", Me.GetPEDString(current, "Cool"), "] [", Me.GetPEDString(current, "Mode"), "] [", Me.GetPEDString(current, "Fan"), "]" }
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(pEDString), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
				End While
			Finally
				If (TypeOf enumerator Is IDisposable) Then
					(TryCast(enumerator, IDisposable)).Dispose()
				End If
			End Try
		End Sub

		Friend Sub LogThermostats(Optional ByVal Log_Level As Integer = 64)
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			HSPI_INSTEON_THERMOSTAT.utils.Log("THERMOSTATS [Name] [Locaction] [InsteonAddr] [Humidistat?] [ExtSensor?] [ForceProtocol=2?] [HVAC]", DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
			Try
				enumerator = Me.gTstats.Values.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = enumerator.Current
					HSPI_INSTEON_THERMOSTAT.utils.Log(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(String.Concat(String.Concat(String.Concat(String.Concat(String.Concat(String.Concat("[", Me.GetPEDString(current, "Name")), "] ["), Me.GetPEDString(current, "Location")), "] ["), Me.GetPEDString(current, "InsteonAddressLeft")), ":"), current("InsteonAddressMid")), ":"), current("InsteonAddressRight")), "] ["), Me.GetPEDString(current, "Humidistat")), "] ["), Me.GetPEDString(current, "ExtSensor")), "] ["), Me.GetPEDString(current, "InsteonProtocol2")), "] ["), Me.GetPEDString(current, "HVACUnit")), "]")), DirectCast(Log_Level, HSPI_INSTEON_THERMOSTAT.utils.LogLevel))
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
		End Sub

		Friend Function NameByNum(ByRef tph As SortedDictionary(Of String, Collection), ByVal iNum As Integer) As String
			Dim str As String
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim flag As Boolean = False
			Dim flag1 As Boolean = False
			Dim num As Integer = 1
			Try
				enumerator = tph.Values.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = enumerator.Current
					If (num <> iNum) Then
						num = num + 1
					Else
						str = Conversions.ToString(current("Name"))
						flag = True
					End If
					If (flag) Then
						Exit While
					End If
				End While
				If (Not flag) Then
					flag1 = True
				End If
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			If (flag OrElse Not flag1) Then
				flag = False
				Return str
			End If
			flag1 = False
			Return Nothing
		End Function

		Public Sub readINI()
			Me.Version = HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hspi_insteon_thermostat", "Version", "", "hspi_insteon_thermostat.ini")
			Me.logLvl = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hspi_insteon_thermostat", "LogLevel", Conversions.ToString(3), "hspi_insteon_thermostat.ini"))
			If ((Me.logLvl And 64) <= 0) Then
				Me._LogDebugInfoChecked = False
			Else
				Me._LogDebugInfoChecked = True
			End If
			If ((Me.logLvl And 128) <= 0) Then
				Me._LogTesting = False
			Else
				Me._LogTesting = True
			End If
			Me.logFileDebug = HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hspi_insteon_thermostat", "DebugLogFile", String.Concat(HSPI_INSTEON_THERMOSTAT.utils.hs.GetAppPath(), "/Logs/InsteonThermostatDebug.log"), "hspi_insteon_thermostat.ini")
			Me.InsteonFlags = CByte(Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hspi_insteon_thermostat", "InsteonFlags", Conversions.ToString(15), "hspi_insteon_thermostat.ini")))
			Me.BoundsTempLow = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hspi_insteon_thermostat", "BoundsTempLow", Conversions.ToString(30), "hspi_insteon_thermostat.ini"))
			Me.BoundsTempHigh = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hspi_insteon_thermostat", "BoundsTempHigh", Conversions.ToString(125), "hspi_insteon_thermostat.ini"))
			Me.BoundsHeatSetLow = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hspi_insteon_thermostat", "BoundsHeatSetLow", Conversions.ToString(30), "hspi_insteon_thermostat.ini"))
			Me.BoundsHeatSetHigh = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hspi_insteon_thermostat", "BoundsHeatSetHigh", Conversions.ToString(125), "hspi_insteon_thermostat.ini"))
			Me.BoundsCoolSetLow = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hspi_insteon_thermostat", "BoundsCoolSetLow", Conversions.ToString(30), "hspi_insteon_thermostat.ini"))
			Me.BoundsCoolSetHigh = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hspi_insteon_thermostat", "BoundsCoolSetHigh", Conversions.ToString(125), "hspi_insteon_thermostat.ini"))
			Me.BoundsHumidityLow = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hspi_insteon_thermostat", "BoundsHumidityLow", Conversions.ToString(5), "hspi_insteon_thermostat.ini"))
			Me.BoundsHumidityHigh = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hspi_insteon_thermostat", "BoundsHumidityHigh", Conversions.ToString(100), "hspi_insteon_thermostat.ini"))
			Me.ControlPageRefresh = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hspi_insteon_thermostat", "ControlPageRefresh", Conversions.ToString(20), "hspi_insteon_thermostat.ini"))
			Dim num As Integer = 1
			Dim nISetting As String = HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("thermostats", String.Concat(Conversions.ToString(num), "_Name"), Nothing, "hspi_insteon_thermostat.ini")
			While Operators.CompareString(nISetting, Nothing, False) <> 0
				Dim collections As Collection = New Collection()
				collections.Add(nISetting, "Name", Nothing, Nothing)
				collections.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("thermostats", String.Concat(Conversions.ToString(num), "_Location"), "", "hspi_insteon_thermostat.ini"), "Location", Nothing, Nothing)
				collections.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("thermostats", String.Concat(Conversions.ToString(num), "_InsteonAddressLeft"), "", "hspi_insteon_thermostat.ini"), "InsteonAddressLeft", Nothing, Nothing)
				collections.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("thermostats", String.Concat(Conversions.ToString(num), "_InsteonAddressMid"), "", "hspi_insteon_thermostat.ini"), "InsteonAddressMid", Nothing, Nothing)
				collections.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("thermostats", String.Concat(Conversions.ToString(num), "_InsteonAddressRight"), "", "hspi_insteon_thermostat.ini"), "InsteonAddressRight", Nothing, Nothing)
				collections.Add(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(collections("InsteonAddressLeft"), "."), collections("InsteonAddressMid")), "."), collections("InsteonAddressRight")), "InsteonAddress", Nothing, Nothing)
				collections.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("thermostats", String.Concat(Conversions.ToString(num), "_Registered"), Conversions.ToString(False), "hspi_insteon_thermostat.ini"), "Registered", Nothing, Nothing)
				collections.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("thermostats", String.Concat(Conversions.ToString(num), "_Humidistat"), Conversions.ToString(False), "hspi_insteon_thermostat.ini"), "Humidistat", Nothing, Nothing)
				collections.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("thermostats", String.Concat(Conversions.ToString(num), "_ExtSensor"), Conversions.ToString(False), "hspi_insteon_thermostat.ini"), "ExtSensor", Nothing, Nothing)
				collections.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("thermostats", String.Concat(Conversions.ToString(num), "_InsteonProtocol2"), Conversions.ToString(False), "hspi_insteon_thermostat.ini"), "InsteonProtocol2", Nothing, Nothing)
				collections.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("thermostats", String.Concat(Conversions.ToString(num), "_HVACUnit"), "None", "hspi_insteon_thermostat.ini"), "HVACUnit", Nothing, Nothing)
				Me.AddTstat(collections)
				num = num + 1
				nISetting = HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("thermostats", String.Concat(Conversions.ToString(num), "_Name"), Nothing, "hspi_insteon_thermostat.ini")
			End While
			num = 1
			Dim str As String = HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("programs", String.Concat(Conversions.ToString(num), "_Name"), Nothing, "hspi_insteon_thermostat.ini")
			While Operators.CompareString(str, Nothing, False) <> 0
				Dim collections1 As Collection = New Collection()
				collections1.Add(str, "Name", Nothing, Nothing)
				collections1.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("programs", String.Concat(Conversions.ToString(num), "_Thermostat"), "Default", "hspi_insteon_thermostat.ini"), "Thermostat", Nothing, Nothing)
				collections1.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("programs", String.Concat(Conversions.ToString(num), "_Heat"), "", "hspi_insteon_thermostat.ini"), "Heat", Nothing, Nothing)
				collections1.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("programs", String.Concat(Conversions.ToString(num), "_Cool"), "", "hspi_insteon_thermostat.ini"), "Cool", Nothing, Nothing)
				collections1.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("programs", String.Concat(Conversions.ToString(num), "_Mode"), "", "hspi_insteon_thermostat.ini"), "Mode", Nothing, Nothing)
				collections1.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("programs", String.Concat(Conversions.ToString(num), "_Fan"), "", "hspi_insteon_thermostat.ini"), "Fan", Nothing, Nothing)
				Me.AddProgram(collections1)
				num = num + 1
				str = HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("programs", String.Concat(Conversions.ToString(num), "_Name"), Nothing, "hspi_insteon_thermostat.ini")
			End While
			num = 1
			Dim nISetting1 As String = HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hvac_units", String.Concat(Conversions.ToString(num), "_Name"), Nothing, "hspi_insteon_thermostat.ini")
			While Operators.CompareString(nISetting1, Nothing, False) <> 0
				Dim collections2 As Collection = New Collection()
				collections2.Add(nISetting1, "Name", Nothing, Nothing)
				collections2.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hvac_units", String.Concat(Conversions.ToString(num), "_Location"), "", "hspi_insteon_thermostat.ini"), "Location", Nothing, Nothing)
				collections2.Add(HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hvac_units", String.Concat(Conversions.ToString(num), "_MaintenanceInterval"), "", "hspi_insteon_thermostat.ini"), "MaintenanceInterval", Nothing, Nothing)
				Me.AddHvac(collections2)
				num = num + 1
				nISetting1 = HSPI_INSTEON_THERMOSTAT.utils.hs.GetINISetting("hvac_units", String.Concat(Conversions.ToString(num), "_Name"), Nothing, "hspi_insteon_thermostat.ini")
			End While
		End Sub

		Friend Sub RemoveFromTPH(ByRef tph As Collection, ByVal command As String)
			If (tph.Contains(command)) Then
				tph.Remove(command)
			End If
		End Sub

		Friend Sub RemoveHvac(ByVal hName As String)
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			If (Not String.IsNullOrEmpty(hName)) Then
				Me.dHVACs.Add(hName, Me.gHVACs(hName))
				Me.gHVACs.Remove(hName)
				Try
					enumerator = Me.gTstats.Values.GetEnumerator()
					While enumerator.MoveNext()
						Dim current As Collection = enumerator.Current
						If (Not Operators.ConditionalCompareObjectEqual(current("HVACUnit"), hName, False)) Then
							Continue While
						End If
						current.Remove("HVACUnit")
						current.Add("None", "HVACUnit", Nothing, Nothing)
					End While
				Finally
					(DirectCast(enumerator, IDisposable)).Dispose()
				End Try
			End If
		End Sub

		Friend Sub RemoveHvacByNum(ByVal hNum As Integer)
			Me.RemoveHvac(Me.NameByNum(Me.gHVACs, hNum))
		End Sub

		Private Sub RemoveHvacDevices(ByVal hName As String)
			If (Not String.IsNullOrEmpty(hName) AndAlso Me.dHVACs.ContainsKey(hName)) Then
				Dim item As Collection = Me.dHVACs(hName)
				Dim strArrays() As String = { "HSREF_MODE", "HSREF_FAN", "HSREF_COOL", "HSREF_HEAT", "HSREF_MAINT" }
				Dim strArrays1 As String() = strArrays
				For i As Integer = 0 To CInt(strArrays1.Length)
					Dim str As String = strArrays1(i)
					Try
						Dim [integer] As Integer = Conversions.ToInteger(item(str))
						Me.DeleteHSDevice([integer])
					Catch exception As System.Exception
						ProjectData.SetProjectError(exception)
						ProjectData.ClearProjectError()
					End Try
				Next

			End If
		End Sub

		Friend Sub RemoveProgramByNum(ByVal pNum As Integer)
			Me.gPrograms.RemoveAt(pNum)
		End Sub

		Friend Sub RemoveTstat(ByVal tName As String)
			Dim enumerator As IEnumerator = Nothing
			If (Not String.IsNullOrEmpty(tName)) Then
				Me.dTstats.Add(tName, Me.gTstats(tName))
				Me.gTstats.Remove(tName)
				Try
					enumerator = Me.gPrograms.GetEnumerator()
					While enumerator.MoveNext()
						Dim current As Collection = DirectCast(enumerator.Current, Collection)
						If (Not Operators.ConditionalCompareObjectEqual(current("Thermostat"), tName, False)) Then
							Continue While
						End If
						current.Remove("Thermostat")
						current.Add("Default", "Thermostat", Nothing, Nothing)
					End While
				Finally
					If (TypeOf enumerator Is IDisposable) Then
						(TryCast(enumerator, IDisposable)).Dispose()
					End If
				End Try
			End If
		End Sub

		Friend Sub RemoveTstatByNum(ByVal tNum As Integer)
			Me.RemoveTstat(Me.NameByNum(Me.gTstats, tNum))
		End Sub

		Private Sub RemoveTstatDevices(ByVal tName As String)
			If (Not String.IsNullOrEmpty(tName) AndAlso Me.dTstats.ContainsKey(tName)) Then
				Dim item As Collection = Me.dTstats(tName)
				Dim strArrays() As String = { "HSREF_MODE", "HSREF_FAN", "HSREF_COOL", "HSREF_HEAT", "HSREF_TEMP", "HSREF_PROGRAM", "HSREF_HOLD", "HSREF_EXTTEMP", "HSREF_HUMIDITY" }
				Dim strArrays1 As String() = strArrays
				For i As Integer = 0 To CInt(strArrays1.Length)
					Dim str As String = strArrays1(i)
					Try
						Dim [integer] As Integer = Conversions.ToInteger(item(str))
						Me.DeleteHSDevice([integer])
					Catch exception As System.Exception
						ProjectData.SetProjectError(exception)
						ProjectData.ClearProjectError()
					End Try
				Next

			End If
		End Sub

		Private Sub SetLogLvl()
			Me.logLvl = 3
			If (Me.LogDebugInfoChecked) Then
				Me.logLvl = Me.logLvl + 64
			End If
			If (Me._LogTesting) Then
				Me.logLvl = Me.logLvl + 128
			End If
		End Sub

		Friend Sub UpdateHvac(ByVal hName As String, ByVal command As String, ByVal value As String)
			If (Not String.IsNullOrEmpty(hName)) Then
				Dim item As Collection = Me.gHVACs(hName)
				Me.RemoveFromTPH(item, command)
				item.Add(value, command, Nothing, Nothing)
			End If
		End Sub

		Friend Sub UpdateHvacByNum(ByVal hNum As Integer, ByVal command As String, ByVal value As String)
			Me.UpdateHvac(Me.NameByNum(Me.gHVACs, hNum), command, value)
		End Sub

		Friend Sub UpdateProgramByNum(ByVal pNum As Integer, ByVal command As String, ByVal value As String)
			Dim item As Collection = DirectCast(Me.gPrograms(pNum), Collection)
			Me.RemoveFromTPH(item, command)
			item.Add(value, command, Nothing, Nothing)
		End Sub

		Friend Sub UpdateTstatAddInsteonLink(ByRef tstat As Collection, ByVal link As String)
			Dim arrayLists As ArrayList = Nothing
			If (Not tstat.Contains("InsteonLinks")) Then
				arrayLists = New ArrayList()
			Else
				arrayLists = DirectCast(tstat("InsteonLinks"), ArrayList)
				tstat.Remove("InsteonLinks")
			End If
			arrayLists.Add(link)
			tstat.Add(arrayLists, "InsteonLinks", Nothing, Nothing)
		End Sub

		Friend Sub UpdateTstatByNum(ByVal tNum As Integer, ByVal command As String, ByVal value As String)
			Me.UpdateTstatStringByName(Me.NameByNum(Me.gTstats, tNum), command, value)
		End Sub

		Friend Sub UpdateTstatDeleteInsteonLinks(ByRef tstat As Collection)
			Me.RemoveFromTPH(tstat, "InsteonLinks")
			tstat.Add(New ArrayList(), "InsteonLinks", Nothing, Nothing)
		End Sub

		Friend Sub UpdateTstatShort(ByRef tstat As Collection, ByVal command As String, ByVal value As Short)
			Me.RemoveFromTPH(tstat, command)
			tstat.Add(value, command, Nothing, Nothing)
		End Sub

		Friend Sub UpdateTstatString(ByRef tstat As Collection, ByVal command As String, ByVal value As String)
			Dim flag As Boolean = False
			Dim flag1 As Boolean = False
			Dim str As String = command
			If (Operators.CompareString(str, "InsteonAddressLeft", False) <> 0) Then
				If (Operators.CompareString(str, "InsteonAddressMid", False) <> 0) Then
					flag = Operators.CompareString(str, "InsteonAddressRight", False) = 0
					If (Not flag) Then
						Me.RemoveFromTPH(tstat, command)
						tstat.Add(value, command, Nothing, Nothing)
						flag1 = True
					End If
				End If
			End If
			If (flag OrElse Not flag1) Then
				flag = False
				If (Not String.IsNullOrEmpty(value)) Then
					value = value.ToUpper()
				End If
				Me.RemoveFromTPH(tstat, command)
				tstat.Add(value, command, Nothing, Nothing)
				Me.RemoveFromTPH(tstat, "InsteonAddress")
				tstat.Add(Operators.AddObject(Operators.AddObject(Operators.AddObject(Operators.AddObject(tstat("InsteonAddressLeft"), "."), tstat("InsteonAddressMid")), "."), tstat("InsteonAddressRight")), "InsteonAddress", Nothing, Nothing)
			End If
			flag1 = False
		End Sub

		Friend Sub UpdateTstatStringByName(ByVal tName As String, ByVal command As String, ByVal value As String)
			If (Not String.IsNullOrEmpty(tName)) Then
				Dim item As Collection = Me.gTstats(tName)
				Me.UpdateTstatString(item, command, value)
			End If
		End Sub

		Public Sub writeINI()
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim enumerator1 As IEnumerator = Nothing
			Dim enumerator2 As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			HSPI_INSTEON_THERMOSTAT.utils.hs.ClearINISection("hspi_insteon_thermostat", "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.ClearINISection("thermostats", "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.ClearINISection("programs", "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.ClearINISection("hvac_units", "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hspi_insteon_thermostat", "Version", "3.0.0.9", "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hspi_insteon_thermostat", "LogLevel", Conversions.ToString(Me.logLvl), "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hspi_insteon_thermostat", "DebugLogFile", Me.logFileDebug, "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hspi_insteon_thermostat", "InsteonFlags", Conversions.ToString(Me.InsteonFlags), "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hspi_insteon_thermostat", "BoundsTempLow", Conversions.ToString(Me.BoundsTempLow), "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hspi_insteon_thermostat", "BoundsTempHigh", Conversions.ToString(Me.BoundsTempHigh), "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hspi_insteon_thermostat", "BoundsHeatSetLow", Conversions.ToString(Me.BoundsHeatSetLow), "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hspi_insteon_thermostat", "BoundsHeatSetHigh", Conversions.ToString(Me.BoundsHeatSetHigh), "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hspi_insteon_thermostat", "BoundsCoolSetLow", Conversions.ToString(Me.BoundsCoolSetLow), "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hspi_insteon_thermostat", "BoundsCoolSetHigh", Conversions.ToString(Me.BoundsCoolSetHigh), "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hspi_insteon_thermostat", "BoundsHumidityLow", Conversions.ToString(Me.BoundsHumidityLow), "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hspi_insteon_thermostat", "BoundsHumidityHigh", Conversions.ToString(Me.BoundsHumidityHigh), "hspi_insteon_thermostat.ini")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hspi_insteon_thermostat", "ControlPageRefresh", Conversions.ToString(Me.ControlPageRefresh), "hspi_insteon_thermostat.ini")
			Dim num As Integer = 1
			Try
				enumerator = Me.gTstats.Values.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = enumerator.Current
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("thermostats", String.Concat(Conversions.ToString(num), "_Name"), Conversions.ToString(current("Name")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("thermostats", String.Concat(Conversions.ToString(num), "_Location"), Conversions.ToString(current("Location")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("thermostats", String.Concat(Conversions.ToString(num), "_InsteonAddressLeft"), Conversions.ToString(current("InsteonAddressLeft")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("thermostats", String.Concat(Conversions.ToString(num), "_InsteonAddressMid"), Conversions.ToString(current("InsteonAddressMid")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("thermostats", String.Concat(Conversions.ToString(num), "_InsteonAddressRight"), Conversions.ToString(current("InsteonAddressRight")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("thermostats", String.Concat(Conversions.ToString(num), "_Humidistat"), Conversions.ToString(current("Humidistat")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("thermostats", String.Concat(Conversions.ToString(num), "_ExtSensor"), Conversions.ToString(current("ExtSensor")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("thermostats", String.Concat(Conversions.ToString(num), "_InsteonProtocol2"), Conversions.ToString(current("InsteonProtocol2")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("thermostats", String.Concat(Conversions.ToString(num), "_HVACUnit"), Conversions.ToString(current("HVACUnit")), "hspi_insteon_thermostat.ini")
					num = num + 1
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			num = 1
			Try
				enumerator1 = Me.gPrograms.GetEnumerator()
				While enumerator1.MoveNext()
					Dim collections As Collection = DirectCast(enumerator1.Current, Collection)
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("programs", String.Concat(Conversions.ToString(num), "_Name"), Conversions.ToString(collections("Name")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("programs", String.Concat(Conversions.ToString(num), "_Thermostat"), Conversions.ToString(collections("Thermostat")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("programs", String.Concat(Conversions.ToString(num), "_Heat"), Conversions.ToString(collections("Heat")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("programs", String.Concat(Conversions.ToString(num), "_Cool"), Conversions.ToString(collections("Cool")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("programs", String.Concat(Conversions.ToString(num), "_Mode"), Conversions.ToString(collections("Mode")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("programs", String.Concat(Conversions.ToString(num), "_Fan"), Conversions.ToString(collections("Fan")), "hspi_insteon_thermostat.ini")
					num = num + 1
				End While
			Finally
				If (TypeOf enumerator1 Is IDisposable) Then
					(TryCast(enumerator1, IDisposable)).Dispose()
				End If
			End Try
			num = 1
			Try
				enumerator2 = Me.gHVACs.Values.GetEnumerator()
				While enumerator2.MoveNext()
					Dim current1 As Collection = enumerator2.Current
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hvac_units", String.Concat(Conversions.ToString(num), "_Name"), Conversions.ToString(current1("Name")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hvac_units", String.Concat(Conversions.ToString(num), "_Location"), Conversions.ToString(current1("Location")), "hspi_insteon_thermostat.ini")
					HSPI_INSTEON_THERMOSTAT.utils.hs.SaveINISetting("hvac_units", String.Concat(Conversions.ToString(num), "_MaintenanceInterval"), Conversions.ToString(current1("MaintenanceInterval")), "hspi_insteon_thermostat.ini")
					num = num + 1
				End While
			Finally
				(DirectCast(enumerator2, IDisposable)).Dispose()
			End Try
		End Sub
	End Class
End Namespace