Imports HomeSeerAPI
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices

Namespace HSPI_INSTEON_THERMOSTAT
	Public Class Insteon
		Public plmAddress As String

		Public insteonPI As PluginAccess

		Public Sub New()
			MyBase.New()
			Me.plmAddress = Nothing
			Me.insteonPI = Nothing
		End Sub

		Friend Sub clearLinks(ByRef tstat As Collection)
			Me.RegisterTstats()
			Dim objectValue As Object = RuntimeHelpers.GetObjectValue(tstat("Name"))
			Me.plmAddress.Split(New Char() { "."C })
			Dim num As Byte = 255
			Dim num1 As Integer = 0
			Do
				Dim numArray() As Byte = { 0, 2, 15, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
				Dim numArray1 As Byte() = numArray
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("<<ADDR:", num.ToString("x"), "=CLEAR>>"), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Test)
				numArray1(3) = num
				numArray1(4) = 8
				numArray1(5) = 0
				numArray1(6) = 0
				numArray1(7) = 0
				numArray1(8) = 0
				numArray1(9) = 0
				numArray1(10) = 0
				numArray1(11) = 0
				numArray1(12) = 0
				Me.TransmitInsteonExtended(Conversions.ToString(objectValue), 47, 0, numArray1, True)
				num = CByte((num - 8))
				num1 = num1 + 1
			Loop While num1 <= 5
		End Sub

		Friend Function ConnectToInsteonPlugin() As Boolean
			' 
			' Current member / type: System.Boolean HSPI_INSTEON_THERMOSTAT.Insteon::ConnectToInsteonPlugin()
			' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
			' 
			' Product version: 2014.1.225.0
			' Exception in: System.Boolean ConnectToInsteonPlugin()
			' 
			' The unary opperator AddressReference is not supported in VisualBasic
			'    at ..( ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\DetermineNotSupportedVBCodeStep.cs:line 22
			'    at ..(MethodBody , ILanguage ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 83
			'    at ..( , ILanguage , MethodBody , & ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 99
			'    at ..(MethodBody , ILanguage , & ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 62
			'    at ..(ILanguage , MethodDefinition ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 116
			' 
			' mailto: JustDecompilePublicFeedback@telerik.com

		End Function

		Friend Sub GetProtocol(ByVal tName As String)
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Request protocol and devcat for ", tName), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Test)
			Dim flag As Boolean = False
			Me.TransmitInsteon(tName, 13, 0, flag)
			flag = False
			Me.TransmitInsteon(tName, 16, 0, flag)
		End Sub

		Public Sub InsteonRcv(ByVal InsteonData As String)
			' 
			' Current member / type: System.Void HSPI_INSTEON_THERMOSTAT.Insteon::InsteonRcv(System.String)
			' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
			' 
			' Product version: 2014.1.225.0
			' Exception in: System.Void InsteonRcv(System.String)
			' 
			' Unsupported target statement for goto jump.
			'    at ..( ,  , String ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\GotoElimination\TotalGotoEliminationStep.cs:line 652
			'    at ..( ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\GotoElimination\TotalGotoEliminationStep.cs:line 351
			'    at ..() in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\GotoElimination\TotalGotoEliminationStep.cs:line 125
			'    at ..( ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\GotoElimination\TotalGotoEliminationStep.cs:line 49
			'    at ..(MethodBody , ILanguage ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 83
			'    at ..( , ILanguage , MethodBody , & ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 99
			'    at ..(MethodBody , ILanguage , & ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 62
			'    at ..(ILanguage , MethodDefinition ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 116
			' 
			' mailto: JustDecompilePublicFeedback@telerik.com

		End Sub

		Private Sub InsteonRcvSetModeFan(ByVal tstat As Collection, ByVal curMode As Integer, ByVal newMode As Integer, ByVal curFan As Integer, ByVal newFan As Integer, ByVal logIntro As String)
			Dim flag As Boolean = False
			If (newMode <> curMode) Then
				flag = True
				HSPI_INSTEON_THERMOSTAT.utils.myTstat.SetTstatHvacPedByTstatHvac(tstat, "HSREF_MODE", "Mode", newMode, Nothing)
			End If
			If (newFan <> curFan) Then
				flag = True
				HSPI_INSTEON_THERMOSTAT.utils.myTstat.SetTstatHvacPedByTstatHvac(tstat, "HSREF_FAN", "Fan", newFan, Nothing)
			End If
			If (flag) Then
				Dim strArrays() As String = { logIntro, " Mode=", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(newMode), " Fan=", HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(newFan) }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
			End If
		End Sub

		Friend Function isTstatRegistered(ByRef tstat As Collection) As Boolean
			Dim flag As Boolean = False
			Try
				flag = Conversions.ToBoolean(tstat("Registered"))
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				tstat.Add(False, "Registered", Nothing, Nothing)
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Friend Sub ReadLinks(ByRef tstat As Collection)
			Me.RegisterTstats()
			Dim str As String = Conversions.ToString(tstat("Name"))
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Reading links from theremostat: ", str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
			Dim numArray() As Byte = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
			Dim numArray1 As Byte() = numArray
			Me.TransmitInsteonExtended(str, 47, 0, numArray1, False)
		End Sub

		Friend Sub RegisterTstats()
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Try
				enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats.Values.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As Collection = enumerator.Current
					If (Me.isTstatRegistered(current)) Then
						Continue While
					End If
					Me.UnregisterTstat(current)
					Dim str As String = Conversions.ToString(current("Name"))
					Dim str1 As String = Conversions.ToString(current("InsteonAddress"))
					Dim numArray() As Integer = { 239, 1, 2, 3, 4 }
					Dim numArray1 As Integer() = numArray
					Dim pluginAccess As HomeSeerAPI.PluginAccess = Me.insteonPI
					Dim objArray() As Object = { str, str1, numArray1 }
					If (Not Conversions.ToBoolean(pluginAccess.PluginFunction("ExtDev_RegisterExternalDeviceSupport2", objArray))) Then
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Error registering Insteon Thermostat with address: ", str1), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
					Else
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Registered Insteon Thermostat ", str, " with address: ", str1), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						current.Remove("Registered")
						current.Add(True, "Registered", Nothing, Nothing)
						Me.GetProtocol(str)
					End If
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
		End Sub

		Friend Sub RemoveFromPLM(ByVal tstat As Collection)
			Dim str As String = Nothing
			Dim str1 As String = Nothing
			Try
				str = Conversions.ToString(tstat("Name"))
				str1 = Conversions.ToString(tstat("InsteonAddress"))
				Dim pluginAccess As HomeSeerAPI.PluginAccess = Me.insteonPI
				Dim objArray() As Object = { str, str1 }
				pluginAccess.PluginFunction("ExtDev_DeleteInterfaceLinksExternalDeviceSupport", objArray)
				HSPI_INSTEON_THERMOSTAT.utils.myWaitSecs(1)
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem removing links from PLM for Insteon Thermostat ", str, " with address: ", str1), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Friend Sub SendPollRequests(ByVal tstat As Collection, Optional ByVal pollMode As Boolean = True, Optional ByVal pollTemp As Boolean = True, Optional ByVal pollSet As Boolean = True, Optional ByVal pollHum As Boolean = True)
			Dim flag As Boolean
			Try
				Dim str As String = Conversions.ToString(tstat("Name"))
				Dim flag1 As Boolean = Conversions.ToBoolean(tstat("Humidistat"))
				If (Not HSPI_INSTEON_THERMOSTAT.utils.isSmarthomeWirelessDEVCAT(tstat)) Then
					If (pollMode) Then
						flag = False
						Me.TransmitInsteon(str, 107, 2, flag)
						HSPI_INSTEON_THERMOSTAT.utils.myWaitSecs(2)
					End If
					If (pollHum And flag1) Then
						flag = False
						Me.TransmitInsteon(str, 106, 96, flag)
						HSPI_INSTEON_THERMOSTAT.utils.myWaitSecs(2)
					End If
					If (pollTemp) Then
						flag = False
						Me.TransmitInsteon(str, 107, 3, flag)
						HSPI_INSTEON_THERMOSTAT.utils.myWaitSecs(2)
					End If
					If (pollSet) Then
						flag = False
						Me.TransmitInsteon(str, 106, 32, flag)
						HSPI_INSTEON_THERMOSTAT.utils.myWaitSecs(2)
					End If
				Else
					Dim numArray() As Byte = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
					Dim numArray1 As Byte() = numArray
					Me.TransmitInsteonExtended(str, 46, 0, numArray1, False)
					HSPI_INSTEON_THERMOSTAT.utils.myWaitSecs(3)
					numArray1 = New Byte() { 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
					numArray = numArray1
					Me.TransmitInsteonExtended(str, 46, 0, numArray, False)
					HSPI_INSTEON_THERMOSTAT.utils.myWaitSecs(3)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Error in SendPollRequests", exception.ToString()), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Friend Sub TransmitInsteon(ByVal mName As String, ByVal cmd1 As Byte, ByVal cmd2 As Byte, Optional ByRef asExtended As Boolean = False)
			Me.RegisterTstats()
			If (Not asExtended) Then
				Dim strArrays() As String = { "TransmitInsteon: [", mName, "] [", HSPI_INSTEON_THERMOSTAT.utils.myConfig.InsteonFlags.ToString("X2"), "] [", cmd1.ToString("X2"), "] [", cmd2.ToString("X2"), "]" }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				Dim pluginAccess As HomeSeerAPI.PluginAccess = Me.insteonPI
				Dim objArray() As Object = { mName, HSPI_INSTEON_THERMOSTAT.utils.myConfig.InsteonFlags, cmd1, cmd2 }
				pluginAccess.PluginFunction("ExtDev_TransmitToExternalDevice", objArray)
			Else
				Dim numArray() As Byte = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
				Dim numArray1 As Byte() = numArray
				Me.TransmitInsteonExtended(mName, cmd1, cmd2, numArray1, True)
			End If
		End Sub

		Friend Sub TransmitInsteonExtended(ByVal mName As String, ByVal cmd1 As Byte, ByVal cmd2 As Byte, ByRef Data As Byte(), ByVal doChksum As Boolean)
			Me.RegisterTstats()
			If (doChksum) Then
				Dim num As Short = cmd1
				num = CShort((num + cmd2))
				Dim length As Integer = CInt(Data.Length) - 1
				Dim num1 As Integer = 0
				Do
					Try
						num = CShort((num + Data(num1)))
					Catch overflowException As System.OverflowException
						ProjectData.SetProjectError(overflowException)
						ProjectData.ClearProjectError()
					End Try
					num1 = num1 + 1
				Loop While num1 <= length
				If (num <> 256) Then
					Data(13) = CByte(((Not num And 255) + 1))
				Else
					Data(13) = 0
				End If
			End If
			Dim strArrays() As String = { "TransmitInsteon: [", mName, "] [", 31.ToString("X2"), "] [", cmd1.ToString("X2"), "] [", cmd2.ToString("X2"), "] (((", HSPI_INSTEON_THERMOSTAT.utils.AsHexString(Data), ")))" }
			HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
			Dim pluginAccess As HomeSeerAPI.PluginAccess = Me.insteonPI
			Dim objArray() As Object = { mName, CByte(31), cmd1, cmd2, Data }
			pluginAccess.PluginFunction("ExtDev_TransmitToExternalDevice2", objArray)
		End Sub

		Friend Sub UnregisterTstat(ByVal tstat As Collection)
			Dim str As String = Nothing
			Dim str1 As String = Nothing
			Try
				str = Conversions.ToString(tstat("Name"))
				str1 = Conversions.ToString(tstat("InsteonAddress"))
				Dim pluginAccess As HomeSeerAPI.PluginAccess = Me.insteonPI
				Dim objArray() As Object = { str, str1 }
				pluginAccess.PluginFunction("ExtDev_UnregisterExternalDeviceSupport", objArray)
				HSPI_INSTEON_THERMOSTAT.utils.myWaitSecs(1)
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Problem unregistering Insteon Thermostat ", str, " with address: ", str1), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Friend Sub UpdateTstatCoolSetPoint(ByVal tName As String, ByVal cool As Short)
			Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
			Dim [integer] As Integer = Conversions.ToInteger(item("InsteonProtocol"))
			If (Conversions.ToBoolean(item("InsteonProtocol2"))) Then
				[integer] = 2
			End If
			Dim flag As Boolean = [integer] = 2
			Me.TransmitInsteon(tName, 108, CByte((cool * 2)), flag)
		End Sub

		Friend Sub UpdateTstatFan(ByVal tName As String, ByVal fan As Integer)
			Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
			Dim [integer] As Integer = Conversions.ToInteger(item("InsteonProtocol"))
			If (Conversions.ToBoolean(item("InsteonProtocol2"))) Then
				[integer] = 2
			End If
			Dim flag As Boolean = [integer] = 2
			Select Case fan
				Case 0
					Me.TransmitInsteon(tName, 107, 8, flag)
					Exit Select
				Case 1
					Me.TransmitInsteon(tName, 107, 7, flag)
					Exit Select
			End Select
		End Sub

		Friend Sub UpdateTstatHeatSetPoint(ByVal tName As String, ByVal heat As Short)
			Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
			Dim [integer] As Integer = Conversions.ToInteger(item("InsteonProtocol"))
			If (Conversions.ToBoolean(item("InsteonProtocol2"))) Then
				[integer] = 2
			End If
			Dim flag As Boolean = [integer] = 2
			Me.TransmitInsteon(tName, 109, CByte((heat * 2)), flag)
		End Sub

		Friend Sub UpdateTstatMode(ByVal tName As String, ByVal mode As Integer)
			Dim flag As Boolean = False
			Try
				Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
				Dim [integer] As Integer = Conversions.ToInteger(item("InsteonProtocol"))
				If (Conversions.ToBoolean(item("InsteonProtocol2"))) Then
					[integer] = 2
				End If
				Dim flag1 As Boolean = [integer] = 2
				Select Case mode
					Case 0
						Me.TransmitInsteon(tName, 107, 9, flag1)
						flag = True
						If (flag) Then
							Exit Select
						End If
					Case 1
						Me.TransmitInsteon(tName, 107, 4, flag1)
						flag = True
						If (flag) Then
							Exit Select
						End If
					Case 2
						Me.TransmitInsteon(tName, 107, 5, flag1)
						flag = True
						If (flag) Then
							Exit Select
						End If
					Case 3
						Me.TransmitInsteon(tName, 107, 6, flag1)
						flag = True
						If (flag) Then
							Exit Select
						End If
				End Select
				If (Not flag) Then
					If (Not flag) Then
						If (Not flag) Then
							If (Not flag) Then
								If (Not HSPI_INSTEON_THERMOSTAT.utils.isSmarthomeDEVCAT(item)) Then
									Select Case mode
										Case 5
											Me.TransmitInsteon(tName, 107, 12, flag1)
											Exit Select
										Case 6
											Me.TransmitInsteon(tName, 107, 10, flag1)
											Exit Select
										Case 7
											Me.TransmitInsteon(tName, 107, 11, flag1)
											Exit Select
									End Select
								ElseIf (mode = 5) Then
									Me.TransmitInsteon(tName, 107, 10, flag1)
								End If
							End If
						End If
					End If
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(tName, " : Problem setting thermostat mode"), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			flag = False
		End Sub

		Friend Sub UpdateTstatTempDown(ByVal tName As String)
			Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
			Dim [integer] As Integer = Conversions.ToInteger(item("InsteonProtocol"))
			If (Conversions.ToBoolean(item("InsteonProtocol2"))) Then
				[integer] = 2
			End If
			Dim flag As Boolean = [integer] = 2
			Me.TransmitInsteon(tName, 105, 2, flag)
		End Sub

		Friend Sub UpdateTstatTempUp(ByVal tName As String)
			Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
			Dim [integer] As Integer = Conversions.ToInteger(item("InsteonProtocol"))
			If (Conversions.ToBoolean(item("InsteonProtocol2"))) Then
				[integer] = 2
			End If
			Dim flag As Boolean = [integer] = 2
			Me.TransmitInsteon(tName, 104, 2, flag)
		End Sub

		Friend Sub writeLinks(ByRef tstat As Collection)
			Dim numArray As Byte()
			Me.RegisterTstats()
			Dim str As String = Nothing
			Try
				str = Conversions.ToString(tstat("Name"))
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str, " : Writing links to theremostat"), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				Dim count As Integer = HSPI_INSTEON_THERMOSTAT.utils.myConfig.getTstatInsteonLinks(tstat).Count
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str, " : Existing Link Count: ", Conversions.ToString(count)), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Test)
				HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatDeleteInsteonLinks(tstat)
				HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatAddInsteonLink(tstat, "E2 EF 00 00 00 00 00 00")
				HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatAddInsteonLink(tstat, "E2 01 00 00 00 00 00 00")
				HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatAddInsteonLink(tstat, "E2 02 00 00 00 00 00 00")
				HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatAddInsteonLink(tstat, "E2 03 00 00 00 00 00 00")
				HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatAddInsteonLink(tstat, "E2 04 00 00 00 00 00 00")
				HSPI_INSTEON_THERMOSTAT.utils.myConfig.UpdateTstatAddInsteonLink(tstat, "A2 EF 00 00 00 00 00 00")
				Dim tstatInsteonLinks As ArrayList = HSPI_INSTEON_THERMOSTAT.utils.myConfig.getTstatInsteonLinks(tstat)
				count = tstatInsteonLinks.Count
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str, " : New Link Count: ", Conversions.ToString(count)), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Test)
				Dim str1 As String = Me.plmAddress
				Dim chrArray() As Char = { "."C }
				Dim strArrays As String() = str1.Split(chrArray)
				Dim num As Byte = 255
				Dim num1 As Integer = count - 1
				Dim num2 As Integer = 0
				Do
					numArray = New Byte() { 0, 2, 15, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
					Dim numArray1 As Byte() = numArray
					If (num2 >= count) Then
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("<<ADDR:", num.ToString("x"), "=CLEAR>>"), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Test)
						numArray1(3) = num
						numArray1(4) = 8
						numArray1(5) = 0
						numArray1(6) = 0
						numArray1(7) = 0
						numArray1(8) = 0
						numArray1(9) = 0
						numArray1(10) = 0
						numArray1(11) = 0
						numArray1(12) = 0
					Else
						Dim str2 As String = Conversions.ToString(tstatInsteonLinks(num2))
						chrArray = New Char() { Strings.ChrW(32) }
						Dim strArrays1 As String() = str2.Split(chrArray)
						numArray1(3) = num
						numArray1(4) = 8
						numArray1(5) = CByte(Math.Round(Conversion.Val(String.Concat("&H", strArrays1(0)))))
						numArray1(6) = CByte(Math.Round(Conversion.Val(String.Concat("&H", strArrays1(1)))))
						numArray1(7) = CByte(Math.Round(Conversion.Val(String.Concat("&H", strArrays(0)))))
						numArray1(8) = CByte(Math.Round(Conversion.Val(String.Concat("&H", strArrays(1)))))
						numArray1(9) = CByte(Math.Round(Conversion.Val(String.Concat("&H", strArrays(2)))))
						numArray1(10) = CByte(Math.Round(Conversion.Val(String.Concat("&H", strArrays1(5)))))
						numArray1(11) = CByte(Math.Round(Conversion.Val(String.Concat("&H", strArrays1(6)))))
						numArray1(12) = CByte(Math.Round(Conversion.Val(String.Concat("&H", strArrays1(7)))))
						Dim strArrays2() As String = { "<<ADDR:", num.ToString("x"), "=", numArray1(5).ToString("x"), " ", numArray1(6).ToString("x"), ">>" }
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays2), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Test)
					End If
					Me.TransmitInsteonExtended(str, 47, 0, numArray1, True)
					num = CByte((num - 8))
					num2 = num2 + 1
				Loop While num2 <= num1
				If (HSPI_INSTEON_THERMOSTAT.utils.isSmarthomeDEVCAT(tstat)) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str, " : Sending check EF so thermostat will send reporting info to HS"), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					numArray = New Byte() { 239, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
					Dim numArray2 As Byte() = numArray
					Me.TransmitInsteonExtended(str, 46, 0, numArray2, True)
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str, " : Problem Writing links to theremostat"), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub
	End Class
End Namespace