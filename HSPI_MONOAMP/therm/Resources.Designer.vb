Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.CodeDom.Compiler
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.Resources
Imports System.Runtime.CompilerServices

Namespace HSPI_INSTEON_THERMOSTAT.My.Resources
	<CompilerGenerated>
	<DebuggerNonUserCode>
	<GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")>
	<HideModuleName>
	<StandardModule>
	Friend NotInheritable Class Resources
		Private Shared resourceMan As System.Resources.ResourceManager

		Private Shared resourceCulture As CultureInfo

		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Friend Shared Property Culture As CultureInfo
			Get
				Return HSPI_INSTEON_THERMOSTAT.My.Resources.Resources.resourceCulture
			End Get
			Set(ByVal value As CultureInfo)
				HSPI_INSTEON_THERMOSTAT.My.Resources.Resources.resourceCulture = value
			End Set
		End Property

		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Friend ReadOnly Shared Property ResourceManager As System.Resources.ResourceManager
			Get
				If (Object.ReferenceEquals(HSPI_INSTEON_THERMOSTAT.My.Resources.Resources.resourceMan, Nothing)) Then
					HSPI_INSTEON_THERMOSTAT.My.Resources.Resources.resourceMan = New System.Resources.ResourceManager("HSPI_INSTEON_THERMOSTAT.Resources", GetType(HSPI_INSTEON_THERMOSTAT.My.Resources.Resources).[Assembly])
				End If
				Return HSPI_INSTEON_THERMOSTAT.My.Resources.Resources.resourceMan
			End Get
		End Property
	End Class
End Namespace