Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization

Namespace HSPI_INSTEON_THERMOSTAT
	<StandardModule>
	Friend NotInheritable Class classes
		<Serializable>
		Public Class action
			Inherits classes.hsCollection
			Public Sub New()
				MyBase.New()
			End Sub

			Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
				MyBase.New(info, context)
			End Sub
		End Class

		<Serializable>
		Public Class hsCollection
			Inherits Dictionary(Of String, Object)
			Private KeyIndex As Collection

			Default Public Property Item(ByVal index As Integer) As Object
				Get
					Return MyBase(Conversions.ToString(Me.KeyIndex(index)))
				End Get
				Set(ByVal value As Object)
					MyBase(Conversions.ToString(Me.KeyIndex(index))) = RuntimeHelpers.GetObjectValue(value)
				End Set
			End Property

			Default Public Property Item(ByVal Key As String) As Object
				Get
					' 
					' Current member / type: System.Object HSPI_INSTEON_THERMOSTAT.classes/hsCollection::get_Item(System.String)
					' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
					' 
					' Product version: 2014.1.225.0
					' Exception in: System.Object get_Item(System.String)
					' 
					' Object reference not set to an instance of an object.
					'    at ..() in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\DTree\BaseDominatorTreeBuilder.cs:line 112
					'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\DTree\BaseDominatorTreeBuilder.cs:line 26
					'    at ..BuildTree( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\DTree\FastDominatorTreeBuilder.cs:line 25
					'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\DominatorTreeDependentStep.cs:line 18
					'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\Loops\LoopBuilder.cs:line 68
					'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\Loops\LoopBuilder.cs:line 59
					'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\Loops\LoopBuilder.cs:line 56
					'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\Loops\LoopBuilder.cs:line 56
					'    at ..() in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\LogicalFlowBuilderStep.cs:line 128
					'    at ..( ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\LogicalFlowBuilderStep.cs:line 51
					'    at ..(MethodBody , ILanguage ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 83
					'    at ..( , ILanguage , MethodBody , & ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 99
					'    at ..(MethodBody , ILanguage , & ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 62
					'    at ..(ILanguage , MethodDefinition ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 116
					' 
					' mailto: JustDecompilePublicFeedback@telerik.com

				End Get
				Set(ByVal value As Object)
					If (MyBase.ContainsKey(Key)) Then
						MyBase(Key) = RuntimeHelpers.GetObjectValue(value)
					Else
						Me.Add(RuntimeHelpers.GetObjectValue(value), Key)
					End If
				End Set
			End Property

			Public Shadows ReadOnly Property Keys(ByVal index As Integer) As Object
				Get
					Dim num As Integer = 0
					Dim enumerator As Dictionary(Of String, Object).KeyCollection.Enumerator = New Dictionary(Of String, Object).KeyCollection.Enumerator()
					Dim flag As Boolean = False
					Dim current As String = Nothing
					Try
						enumerator = MyBase.Keys.GetEnumerator()
						While enumerator.MoveNext()
							current = enumerator.Current
							If (num <> index) Then
								num = num + 1
							Else
								flag = True
							End If
							If (flag) Then
								Exit While
							End If
						End While
					Finally
						(DirectCast(enumerator, IDisposable)).Dispose()
					End Try
					flag = False
					Return current
				End Get
			End Property

			Public Sub New()
				MyBase.New()
				Me.KeyIndex = New Collection()
			End Sub

			Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
				MyBase.New(info, context)
				Me.KeyIndex = New Collection()
			End Sub

			Public Sub Add(ByVal value As Object, ByVal Key As String)
				If (MyBase.ContainsKey(Key)) Then
					MyBase(Key) = RuntimeHelpers.GetObjectValue(value)
				Else
					MyBase.Add(Key, RuntimeHelpers.GetObjectValue(value))
					Me.KeyIndex.Add(Key, Key, Nothing, Nothing)
				End If
			End Sub

			Public Sub Remove(ByVal Key As String)
				' 
				' Current member / type: System.Void HSPI_INSTEON_THERMOSTAT.classes/hsCollection::Remove(System.String)
				' File path: C:\Work\Monoprice\AMP_Owin\HSPI_MONOAMP\therm\HSPI_INSTEON_THERMOSTAT.exe
				' 
				' Product version: 2014.1.225.0
				' Exception in: System.Void Remove(System.String)
				' 
				' Object reference not set to an instance of an object.
				'    at ..() in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\DTree\BaseDominatorTreeBuilder.cs:line 112
				'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\DTree\BaseDominatorTreeBuilder.cs:line 26
				'    at ..BuildTree( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\DTree\FastDominatorTreeBuilder.cs:line 25
				'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\DominatorTreeDependentStep.cs:line 18
				'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\Loops\LoopBuilder.cs:line 68
				'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\Loops\LoopBuilder.cs:line 59
				'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\Loops\LoopBuilder.cs:line 56
				'    at ..( ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\Loops\LoopBuilder.cs:line 56
				'    at ..() in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\LogicalFlowBuilderStep.cs:line 128
				'    at ..( ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\LogicFlow\LogicalFlowBuilderStep.cs:line 51
				'    at ..(MethodBody , ILanguage ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:line 83
				'    at ..( , ILanguage , MethodBody , & ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 99
				'    at ..(MethodBody , ILanguage , & ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\Extensions.cs:line 62
				'    at ..(ILanguage , MethodDefinition ,  ) in c:\Builds\245\Behemoth\ReleaseBranch Production Build\Sources\Decompiler\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:line 116
				' 
				' mailto: JustDecompilePublicFeedback@telerik.com

			End Sub

			Public Sub Remove(ByVal Index As Integer)
				MyBase.Remove(Conversions.ToString(Me.KeyIndex(Index)))
				Me.KeyIndex.Remove(Index)
			End Sub
		End Class

		Public Enum parmReqLevel
			parmInvalid
			parmAllowed
			parmRequired
		End Enum

		<Serializable>
		Public Class trigger
			Inherits classes.hsCollection
			Public Sub New()
				MyBase.New()
			End Sub

			Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
				MyBase.New(info, context)
			End Sub
		End Class

		<Serializable>
		Public Class trigger_action_event
			Public Property Name As String

			Public Property Num As Integer

			Public Property Option1_Required As classes.parmReqLevel

			Public Property Option2_Required As classes.parmReqLevel

			Public Sub New(ByVal Num As Integer, ByVal Name As String, Optional ByVal Option1_Required As classes.parmReqLevel = 0, Optional ByVal Option2_Required As classes.parmReqLevel = 0)
				MyBase.New()
				Me.Num = Num
				Me.Name = Name
				Me.Option1_Required = Option1_Required
				Me.Option2_Required = Option2_Required
			End Sub

			Public Overrides Function ToString() As String
				Dim str As String = Nothing
				Dim str1 As String = Nothing
				Select Case Me.Option1_Required
					Case classes.parmReqLevel.parmInvalid
						str = "Invalid"
						Exit Select
					Case classes.parmReqLevel.parmAllowed
						str = "Allowed"
						Exit Select
					Case classes.parmReqLevel.parmRequired
						str = "Required"
						Exit Select
				End Select
				Select Case Me.Option2_Required
					Case classes.parmReqLevel.parmInvalid
						str1 = "Invalid"
						Exit Select
					Case classes.parmReqLevel.parmAllowed
						str1 = "Allowed"
						Exit Select
					Case classes.parmReqLevel.parmRequired
						str1 = "Required"
						Exit Select
				End Select
				Dim name() As String = { "Trigger Action Event: Num=[", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing }
				name(1) = Me.Num.ToString("X2")
				name(2) = "] Name=["
				name(3) = Me.Name
				name(4) = "] Option1? ["
				name(5) = str
				name(6) = "] Option2? ["
				name(7) = str1
				name(8) = "]"
				Return String.Concat(name)
			End Function
		End Class
	End Class
End Namespace