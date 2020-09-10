Imports HomeSeerAPI
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Scheduler
Imports Scheduler.Classes
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Timers

Namespace HSPI_INSTEON_THERMOSTAT
	Public Class plugin
		Public sConfigPage As String

		Public sStatusPage As String

		Public sTestingPage As String

		Public sHelpPage As String

		Private ConfigPage As web_config

		Private StatusPage As web_status

		Private TestingPage As web_testing

		Private HelpPage As web_help

		Private WebPage As Object

		Private triggers As classes.hsCollection

		Private trigger As classes.trigger

		Private actions As classes.hsCollection

		Private action As classes.action

		Private appStarted As Boolean

		Private waitTimer As Timer

		Private mvarActionAdvanced As Boolean

		Public Property ActionAdvancedMode As Boolean
			Get
				Return Me.mvarActionAdvanced
			End Get
			Set(ByVal value As Boolean)
				Me.mvarActionAdvanced = value
			End Set
		End Property

		Public ReadOnly Property ActionName(ByVal ActionNumber As Integer) As String
			Get
				Dim str As String
				str = If(ActionNumber <= 0 OrElse ActionNumber > Me.actions.Count, "", Conversions.ToString(Operators.ConcatenateObject("Insteon Thermostat: ", Me.actions(ActionNumber - 1))))
				Return str
			End Get
		End Property

		Public Property Condition(ByVal TrigInfo As IPlugInAPI.strTrigActInfo) As Boolean
			Get
				Dim flag As Boolean = False
				If (TrigInfo.TANumber = 1) Then
					flag = True
				End If
				Return flag
			End Get
			Set(ByVal value As Boolean)
			End Set
		End Property

		Public ReadOnly Property HasConditions(ByVal TriggerNumber As Integer) As Boolean
			Get
				Dim flag As Boolean = False
				If (TriggerNumber = 1) Then
					flag = True
				End If
				Return flag
			End Get
		End Property

		Public ReadOnly Property HasTriggers As Boolean
			Get
				Dim flag As Boolean = Conversions.ToBoolean(Interaction.IIf(Me.triggers.Count > 0, True, False))
				Return flag
			End Get
		End Property

		Public ReadOnly Property SubTriggerCount(ByVal TriggerNumber As Integer) As Integer
			Get
				Dim num As Integer
				If (Not Me.ValidTrig(TriggerNumber)) Then
					num = 0
				Else
					Dim item As classes.trigger = DirectCast(Me.triggers(TriggerNumber), classes.trigger)
					num = If(item Is Nothing, 0, item.Count)
				End If
				Return num
			End Get
		End Property

		Public ReadOnly Property SubTriggerName(ByVal TriggerNumber As Integer, ByVal SubTriggerNumber As Integer) As String
			Get
				Dim str As String
				If (Not Me.ValidSubTrig(TriggerNumber, SubTriggerNumber)) Then
					str = ""
				Else
					Dim item As classes.trigger = DirectCast(Me.triggers(TriggerNumber), classes.trigger)
					str = Conversions.ToString(item(SubTriggerNumber - 1))
				End If
				Return str
			End Get
		End Property

		Public ReadOnly Property TriggerConfigured(ByVal TrigInfo As IPlugInAPI.strTrigActInfo) As Boolean
			Get
				Dim enumerator As Dictionary(Of String, Object).KeyCollection.Enumerator = New Dictionary(Of String, Object).KeyCollection.Enumerator()
				Dim flag As Boolean = False
				Dim str As String = TrigInfo.UID.ToString()
				Dim tANumber As Integer = TrigInfo.TANumber
				Dim subTANumber As Integer = TrigInfo.SubTANumber
				If (TrigInfo.DataIn Is Nothing) Then
					Me.trigger = New classes.trigger()
				Else
					Dim obj As Object = Me.trigger
					HSPI_INSTEON_THERMOSTAT.utils.DeSerializeObject(TrigInfo.DataIn, obj)
					Me.trigger = DirectCast(obj, classes.trigger)
				End If
				If (subTANumber > 0) Then
					Dim item As Object = Me.triggers(tANumber)
					Dim objArray() As Object = { subTANumber }
					Conversions.ToString(NewLateBinding.LateIndexGet(item, objArray, Nothing))
					Dim str1 As String = ""
					Dim str2 As String = ""
					Try
						enumerator = Me.trigger.Keys.GetEnumerator()
						While enumerator.MoveNext()
							Dim current As String = enumerator.Current
							Dim flag1 As Boolean = True
							If (flag1 <> Strings.InStr(current, String.Concat("TSTAT_", str), CompareMethod.Binary) > 0) Then
								If (flag1 <> Strings.InStr(current, String.Concat("PROGRAM_", str), CompareMethod.Binary) > 0) Then
									Continue While
								End If
								str2 = Conversions.ToString(Me.trigger(current))
							Else
								str1 = Conversions.ToString(Me.trigger(current))
							End If
						End While
					Finally
						(DirectCast(enumerator, IDisposable)).Dispose()
					End Try
					If (subTANumber = 1 AndAlso Not String.IsNullOrEmpty(str1) AndAlso Operators.CompareString(str1, "--Please Select--", False) <> 0 AndAlso Not String.IsNullOrEmpty(str2) AndAlso Operators.CompareString(str2, "--Please Select--", False) <> 0) Then
						flag = True
					End If
				End If
				Return flag
			End Get
		End Property

		Public ReadOnly Property TriggerName(ByVal TriggerNumber As Integer) As String
			Get
				Dim str As String
				str = If(Me.ValidTrig(TriggerNumber), Conversions.ToString(Me.triggers(TriggerNumber - 1)), "")
				Return str
			End Get
		End Property

		Public Sub New()
			MyBase.New()
			Me.sConfigPage = "Insteon_Thermostat_Config"
			Me.sStatusPage = "Insteon_Thermostat_Status"
			Me.sTestingPage = "Insteon_Thermostat_Testing"
			Me.sHelpPage = "Insteon_Thermostat_Help"
			Me.ConfigPage = New web_config(Me.sConfigPage)
			Me.StatusPage = New web_status(Me.sStatusPage)
			Me.TestingPage = New web_testing(Me.sTestingPage)
			Me.HelpPage = New web_help(Me.sHelpPage)
			Me.triggers = New classes.hsCollection()
			Me.trigger = New classes.trigger()
			Me.actions = New classes.hsCollection()
			Me.action = New classes.action()
			Me.appStarted = False
			Me.mvarActionAdvanced = False
		End Sub

		Public Function AccessLevel() As Integer
			Return 2
		End Function

		Public Function ActionBuildUI(ByVal sUnique As String, ByVal ActInfo As IPlugInAPI.strTrigActInfo) As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.plugin::ActionBuildUI(System.String,HomeSeerAPI.IPlugInAPI/strTrigActInfo)
			' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
			' 
			' Product version: 2014.1.225.0
			' Exception in: System.String ActionBuildUI(System.String,HomeSeerAPI.IPlugInAPI/strTrigActInfo)
			' 
			' Object reference not set to an instance of an object.
			'    at ..( , Int32 , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:line 78
			'    at ..( , Int32& , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:line 33
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 60
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 61
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 453
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 87
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..Visit[,]( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 288
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 329
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 471
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 91
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..Visit[,]( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 288
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 319
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 339
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 36
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 61
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..( ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 30
			'    at ..(MethodBody , ILanguage ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 83
			'    at ..( , ILanguage , MethodBody , & ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 99
			'    at ..(MethodBody , ILanguage , & ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 62
			'    at ..(ILanguage , MethodDefinition ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 116
			' 
			' mailto: JustDecompilePublicFeedback@telerik.com

		End Function

		Public Function ActionConfigured(ByVal ActInfo As IPlugInAPI.strTrigActInfo) As Boolean
			Dim enumerator As Dictionary(Of String, Object).KeyCollection.Enumerator = New Dictionary(Of String, Object).KeyCollection.Enumerator()
			Dim flag As Boolean = False
			Dim str As String = ActInfo.UID.ToString()
			Dim tANumber As Integer = ActInfo.TANumber
			If (ActInfo.DataIn Is Nothing) Then
				Me.action = New classes.action()
			Else
				Dim obj As Object = Me.action
				HSPI_INSTEON_THERMOSTAT.utils.DeSerializeObject(ActInfo.DataIn, obj)
				Me.action = DirectCast(obj, classes.action)
			End If
			Dim str1 As String = ""
			Dim str2 As String = ""
			Dim str3 As String = ""
			Dim str4 As String = ""
			Dim str5 As String = ""
			Dim str6 As String = ""
			Dim str7 As String = ""
			Dim str8 As String = ""
			Dim str9 As String = ""
			Dim str10 As String = ""
			Try
				enumerator = Me.action.Keys.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As String = enumerator.Current
					Dim flag1 As Boolean = True
					If (flag1 = Strings.InStr(current, String.Concat("TSTAT_", str), CompareMethod.Binary) > 0) Then
						str1 = Conversions.ToString(Me.action(current))
					ElseIf (flag1 = Strings.InStr(current, String.Concat("MODE_", str), CompareMethod.Binary) > 0) Then
						str2 = Conversions.ToString(Me.action(current))
					ElseIf (flag1 = Strings.InStr(current, String.Concat("FAN_", str), CompareMethod.Binary) > 0) Then
						str3 = Conversions.ToString(Me.action(current))
					ElseIf (flag1 = Strings.InStr(current, String.Concat("HEAT_", str), CompareMethod.Binary) > 0) Then
						str4 = Conversions.ToString(Me.action(current))
					ElseIf (flag1 = Strings.InStr(current, String.Concat("HEAT_INCDEC_", str), CompareMethod.Binary) > 0) Then
						str5 = Conversions.ToString(Me.action(current))
					ElseIf (flag1 = Strings.InStr(current, String.Concat("COOL_", str), CompareMethod.Binary) > 0) Then
						str6 = Conversions.ToString(Me.action(current))
					ElseIf (flag1 = Strings.InStr(current, String.Concat("COOL_INCDEC_", str), CompareMethod.Binary) > 0) Then
						str7 = Conversions.ToString(Me.action(current))
					ElseIf (flag1 = Strings.InStr(current, String.Concat("HOLDRUN_", str), CompareMethod.Binary) > 0) Then
						str8 = Conversions.ToString(Me.action(current))
					ElseIf (flag1 <> Strings.InStr(current, String.Concat("PROGRAM_", str), CompareMethod.Binary) > 0) Then
						If (flag1 <> Strings.InStr(current, String.Concat("POLL_", str), CompareMethod.Binary) > 0) Then
							Continue While
						End If
						str10 = Conversions.ToString(Me.action(current))
					Else
						str9 = Conversions.ToString(Me.action(current))
					End If
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			Select Case tANumber
				Case 1
					If (Not (Not String.IsNullOrEmpty(str1) And Operators.CompareString(str1, "--Please Select--", False) <> 0 And Not String.IsNullOrEmpty(str2) And Operators.CompareString(str2, "--Please Select--", False) <> 0)) Then
						Exit Select
					End If
					flag = True
					Exit Select
				Case 2
					If (Not (Not String.IsNullOrEmpty(str1) And Operators.CompareString(str1, "--Please Select--", False) <> 0 And Not String.IsNullOrEmpty(str3) And Operators.CompareString(str3, "--Please Select--", False) <> 0)) Then
						Exit Select
					End If
					flag = True
					Exit Select
				Case 3
					If (Not (Not String.IsNullOrEmpty(str1) And Operators.CompareString(str1, "--Please Select--", False) <> 0)) Then
						Exit Select
					End If
					If (Not String.IsNullOrEmpty(str4) And Operators.CompareString(str4, "--Please Select--", False) <> 0 Or Not String.IsNullOrEmpty(str5) And Operators.CompareString(str5, "--Please Select--", False) <> 0) Then
						flag = True
					End If
					Exit Select
				Case 4
					If (Not (Not String.IsNullOrEmpty(str1) And Operators.CompareString(str1, "--Please Select--", False) <> 0)) Then
						Exit Select
					End If
					If (Not String.IsNullOrEmpty(str6) And Operators.CompareString(str6, "--Please Select--", False) <> 0 Or Not String.IsNullOrEmpty(str7) And Operators.CompareString(str7, "--Please Select--", False) <> 0) Then
						flag = True
					End If
					Exit Select
				Case 5
					If (Not (Not String.IsNullOrEmpty(str1) And Operators.CompareString(str1, "--Please Select--", False) <> 0 And Not String.IsNullOrEmpty(str8) And Operators.CompareString(str8, "--Please Select--", False) <> 0)) Then
						Exit Select
					End If
					flag = True
					Exit Select
				Case 6
					If (Not (Not String.IsNullOrEmpty(str1) And Operators.CompareString(str1, "--Please Select--", False) <> 0 And Not String.IsNullOrEmpty(str9) And Operators.CompareString(str9, "--Please Select--", False) <> 0)) Then
						Exit Select
					End If
					flag = True
					Exit Select
				Case 7
					If (Not (Not String.IsNullOrEmpty(str1) And Operators.CompareString(str1, "--Please Select--", False) <> 0)) Then
						Exit Select
					End If
					flag = True
					Exit Select
			End Select
			Return flag
		End Function

		Public Function ActionCount() As Integer
			Return Me.actions.Count
		End Function

		Public Function ActionFormatUI(ByVal ActInfo As IPlugInAPI.strTrigActInfo) As String
			Dim enumerator As Dictionary(Of String, Object).KeyCollection.Enumerator = New Dictionary(Of String, Object).KeyCollection.Enumerator()
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim str As String = ActInfo.UID.ToString()
			Dim tANumber As Integer = ActInfo.TANumber
			If (ActInfo.DataIn Is Nothing) Then
				Me.action = New classes.action()
			Else
				Dim obj As Object = Me.action
				HSPI_INSTEON_THERMOSTAT.utils.DeSerializeObject(ActInfo.DataIn, obj)
				Me.action = DirectCast(obj, classes.action)
			End If
			Dim str1 As String = ""
			Dim str2 As String = ""
			Dim str3 As String = ""
			Dim str4 As String = ""
			Dim str5 As String = ""
			Dim str6 As String = ""
			Dim str7 As String = ""
			Dim str8 As String = ""
			Dim str9 As String = ""
			Dim str10 As String = ""
			Try
				enumerator = Me.action.Keys.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As String = enumerator.Current
					Dim flag As Boolean = True
					If (flag = Strings.InStr(current, String.Concat("TSTAT_", str), CompareMethod.Binary) > 0) Then
						str1 = Conversions.ToString(Me.action(current))
					ElseIf (flag = Strings.InStr(current, String.Concat("MODE_", str), CompareMethod.Binary) > 0) Then
						str2 = Conversions.ToString(Me.action(current))
					ElseIf (flag = Strings.InStr(current, String.Concat("FAN_", str), CompareMethod.Binary) > 0) Then
						str3 = Conversions.ToString(Me.action(current))
					ElseIf (flag = Strings.InStr(current, String.Concat("HEAT_", str), CompareMethod.Binary) > 0) Then
						str4 = Conversions.ToString(Me.action(current))
					ElseIf (flag = Strings.InStr(current, String.Concat("HEAT_INCDEC_", str), CompareMethod.Binary) > 0) Then
						str5 = Conversions.ToString(Me.action(current))
					ElseIf (flag = Strings.InStr(current, String.Concat("COOL_", str), CompareMethod.Binary) > 0) Then
						str6 = Conversions.ToString(Me.action(current))
					ElseIf (flag = Strings.InStr(current, String.Concat("COOL_INCDEC_", str), CompareMethod.Binary) > 0) Then
						str7 = Conversions.ToString(Me.action(current))
					ElseIf (flag = Strings.InStr(current, String.Concat("HOLDRUN_", str), CompareMethod.Binary) > 0) Then
						str8 = Conversions.ToString(Me.action(current))
					ElseIf (flag <> Strings.InStr(current, String.Concat("PROGRAM_", str), CompareMethod.Binary) > 0) Then
						If (flag <> Strings.InStr(current, String.Concat("POLL_", str), CompareMethod.Binary) > 0) Then
							Continue While
						End If
						str10 = Conversions.ToString(Me.action(current))
					Else
						str9 = Conversions.ToString(Me.action(current))
					End If
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			stringBuilder.Append("Insteon Thermostat")
			stringBuilder.Append(" : ")
			Select Case tANumber
				Case 1
					stringBuilder.Append(str1)
					If (Not (Operators.CompareString(str2, "Next Operating Mode", False) = 0 Or Operators.CompareString(str2, "Previous Operating Mode", False) = 0)) Then
						stringBuilder.Append(" set mode to ")
					Else
						stringBuilder.Append(" set ")
					End If
					stringBuilder.Append(str2)
					Exit Select
				Case 2
					stringBuilder.Append(str1)
					stringBuilder.Append(" set fan to ")
					stringBuilder.Append(str3)
					Exit Select
				Case 3
					stringBuilder.Append(str1)
					If (Not String.IsNullOrEmpty(str4) And Operators.CompareString(str4, "--Please Select--", False) <> 0) Then
						stringBuilder.Append(" set heat setpoint to ")
						stringBuilder.Append(str4)
					End If
					If (Not (Not String.IsNullOrEmpty(str5) And Operators.CompareString(str5, "--Please Select--", False) <> 0)) Then
						Exit Select
					End If
					stringBuilder.Append(" adjust heat setpoint by ")
					stringBuilder.Append(str5)
					Exit Select
				Case 4
					stringBuilder.Append(str1)
					If (Not String.IsNullOrEmpty(str6) And Operators.CompareString(str6, "--Please Select--", False) <> 0) Then
						stringBuilder.Append(" set cool setpoint to ")
						stringBuilder.Append(str6)
					End If
					If (Not (Not String.IsNullOrEmpty(str7) And Operators.CompareString(str7, "--Please Select--", False) <> 0)) Then
						Exit Select
					End If
					stringBuilder.Append(" adjust cool setpoint by ")
					stringBuilder.Append(str7)
					Exit Select
				Case 5
					stringBuilder.Append(str1)
					stringBuilder.Append(" set run/hold to ")
					stringBuilder.Append(str8)
					Exit Select
				Case 6
					stringBuilder.Append(str1)
					stringBuilder.Append(" set program to ")
					stringBuilder.Append(str9)
					Exit Select
				Case 7
					If (Operators.CompareString(str1, "All", False) <> 0) Then
						stringBuilder.Append(String.Concat(" poll ", str1))
						Exit Select
					Else
						stringBuilder.Append(" poll all thermostats")
						Exit Select
					End If
			End Select
			Return stringBuilder.ToString()
		End Function

		Public Function ActionProcessPostUI(ByVal PostData As System.Collections.Specialized.NameValueCollection, ByVal ActInfo As IPlugInAPI.strTrigActInfo) As IPlugInAPI.strMultiReturn
			Dim _strMultiReturn As IPlugInAPI.strMultiReturn
			Dim obj As Object
			Dim enumerator As IEnumerator = Nothing
			Dim flag As Boolean = False
			Dim dataIn As IPlugInAPI.strMultiReturn = New IPlugInAPI.strMultiReturn()
			Dim str As String = ActInfo.UID.ToString()
			dataIn.sResult = ""
			dataIn.DataOut = ActInfo.DataIn
			dataIn.TrigActInfo = ActInfo
			If (PostData Is Nothing) Then
				Return dataIn
			End If
			If (PostData.Count < 1) Then
				Return dataIn
			End If
			If (ActInfo.DataIn Is Nothing) Then
				Me.action = New classes.action()
			Else
				obj = Me.action
				HSPI_INSTEON_THERMOSTAT.utils.DeSerializeObject(ActInfo.DataIn, obj)
				Me.action = DirectCast(obj, classes.action)
			End If
			Dim nameValueCollection As System.Collections.Specialized.NameValueCollection = PostData
			Dim flag1 As Boolean = False
			Dim str1 As String = ""
			Dim flag2 As Boolean = False
			Dim str2 As String = ""
			Dim flag3 As Boolean = False
			Dim str3 As String = ""
			Dim flag4 As Boolean = False
			Dim str4 As String = ""
			Try
				Try
					enumerator = nameValueCollection.Keys.GetEnumerator()
					While enumerator.MoveNext()
						Dim str5 As String = Conversions.ToString(enumerator.Current)
						If (str5 IsNot Nothing) Then
							If (Not String.IsNullOrEmpty(str5.Trim())) Then
								Dim flag5 As Boolean = True
								If (flag5 = Strings.InStr(str5, String.Concat("TSTAT_", str), CompareMethod.Binary) > 0) Then
									Me.action.Add(nameValueCollection(str5), str5)
								ElseIf (flag5 = Strings.InStr(str5, String.Concat("MODE_", str), CompareMethod.Binary) > 0) Then
									Me.action.Add(nameValueCollection(str5), str5)
								ElseIf (flag5 = Strings.InStr(str5, String.Concat("FAN_", str), CompareMethod.Binary) > 0) Then
									Me.action.Add(nameValueCollection(str5), str5)
								ElseIf (flag5 = Strings.InStr(str5, String.Concat("HEAT_", str), CompareMethod.Binary) > 0) Then
									str1 = str5
									If (Conversions.ToBoolean(Operators.AndObject(Me.action.ContainsKey(str5), Operators.CompareObjectNotEqual(Me.action(str5), nameValueCollection(str5), False)))) Then
										flag1 = True
									End If
									Me.action.Add(nameValueCollection(str5), str5)
								ElseIf (flag5 = Strings.InStr(str5, String.Concat("HEAT_INCDEC_", str), CompareMethod.Binary) > 0) Then
									str2 = str5
									If (Conversions.ToBoolean(Operators.AndObject(Me.action.ContainsKey(str5), Operators.CompareObjectNotEqual(Me.action(str5), nameValueCollection(str5), False)))) Then
										flag2 = True
									End If
									Me.action.Add(nameValueCollection(str5), str5)
								ElseIf (flag5 = Strings.InStr(str5, String.Concat("COOL_", str), CompareMethod.Binary) > 0) Then
									str3 = str5
									If (Conversions.ToBoolean(Operators.AndObject(Me.action.ContainsKey(str5), Operators.CompareObjectNotEqual(Me.action(str5), nameValueCollection(str5), False)))) Then
										flag3 = True
									End If
									Me.action.Add(nameValueCollection(str5), str5)
								ElseIf (flag5 = Strings.InStr(str5, String.Concat("COOL_INCDEC_", str), CompareMethod.Binary) > 0) Then
									str4 = str5
									If (Conversions.ToBoolean(Operators.AndObject(Me.action.ContainsKey(str5), Operators.CompareObjectNotEqual(Me.action(str5), nameValueCollection(str5), False)))) Then
										flag4 = True
									End If
									Me.action.Add(nameValueCollection(str5), str5)
								ElseIf (flag5 = Strings.InStr(str5, String.Concat("HOLDRUN_", str), CompareMethod.Binary) > 0) Then
									Me.action.Add(nameValueCollection(str5), str5)
								ElseIf (flag5 <> Strings.InStr(str5, String.Concat("PROGRAM_", str), CompareMethod.Binary) > 0) Then
									If (flag5 <> Strings.InStr(str5, String.Concat("POLL_", str), CompareMethod.Binary) > 0) Then
										Continue While
									End If
									Me.action.Add(nameValueCollection(str5), str5)
								Else
									Me.action.Add(nameValueCollection(str5), str5)
								End If
							End If
						End If
					End While
				Finally
					If (TypeOf enumerator Is IDisposable) Then
						(TryCast(enumerator, IDisposable)).Dispose()
					End If
				End Try
				If (flag1) Then
					If (Me.action.ContainsKey(str2)) Then
						Me.action.Remove(str2)
					End If
					Me.action.Add("--Please Select--", str2)
				End If
				If (flag2) Then
					If (Me.action.ContainsKey(str1)) Then
						Me.action.Remove(str1)
					End If
					Me.action.Add("--Please Select--", str1)
				End If
				If (flag3) Then
					If (Me.action.ContainsKey(str4)) Then
						Me.action.Remove(str4)
					End If
					Me.action.Add("--Please Select--", str4)
				End If
				If (flag4) Then
					If (Me.action.ContainsKey(str3)) Then
						Me.action.Remove(str3)
					End If
					Me.action.Add("--Please Select--", str3)
				End If
				obj = Me.action
				Dim flag6 As Boolean = HSPI_INSTEON_THERMOSTAT.utils.SerializeObject(obj, dataIn.DataOut)
				Me.action = DirectCast(obj, classes.action)
				If (flag6) Then
					flag = True
				Else
					dataIn.sResult = "Insteon Thermostat Error, Serialization failed. Signal Action not added."
					_strMultiReturn = dataIn
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				dataIn.sResult = String.Concat("ERROR, Exception in Action UI of Insteon Thermostat: ", exception.Message)
				_strMultiReturn = dataIn
				ProjectData.ClearProjectError()
			End Try
			If (Not flag) Then
				Return _strMultiReturn
			End If
			flag = False
			dataIn.sResult = ""
			Return dataIn
		End Function

		Public Function ActionReferencesDevice(ByVal ActInfo As IPlugInAPI.strTrigActInfo, ByVal dvRef As Integer) As Boolean
			Dim flag As Boolean = False
			HSPI_INSTEON_THERMOSTAT.utils.Log(Conversions.ToString(Operators.ConcatenateObject(String.Concat(String.Concat(String.Concat(String.Concat("ActionReferencesDevice : dvRef=", Conversions.ToString(dvRef)), " retVal="), Conversions.ToString(flag)), " "), HSPI_INSTEON_THERMOSTAT.utils.TAInfoToString(ActInfo))), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Test)
			Return flag
		End Function

		Public Function AdjustCoolSetpoint(ByVal tName As String, ByVal posNegAdjustment As Short) As String
			Dim str As String = Nothing
			Try
				Dim [integer] As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_COOL", "Cool"))
				str = Me.SetCoolSetpoint(tName, CShort(([integer] + posNegAdjustment)))
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = String.Concat("AdjustCoolSetpoint: ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function AdjustHeatSetpoint(ByVal tName As String, ByVal posNegAdjustment As Short) As String
			Dim str As String = Nothing
			Try
				Dim [integer] As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_HEAT", "Heat"))
				str = Me.SetHeatSetpoint(tName, CShort(([integer] + posNegAdjustment)))
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = String.Concat("AdjustHeatSetpoint: ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function ConfigDevice(ByVal dvRef As Integer, ByVal user As String, ByVal userRights As Integer, ByVal newDevice As Boolean) As String
			Dim str As String
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			Try
				Dim deviceByRef As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(dvRef), Scheduler.Classes.DeviceClass)
				Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				Dim flag As Boolean = False
				Dim flag1 As Boolean = False
				If (plugExtraDataGet Is Nothing) Then
					stringBuilder.Append("<p>No Plugin Extra Data (PED) for the device</p>")
				Else
					Dim namedKeys As String() = plugExtraDataGet.GetNamedKeys()
					Dim num As Integer = 0
					Do
						Dim str1 As String = namedKeys(num)
						If (Not flag) Then
							stringBuilder.Append("<table border='1'><tr><th colspan='2'>PED Named Entries</th></tr>")
							stringBuilder.Append("<tr><th>Key</th><th>Value</th></tr>")
							flag = True
						End If
						Try
							stringBuilder.Append(Operators.ConcatenateObject(Operators.ConcatenateObject(String.Concat(String.Concat("<tr><td>", str1), "</td><td>"), plugExtraDataGet.GetNamed(str1)), "</td></tr>"))
						Catch exception As System.Exception
							ProjectData.SetProjectError(exception)
							stringBuilder.Append(String.Concat("<tr><td>", str1, "</td><td>???</td></tr>"))
							ProjectData.ClearProjectError()
						End Try
						num = num + 1
					Loop While num < CInt(namedKeys.Length)
					If (flag) Then
						stringBuilder.Append("</table><br/>")
					End If
					Dim num1 As Integer = plugExtraDataGet.UnNamedCount() - 1
					Dim num2 As Integer = 0
					Do
						If (Not flag1) Then
							stringBuilder.Append("<table border='1'><tr><th colspan='2'>PED Un-named Entries</th></tr>")
							stringBuilder.Append("<tr><th>#</th><th>Value</th></tr>")
							flag1 = True
						End If
						Try
							stringBuilder.Append(Operators.ConcatenateObject(Operators.ConcatenateObject(String.Concat(String.Concat("<tr><td>", Conversions.ToString(num2)), "</td><td>"), plugExtraDataGet.GetUnNamed(num2)), "</td></tr>"))
						Catch exception1 As System.Exception
							ProjectData.SetProjectError(exception1)
							stringBuilder.Append(String.Concat("<tr><td>", Conversions.ToString(num2), "</td><td>???</td></tr>"))
							ProjectData.ClearProjectError()
						End Try
						num2 = num2 + 1
					Loop While num2 <= num1
					If (flag1) Then
						stringBuilder.Append("</table><br/>")
					End If
				End If
				Dim flag2 As Boolean = False
				Dim num3 As Integer = 0
				Dim associatedDevices As Integer() = deviceByRef.get_AssociatedDevices(HSPI_INSTEON_THERMOSTAT.utils.hs)
				Dim num4 As Integer = 0
				Do
					Dim num5 As Integer = associatedDevices(num4)
					If (Not flag2) Then
						stringBuilder.Append("<table border='1'><tr><th colspan='3'>Related Devices</th></tr>")
						stringBuilder.Append("<tr><th>Name</th><th>Ref</th><th>Relationship</th></tr>")
						flag2 = True
					End If
					Dim deviceClass As Scheduler.Classes.DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(num5), Scheduler.Classes.DeviceClass)
					Try
						Dim name() As String = { "<tr><td>", deviceClass.get_Name(HSPI_INSTEON_THERMOSTAT.utils.hs), "</td><td>", Conversions.ToString(deviceClass.get_Ref(HSPI_INSTEON_THERMOSTAT.utils.hs)), "</td><td>", HSPI_INSTEON_THERMOSTAT.utils.RelationshipString(CInt(deviceClass.get_Relationship(HSPI_INSTEON_THERMOSTAT.utils.hs))), "</td></tr>" }
						stringBuilder.Append(String.Concat(name))
					Catch exception3 As System.Exception
						ProjectData.SetProjectError(exception3)
						Dim exception2 As System.Exception = exception3
						stringBuilder.Append(String.Concat("<tr><td colspan='3'>", exception2.Message, "</td></tr>"))
						ProjectData.ClearProjectError()
					End Try
					num3 = num3 + 1
					num4 = num4 + 1
				Loop While num4 < CInt(associatedDevices.Length)
				If (flag2) Then
					stringBuilder.Append("</table><br/>")
				End If
				If (num3 = 0) Then
					stringBuilder.Append("<p>No Relationships for the device</p>")
				End If
				str = stringBuilder.ToString()
			Catch exception4 As System.Exception
				ProjectData.SetProjectError(exception4)
				str = Information.Err().Description
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function ConfigDevicePost(ByVal dvRef As Integer, ByVal data As String, ByVal user As String, ByVal userRights As Integer) As Enums.ConfigDevicePostReturn
			Return Enums.ConfigDevicePostReturn.DoneAndCancel
		End Function

		Public Function GetPagePlugin(ByVal pageName As String, ByVal user As String, ByVal userRights As Integer, ByVal queryString As String) As String
			Me.WebPage = RuntimeHelpers.GetObjectValue(Me.SelectPage(pageName))
			Dim webPage As Object = Me.WebPage
			Dim objArray() As Object = { pageName, user, userRights, queryString }
			Dim objArray1 As Object() = objArray
			Dim flagArray() As Boolean = { True, True, True, True }
			Dim obj As Object = NewLateBinding.LateGet(webPage, Nothing, "GetPagePlugin", objArray1, Nothing, Nothing, flagArray)
			If (flagArray(0)) Then
				pageName = CStr(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(objArray1(0)), GetType(String)))
			End If
			If (flagArray(1)) Then
				user = CStr(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(objArray1(1)), GetType(String)))
			End If
			If (flagArray(2)) Then
				userRights = CInt(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(objArray1(2)), GetType(Integer)))
			End If
			If (flagArray(3)) Then
				queryString = CStr(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(objArray1(3)), GetType(String)))
			End If
			Return Conversions.ToString(obj)
		End Function

		Public Function HandleAction(ByVal ActInfo As IPlugInAPI.strTrigActInfo) As Boolean
			Dim enumerator As Dictionary(Of String, Object).KeyCollection.Enumerator = New Dictionary(Of String, Object).KeyCollection.Enumerator()
			Dim flag As Boolean = False
			Dim str As String = ActInfo.UID.ToString()
			Try
				If (Not Me.ValidActInfo(ActInfo)) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log(Conversions.ToString(Operators.ConcatenateObject("HandleAction - action appears invalid - ", HSPI_INSTEON_THERMOSTAT.utils.TAInfoToString(ActInfo))), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				Else
					If (ActInfo.DataIn Is Nothing) Then
						Me.action = New classes.action()
					Else
						Dim obj As Object = Me.action
						HSPI_INSTEON_THERMOSTAT.utils.DeSerializeObject(ActInfo.DataIn, obj)
						Me.action = DirectCast(obj, classes.action)
					End If
					Dim str1 As String = ""
					Dim str2 As String = ""
					Dim str3 As String = ""
					Dim str4 As String = ""
					Dim str5 As String = ""
					Dim str6 As String = ""
					Dim str7 As String = ""
					Dim str8 As String = ""
					Dim str9 As String = ""
					Dim str10 As String = ""
					Try
						enumerator = Me.action.Keys.GetEnumerator()
						While enumerator.MoveNext()
							Dim current As String = enumerator.Current
							Dim flag1 As Boolean = True
							If (flag1 = Strings.InStr(current, String.Concat("TSTAT_", str), CompareMethod.Binary) > 0) Then
								str1 = Conversions.ToString(Me.action(current))
							ElseIf (flag1 = Strings.InStr(current, String.Concat("MODE_", str), CompareMethod.Binary) > 0) Then
								str2 = Conversions.ToString(Me.action(current))
							ElseIf (flag1 = Strings.InStr(current, String.Concat("FAN_", str), CompareMethod.Binary) > 0) Then
								str3 = Conversions.ToString(Me.action(current))
							ElseIf (flag1 = Strings.InStr(current, String.Concat("HEAT_", str), CompareMethod.Binary) > 0) Then
								str4 = Conversions.ToString(Me.action(current))
							ElseIf (flag1 = Strings.InStr(current, String.Concat("HEAT_INCDEC_", str), CompareMethod.Binary) > 0) Then
								str5 = Conversions.ToString(Me.action(current))
							ElseIf (flag1 = Strings.InStr(current, String.Concat("COOL_", str), CompareMethod.Binary) > 0) Then
								str6 = Conversions.ToString(Me.action(current))
							ElseIf (flag1 = Strings.InStr(current, String.Concat("COOL_INCDEC_", str), CompareMethod.Binary) > 0) Then
								str7 = Conversions.ToString(Me.action(current))
							ElseIf (flag1 = Strings.InStr(current, String.Concat("HOLDRUN_", str), CompareMethod.Binary) > 0) Then
								str8 = Conversions.ToString(Me.action(current))
							ElseIf (flag1 <> Strings.InStr(current, String.Concat("PROGRAM_", str), CompareMethod.Binary) > 0) Then
								If (flag1 <> Strings.InStr(current, String.Concat("POLL_", str), CompareMethod.Binary) > 0) Then
									Continue While
								End If
								str10 = Conversions.ToString(Me.action(current))
							Else
								str9 = Conversions.ToString(Me.action(current))
							End If
						End While
					Finally
						(DirectCast(enumerator, IDisposable)).Dispose()
					End Try
					Select Case ActInfo.TANumber
						Case 1
							If (Operators.CompareString(str2, "Next Operating Mode", False) = 0) Then
								Me.SetModeNext(str1)
								Exit Select
							ElseIf (Operators.CompareString(str2, "Previous Operating Mode", False) <> 0) Then
								Me.SetModeByString(str1, str2)
								Exit Select
							Else
								Me.SetModePrev(str1)
								Exit Select
							End If
						Case 2
							Select Case HSPI_INSTEON_THERMOSTAT.utils.gFanOptsByName(str3)
								Case 0
									Me.SetFanAuto(str1)

								Case 1
									Me.SetFanOn(str1)

								Case 2
									Me.SetFanToggle(str1)

							End Select

						Case 3
							If (Not (Not String.IsNullOrEmpty(str4) And Operators.CompareString(str4, "--Please Select--", False) <> 0)) Then
								Me.AdjustHeatSetpoint(str1, Conversions.ToShort(str5))
								Exit Select
							Else
								Me.SetHeatSetpoint(str1, Conversions.ToShort(str4))
								Exit Select
							End If
						Case 4
							If (Not (Not String.IsNullOrEmpty(str6) And Operators.CompareString(str6, "--Please Select--", False) <> 0)) Then
								Me.AdjustCoolSetpoint(str1, Conversions.ToShort(str7))
								Exit Select
							Else
								Me.SetCoolSetpoint(str1, Conversions.ToShort(str6))
								Exit Select
							End If
						Case 5
							Select Case HSPI_INSTEON_THERMOSTAT.utils.gHoldOptsByName(str8)
								Case 0
									Me.SetRun(str1)

								Case 1
									Me.SetHold(str1)

								Case 2
									Me.SetHoldToggle(str1)

							End Select

						Case 6
							If (Operators.CompareString(str1, "Master Program", False) <> 0) Then
								Me.SetProgram(str1, str9, True)
								Exit Select
							Else
								Me.SetMasterProgram(str9, True)
								Exit Select
							End If
						Case 7
							If (Operators.CompareString(str1, "All", False) <> 0) Then
								Me.PollStat(str1)
								Exit Select
							Else
								Me.PollAllStats()
								Exit Select
							End If
						Case Else
							HSPI_INSTEON_THERMOSTAT.utils.Log(Conversions.ToString(Operators.ConcatenateObject("HandleAction - action number appears invalid - ", HSPI_INSTEON_THERMOSTAT.utils.TAInfoToString(ActInfo))), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
							Exit Select
					End Select
					flag = True
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("HandleAction - Error executing action: ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Public Sub HSEvent(ByVal EventType As Enums.HSEvent, ByVal parms As Object())
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			stringBuilder.Append("HSEvent [")
			stringBuilder.Append(CInt(EventType))
			stringBuilder.Append("] ")
			Dim length As Integer = CInt(parms.Length) - 1
			Dim num As Integer = 0
			Do
				stringBuilder.Append(String.Concat(Conversions.ToString(num), "=["))
				Try
					stringBuilder.Append(RuntimeHelpers.GetObjectValue(parms(num)))
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					stringBuilder.Append("???")
					ProjectData.ClearProjectError()
				End Try
				stringBuilder.Append("] ")
				num = num + 1
			Loop While num <= length
			If (Information.UBound(parms, 1) < 0) Then
				Return
			End If
			If (EventType = Enums.HSEvent.GENERIC) Then
				If (Operators.CompareString(Conversions.ToString(parms(2)), "Insteon", False) <> 0) Then
					Return
				End If
				Dim str As String = Conversions.ToString(parms(3))
				If (Operators.CompareString(str, "Ready for Registration", False) = 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.RegisterTstats()
				ElseIf (Operators.CompareString(str, "Data Received", False) = 0) Then
					Conversions.ToString(parms(4))
					Conversions.ToString(parms(5))
					Dim str1 As String = Conversions.ToString(parms(6))
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.InsteonRcv(str1)
				End If
			End If
		End Sub

		Public Function InitIO(ByVal port As String) As String
			Dim message As String
			Dim flag As Boolean = False
			If (HSPI_INSTEON_THERMOSTAT.utils.hs.InterfaceVersion() < 4) Then
				Dim str As String = String.Concat("Must have HomeSeer 3 (interface 4) or higher.  Found HS Version", HSPI_INSTEON_THERMOSTAT.utils.hs.Version(), " and HS Interface Version : ", Conversions.ToString(HSPI_INSTEON_THERMOSTAT.utils.hs.InterfaceVersion()))
				Console.WriteLine(String.Concat("" & VbCrLf & "Insteon Thermostat : ", str, "" & VbCrLf & ""))
				HSPI_INSTEON_THERMOSTAT.utils.myConfig.LogDebugInfoChecked = True
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				Return str
			End If
			Try
				HSPI_INSTEON_THERMOSTAT.utils.initGlobals()
				HSPI_INSTEON_THERMOSTAT.utils.myConfig.readINI()
				HSPI_INSTEON_THERMOSTAT.utils.Log("---------------------------------------------------------------------", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
				HSPI_INSTEON_THERMOSTAT.utils.Log("Starting Up... Insteon Thermostat plug-in version = 3.0.0.9", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
				HSPI_INSTEON_THERMOSTAT.utils.Log("---------------------------------------------------------------------", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("hs.Version           = ", HSPI_INSTEON_THERMOSTAT.utils.hs.Version()), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("hs.InterfaceVersion  = ", Conversions.ToString(HSPI_INSTEON_THERMOSTAT.utils.hs.InterfaceVersion())), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("hs.PluginLicenseMode = ", HSPI_INSTEON_THERMOSTAT.utils.GetLicenseModeString()), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
				HSPI_INSTEON_THERMOSTAT.utils.myConfig.LogConfig(64)
				Me.InitIOAddActionsTriggers()
				HSPI_INSTEON_THERMOSTAT.utils.myTstat.InitDevices()
				HSPI_INSTEON_THERMOSTAT.utils.myConfig.LogConfigTPH(64)
				HSPI_INSTEON_THERMOSTAT.utils.RegisterWebPage(Me.sConfigPage, Me.sConfigPage.Replace("_", " "), "", True)
				HSPI_INSTEON_THERMOSTAT.utils.RegisterWebPage(Me.sStatusPage, Me.sStatusPage.Replace("_", " "), "", False)
				HSPI_INSTEON_THERMOSTAT.utils.RegisterWebPage(Me.sHelpPage, Me.sHelpPage.Replace("_", " "), "", False)
				If ((HSPI_INSTEON_THERMOSTAT.utils.myConfig.logLvl And 128) > 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.RegisterWebPage(Me.sTestingPage, Me.sTestingPage.Replace("_", " "), "", False)
				End If
				flag = True
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				message = exception.Message
				ProjectData.ClearProjectError()
			End Try
			If (Not flag) Then
				Return message
			End If
			flag = False
			Me.waitTimer = New Timer(5000) With
			{
				.AutoReset = False
			}
			Dim _plugin As plugin = Me
			AddHandler Me.waitTimer.Elapsed,  New ElapsedEventHandler(AddressOf _plugin.waitToRegister)
			Me.waitTimer.Start()
			Return ""
		End Function

		Private Sub InitIOAddActionsTriggers()
			Me.actions.Add("Change Mode", "Change Mode")
			Me.actions.Add("Change Fan", "Change Fan")
			Me.actions.Add("Change Heat SetPoint", "Change Heat SetPoint")
			Me.actions.Add("Change Cool SetPoint", "Change Cool SetPoint")
			Me.actions.Add("Change Hold/Run", "Change Hold/Run")
			Me.actions.Add("Set Program", "Set Program")
			Me.actions.Add("Poll Thermostat", "Poll Thermostat")
			Me.trigger = New classes.trigger() From
			{
				{ "Program changed", "Program changed" }
			}
			Me.triggers.Add(Me.trigger, "Insteon Thermostat : Special Triggers")
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveEventsDevices()
		End Sub

		Public Function InstanceFriendlyName() As String
			Return ""
		End Function

		Public Function InterfaceStatus() As IPlugInAPI.strInterfaceStatus
			Dim strInterfaceStatu As IPlugInAPI.strInterfaceStatus = New IPlugInAPI.strInterfaceStatus() With
			{
				.intStatus = IPlugInAPI.enumInterfaceStatus.OK
			}
			Try
				If (Not HSPI_INSTEON_THERMOSTAT.utils.bInitIOComplete) Then
					Dim str As String = "Initializing..."
					strInterfaceStatu.intStatus = IPlugInAPI.enumInterfaceStatus.INFO
					strInterfaceStatu.sStatus = str
					HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				ElseIf (String.IsNullOrEmpty(HSPI_INSTEON_THERMOSTAT.utils.myInsteon.plmAddress)) Then
					Dim str1 As String = "Not connected to Insteon plugin!"
					strInterfaceStatu.intStatus = IPlugInAPI.enumInterfaceStatus.WARNING
					strInterfaceStatu.sStatus = str1
					HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				strInterfaceStatu.intStatus = IPlugInAPI.enumInterfaceStatus.FATAL
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("InterfaceStatus=[", Conversions.ToString(CInt(strInterfaceStatu.intStatus)), "] ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return strInterfaceStatu
		End Function

		Public Function name() As String
			Return "Insteon Thermostat"
		End Function

		Public Function PollAllStats() As String
			Dim enumerator As SortedDictionary(Of String, Collection).KeyCollection.Enumerator = New SortedDictionary(Of String, Collection).KeyCollection.Enumerator()
			Dim str As String = Nothing
			Try
				enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats.Keys.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As String = enumerator.Current
					str = String.Concat(str, Me.PollStat(current))
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			Return str
		End Function

		Public Function PollDevice(ByVal dvRef As Integer) As IPlugInAPI.PollResultInfo
			Dim pollResultInfo As IPlugInAPI.PollResultInfo = New IPlugInAPI.PollResultInfo()
			pollResultInfo.Result = IPlugInAPI.enumPollResult.OK
			Dim str As String = ""
			Try
				Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(dvRef), DeviceClass)
				Dim deviceTypeGet As DeviceTypeInfo_m.DeviceTypeInfo = deviceByRef.get_DeviceType_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				str = Conversions.ToString(plugExtraDataGet.GetNamed("ParentName"))
				If (deviceTypeGet.Device_Type = 3) Then
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.SendPollRequests(HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(str), True, False, False, False)
				ElseIf (deviceTypeGet.Device_Type = 4) Then
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.SendPollRequests(HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(str), True, False, False, False)
				ElseIf (deviceTypeGet.Device_Type = 6) Then
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.SendPollRequests(HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(str), False, False, True, False)
				ElseIf (deviceTypeGet.Device_Type = 2) Then
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.SendPollRequests(HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(str), False, True, False, False)
				ElseIf (deviceTypeGet.Device_Type = 10) Then
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.SendPollRequests(HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(str), False, True, False, False)
				ElseIf (deviceTypeGet.Device_SubType <> 1) Then
					pollResultInfo.Result = IPlugInAPI.enumPollResult.Device_Not_Found
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str, " - Unexpected device type requested for polling."), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				Else
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.SendPollRequests(HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(str), False, False, False, True)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str, " : ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				pollResultInfo.Result = IPlugInAPI.enumPollResult.Error_Getting_Status
				ProjectData.ClearProjectError()
			End Try
			Return pollResultInfo
		End Function

		Public Function PollStat(ByVal tName As String) As String
			Dim str As String = Nothing
			Try
				Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
				HSPI_INSTEON_THERMOSTAT.utils.myInsteon.SendPollRequests(item, True, True, True, True)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				str = String.Concat("PollStat(", tName, ") problem polling : ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function PollStatByNum(ByVal statNum As String) As String
			Dim str As String = Nothing
			Try
				str = Me.PollStat(HSPI_INSTEON_THERMOSTAT.utils.myConfig.NameByNum(HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats, Conversions.ToInteger(statNum)))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				str = String.Concat("PollStat(num=", statNum, ") problem polling : ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function postBackProc(ByVal page As String, ByVal data As String, ByVal user As String, ByVal userRights As Integer) As String
			Me.WebPage = RuntimeHelpers.GetObjectValue(Me.SelectPage(page))
			Dim webPage As Object = Me.WebPage
			Dim objArray() As Object = { page, data, user, userRights }
			Dim objArray1 As Object() = objArray
			Dim flagArray() As Boolean = { True, True, True, True }
			Dim obj As Object = NewLateBinding.LateGet(webPage, Nothing, "postBackProc", objArray1, Nothing, Nothing, flagArray)
			If (flagArray(0)) Then
				page = CStr(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(objArray1(0)), GetType(String)))
			End If
			If (flagArray(1)) Then
				data = CStr(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(objArray1(1)), GetType(String)))
			End If
			If (flagArray(2)) Then
				user = CStr(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(objArray1(2)), GetType(String)))
			End If
			If (flagArray(3)) Then
				userRights = CInt(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(objArray1(3)), GetType(Integer)))
			End If
			Return Conversions.ToString(obj)
		End Function

		Private Function SelectPage(ByVal pageName As String) As Object
			Dim configPage As Object = Nothing
			Dim str As String = pageName
			If (Operators.CompareString(str, Me.ConfigPage.PageName, False) = 0) Then
				configPage = Me.ConfigPage
			ElseIf (Operators.CompareString(str, Me.StatusPage.PageName, False) = 0) Then
				configPage = Me.StatusPage
			ElseIf (Operators.CompareString(str, Me.HelpPage.PageName, False) = 0) Then
				configPage = Me.HelpPage
			ElseIf (Operators.CompareString(str, Me.TestingPage.PageName, False) <> 0) Then
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Unknown page selected = [", pageName, "] Defaulting to config page!"), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				configPage = Me.ConfigPage
			Else
				configPage = Me.TestingPage
			End If
			Return configPage
		End Function

		Public Function SetCoolSetpoint(ByVal tName As String, ByVal setPoint As Short) As String
			Dim str As String = Nothing
			Try
				Dim [integer] As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_COOL", "Cool"))
				Dim num As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_HEAT", "Heat"))
				If (setPoint = [integer]) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log("SetCoolSetpoint: Current and requested cool set points match.  No change required.", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				ElseIf (Not (setPoint >= HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsCoolSetLow And setPoint <= HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsCoolSetHigh And setPoint > num)) Then
					str = "SetCoolSetpoint: Cool Setpoint has to be within the low/high bounds as configured and greater than the heat setpoint"
					HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				Else
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetCoolSetpoint: ", tName, " Cool SetPoint = ", Conversions.ToString(CInt(setPoint))), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
					HSPI_INSTEON_THERMOSTAT.utils.myTstat.SetTstatPedByName(tName, "HSREF_COOL", "Cool", setPoint, Nothing)
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.UpdateTstatCoolSetPoint(tName, setPoint)
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = String.Concat("SetCoolSetpoint: ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function SetFanAuto(ByVal tName As String) As String
			Dim str As String
			Dim flag As Boolean = False
			Dim str1 As String = Nothing
			Dim item As Collection = Nothing
			Try
				item = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
				flag = True
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str1 = String.Concat("SetFanAuto: Thermostat [", tName, "] was not found.")
				HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				str = str1
				ProjectData.ClearProjectError()
			End Try
			If (Not flag) Then
				Return str
			End If
			flag = False
			Try
				Dim [integer] As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatHvacPedByTstatHvac(item, "HSREF_FAN", "Fan"))
				If ([integer] <> 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetFanAuto: ", tName, " Fan = ", HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(0)), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					HSPI_INSTEON_THERMOSTAT.utils.myTstat.SetTstatHvacPedByTstatHvac(item, "HSREF_FAN", "Fan", 0, Nothing)
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.UpdateTstatFan(tName, 0)
				Else
					Dim strArrays() As String = { "SetFanAuto: ", tName, " Fan = ", HSPI_INSTEON_THERMOSTAT.utils.gFanOpts([integer]), " no change so skipping device update" }
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				str1 = String.Concat("SetFanAuto: ", exception1.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str1
		End Function

		Public Function SetFanOn(ByVal tName As String) As String
			Dim str As String
			Dim flag As Boolean = False
			Dim str1 As String = Nothing
			Dim item As Collection = Nothing
			Try
				item = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
				flag = True
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str1 = String.Concat("SetFanOn: Thermostat [", tName, "] was not found.")
				HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				str = str1
				ProjectData.ClearProjectError()
			End Try
			If (Not flag) Then
				Return str
			End If
			flag = False
			Try
				Dim [integer] As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatHvacPedByTstatHvac(item, "HSREF_FAN", "Fan"))
				If ([integer] <> 1) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetFanOn: ", tName, " Fan = ", HSPI_INSTEON_THERMOSTAT.utils.gFanOpts(1)), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					HSPI_INSTEON_THERMOSTAT.utils.myTstat.SetTstatHvacPedByTstatHvac(item, "HSREF_FAN", "Fan", 1, Nothing)
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.UpdateTstatFan(tName, 1)
				Else
					Dim strArrays() As String = { "SetFanOn: ", tName, " Fan = ", HSPI_INSTEON_THERMOSTAT.utils.gFanOpts([integer]), " no change so skipping device update" }
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				str1 = String.Concat("SetFanOn: ", exception1.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str1
		End Function

		Public Function SetFanToggle(ByVal tName As String) As String
			Dim str As String
			Dim flag As Boolean = False
			Dim str1 As String = Nothing
			Dim item As Collection = Nothing
			Try
				item = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
				flag = True
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str1 = String.Concat("SetFanToggle: Thermostat [", tName, "] was not found.")
				HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				str = str1
				ProjectData.ClearProjectError()
			End Try
			If (Not flag) Then
				Return str
			End If
			flag = False
			Try
				str1 = If(Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatHvacPedByTstatHvac(item, "HSREF_FAN", "Fan")) <> 0, Me.SetFanAuto(tName), Me.SetFanOn(tName))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				str1 = String.Concat("SetFanToggle: ", exception1.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str1
		End Function

		Public Function SetHeatSetpoint(ByVal tName As String, ByVal setPoint As Short) As String
			Dim str As String = Nothing
			Try
				Dim [integer] As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_HEAT", "Heat"))
				Dim num As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_COOL", "Cool"))
				If (setPoint = [integer]) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log("SetHeatSetpoint: Current and requested heat set points match.  No change required.", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				ElseIf (Not (setPoint >= HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHeatSetLow And setPoint <= HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHeatSetHigh And setPoint < num)) Then
					str = "SetHeatSetpoint: Heat Setpoint has to be within the low/high bounds as configured and less than the cool setpoint"
					HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				Else
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetHeatSetpoint: ", tName, " Heat SetPoint = ", Conversions.ToString(CInt(setPoint))), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
					HSPI_INSTEON_THERMOSTAT.utils.myTstat.SetTstatPedByName(tName, "HSREF_HEAT", "Heat", setPoint, Nothing)
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.UpdateTstatHeatSetPoint(tName, setPoint)
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = String.Concat("SetHeatSetpoint: ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function SetHold(ByVal tName As String) As String
			Dim str As String
			Dim flag As Boolean = False
			Dim str1 As String = Nothing
			Dim item As Collection = Nothing
			Try
				item = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
				flag = True
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str1 = String.Concat("SetHold: Thermostat [", tName, "] was not found.")
				HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				str = str1
				ProjectData.ClearProjectError()
			End Try
			If (Not flag) Then
				Return str
			End If
			flag = False
			Try
				If (Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatHvacPedByTstatHvac(item, "HSREF_HOLD", "Hold")) <> 1) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetHold: Thermostat ", tName, " set to hold."), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					HSPI_INSTEON_THERMOSTAT.utils.myTstat.SetTstatHvacPedByTstatHvac(item, "HSREF_HOLD", "Hold", 1, Nothing)
				Else
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetHold: Thermostat ", tName, " already set to hold.  No change required."), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				str1 = String.Concat("SetHold: ", exception1.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str1
		End Function

		Public Function SetHoldToggle(ByVal tName As String) As String
			Dim str As String
			Dim flag As Boolean = False
			Dim str1 As String = Nothing
			Dim item As Collection = Nothing
			Try
				item = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
				flag = True
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str1 = String.Concat("SetHoldToggle: Thermostat [", tName, "] was not found.")
				HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				str = str1
				ProjectData.ClearProjectError()
			End Try
			If (Not flag) Then
				Return str
			End If
			flag = False
			Try
				str1 = If(Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatHvacPedByTstatHvac(item, "HSREF_HOLD", "Hold")) <> 1, Me.SetHold(tName), Me.SetRun(tName))
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				str1 = String.Concat("SetHoldToggle: ", exception1.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str1
		End Function

		Public Sub SetIOMulti(ByVal colSend As List(Of CAPI.CAPIControl))
			Dim enumerator As List(Of CAPI.CAPIControl).Enumerator = New List(Of CAPI.CAPIControl).Enumerator()
			Try
				Try
					enumerator = colSend.GetEnumerator()
					While enumerator.MoveNext()
						Dim current As CAPI.CAPIControl = enumerator.Current
						Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(current.Ref), DeviceClass)
						Dim deviceTypeGet As DeviceTypeInfo_m.DeviceTypeInfo = deviceByRef.get_DeviceType_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
						Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
						If (CLng(current.Ref) = HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef) Then
							Dim cAPIControl As CAPI.CAPIControl = HSPI_INSTEON_THERMOSTAT.utils.hs.CAPIGetControl(current.Ref)(CInt(Math.Round(current.ControlValue)))
							Me.SetMasterProgram(cAPIControl.Label, True)
						ElseIf (deviceTypeGet.Device_Type = 99) Then
							Dim str As String = Conversions.ToString(plugExtraDataGet.GetNamed("ParentName"))
							Dim cAPIControl1 As CAPI.CAPIControl = HSPI_INSTEON_THERMOSTAT.utils.hs.CAPIGetControl(current.Ref)(CInt(Math.Round(current.ControlValue)))
							Me.SetProgram(str, cAPIControl1.Label, True)
						ElseIf (deviceTypeGet.Device_Type = 8) Then
							Dim controlValue As Double = current.ControlValue
							If (controlValue = 1) Then
								Me.SetHold(Conversions.ToString(plugExtraDataGet.GetNamed("ParentName")))
							ElseIf (controlValue <> 0) Then
								If (controlValue <> 2) Then
									Continue While
								End If
								Me.SetHoldToggle(Conversions.ToString(plugExtraDataGet.GetNamed("ParentName")))
							Else
								Me.SetRun(Conversions.ToString(plugExtraDataGet.GetNamed("ParentName")))
							End If
						ElseIf (deviceTypeGet.Device_Type = 3) Then
							Me.SetMode(Conversions.ToString(plugExtraDataGet.GetNamed("ParentName")), CInt(Math.Round(current.ControlValue)))
						ElseIf (deviceTypeGet.Device_Type = 4) Then
							Dim num As Double = current.ControlValue
							If (num = 0) Then
								Me.SetFanAuto(Conversions.ToString(plugExtraDataGet.GetNamed("ParentName")))
							ElseIf (num <> 1) Then
								If (num <> 2) Then
									Continue While
								End If
								Me.SetFanToggle(Conversions.ToString(plugExtraDataGet.GetNamed("ParentName")))
							Else
								Me.SetFanOn(Conversions.ToString(plugExtraDataGet.GetNamed("ParentName")))
							End If
						ElseIf (deviceTypeGet.Device_Type = 6) Then
							Dim str1 As String = Conversions.ToString(plugExtraDataGet.GetNamed("ParentName"))
							Dim [integer] As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(str1, "HSREF_HEAT", "Heat"))
							Dim integer1 As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(str1, "HSREF_COOL", "Cool"))
							Select Case deviceTypeGet.Device_SubType
								Case 1
									Dim label As String = current.Label
									If (Operators.CompareString(label, "+", False) = 0) Then
										Me.SetHeatSetpoint(str1, CShort(([integer] + 1)))
										Continue Select
									ElseIf (Operators.CompareString(label, "-", False) <> 0) Then
										Me.SetHeatSetpoint(str1, CShort(Math.Round(current.ControlValue)))
										Continue Select
									Else
										Me.SetHeatSetpoint(str1, CShort(([integer] - 1)))
										Continue Select
									End If
								Case 2
									Dim label1 As String = current.Label
									If (Operators.CompareString(label1, "+", False) = 0) Then
										Me.SetCoolSetpoint(str1, CShort((integer1 + 1)))
									ElseIf (Operators.CompareString(label1, "-", False) <> 0) Then
										Me.SetCoolSetpoint(str1, CShort(Math.Round(current.ControlValue)))
									Else
										Me.SetCoolSetpoint(str1, CShort((integer1 - 1)))
									End If
									Continue Select
								Case Else
									Continue Select
							End Select
						ElseIf (deviceTypeGet.Device_Type = 12) Then
							HSPI_INSTEON_THERMOSTAT.utils.myTstat.AdjustHvacCounterByPedRef(current.Ref, "MaintenanceInterval", 0, True)
						ElseIf (deviceTypeGet.Device_Type <> 7) Then
							HSPI_INSTEON_THERMOSTAT.utils.Log("Unsupported CAPI request:", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
							HSPI_INSTEON_THERMOSTAT.utils.Log("CAPI [CC.Ref] [Idx] [Val] [Row] [Col] [Span] [Type] [String] [Label]", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
							Dim strArrays() As String = { "     [", Conversions.ToString(current.Ref), "] [", Conversions.ToString(current.CCIndex), "] [", Conversions.ToString(current.ControlValue), "] [", Conversions.ToString(current.ControlLoc_Row), "] [", Conversions.ToString(current.ControlLoc_Column), "] [", Conversions.ToString(current.ControlLoc_ColumnSpan), "] [", Conversions.ToString(CInt(current.ControlType)), "] [", current.ControlString, "] [", current.Label, "]" }
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
						Else
							Select Case deviceTypeGet.Device_SubType
								Case 1
									HSPI_INSTEON_THERMOSTAT.utils.myTstat.AdjustHvacCounterByPedRef(current.Ref, "Heat", 0, True)
									Continue Select
								Case 2
									HSPI_INSTEON_THERMOSTAT.utils.myTstat.AdjustHvacCounterByPedRef(current.Ref, "Cool", 0, True)
									Continue Select
							End Select
							Throw New System.Exception("Unsupported CAPI request for an HVAC device")
						End If
					End While
				Finally
					(DirectCast(enumerator, IDisposable)).Dispose()
				End Try
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Error during CAPI request - ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			HSPI_INSTEON_THERMOSTAT.utils.hs.SaveEventsDevices()
		End Sub

		Public Function SetMasterProgram(ByVal pName As String, Optional ByVal reset_flg As Boolean = True) As String
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Dim str As String = Nothing
			Try
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetMasterProgram: ", pName), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
				Dim num As Integer = 0
				Dim cAPIControlArray As CAPI.CAPIControl() = HSPI_INSTEON_THERMOSTAT.utils.hs.CAPIGetControl(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef))
				Dim num1 As Integer = 0
				While num1 < CInt(cAPIControlArray.Length)
					Dim cAPIControl As CAPI.CAPIControl = cAPIControlArray(num1)
					If (Operators.CompareString(cAPIControl.Label, pName, False) <> 0) Then
						num1 = num1 + 1
					Else
						num = CInt(Math.Round(cAPIControl.ControlValue))
						Exit While
					End If
				End While
				Dim deviceByRef As DeviceClass = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.GetDeviceByRef(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef)), DeviceClass)
				Dim plugExtraDataGet As PlugExtraData.clsPlugExtraData = deviceByRef.get_PlugExtraData_Get(HSPI_INSTEON_THERMOSTAT.utils.hs)
				plugExtraDataGet.RemoveNamed("Program")
				plugExtraDataGet.AddNamed("Program", pName)
				deviceByRef.set_PlugExtraData_Set(HSPI_INSTEON_THERMOSTAT.utils.hs, plugExtraDataGet)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceLastChange(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef), DateAndTime.Now)
				HSPI_INSTEON_THERMOSTAT.utils.hs.SetDeviceValueByRef(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef), CDbl(num), True)
				Me.TriggerFire(1, 1, "Master Program", pName)
				Try
					enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats.Values.GetEnumerator()
					While enumerator.MoveNext()
						Dim current As Collection = enumerator.Current
						str = String.Concat(str, Me.SetProgram(Conversions.ToString(current("Name")), pName, reset_flg))
					End While
				Finally
					(DirectCast(enumerator, IDisposable)).Dispose()
				End Try
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = String.Concat("SetMasterProgram: ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function SetMode(ByVal tName As String, ByVal mode As Integer) As String
			Dim str As String = Nothing
			Try
				Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
				Dim [integer] As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatHvacPedByTstatHvac(item, "HSREF_MODE", "Mode"))
				If ((mode = 7 Or mode = 6) And Not HSPI_INSTEON_THERMOSTAT.utils.isVenstarDEVCAT(item)) Then
					str = String.Concat("SetMode: ", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(mode), " is not a valid mode for ", tName)
					HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ElseIf (mode <> [integer]) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetMode: ", tName, " Mode = ", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(mode)), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
					HSPI_INSTEON_THERMOSTAT.utils.myTstat.SetTstatPedByName(tName, "HSREF_MODE", "Mode", mode, Nothing)
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.UpdateTstatMode(tName, mode)
				Else
					Dim strArrays() As String = { "SetMode: ", tName, " Mode = ", HSPI_INSTEON_THERMOSTAT.utils.gModeOpts(mode), " no change so skipping device update" }
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(strArrays), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = String.Concat("SetMode: ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function SetModeByString(ByVal tName As String, ByVal mode As String) As String
			Dim str As String = Nothing
			Try
				Dim lower As String = mode.ToLower()
				If (Operators.CompareString(lower, "Off".ToLower(), False) = 0) Then
					str = Me.SetMode(tName, 0)
				ElseIf (Operators.CompareString(lower, "Heat".ToLower(), False) = 0) Then
					str = Me.SetMode(tName, 1)
				ElseIf (Operators.CompareString(lower, "Cool".ToLower(), False) = 0) Then
					str = Me.SetMode(tName, 2)
				ElseIf (Operators.CompareString(lower, "Auto".ToLower(), False) = 0) Then
					str = Me.SetMode(tName, 3)
				ElseIf (Operators.CompareString(lower, "Program".ToLower(), False) = 0) Then
					str = Me.SetMode(tName, 5)
				ElseIf (Operators.CompareString(lower, "Program-Heat".ToLower(), False) = 0) Then
					str = Me.SetMode(tName, 6)
				ElseIf (Operators.CompareString(lower, "Program-Cool".ToLower(), False) <> 0) Then
					str = String.Concat("SetModeByString: Invalid mode string passed in: ", mode)
					HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				Else
					str = Me.SetMode(tName, 7)
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = String.Concat("SetModeByString: ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function SetModeNext(ByVal tName As String) As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.plugin::SetModeNext(System.String)
			' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
			' 
			' Product version: 2014.1.225.0
			' Exception in: System.String SetModeNext(System.String)
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

		End Function

		Public Function SetModePrev(ByVal tName As String) As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.plugin::SetModePrev(System.String)
			' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
			' 
			' Product version: 2014.1.225.0
			' Exception in: System.String SetModePrev(System.String)
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

		End Function

		Public Function SetProgram(ByVal tName As String, ByVal pName As String, Optional ByVal reset_flg As Boolean = True) As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.plugin::SetProgram(System.String,System.String,System.Boolean)
			' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
			' 
			' Product version: 2014.1.225.0
			' Exception in: System.String SetProgram(System.String,System.String,System.Boolean)
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

		End Function

		Public Function SetProgramParams(ByVal ParmString As String) As String
			Dim str As String = Nothing
			Try
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetProgramParams: ", ParmString), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
				Dim strArrays As String() = Strings.Split(ParmString, " ", -1, CompareMethod.Binary)
				Dim str1 As String = String.Join(" ", strArrays, 0, Information.UBound(strArrays, 1))
				Dim str2 As String = strArrays(Information.UBound(strArrays, 1))
				str = Me.SetProgram(str1, str2, True)
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				str = String.Concat("SetProgramParams: ", ParmString, " : ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function SetRun(ByVal tName As String) As String
			Dim str As String
			Dim flag As Boolean = False
			Dim str1 As String = Nothing
			Dim item As Collection = Nothing
			Try
				item = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(tName)
				flag = True
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str1 = String.Concat("SetRun: Thermostat [", tName, "] was not found.")
				HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				str = str1
				ProjectData.ClearProjectError()
			End Try
			If (Not flag) Then
				Return str
			End If
			flag = False
			Try
				If (Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatHvacPedByTstatHvac(item, "HSREF_HOLD", "Hold")) <> 0) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetRun: Thermostat ", tName, " set to run."), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
					HSPI_INSTEON_THERMOSTAT.utils.myTstat.SetTstatHvacPedByTstatHvac(item, "HSREF_HOLD", "Hold", 0, Nothing)
					str1 = String.Concat(str1, Me.SetProgram(tName, Nothing, True))
				Else
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetRun: Thermostat ", tName, " already set to run.  No change required."), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				str1 = String.Concat("SetRun: ", exception1.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str1
		End Function

		Public Function SetTempDown(ByVal tName As String) As String
			Dim str As String = Nothing
			Try
				Dim [integer] As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_HEAT", "Heat"))
				Dim num As Integer = [integer] - 1
				Dim integer1 As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_COOL", "Cool"))
				Dim num1 As Integer = integer1 - 1
				If (num < HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHeatSetLow Or num > HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHeatSetHigh) Then
					str = "SetTempDown: Heat Setpoint has to be within the low/high bounds as configured"
					HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ElseIf (Not (num1 < HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsCoolSetLow Or num1 > HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsCoolSetHigh)) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetTempDown: ", tName, " Heat SetPoint = ", Conversions.ToString(num)), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetTempDown: ", tName, " Cool SetPoint = ", Conversions.ToString(num1)), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
					HSPI_INSTEON_THERMOSTAT.utils.myTstat.SetTstatPedByName(tName, "HSREF_HEAT", "Heat", num, Nothing)
					HSPI_INSTEON_THERMOSTAT.utils.myTstat.SetTstatPedByName(tName, "HSREF_COOL", "Cool", num1, Nothing)
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.UpdateTstatTempDown(tName)
				Else
					str = "SetTempDown: Cool Setpoint has to be within the low/high bounds as configured"
					HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = String.Concat("SetTempDown: ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function SetTempUp(ByVal tName As String) As String
			Dim str As String = Nothing
			Try
				Dim [integer] As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_HEAT", "Heat"))
				Dim num As Integer = [integer] + 1
				Dim integer1 As Integer = Conversions.ToInteger(HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatPedByName(tName, "HSREF_COOL", "Cool"))
				Dim num1 As Integer = integer1 + 1
				If (num < HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHeatSetLow Or num > HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsHeatSetHigh) Then
					str = "SetTempUp: Heat Setpoint has to be within the low/high bounds as configured"
					HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ElseIf (Not (num1 < HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsCoolSetLow Or num1 > HSPI_INSTEON_THERMOSTAT.utils.myConfig.BoundsCoolSetHigh)) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetTempUp: ", tName, " Heat SetPoint = ", Conversions.ToString(num)), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
					HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("SetTempUp: ", tName, " Cool SetPoint = ", Conversions.ToString(num1)), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Info)
					HSPI_INSTEON_THERMOSTAT.utils.myTstat.SetTstatPedByName(tName, "HSREF_HEAT", "Heat", num, Nothing)
					HSPI_INSTEON_THERMOSTAT.utils.myTstat.SetTstatPedByName(tName, "HSREF_COOL", "Cool", num1, Nothing)
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.UpdateTstatTempUp(tName)
				Else
					str = "SetTempUp: Cool Setpoint has to be within the low/high bounds as configured"
					HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = String.Concat("SetTempUp: ", exception.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function SetTStatSetpoint(ByVal ParmString As String) As String
			Dim str As String
			Dim flag As Boolean = False
			Dim str1 As String = Nothing
			Try
				Dim strArrays As String() = Strings.Split(ParmString, " ", -1, CompareMethod.Binary)
				If (Information.UBound(strArrays, 1) >= 2) Then
					Dim item As Collection = Nothing
					Try
						item = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(String.Join(" ", strArrays, 0, Information.UBound(strArrays, 1) - 1))
					Catch exception As System.Exception
						ProjectData.SetProjectError(exception)
						str1 = String.Concat("SetTStatSetpoint: Thermostat [", strArrays(0), "] was not found.")
						HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
						str = str1
						ProjectData.ClearProjectError()
						flag = True
					End Try
					If (Not flag) Then
						Dim lower As String = strArrays(Information.UBound(strArrays, 1) - 1).ToLower()
						Dim str2 As String = strArrays(Information.UBound(strArrays, 1))
						If (Operators.CompareString(lower, "heat", False) = 0) Then
							str1 = Me.SetHeatSetpoint(Conversions.ToString(item("Name")), Conversions.ToShort(str2))
						ElseIf (Operators.CompareString(lower, "cool", False) = 0) Then
							str1 = Me.SetCoolSetpoint(Conversions.ToString(item("Name")), Conversions.ToShort(str2))
						End If
					End If
				Else
					str1 = "SetTStatSetpoint: Too few arguments"
					HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
					str = str1
					flag = True
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				str1 = String.Concat("SetTStatSetpoint: ", exception1.Message)
				HSPI_INSTEON_THERMOSTAT.utils.Log(str1, HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			If (Not flag) Then
				Return str1
			End If
			flag = False
			Return str
		End Function

		Public Sub ShutdownIO()
			Dim enumerator As SortedDictionary(Of String, Collection).ValueCollection.Enumerator = New SortedDictionary(Of String, Collection).ValueCollection.Enumerator()
			Try
				Try
					enumerator = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats.Values.GetEnumerator()
					While enumerator.MoveNext()
						Dim current As Collection = enumerator.Current
						Try
							HSPI_INSTEON_THERMOSTAT.utils.myInsteon.UnregisterTstat(current)
						Catch exception As System.Exception
							ProjectData.SetProjectError(exception)
							ProjectData.ClearProjectError()
						End Try
					End While
				Finally
					(DirectCast(enumerator, IDisposable)).Dispose()
				End Try
				HSPI_INSTEON_THERMOSTAT.utils.hs.SaveEventsDevices()
				HSPI_INSTEON_THERMOSTAT.utils.bShutDown = True
			Catch exception2 As System.Exception
				ProjectData.SetProjectError(exception2)
				Dim exception1 As System.Exception = exception2
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("Error ending Insteon Thermostat Plug-In : ", exception1.ToString()), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Function TriggerBuildUI(ByVal sUnique As String, ByVal TrigInfo As IPlugInAPI.strTrigActInfo) As String
			' 
			' Current member / type: System.String HSPI_INSTEON_THERMOSTAT.plugin::TriggerBuildUI(System.String,HomeSeerAPI.IPlugInAPI/strTrigActInfo)
			' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
			' 
			' Product version: 2014.1.225.0
			' Exception in: System.String TriggerBuildUI(System.String,HomeSeerAPI.IPlugInAPI/strTrigActInfo)
			' 
			' Object reference not set to an instance of an object.
			'    at ..( , Int32 , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\ObjectInitialisationPattern.cs:line 78
			'    at ..( , Int32& , & , Int32& ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatterns\BaseInitialisationPattern.cs:line 33
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 60
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 61
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 363
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 67
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..Visit[,]( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 288
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 319
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 339
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 36
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 61
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 363
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 67
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..Visit[,]( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 288
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 319
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 339
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 36
			'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 61
			'    at ..Visit( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Ast\BaseCodeTransformer.cs:line 278
			'    at ..( ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Steps\CodePatternsStep.cs:line 30
			'    at ..(MethodBody , ILanguage ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 83
			'    at ..( , ILanguage , MethodBody , & ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 99
			'    at ..(MethodBody , ILanguage , & ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 62
			'    at ..(ILanguage , MethodDefinition ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 116
			' 
			' mailto: JustDecompilePublicFeedback@telerik.com

		End Function

		Public Function TriggerCount() As Integer
			Return Me.triggers.Count
		End Function

		Public Sub TriggerFire(ByVal TAN As Integer, ByVal SubTAN As Integer, ByVal tName As String, Optional ByVal param1 As String = Nothing)
			Dim enumerator As Dictionary(Of String, Object).KeyCollection.Enumerator = New Dictionary(Of String, Object).KeyCollection.Enumerator()
			Dim str As String()
			Dim flag As Boolean = False
			Try
				Dim strTrigActInfoArray As IPlugInAPI.strTrigActInfo() = HSPI_INSTEON_THERMOSTAT.utils.callback.TriggerMatches("Insteon Thermostat", TAN, SubTAN)
				For i As Integer = 0 To CInt(strTrigActInfoArray.Length)
					Dim _strTrigActInfo As IPlugInAPI.strTrigActInfo = strTrigActInfoArray(i)
					Try
						Dim _trigger As classes.trigger = New classes.trigger()
						If (_strTrigActInfo.DataIn Is Nothing) Then
							_trigger = New classes.trigger()
						Else
							Dim obj As Object = _trigger
							HSPI_INSTEON_THERMOSTAT.utils.DeSerializeObject(_strTrigActInfo.DataIn, obj)
							_trigger = DirectCast(obj, classes.trigger)
						End If
						Dim str1 As String = ""
						Dim str2 As String = ""
						Try
							enumerator = _trigger.Keys.GetEnumerator()
							While enumerator.MoveNext()
								Dim current As String = enumerator.Current
								Dim flag1 As Boolean = True
								If (flag1 <> Strings.InStr(current, String.Concat("TSTAT_", Conversions.ToString(_strTrigActInfo.UID)), CompareMethod.Binary) > 0) Then
									If (flag1 <> Strings.InStr(current, String.Concat("PROGRAM_", Conversions.ToString(_strTrigActInfo.UID)), CompareMethod.Binary) > 0) Then
										Continue While
									End If
									str2 = Conversions.ToString(_trigger(current))
								Else
									str1 = Conversions.ToString(_trigger(current))
								End If
							End While
						Finally
							(DirectCast(enumerator, IDisposable)).Dispose()
						End Try
						If (TAN <> 1) Then
							str = New String() { "TriggerFire : Unhandled TAN : TAN=[", Conversions.ToString(TAN), "] SubTAN=[", Conversions.ToString(SubTAN), "] thermostat=[", tName, "] param1=[", param1, "]" }
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
						ElseIf (SubTAN <> 1) Then
							str = New String() { "TriggerFire : Unhandled SubTAN : TAN=[", Conversions.ToString(TAN), "] SubTAN=[", Conversions.ToString(SubTAN), "] thermostat=[", tName, "] param1=[", param1, "]" }
							HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
						ElseIf (Operators.CompareString(str1, tName, False) = 0 AndAlso Operators.CompareString(str2, param1, False) = 0) Then
							HSPI_INSTEON_THERMOSTAT.utils.callback.TriggerFire("Insteon Thermostat", _strTrigActInfo)
							flag = True
						End If
					Catch exception1 As System.Exception
						ProjectData.SetProjectError(exception1)
						Dim exception As System.Exception = exception1
						str = New String() { "TriggerFire : TAN=[", Conversions.ToString(TAN), "] SubTAN=[", Conversions.ToString(SubTAN), "] thermostat=[", tName, "] param1=[", param1, "] : ", exception.Message }
						HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
						ProjectData.ClearProjectError()
					End Try
					flag = False
				Next

			Catch exception3 As System.Exception
				ProjectData.SetProjectError(exception3)
				Dim exception2 As System.Exception = exception3
				str = New String() { "TriggerFire : TAN=[", Conversions.ToString(TAN), "] SubTAN=[", Conversions.ToString(SubTAN), "] thermostat=[", tName, "] param1=[", param1, "] : ", exception2.Message }
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat(str), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Public Function TriggerFormatUI(ByVal TrigInfo As IPlugInAPI.strTrigActInfo) As String
			Dim enumerator As Dictionary(Of String, Object).KeyCollection.Enumerator = New Dictionary(Of String, Object).KeyCollection.Enumerator()
			Dim stringBuilder As System.Text.StringBuilder = New System.Text.StringBuilder()
			Dim str As String = TrigInfo.UID.ToString()
			Dim tANumber As Integer = TrigInfo.TANumber
			Dim subTANumber As Integer = TrigInfo.SubTANumber
			If (TrigInfo.DataIn Is Nothing) Then
				Me.trigger = New classes.trigger()
			Else
				Dim obj As Object = Me.trigger
				HSPI_INSTEON_THERMOSTAT.utils.DeSerializeObject(TrigInfo.DataIn, obj)
				Me.trigger = DirectCast(obj, classes.trigger)
			End If
			Dim item As Object = Me.triggers(tANumber)
			Dim objArray() As Object = { subTANumber }
			Conversions.ToString(NewLateBinding.LateIndexGet(item, objArray, Nothing))
			Dim str1 As String = ""
			Dim str2 As String = ""
			Try
				enumerator = Me.trigger.Keys.GetEnumerator()
				While enumerator.MoveNext()
					Dim current As String = enumerator.Current
					Dim flag As Boolean = True
					If (flag <> Strings.InStr(current, String.Concat("TSTAT_", str), CompareMethod.Binary) > 0) Then
						If (flag <> Strings.InStr(current, String.Concat("PROGRAM_", str), CompareMethod.Binary) > 0) Then
							Continue While
						End If
						str2 = Conversions.ToString(Me.trigger(current))
					Else
						str1 = Conversions.ToString(Me.trigger(current))
					End If
				End While
			Finally
				(DirectCast(enumerator, IDisposable)).Dispose()
			End Try
			stringBuilder.Append("Insteon Thermostat")
			stringBuilder.Append(" : thermostat [")
			stringBuilder.Append(str1)
			stringBuilder.Append("] ")
			If (subTANumber = 1) Then
				stringBuilder.Append("sets program to [")
				stringBuilder.Append(str2)
				stringBuilder.Append("]")
			End If
			Return stringBuilder.ToString()
		End Function

		Public Function TriggerProcessPostUI(ByVal PostData As System.Collections.Specialized.NameValueCollection, ByVal TrigInfo As IPlugInAPI.strTrigActInfo) As IPlugInAPI.strMultiReturn
			Dim _strMultiReturn As IPlugInAPI.strMultiReturn
			Dim obj As Object
			Dim enumerator As IEnumerator = Nothing
			Dim flag As Boolean = False
			Dim dataIn As IPlugInAPI.strMultiReturn = New IPlugInAPI.strMultiReturn()
			Dim str As String = TrigInfo.UID.ToString()
			dataIn.sResult = ""
			dataIn.DataOut = TrigInfo.DataIn
			dataIn.TrigActInfo = TrigInfo
			If (PostData Is Nothing) Then
				Return dataIn
			End If
			If (PostData.Count < 1) Then
				Return dataIn
			End If
			If (TrigInfo.DataIn Is Nothing) Then
				Me.trigger = New classes.trigger()
			Else
				obj = Me.trigger
				HSPI_INSTEON_THERMOSTAT.utils.DeSerializeObject(TrigInfo.DataIn, obj)
				Me.trigger = DirectCast(obj, classes.trigger)
			End If
			Dim nameValueCollection As System.Collections.Specialized.NameValueCollection = PostData
			Try
				Try
					enumerator = nameValueCollection.Keys.GetEnumerator()
					While enumerator.MoveNext()
						Dim str1 As String = Conversions.ToString(enumerator.Current)
						If (str1 IsNot Nothing) Then
							If (Not String.IsNullOrEmpty(str1.Trim())) Then
								Dim flag1 As Boolean = True
								If (flag1 = Strings.InStr(str1, String.Concat("TSTAT_", str), CompareMethod.Binary) > 0) Then
									Me.trigger.Add(nameValueCollection(str1), str1)
								ElseIf (flag1 <> Strings.InStr(str1, String.Concat("PROGRAM_", str), CompareMethod.Binary) > 0) Then
									HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("TriggerProcessPostUI: Unexpected skey: ", str1), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
								Else
									Me.trigger.Add(nameValueCollection(str1), str1)
								End If
							End If
						End If
					End While
				Finally
					If (TypeOf enumerator Is IDisposable) Then
						(TryCast(enumerator, IDisposable)).Dispose()
					End If
				End Try
				obj = Me.trigger
				Dim flag2 As Boolean = HSPI_INSTEON_THERMOSTAT.utils.SerializeObject(obj, dataIn.DataOut)
				Me.trigger = DirectCast(obj, classes.trigger)
				If (flag2) Then
					flag = True
				Else
					dataIn.sResult = "Insteon Thermostat Error, Serialization failed.  Trigger not added."
					_strMultiReturn = dataIn
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				dataIn.sResult = String.Concat("ERROR, Exception in Trigger UI of Insteon Thermostat: ", exception.Message)
				_strMultiReturn = dataIn
				ProjectData.ClearProjectError()
			End Try
			If (Not flag) Then
				Return _strMultiReturn
			End If
			flag = False
			dataIn.sResult = ""
			Return dataIn
		End Function

		Public Function TriggerReferencesDevice(ByVal TrigInfo As IPlugInAPI.strTrigActInfo, ByVal dvRef As Integer) As Boolean
			Dim flag As Boolean = False
			HSPI_INSTEON_THERMOSTAT.utils.Log(Conversions.ToString(Operators.ConcatenateObject(String.Concat(String.Concat(String.Concat(String.Concat("TriggerReferencesDevice : dvRef=", Conversions.ToString(dvRef)), " retVal="), Conversions.ToString(flag)), " "), HSPI_INSTEON_THERMOSTAT.utils.TAInfoToString(TrigInfo))), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Test)
			Return flag
		End Function

		Public Function TriggerTrue(ByVal TrigInfo As IPlugInAPI.strTrigActInfo) As Boolean
			Dim enumerator As Dictionary(Of String, Object).KeyCollection.Enumerator = New Dictionary(Of String, Object).KeyCollection.Enumerator()
			Dim flag As Boolean = False
			Try
				Dim tANumber As Integer = TrigInfo.TANumber
				Dim subTANumber As Integer = TrigInfo.SubTANumber
				If (Not Me.ValidSubTrig(tANumber, subTANumber)) Then
					HSPI_INSTEON_THERMOSTAT.utils.Log(Conversions.ToString(Operators.ConcatenateObject("TriggerTrue - trigger/subtrigger appears invalid - ", HSPI_INSTEON_THERMOSTAT.utils.TAInfoToString(TrigInfo))), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				Else
					If (TrigInfo.DataIn Is Nothing) Then
						Me.trigger = New classes.trigger()
					Else
						Dim obj As Object = Me.trigger
						HSPI_INSTEON_THERMOSTAT.utils.DeSerializeObject(TrigInfo.DataIn, obj)
						Me.trigger = DirectCast(obj, classes.trigger)
					End If
					Dim str As String = ""
					Dim str1 As String = ""
					Try
						enumerator = Me.trigger.Keys.GetEnumerator()
						While enumerator.MoveNext()
							Dim current As String = enumerator.Current
							Dim flag1 As Boolean = True
							If (flag1 <> Strings.InStr(current, String.Concat("TSTAT_", Conversions.ToString(TrigInfo.UID)), CompareMethod.Binary) > 0) Then
								If (flag1 <> Strings.InStr(current, String.Concat("PROGRAM_", Conversions.ToString(TrigInfo.UID)), CompareMethod.Binary) > 0) Then
									Continue While
								End If
								str1 = Conversions.ToString(Me.trigger(current))
							Else
								str = Conversions.ToString(Me.trigger(current))
							End If
						End While
					Finally
						(DirectCast(enumerator, IDisposable)).Dispose()
					End Try
					If (tANumber <> 1) Then
						HSPI_INSTEON_THERMOSTAT.utils.Log(Conversions.ToString(Operators.ConcatenateObject("TriggerTrue - trigger/subtrigger not supported as valid condition - ", HSPI_INSTEON_THERMOSTAT.utils.TAInfoToString(TrigInfo))), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
					ElseIf (subTANumber <> 1) Then
						HSPI_INSTEON_THERMOSTAT.utils.Log(Conversions.ToString(Operators.ConcatenateObject("TriggerTrue - trigger/subtrigger not supported as valid condition - ", HSPI_INSTEON_THERMOSTAT.utils.TAInfoToString(TrigInfo))), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
					ElseIf (Operators.CompareString(str, "Master Program", False) <> 0) Then
						Dim item As Collection = HSPI_INSTEON_THERMOSTAT.utils.myConfig.gTstats(str)
						Dim str2 As String = Conversions.ToString(item("Name"))
						Dim tstatHvacPedByTstatHvac As String = HSPI_INSTEON_THERMOSTAT.utils.myTstat.GetTstatHvacPedByTstatHvac(item, "HSREF_PROGRAM", "Program")
						flag = Operators.CompareString(str, str2, False) = 0 And Operators.CompareString(str1, tstatHvacPedByTstatHvac, False) = 0
					Else
						Dim cAPIStatu As CAPI.CAPIStatus = DirectCast(HSPI_INSTEON_THERMOSTAT.utils.hs.CAPIGetStatus(CInt(HSPI_INSTEON_THERMOSTAT.utils.masterProgramRef)), CAPI.CAPIStatus)
						Dim status As String = cAPIStatu.Status
						flag = Operators.CompareString(str1, status, False) = 0
					End If
				End If
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Dim exception As System.Exception = exception1
				HSPI_INSTEON_THERMOSTAT.utils.Log(String.Concat("TriggerTrue: ", exception.Message), HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Errors)
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Public Function ValidActInfo(ByVal ActInfo As IPlugInAPI.strTrigActInfo) As Boolean
			Dim flag As Boolean = False
			If (ActInfo.TANumber > 0 AndAlso ActInfo.TANumber <= Me.actions.Count) Then
				flag = True
			End If
			Return flag
		End Function

		Public Function ValidSubTrig(ByVal TrigIn As Integer, ByVal SubTrigIn As Integer) As Boolean
			Dim item As classes.trigger = Nothing
			Dim flag As Boolean = False
			If (Me.ValidTrig(TrigIn)) Then
				item = DirectCast(Me.triggers(TrigIn), classes.trigger)
				If (item IsNot Nothing AndAlso SubTrigIn > 0 AndAlso SubTrigIn <= item.Count) Then
					flag = True
				End If
			End If
			Return flag
		End Function

		Public Function ValidTrig(ByVal TrigIn As Integer) As Boolean
			Dim flag As Boolean
			flag = If(TrigIn <= 0 OrElse TrigIn > Me.triggers.Count, False, True)
			Return flag
		End Function

		Private Sub waitToRegister(ByVal sender As Object, ByVal e As ElapsedEventArgs)
			If (HSPI_INSTEON_THERMOSTAT.utils.hs.AppStarting(False)) Then
				HSPI_INSTEON_THERMOSTAT.utils.Log("HS3 still starting...", HSPI_INSTEON_THERMOSTAT.utils.LogLevel.Debug)
				Me.waitTimer.Start()
			ElseIf (Not Me.appStarted) Then
				Me.appStarted = True
				Me.waitTimer.Interval = 10000
				Me.waitTimer.Start()
			Else
				Me.waitTimer.[Stop]()
				If (HSPI_INSTEON_THERMOSTAT.utils.myInsteon.ConnectToInsteonPlugin()) Then
					HSPI_INSTEON_THERMOSTAT.utils.myInsteon.RegisterTstats()
				End If
				HSPI_INSTEON_THERMOSTAT.utils.bInitIOComplete = True
			End If
		End Sub
	End Class
End Namespace