Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.CodeDom.Compiler
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Namespace HSPI_INSTEON_THERMOSTAT.My
	<GeneratedCode("MyTemplate", "10.0.0.0")>
	<HideModuleName>
	<StandardModule>
	Friend NotInheritable Class MyProject
		Private ReadOnly Shared m_ComputerObjectProvider As MyProject.ThreadSafeObjectProvider(Of MyComputer)

		Private ReadOnly Shared m_AppObjectProvider As MyProject.ThreadSafeObjectProvider(Of MyApplication)

		Private ReadOnly Shared m_UserObjectProvider As MyProject.ThreadSafeObjectProvider(Of Microsoft.VisualBasic.ApplicationServices.User)

		Private ReadOnly Shared m_MyWebServicesObjectProvider As MyProject.ThreadSafeObjectProvider(Of MyProject.MyWebServices)

		<HelpKeyword("My.Application")>
		Friend ReadOnly Shared Property Application As MyApplication
			<DebuggerHidden>
			Get
				Return MyProject.m_AppObjectProvider.GetInstance
			End Get
		End Property

		<HelpKeyword("My.Computer")>
		Friend ReadOnly Shared Property Computer As MyComputer
			<DebuggerHidden>
			Get
				Return MyProject.m_ComputerObjectProvider.GetInstance
			End Get
		End Property

		<HelpKeyword("My.User")>
		Friend ReadOnly Shared Property User As Microsoft.VisualBasic.ApplicationServices.User
			<DebuggerHidden>
			Get
				Return MyProject.m_UserObjectProvider.GetInstance
			End Get
		End Property

		<HelpKeyword("My.WebServices")>
		Friend ReadOnly Shared Property WebServices As MyProject.MyWebServices
			<DebuggerHidden>
			Get
				Return MyProject.m_MyWebServicesObjectProvider.GetInstance
			End Get
		End Property

		Shared Sub New()
			MyProject.m_ComputerObjectProvider = New MyProject.ThreadSafeObjectProvider(Of MyComputer)()
			MyProject.m_AppObjectProvider = New MyProject.ThreadSafeObjectProvider(Of MyApplication)()
			MyProject.m_UserObjectProvider = New MyProject.ThreadSafeObjectProvider(Of Microsoft.VisualBasic.ApplicationServices.User)()
			MyProject.m_MyWebServicesObjectProvider = New MyProject.ThreadSafeObjectProvider(Of MyProject.MyWebServices)()
		End Sub

		<EditorBrowsable(EditorBrowsableState.Never)>
		<MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")>
		Friend NotInheritable Class MyWebServices
			<DebuggerHidden>
			<EditorBrowsable(EditorBrowsableState.Never)>
			Public Sub New()
				MyBase.New()
			End Sub

			<DebuggerHidden>
			Private Shared Function Create__Instance__(Of T As New)(ByVal instance As T) As T
				If (instance Is Nothing) Then
					Return Activator.CreateInstance(Of T)()
				End If
				Return instance
			End Function

			<DebuggerHidden>
			Private Sub Dispose__Instance__(Of T)(ByRef instance As T)
				instance = Nothing
			End Sub

			<DebuggerHidden>
			<EditorBrowsable(EditorBrowsableState.Never)>
			Public Overrides Function Equals(ByVal o As Object) As Boolean
				Return Me.Equals(RuntimeHelpers.GetObjectValue(o))
			End Function

			<DebuggerHidden>
			<EditorBrowsable(EditorBrowsableState.Never)>
			Public Overrides Function GetHashCode() As Integer
				Return Me.GetHashCode()
			End Function

			<DebuggerHidden>
			<EditorBrowsable(EditorBrowsableState.Never)>
			Friend Shadows Function [GetType]() As Type
				Return GetType(MyProject.MyWebServices)
			End Function

			<DebuggerHidden>
			<EditorBrowsable(EditorBrowsableState.Never)>
			Public Overrides Function ToString() As String
				Return Me.ToString()
			End Function
		End Class

		<ComVisible(False)>
		<EditorBrowsable(EditorBrowsableState.Never)>
		Friend NotInheritable Class ThreadSafeObjectProvider(Of T As New)
			<CompilerGenerated>
			<ThreadStatic>
			Private Shared m_ThreadStaticValue As T

			Friend ReadOnly Property GetInstance As T
				<DebuggerHidden>
				Get
					If (MyProject.ThreadSafeObjectProvider(Of T).m_ThreadStaticValue Is Nothing) Then
						MyProject.ThreadSafeObjectProvider(Of T).m_ThreadStaticValue = Activator.CreateInstance(Of T)()
					End If
					Return MyProject.ThreadSafeObjectProvider(Of T).m_ThreadStaticValue
				End Get
			End Property

			<DebuggerHidden>
			<EditorBrowsable(EditorBrowsableState.Never)>
			Public Sub New()
				MyBase.New()
			End Sub
		End Class
	End Class
End Namespace