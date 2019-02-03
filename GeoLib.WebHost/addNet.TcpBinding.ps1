Import-Module WebAdministration

$websites = Get-ChildItem 'IIS:\Sites'
$site = $websites | Where-object { $_.Name -eq 'Default Web Site' }
$netTcpExists = [bool]($site.bindings.Collection | ? { $_.bindingInformation -eq '808:*' -and $_.protocol -eq 'net.tcp' })

if (!$netTcpExists)
{
    Write-Output "Net TCP binding does not exist. Creating binding now..."
    # Create the binding
    New-ItemProperty 'IIS:\Sites\Default Web Site' -name bindings -Value @{protocol="net.tcp";bindingInformation="808:*"}

    Write-Output "Binding created"
}
else
{
    Write-Output "TCP Binding already exists"
}

Write-Output "Updating enabled protocols..."

Set-ItemProperty 'IIS:\sites\Default Web Site' -name EnabledProtocols -Value "http,net.tcp"

Write-Output "Enabled protocols updated"