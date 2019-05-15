Import-Module **Path**\Export_Solution_BH.dll

Function LogWrite
				{
				   Param ([string]$logstring)
				   Add-content $Logfile -value $logstring
				}
Try{
	#Update name of solution in below line
	$solutionToExport = "**Path**\SolutionAutomation\Exportar\SolucionesExportar.xml"
	$Managed= $TRUE
	[xml]$XmlDocument = Get-Content -Path "**Path**\SolutionAutomation\Exportar\AuthFileConnectionDataExport.xml"	
	$rootFolder =  "**Path**\SolutionAutomation\Importar\"	
	$crmUrl = $XmlDocument.Authentication.crmUrl
	$organization=$XmlDocument.Authentication.organization
	Get-Export_Solution_BH -SolutionCreatePath $rootFolder -SolutionToExport $solutionToExport -ManagedSolution $Managed -CrmUrl $crmUrl -Organization $organization			
}
Catch
{
    $ErrorMessage = $_.Exception.Message
    Write-host "Error: $ErrorMessage"
	LogWrite "---------------ERROR---------------"
	LogWrite "Error: $ErrorMessage"
	LogWrite "---------------ERROR---------------"
}
